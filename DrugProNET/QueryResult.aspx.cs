using DrugProNET.Advertisement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.WebControls;
using DrugProNET.CalculateDistance;

namespace DrugProNET
{
    public partial class QueryResult : AdvertiseablePage
    {
        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!IsPostBack)
            {
                Protein_Information protein = null;
                Drug_Information drug = null;
                PDB_Information PDB = null;

                int interaction_distance = int.Parse(Request.QueryString["interaction_distance"]);
                bool protein_chain = bool.Parse(Request.QueryString["protein_chain"]);
                bool protein_atoms = bool.Parse(Request.QueryString["protein_atoms"]);
                bool protein_residues = bool.Parse(Request.QueryString["protein_residues"]);
                bool protein_residue_numbers = bool.Parse(Request.QueryString["protein_residue_numbers"]);
                bool drug_atoms = bool.Parse(Request.QueryString["drug_atoms"]);

                string drug_specification = Request.QueryString["drug_specification"];
                string protein_specification = Request.QueryString["protein_specification"];

                string query_string = Request.QueryString["query_string"];

                if (drug_specification == null)
                {
                    drug_specification = query_string;
                }
                else if (protein_specification == null)
                {
                    protein_specification = query_string;
                }

                drug = EF_Data.GetDrugUsingDropDownName(drug_specification);
                protein = EF_Data.GetProtein(protein_specification);

                PDB = EF_Data.GetPDBInfo(protein.Uniprot_ID, drug.Drug_PDB_ID);

                LoadProtein(protein);
                LoadDrug(drug, PDB);
                LoadPDB_Info(PDB);

                GetDrugAtomNumberingImage(drug);

                CreateInteractionList(interaction_list, PDB, interaction_distance, protein_chain, protein_atoms, protein_residues, protein_residue_numbers, drug_atoms);

                CreateInteractionSummary(interaction_summary, PDB);

                ScriptManager.RegisterStartupScript(Page, GetType(), "D_3DViewer", "javascript:loadDrugLigand('" + drug.Drug_PDB_ID + "');", true);
                ScriptManager.RegisterStartupScript(Page, GetType(), "PDB_3DViewer", "javascript:loadStage('" + drug.PDB_File_ID + "', '" + drug.Drug_PDB_ID + "');", true);
            }
        }

        private void CreateInteractionSummary(Table interaction_summary, PDB_Information pDB)
        {
            TableHeaderRow tableHeaderRow = new TableHeaderRow();

            tableHeaderRow.Cells.Add(new TableHeaderCell
            {
                Text = "Protein Amino Acid Residue"
            });

            tableHeaderRow.Cells.Add(new TableHeaderCell
            {
                Text = "Number of Interactions with Drug Atoms"
            });

            tableHeaderRow.Cells.Add(new TableHeaderCell
            {
                Text = "Average Distance of All Interactions(Å)"
            });

            tableHeaderRow.Cells.Add(new TableHeaderCell
            {
                Text = "# Interactions : Distance Ratio"
            });

            interaction_summary.Rows.Add(tableHeaderRow);
        }

        private void CreateInteractionList(Table interaction_list, PDB_Information PDB, int interaction_distance, bool protein_chain, bool protein_atoms, bool protein_residues, bool protein_residue_numbers, bool drug_atoms)
        {
            TableHeaderRow tableHeaderRow = new TableHeaderRow();

            tableHeaderRow.Cells.Add(new TableHeaderCell
            {
                Text = "Distance (Å)"
            });

            if (protein_chain)
            {
                tableHeaderRow.Cells.Add(new TableHeaderCell
                {
                    Text = "Protein Chain"
                });
            }
            if (protein_residue_numbers)
            {
                tableHeaderRow.Cells.Add(new TableHeaderCell
                {
                    Text = "Protein Residue #"
                });
            }

            if (protein_residues)
            {
                tableHeaderRow.Cells.Add(new TableHeaderCell
                {
                    Text = "Amino Acid Residue Type"
                });
            }

            if (protein_atoms)
            {
                tableHeaderRow.Cells.Add(new TableHeaderCell
                {
                    Text = "Amino Acid Residue Atom"
                });
            }

            if (drug_atoms)
            {
                tableHeaderRow.Cells.Add(new TableHeaderCell
                {
                    Text = "Drug Atom"
                });
            }

            interaction_list.Rows.Add(tableHeaderRow);

            List<PDB_Distances> distances = EF_Data.GetPDB_DistancesByPDB_Entry(PDB.PDB_File_ID);

            for (int i = 0; i < distances.Count; i++)
            {
                TableRow tableRow = new TableRow();

                if (double.Parse(distances[i].Distance) < interaction_distance)
                {
                    TableCell distanceCell = new TableCell
                    {
                        Text = (double.Parse(distances[i].Distance)).ToString("0.00")
                    };

                    tableRow.Cells.Add(distanceCell);

                    if (protein_chain)
                    {
                        TableCell protein_chainCell = new TableCell
                        {
                            Text = distances[i].Protein_Chain
                        };
                        tableRow.Cells.Add(protein_chainCell);
                    }

                    if (protein_residue_numbers)
                    {
                        TableCell protein_residue_numberCell = new TableCell
                        {
                            Text = distances[i].Protein_Residue_
                        };
                        tableRow.Cells.Add(protein_residue_numberCell);
                    }

                    if (protein_residues)
                    {
                        TableCell protein_residue_numberCell = new TableCell
                        {
                            Text = distances[i].Protein_Residue
                        };
                        tableRow.Cells.Add(protein_residue_numberCell);
                    }

                    if (protein_atoms)
                    {
                        TableCell protein_atomCell = new TableCell
                        {
                            Text = distances[i].Protein_Atom
                        };
                        tableRow.Cells.Add(protein_atomCell);
                    }

                    if (drug_atoms)
                    {
                        TableCell drug_atomCell = new TableCell
                        {
                            Text = distances[i].Compound_Atom
                        };
                        tableRow.Cells.Add(drug_atomCell);
                    }
                }

                interaction_list.Rows.Add(tableRow);
            }
        }

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

        private void GetDrugAtomNumberingImage(Drug_Information drug)
        {
            selected_amino_acid_residue_atom_numbering.ImageUrl =
                "https://cdn.rcsb.org/images/ligands/" +
                drug.Drug_PDB_ID.Substring(0, 1) + "/" + drug.Drug_PDB_ID + "/" + drug.Drug_PDB_ID + "-large.png";
        }

        public void LoadPDB_Info(PDB_Information PDB_Info)
        {
            PDB_entry.InnerText = PDB_Info.PDB_File_ID;
            release_date.InnerText = PDB_Info.PDB_Released;
            resolution.InnerText = PDB_Info.Resolution;
            title.InnerText = PDB_Info.PDB_Entry_Title;
            authors.InnerText = PDB_Info.Authors;
            reference.InnerText = PDB_Info.Journal_Reference;
        }

        public void LoadDrug(Drug_Information drug, PDB_Information PDB_Info)
        {
            PDB_drug_ID.InnerText = drug.Drug_PDB_ID;
            drug_name.InnerText = drug.Drug_Common_Name;
            drug_chemical_name.InnerText = drug.Drug_Chemical_Name;
            drug_alias.InnerText = drug.Other_Drug_Name_Alias;
            drug_formula.InnerText = drug.Drug_Formula;
            drug_mass_da.InnerText = drug.Molecular_Mass;

            potency.InnerText = "IC50 (nM):" + PDB_Info.IC50_nM_
                + " (BINDINGDB); Ki (nM): " + PDB_Info.Ki_nM_
                + " (BINDINGDB); Kd(nM): " + PDB_Info.Kd_nM_
                + " (BINDINGDB)";

            drug_information_result_url.NavigateUrl = "http://localhost:50542/DrugInfoResult.aspx?query_string=" + drug.Drug_PDB_ID;
        }

        public void LoadProtein(Protein_Information protein)
        {
            protein_name.InnerText = protein.Protein_Short_Name;
            protein_full_name.InnerText = protein.Protein_Full_Name;
            p_alias.InnerText = protein.Protein_Alias;
            uniprot_ID.InnerText = protein.Uniprot_ID;
            NCBI_ID.InnerText = protein.NCBI_RefSeq_NP_ID;
            protein_type.InnerText = protein.Protein_Type_Specific_;
            kinase_group.InnerText = protein.Kinase_Group;
            kinase_family.InnerText = protein.Kinase_Family;
            number_aa.InnerText = protein.Protein_AA_Number;
            drug_mass_da.InnerText = protein.Protein_Mass;

            protein_information_result_url.NavigateUrl = "http://localhost:50542/ProteinInfoResult.aspx?query_string=" + protein.Uniprot_ID;
        }

        // Called via ASP control
        public void AminoAcidImage_Change(object sender, EventArgs e)
        {
            string amino_acid_name = ((ListControl)sender).SelectedValue;
            drug_atom_numbering.ImageUrl = "~/Images/AminoAcidImages/" + amino_acid_name + ".jpg";
        }
    }
}