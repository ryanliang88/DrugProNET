using DrugProNET.Advertisement;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DrugProNET.Utility;
using System.Linq;

namespace DrugProNET
{
    public partial class QueryResult : AdvertiseablePage
    {
        private const string PROTEIN_QUERY_PAGE = "ProteinQuery.aspx";
        private const string DRUG_QUERY_PAGE = "DrugQuery.aspx";
        private const string DEFAULT_REDIRECT_PAGE = "Default.aspx";

        private string drug_specification;
        private string protein_specification;
        private double interaction_distance;
        private bool protein_chain;
        private bool protein_atoms;
        private bool protein_residues;
        private bool protein_residue_numbers;
        private bool drug_atoms;
        private Drug_Information drug;
        private Protein_Information protein;
        private PDB_Information PDB;

        private List<PDB_Distance> distances = new List<PDB_Distance>();

        /// <summary>
        /// Author: Andy Tang, Ryan Liang
        /// </summary>
        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!IsPostBack)
            {
                string fromPage = null;
                string query_string = null;

                try
                {
                    query_string = Request.QueryString["query_string"];

                    drug_specification = Request.QueryString["drug_specification"];
                    protein_specification = Request.QueryString["protein_specification"];

                    if (!string.IsNullOrEmpty(drug_specification))
                    {
                        protein_specification = query_string;
                        fromPage = "protein";
                    }
                    else if (!string.IsNullOrEmpty(protein_specification))
                    {
                        drug_specification = query_string;
                        fromPage = "drug";
                    }

                    drug = EF_Data.GetDrugsQuery(drug_specification).FirstOrDefault();
                    protein = EF_Data.GetProteinsInfoQuery(protein_specification).FirstOrDefault();
                    PDB = EF_Data.GetPDBInfo(protein, drug);
                    Session["PDB_File_ID"] = PDB.PDB_File_ID;

                    interaction_distance = double.Parse(Request.QueryString["interaction_distance"]);
                    protein_chain = bool.Parse(Request.QueryString["protein_chain"]);
                    protein_atoms = bool.Parse(Request.QueryString["protein_atoms"]);
                    protein_residues = bool.Parse(Request.QueryString["protein_residues"]);
                    protein_residue_numbers = bool.Parse(Request.QueryString["protein_residue_numbers"]);
                    drug_atoms = bool.Parse(Request.QueryString["drug_atoms"]);

                    Session["interaction_distance"] = interaction_distance;
                }
                catch (Exception)
                {
                    throw;
                }

                if (drug == null || protein == null || PDB == null)
                {
                    Page.Master.FindControl("BodyContentPlaceHolder").Visible = false;

                    if (fromPage != null)
                    {
                        ExceptionUtilities.DisplayAlert(this, fromPage.Equals("drug") ? DRUG_QUERY_PAGE : PROTEIN_QUERY_PAGE);
                    }
                    else
                    {
                        ExceptionUtilities.DisplayAlert(this, DEFAULT_REDIRECT_PAGE);
                    }
                }
                else
                {
                    Session["interaction_distance"] = interaction_distance;
                    Session["protein_chain"] = protein_chain;
                    Session["protein_atoms"] = protein_atoms;
                    Session["protein_residues"] = protein_residues;
                    Session["protein_residue_numbers"] = protein_residue_numbers;
                    Session["drug_atoms"] = drug_atoms;

                    LoadProtein(protein);
                    LoadDrug(drug, PDB);
                    LoadPDB_Info(PDB);

                    GetDrugAtomNumberingImage(drug);

                    CreateInteractionList(PDB, interaction_distance, protein_chain, protein_atoms, protein_residues, protein_residue_numbers, drug_atoms);

                    CreateInteractionSummary();

                    ScriptManager.RegisterStartupScript(Page, GetType(), "D_3DViewer", "javascript:loadDrugLigand('" + drug.Drug_PDB_ID + "');", true);
                    ScriptManager.RegisterStartupScript(Page, GetType(), "PDB_3DViewer", "javascript:loadStage('" + drug.PDB_File_ID + "', '" + drug.Drug_PDB_ID + "');", true);
                }
            }
        }

        public class InteractionSummaryRow
        {
            public string proteinAAResidue;
            public double numberOfInteractionsWithDrugAtom;
            public double averageDistanceOfAllInteractions;
            public double interactionToDistanceRatio;

            public InteractionSummaryRow(string proteinAAResidue, double numberOfInteractionsWithDrugAtom,
                double averageDistanceOfAllInteractions, double interactionToDistanceRatio)
            {
                this.proteinAAResidue = proteinAAResidue;
                this.numberOfInteractionsWithDrugAtom = numberOfInteractionsWithDrugAtom;
                this.averageDistanceOfAllInteractions = averageDistanceOfAllInteractions;
                this.interactionToDistanceRatio = interactionToDistanceRatio;
            }
        }

        /// <summary>
        /// Author: Ryan Liang
        /// </summary>
        private void CreateInteractionSummary()
        {
            TableHeaderRow tableHeaderRow = new TableHeaderRow
            {
                TableSection = TableRowSection.TableHeader
            };

            tableHeaderRow.Cells.Add(new TableHeaderCell { Text = "Protein Amino Acid Residue" });
            tableHeaderRow.Cells.Add(new TableHeaderCell { Text = "Number of Interactions with Drug Atoms" });
            tableHeaderRow.Cells.Add(new TableHeaderCell { Text = "Average Distance of All Interactions (Å)" });
            tableHeaderRow.Cells.Add(new TableHeaderCell { Text = "Number of Interactions : Distance Ratio" });

            interaction_summary.Rows.Add(tableHeaderRow);

            List<PDB_Distance> distances = (List<PDB_Distance>)Session["distances"];

            // Actual distance value is stored in DB as datatype string
            Dictionary<string, List<double>> proteinAAResidueAndDistances = new Dictionary<string, List<double>>();

            Dictionary<PDB_Distance, string> DistanceAndUniprotResidueNumbers = (Dictionary<PDB_Distance, string>)Session["DistanceAndUniprotResidueNumbers"];

            foreach (KeyValuePair<PDB_Distance, string> kvp in DistanceAndUniprotResidueNumbers)
            {
                string proteinAAResidueValue = kvp.Key.Protein_Residue + "-" + kvp.Value;
                if (!proteinAAResidueAndDistances.ContainsKey(proteinAAResidueValue))
                {
                    proteinAAResidueAndDistances.Add(proteinAAResidueValue, new List<double>() { kvp.Key.Distance, });
                }
                else
                {
                    proteinAAResidueAndDistances[proteinAAResidueValue].Add(kvp.Key.Distance);
                }
            }

            List<InteractionSummaryRow> interactionSummaryRows = new List<InteractionSummaryRow>();

            foreach (KeyValuePair<string, List<double>> proteinAAResidueAndDistance in proteinAAResidueAndDistances)
            {
                double numberOfInteractionsWithDrugAtom = proteinAAResidueAndDistance.Value.Count;

                List<double> distancesNumericForm = proteinAAResidueAndDistance.Value.ToList();
                double averageDistanceOfAllInteractions = distancesNumericForm.Average();

                double interactionToDistanceRatio = numberOfInteractionsWithDrugAtom / averageDistanceOfAllInteractions;

                interactionSummaryRows.Add(new InteractionSummaryRow(proteinAAResidueAndDistance.Key, numberOfInteractionsWithDrugAtom, averageDistanceOfAllInteractions, interactionToDistanceRatio));
            }

            interactionSummaryRows = interactionSummaryRows.OrderByDescending(i => i.interactionToDistanceRatio).ToList();
            Session["interactionSummaryRows"] = interactionSummaryRows;

            foreach (InteractionSummaryRow interactionSummaryRow in interactionSummaryRows)
            {
                TableRow tableRow = new TableRow();

                TableCell proteinAminoAcidResidueCell = new TableCell();

                string navigateUrl =
                    "http://www.drugpronet.ca/SNVIDResult.aspx" +
                    "?query_string=" +
                    protein_specification +
                    "&drug_specification=" +
                    drug_specification +
                    "&snv_id_key=" +
                    protein_specification + "-" + interactionSummaryRow.proteinAAResidue + "-" + drug.Drug_PDB_ID + "-" + PDB.PDB_File_ID;

                proteinAminoAcidResidueCell.Controls.Add(new HyperLink()
                {
                    Target = "_blank",
                    NavigateUrl = navigateUrl,
                    Text = interactionSummaryRow.proteinAAResidue,
                });
                tableRow.Cells.Add(proteinAminoAcidResidueCell);

                tableRow.Cells.Add(new TableCell { Text = interactionSummaryRow.numberOfInteractionsWithDrugAtom.ToString() });
                tableRow.Cells.Add(new TableCell { Text = interactionSummaryRow.averageDistanceOfAllInteractions.ToString("0.00") });
                tableRow.Cells.Add(new TableCell { Text = interactionSummaryRow.interactionToDistanceRatio.ToString("0.00") });

                interaction_summary.Rows.Add(tableRow);
            }
        }

        /// <summary>
        /// Author: Ryan Liang
        /// </summary>
        private void CreateInteractionList(PDB_Information PDB, double interaction_distance, bool protein_chain, bool protein_atoms, bool protein_residues, bool protein_residue_numbers, bool drug_atoms)
        {
            TableHeaderRow tableHeaderRow = new TableHeaderRow { TableSection = TableRowSection.TableHeader };

            tableHeaderRow.Cells.Add(new TableHeaderCell { Text = "Distance (Å)" });

            if (protein_chain)
            {
                tableHeaderRow.Cells.Add(new TableHeaderCell { Text = "Protein Chain" });
            }

            if (protein_residue_numbers)
            {
                tableHeaderRow.Cells.Add(new TableHeaderCell { Text = "Protein Residue Number" });
            }

            if (protein_residues)
            {
                tableHeaderRow.Cells.Add(new TableHeaderCell { Text = "Amino Acid Residue Type" });
            }

            if (protein_atoms)
            {
                tableHeaderRow.Cells.Add(new TableHeaderCell { Text = "Amino Acid Residue Atom" });
            }

            if (drug_atoms)
            {
                tableHeaderRow.Cells.Add(new TableHeaderCell { Text = "Drug Atom" });
            }

            interaction_list.Rows.Add(tableHeaderRow);

            var DistanceAndUniprotResidueNumbers = EF_Data.GetDistanceAndUniprotResidueNumbers(PDB.PDB_File_ID, interaction_distance);

            Session["DistanceAndUniprotResidueNumbers"] = DistanceAndUniprotResidueNumbers;

            foreach (KeyValuePair<PDB_Distance, string> kvp in DistanceAndUniprotResidueNumbers)
            {
                TableRow tableRow = new TableRow();

                tableRow.Cells.Add(new TableCell { Text = kvp.Key.Distance.ToString("0.00") });

                if (protein_chain)
                {
                    tableRow.Cells.Add(new TableCell { Text = kvp.Key.Protein_Chain });
                }

                if (protein_residue_numbers)
                {
                    tableRow.Cells.Add(new TableCell { Text = kvp.Value });
                }

                if (protein_residues)
                {
                    tableRow.Cells.Add(new TableCell { Text = kvp.Key.Protein_Residue });
                }

                if (protein_atoms)
                {
                    tableRow.Cells.Add(new TableCell { Text = kvp.Key.Protein_Atom });
                }

                if (drug_atoms)
                {
                    tableRow.Cells.Add(new TableCell { Text = kvp.Key.Compound_Atom });
                }

                interaction_list.Rows.Add(tableRow);
            }
        }

        /// <summary>
        /// Author: Ryan Liang
        /// </summary>
        private Control GetControlThatCausedPostBack(Page page)
        {
            //initialize a control and set it to null
            Control ctrl = null;

            //get the event target name and find the control
            string ctrlName = page.Request.Params.Get("__EVENTTARGET");
            if (!String.IsNullOrEmpty(ctrlName))
                ctrl = page.FindControl(ctrlName);

            //return the control to the calling method
            return ctrl;
        }

        /// <summary>
        /// Author: Ryan Liang
        /// </summary>
        private void GetDrugAtomNumberingImage(Drug_Information drug)
        {
            selected_amino_acid_residue_atom_numbering.ImageUrl =
                "https://cdn.rcsb.org/images/ligands/" +
                drug.Drug_PDB_ID.Substring(0, 1) + "/" + drug.Drug_PDB_ID + "/" + drug.Drug_PDB_ID + "-large.png";
        }

        /// <summary>
        /// Author: Ryan Liang
        /// </summary>
        public void LoadPDB_Info(PDB_Information PDB_Info)
        {
            ProcessRow(PDB_File_ID_row, PDB_File_ID, PDB_Info.PDB_File_ID, "https://www.rcsb.org/structure/" + PDB_Info.PDB_File_ID);
            ProcessRow(release_date_row, release_date, PDB_Info.PDB_Released);
            ProcessRow(resolution_row, resolution, PDB_Info.Resolution);
            ProcessRow(title_row, title, PDB_Info.PDB_Entry_Title);
            ProcessRow(authors_row, authors, PDB_Info.Authors);
            ProcessRow(reference_row, reference, PDB_Info.Journal_Reference);
        }

        /// <summary>
        /// Author: Ryan Liang
        /// </summary>
        public void LoadDrug(Drug_Information drug, PDB_Information PDB_Info)
        {
            ProcessRow(PDB_drug_ID_row, PDB_drug_ID, drug.Drug_PDB_ID);
            ProcessRow(drug_name_row, drug_name, drug.Drug_Common_Name);
            ProcessRow(drug_chemical_name, drug_chemical_name, drug.Drug_Chemical_Name);
            ProcessRow(drug_alias_row, drug_alias, drug.Other_Drug_Name_Alias);
            ProcessRow(drug_formula_row, drug_formula, drug.Drug_Formula);
            ProcessRow(drug_mass_da_row, drug_mass_da, drug.Molecular_Mass);

            ProcessRow(potency_row, potency, "IC50 (nM):" + PDB_Info.IC50_nM_
                + " (BINDINGDB); Ki (nM): " + PDB_Info.Ki_nM_
                + " (BINDINGDB); Kd(nM): " + PDB_Info.Kd_nM_
                + " (BINDINGDB)");

            ProcessRow(drug_information_result_url_row, drug_information_result_url, "Link to further drug information", "DrugInfoResult.aspx?query_string=" + drug.Drug_Name_for_Pull_Down_Menu);
        }

        /// <summary>
        /// Author: Ryan Liang
        /// </summary>
        public void LoadProtein(Protein_Information protein)
        {
            ProcessRow(protein_name_row, protein_name, protein.Protein_Short_Name);
            ProcessRow(protein_full_name_row, protein_full_name, protein.Protein_Full_Name);
            ProcessRow(p_alias_row, p_alias, protein.Protein_Alias);
            ProcessRow(uniprot_ID_row, uniprot_ID, protein.UniProt_ID);
            ProcessRow(NCBI_ID_row, NCBI_ID, protein.NCBI_RefSeq_NP_ID);
            ProcessRow(protein_type_row, protein_type, protein.Protein_Type_Specific);
            ProcessRow(kinase_group_row, kinase_group, protein.Kinase_Group);
            ProcessRow(kinase_family_row, kinase_family, protein.Kinase_Family);
            ProcessRow(number_aa_row, number_aa, protein.Protein_AA_Number);
            ProcessRow(protein_mass_da_row, protein_mass_da, protein.Protein_Mass);

            ProcessRow(protein_information_result_url_row, protein_information_result_url, "Link to further protein information", "ProteinInfoResult.aspx?query_string=" + protein.UniProt_ID);
        }

        /// <summary>
        /// Author: Ryan Liang
        /// Called via ASP control
        /// </summary>
        public void AminoAcidImage_Change(object sender, EventArgs e)
        {
            string amino_acid_name = ((ListControl)sender).SelectedValue;
            drug_atom_numbering.ImageUrl = "~/Images/AminoAcidImages/" + amino_acid_name + ".jpg";
        }

        /// <summary>
        /// Author: Ryan Liang
        /// </summary>
        protected void Download_Summary_Click(object sender, EventArgs e)
        {
            try
            {
                Response.ClearContent();
                Response.Clear();
                Response.ContentType = "application/x-unknown";
                Response.AddHeader("Content-Disposition", "attachment; " +
                    "filename=DrugProNET_PDB ID " + Session["PDB_File_ID"] + "_Bond Distance " +
                    Session["interaction_distance"] + " Å_Side Chain Interactions.xlsx");

                List<string> header = new List<string>()
                {
                    "Protein Amino Acid Residue",
                    "Number of Interactions with Drug Atoms",
                    "Average Distance of All Interactions(Å)",
                    "# Interactions : Distance Ratio",
                };

                List<InteractionSummaryRow> interactionSummaryRows = (List<InteractionSummaryRow>)Session["interactionSummaryRows"];

                List<List<string>> data = new List<List<string>>();

                foreach (InteractionSummaryRow interactionSummaryRow in interactionSummaryRows)
                {
                    List<string> dataRow = new List<string>
                    {
                        interactionSummaryRow.proteinAAResidue,
                        interactionSummaryRow.numberOfInteractionsWithDrugAtom.ToString(),
                        interactionSummaryRow.averageDistanceOfAllInteractions.ToString(),
                        interactionSummaryRow.interactionToDistanceRatio.ToString(),
                    };

                    data.Add(dataRow);
                }

                Response.BinaryWrite(ExcelWriter.CreateAsStream(header, data).ToArray());

                Response.Flush();
                Response.SuppressContent = true;
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception)
            {
                ExceptionUtilities.DisplayAlert(this, DEFAULT_REDIRECT_PAGE);
            }
        }

        /// <summary>
        /// Author: Ryan Liang
        /// </summary>
        protected void Download_List_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<PDB_Distance, string> DistanceAndUniprotResidueNumbers = (Dictionary<PDB_Distance, string>)Session["DistanceAndUniprotResidueNumbers"];
                interaction_distance = (double)Session["interaction_distance"];
                protein_chain = (bool)Session["protein_chain"];
                protein_atoms = (bool)Session["protein_atoms"];
                protein_residues = (bool)Session["protein_residues"];
                protein_residue_numbers = (bool)Session["protein_residue_numbers"];
                drug_atoms = (bool)Session["drug_atoms"];

                Response.ClearContent();
                Response.Clear();
                Response.ContentType = "application/x-unknown";
                Response.AddHeader("Content-Disposition", "attachment; " +
                    "filename=DrugProNET_PDB ID " + Session["PDB_File_ID"] + "_Bond Distance " +
                    Session["interaction_distance"] + " Å_Atom Interactions.xlsx");

                List<string> header = new List<string>()
                {
                    "Distance (Å)",
                };

                if (protein_chain)
                {
                    header.Add("Protein Chain");
                }

                if (protein_residue_numbers)
                {
                    header.Add("Protein Residue #");
                }

                if (protein_residues)
                {
                    header.Add("Amino Acid Residue Type");
                }

                if (protein_atoms)
                {
                    header.Add("Amino Acid Residue Atom");
                }

                if (drug_atoms)
                {
                    header.Add("Drug Atom");
                }

                List<List<string>> data = new List<List<string>>();

                foreach (KeyValuePair<PDB_Distance, string> kvp in DistanceAndUniprotResidueNumbers)
                {
                    List<string> dataRow = new List<string>
                    {
                        kvp.Key.Distance.ToString()
                    };

                    if (protein_chain)
                    {
                        dataRow.Add(kvp.Key.Protein_Chain);
                    }

                    if (protein_residue_numbers)
                    {
                        dataRow.Add(kvp.Value);
                    }

                    if (protein_residues)
                    {
                        dataRow.Add(kvp.Key.Protein_Residue);
                    }

                    if (protein_atoms)
                    {
                        dataRow.Add(kvp.Key.Protein_Atom);
                    }

                    if (drug_atoms)
                    {
                        dataRow.Add(kvp.Key.Compound_Atom);
                    }

                    data.Add(dataRow);
                }

                Response.BinaryWrite(ExcelWriter.CreateAsStream(header, data).ToArray());

                Response.Flush();
                Response.SuppressContent = true;
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception)
            {
                ExceptionUtilities.DisplayAlert(this, DEFAULT_REDIRECT_PAGE);
            }
        }

        /// <summary>
        /// Author: Ryan Liang
        /// </summary>
        private static void ProcessRow(Control rowControl, Control textControl, string text, string url = null)
        {
            string[] arr = { "N/A" };
            if (!string.IsNullOrEmpty(text) && !Array.Exists(arr, element => element == text))
            {
                if (textControl.GetType() == typeof(HtmlGenericControl))
                {
                    ((HtmlGenericControl)textControl).InnerText = text;
                }
                if (textControl.GetType() == typeof(HyperLink))
                {
                    ((HyperLink)textControl).Text = text;
                    ((HyperLink)textControl).NavigateUrl = url;
                }
            }
            else
            {
                if (rowControl != null)
                {
                    ((HtmlGenericControl)rowControl).Visible = false;
                }
            }
        }
    }
}
