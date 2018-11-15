using DrugProNET.Advertisement;
using DrugProNET.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DrugProNET
{
    public partial class SNVIDResult : AdvertiseablePage
    {
        private SNV_Mutations mutation;

        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            string protein_specification = Request.QueryString["query_string"];
            string drug_specification = Request.QueryString["drug_specification"];
            string SNV_Key = Request.QueryString["snv_id_key"];

            //protein_specification = "O00141";
            //drug_specification = "MMG";
            //amino_acid_specification = "ALA-125";

            Protein_Information protein = EF_Data.GetProtein(protein_specification);
            Drug_Information drug = EF_Data.GetDrug(drug_specification);

            if (protein == null || drug == null)
            {
                ExceptionUtilities.DisplayAlert(this, "SNVIDQuery.aspx");
            }

            PDB_Information PDB = EF_Data.GetPDBInfo(protein, drug);

            int firstHyphen = SNV_Key.IndexOf('-');
            int secondHyphen = SNV_Key.Substring(firstHyphen + 1).IndexOf('-') + firstHyphen;

            // extra '-' at end
            string amino_acid_specification = SNV_Key.Substring(firstHyphen + 1, secondHyphen - 2);

            PDB_Interactions interaction = EF_Data.GetPDB_Interaction(protein.Uniprot_ID, drug.Drug_PDB_ID, amino_acid_specification);
            mutation = EF_Data.GetMutationBySNVKey(SNV_Key);

            Session["mutation"] = mutation;

            LoadProtein(protein, interaction, mutation);
            LoadDrug(drug, mutation);
            LoadPDB_Info(PDB);

            CreateSNVIdentificationTable(mutation);
        }

        public void LoadProtein(Protein_Information protein, PDB_Interactions interaction, SNV_Mutations mutation)
        {
            ProcessRow(gene_name_row, gene_name, mutation.NCBI_Gene_Name);
            ProcessRow(uniprot_id_row, uniprot_id, mutation.UniProt_ID, protein.UniProt_Entry_URL);
            ProcessRow(refseq_id_row, refseq_id, protein.NCBI_RefSeq_NP_ID, protein.NCBI_RefSeq_NP_ID_URL);
            ProcessRow(nucleotide_id_row, nucleotide_id, protein.NCBI_Nucleotide_ID, protein.NCBI_Nucleotide_ID_URL);
            ProcessRow(gene_id_row, gene_id, protein.NCBI_Gene_ID, protein.NCBI_Gene_URL);
            ProcessRow(chromosome_location_row, chromosome_location, protein.Human_Chromosome_Location);
            ProcessRow(gene_location_row, gene_location, protein.Human_Gene_Location);
            ProcessRow(aa_residue_no_row, aa_residue_no, interaction.Uniprot_Residue_Number);
            ProcessRow(atomic_interactions_row, atomic_interactions, interaction.Number_of_Atomic_Interactions);
            ProcessRow(aa_residue_type_row, aa_residue_type, interaction.AA_Residue_Type);
            ProcessRow(avg_atom_distance_row, avg_atom_distance, interaction.Average_Distance_Between_Atoms);
            ProcessRow(interaction_distance_ratio_row, interaction_distance_ratio, interaction.Interaction_Distance_Ratio);
        }

        public void LoadDrug(Drug_Information drug, SNV_Mutations mutation)
        {
            ProcessRow(PDB_drug_ID_row, PDB_drug_ID, mutation.Drug_PDB_ID, drug.Drug_PDB_ID_URL);
            ProcessRow(drug_name_row, drug_name, drug.Drug_Common_Name);
            ProcessRow(pubchem_cid_row, pubchem_cid, drug.PubChem_CID, drug.PubChem_Compound_Link);
            ProcessRow(chembl_id_row, chembl_id, drug.ChEMBL_ID, drug.ChEMBL_Link);
            ProcessRow(chemspider_id_row, chemspider_id, drug.ChemSpider_ID, drug.ChemSpider_Link);
            ProcessRow(drugbank_id_row, drugbank_id, drug.DrugBank_ID, drug.DrugBank_Link);

            ProcessRow(drug_information_result_url_row, drug_information_result_url, "Click here for more drug information from DrugProNET",
                "DrugInfoResult.aspx?query_string=" + drug.PDB_File_ID);
        }

        public void LoadPDB_Info(PDB_Information PDB_Info)
        {
            ProcessRow(PDB_entry_row, PDB_entry, PDB_Info.PDB_File_ID);
        }

        protected void Download_SNV_Identification_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.Clear();
            Response.ContentType = "application/x-unknown";
            Response.AddHeader("Content-Disposition", "attachment; filename=spreadsheet.xlsx");

            mutation = (SNV_Mutations)Session["mutation"];

            List<string> header = new List<string>() { "Variant ID", "SNV", "Predicted Effect on Drug Binding" };
            List<List<string>> data = new List<List<string>>() {
                new List<string>() { mutation.P1W, mutation.SNV_P1W_ID, mutation.Drug_Inter__Pred__P1W },
                new List<string>() { mutation.P2W, mutation.SNV_P2W_ID, mutation.Drug_Inter__Pred__P2W },
                new List<string>() { mutation.P3W, mutation.SNV_P3W_ID, mutation.Drug_Inter__Pred__P3W },
                new List<string>() { mutation.P1M1, mutation.SNV_P1M1_ID, mutation.Drug_Inter__Pred__P1M1 },
                new List<string>() { mutation.P1M2, mutation.SNV_P1M2_ID, mutation.Drug_Inter__Pred__P1M2 },
                new List<string>() { mutation.P1M3, mutation.SNV_P1M3_ID, mutation.Drug_Inter__Pred__P1M3 },
                new List<string>() { mutation.P2M1, mutation.SNV_P2M1_ID, mutation.Drug_Inter__Pred__P2M1 },
                new List<string>() { mutation.P2M2, mutation.SNV_P2M2_ID, mutation.Drug_Inter__Pred__P2M2 },
                new List<string>() { mutation.P2M3, mutation.SNV_P2M3_ID, mutation.Drug_Inter__Pred__P2M3 },
                new List<string>() { mutation.P3M1, mutation.SNV_P3M1_ID, mutation.Drug_Inter__Pred__P3M1 },
                new List<string>() { mutation.P3M2, mutation.SNV_P3M2_ID, mutation.Drug_Inter__Pred__P3M2 },
                new List<string>() { mutation.P3M3, mutation.SNV_P3M3_ID, mutation.Drug_Inter__Pred__P3M3 },
            };

            Response.BinaryWrite(ExcelWriter.CreateAsStream(header, data).ToArray());

            Response.Flush();
            Response.SuppressContent = true;
            System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        private void CreateSNVIdentificationTable(SNV_Mutations mutation)
        {
            TableHeaderRow tableHeaderRow = new TableHeaderRow { TableSection = TableRowSection.TableHeader };

            tableHeaderRow.Cells.Add(new TableHeaderCell { Text = "Variant ID" });
            tableHeaderRow.Cells.Add(new TableHeaderCell { Text = "SNV" });
            tableHeaderRow.Cells.Add(new TableHeaderCell { Text = "Predicted Effect on Drug Binding" });

            SNV_Identification_Table.Rows.Add(tableHeaderRow);

            SNV_Identification_Table.Rows.Add(CreateSNVIDTableRow(mutation.P1W, mutation.SNV_P1W_ID, mutation.Drug_Inter__Pred__P1W));
            SNV_Identification_Table.Rows.Add(CreateSNVIDTableRow(mutation.P2W, mutation.SNV_P2W_ID, mutation.Drug_Inter__Pred__P2W));
            SNV_Identification_Table.Rows.Add(CreateSNVIDTableRow(mutation.P3W, mutation.SNV_P3W_ID, mutation.Drug_Inter__Pred__P3W));
            SNV_Identification_Table.Rows.Add(CreateSNVIDTableRow(mutation.P1M1, mutation.SNV_P1M1_ID, mutation.Drug_Inter__Pred__P1M1));
            SNV_Identification_Table.Rows.Add(CreateSNVIDTableRow(mutation.P1M2, mutation.SNV_P1M2_ID, mutation.Drug_Inter__Pred__P1M2));
            SNV_Identification_Table.Rows.Add(CreateSNVIDTableRow(mutation.P1M3, mutation.SNV_P1M3_ID, mutation.Drug_Inter__Pred__P1M3));
            SNV_Identification_Table.Rows.Add(CreateSNVIDTableRow(mutation.P2M1, mutation.SNV_P2M1_ID, mutation.Drug_Inter__Pred__P2M1));
            SNV_Identification_Table.Rows.Add(CreateSNVIDTableRow(mutation.P2M2, mutation.SNV_P2M2_ID, mutation.Drug_Inter__Pred__P2M2));
            SNV_Identification_Table.Rows.Add(CreateSNVIDTableRow(mutation.P2M3, mutation.SNV_P2M3_ID, mutation.Drug_Inter__Pred__P2M3));
            SNV_Identification_Table.Rows.Add(CreateSNVIDTableRow(mutation.P3M1, mutation.SNV_P3M1_ID, mutation.Drug_Inter__Pred__P3M1));
            SNV_Identification_Table.Rows.Add(CreateSNVIDTableRow(mutation.P3M2, mutation.SNV_P3M2_ID, mutation.Drug_Inter__Pred__P3M2));
            SNV_Identification_Table.Rows.Add(CreateSNVIDTableRow(mutation.P3M3, mutation.SNV_P3M3_ID, mutation.Drug_Inter__Pred__P3M3));
        }

        private TableRow CreateSNVIDTableRow(string variantID, string SNV, string predictedEffect)
        {
            TableRow tableRow = new TableRow();

            tableRow.Cells.Add(new TableCell() { Text = variantID });
            tableRow.Cells.Add(new TableCell() { Text = SNV });
            tableRow.Cells.Add(new TableCell() { Text = predictedEffect });

            return tableRow;
        }

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