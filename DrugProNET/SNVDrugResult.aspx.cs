using DrugProNET.Advertisement;
using DrugProNET.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DrugProNET
{
    public partial class SNVDrugResult : AdvertiseablePage
    {
        private const string QUERY_PAGE = "SNVDrugQuery.aspx";

        private List<Drug_Information> drugs = new List<Drug_Information>();
        private List<SNV_Mutation> mutations;
        private List<PDB_Interaction> interactions = new List<PDB_Interaction>();
        private Protein_Information protein;
        private SNV_Mutation proteinMutation;

        public string SNV_ID_Key;

        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            SNV_ID_Key = Request.QueryString["query_string"];

            try
            {
                string stringAfterPDot = SNV_ID_Key.Substring(SNV_ID_Key.IndexOf("p.") + 2);
                string specifiedAAType = new string(stringAfterPDot.TakeWhile(c => char.IsLetter(c)).ToArray());
                string specifiedAANumber = new string(stringAfterPDot.Substring(specifiedAAType.Length).TakeWhile(c => char.IsNumber(c)).ToArray());
                string specifiedAA = specifiedAAType + "-" + specifiedAANumber;

                mutations = EF_Data.GetMutationsBySNVIDKey(SNV_ID_Key);
                proteinMutation = EF_Data.GetMutationBySNVIDKey(SNV_ID_Key);
                protein = EF_Data.GetProteinByUniprotID(proteinMutation.UniProt_ID);

                foreach (SNV_Mutation mutation in mutations)
                {
                    Drug_Information drug = EF_Data.GetDrugByDrugPDBID(mutation.Drug_PDB_ID);

                    drugs.Add(drug);

                    PDB_Interaction interaction = EF_Data.GetPDB_Interaction(mutation.UniProt_ID, mutation.Drug_PDB_ID, specifiedAA);

                    interactions.Add(interaction);
                }

                Session["drugs"] = drugs;
                Session["interactions"] = interactions;
                Session["mutations"] = mutations;
                Session["SNV_ID_Key"] = SNV_ID_Key;

                LoadSNVID(SNV_ID_Key);
                LoadTargetGeneID(protein, mutations[0]);

                CreateIDofPDILinkedSNVTable(drugs, interactions, mutations);
            }
            catch (Exception)
            {
                Page.Master.FindControl("BodyContentPlaceHolder").Visible = false;
                ExceptionUtilities.DisplayAlert(this, QUERY_PAGE);
            }
        }

        private void LoadSNVID(string SNV_Key)
        {
            ProcessRow(snv_id_row, snv_id, SNV_Key);
        }

        private void LoadTargetGeneID(Protein_Information protein, SNV_Mutation mutation)
        {
            ProcessRow(gene_name_row, gene_name, mutation.NCBI_Gene_Name);
            ProcessRow(uniprot_id_row, uniprot_id, protein.Uniprot_ID);
            ProcessRow(ncbi_refseq_id_row, ncbi_refseq_id, protein.NCBI_RefSeq_NP_ID);
            ProcessRow(chromosome_location_row, chromosome_location, protein.Human_Chromosome_Location);

            gene_and_protein_info_url.Text = "Link to further gene and protein information";
            gene_and_protein_info_url.NavigateUrl = "ProteinInfoResult.aspx?query_string=" + protein.Uniprot_ID;
            gene_and_protein_info_url.Target = "_blank";
        }

        private void CreateIDofPDILinkedSNVTable(List<Drug_Information> drugs, List<PDB_Interaction> interactions, List<SNV_Mutation> mutations)
        {
            TableHeaderRow tableHeaderRow = new TableHeaderRow();

            tableHeaderRow.Cells.Add(new TableHeaderCell { Text = "Drug Name" });
            tableHeaderRow.Cells.Add(new TableHeaderCell { Text = "PDB File No." });
            tableHeaderRow.Cells.Add(new TableHeaderCell { Text = "Drug PDB No." });
            tableHeaderRow.Cells.Add(new TableHeaderCell { Text = "Drug PubChem CID" });
            tableHeaderRow.Cells.Add(new TableHeaderCell { Text = "Drug ChEMBL ID" });
            tableHeaderRow.Cells.Add(new TableHeaderCell { Text = "Importance for Drug" });
            tableHeaderRow.Cells.Add(new TableHeaderCell { Text = "Effect of Mutation" });

            IDofPDILinkedSNVTable.Rows.Add(tableHeaderRow);

            List<DrugResultRow> rows = new List<DrugResultRow>();

            for (int i = 0; i < drugs.Count; i++)
            {
                rows.Add(new DrugResultRow
                {
                    drug = drugs[i],
                    interaction = interactions[i],
                    mutation = mutations[i]
                });
            }

            rows = rows.OrderByDescending(r => double.Parse(r.interaction?.Interaction_Distance_Ratio ?? "0")).ToList();

            for (int i = 0; i < rows.Count; i++)
            {
                IDofPDILinkedSNVTable.Rows.Add(CreateTableRow(rows[i].drug, rows[i].interaction, rows[i].mutation));
            }
        }

        public class DrugResultRow {

            public Drug_Information drug;
            public PDB_Interaction interaction;
            public SNV_Mutation mutation;
        }


        private TableRow CreateTableRow(Drug_Information drug, PDB_Interaction interaction, SNV_Mutation mutation)
        {
            TableRow tableRow = new TableRow();

            tableRow.Cells.Add(new TableCell() { Text = drug.Drug_Common_Name });
            tableRow.Cells.Add(new TableCell() { Text = mutation.PDB_File_No });
            tableRow.Cells.Add(new TableCell() { Text = mutation.Drug_PDB_ID });
            tableRow.Cells.Add(new TableCell() { Text = drug.PubChem_CID });
            tableRow.Cells.Add(new TableCell() { Text = drug.ChEMBL_ID });

            if (double.TryParse(interaction?.Interaction_Distance_Ratio, out double result))
            {
                tableRow.Cells.Add(new TableCell() { Text = result.ToString("0.0") });
            }
            else
            {
                tableRow.Cells.Add(new TableCell() { Text = "" });
            }

            string predictedEffiency = FindPredictedEffiency(mutation);

            tableRow.Cells.Add(new TableCell() { Text = predictedEffiency });

            return tableRow;
        }

        private string FindPredictedEffiency(SNV_Mutation mutation)
        {
            if (mutation.SNV_P1M1_ID.Equals(SNV_ID_Key, StringComparison.OrdinalIgnoreCase))
            {
                return mutation.Drug_Inter__Pred__P1M1;
            }
            else if (mutation.SNV_P1M2_ID.Equals(SNV_ID_Key, StringComparison.OrdinalIgnoreCase))
            {
                return mutation.Drug_Inter__Pred__P1M2;
            }
            else if (mutation.SNV_P1M3_ID.Equals(SNV_ID_Key, StringComparison.OrdinalIgnoreCase))
            {
                return mutation.Drug_Inter__Pred__P1M3;
            }
            else if (mutation.SNV_P2M1_ID.Equals(SNV_ID_Key, StringComparison.OrdinalIgnoreCase))
            {
                return mutation.Drug_Inter__Pred__P2M1;
            }
            else if (mutation.SNV_P2M2_ID.Equals(SNV_ID_Key, StringComparison.OrdinalIgnoreCase))
            {
                return mutation.Drug_Inter__Pred__P2M2;
            }
            else if (mutation.SNV_P2M3_ID.Equals(SNV_ID_Key, StringComparison.OrdinalIgnoreCase))
            {
                return mutation.Drug_Inter__Pred__P2M3;
            }
            else if (mutation.SNV_P3M1_ID.Equals(SNV_ID_Key, StringComparison.OrdinalIgnoreCase))
            {
                return mutation.Drug_Inter__Pred__P3M1;
            }
            else if (mutation.SNV_P3M2_ID.Equals(SNV_ID_Key, StringComparison.OrdinalIgnoreCase))
            {
                return mutation.Drug_Inter__Pred__P3M2;
            }
            else if (mutation.SNV_P3M3_ID.Equals(SNV_ID_Key, StringComparison.OrdinalIgnoreCase))
            {
                return mutation.Drug_Inter__Pred__P3M3;
            }

            return null;
        }

        protected void Download_Button_Click(object sender, EventArgs e)
        {
            SNV_ID_Key = (string)Session["SNV_ID_Key"];

            Response.ClearContent();
            Response.Clear();
            Response.ContentType = "application/x-unknown";
            Response.AddHeader("Content-Disposition", "attachment; " +
                "filename=DrugProNET_SNV ID " + SNV_ID_Key + ".xlsx");

            List<string> header = new List<string>() { "Drug Name", "PDB File No.", "Drug PDB No.",
                "Drug PubChem CID",  "Drug ChEMBL ID", "Importance for Drug", "Effect of Mutation" };

            drugs = (List<Drug_Information>)Session["drugs"];
            interactions = (List<PDB_Interaction>)Session["interactions"];
            mutations = (List<SNV_Mutation>)Session["mutations"];

            List<List<string>> data = new List<List<string>>();

            for (int i = 0; i < mutations.Count; i++)
            {
                List<string> dataRow = new List<string>
                {
                    drugs[i].Drug_Common_Name,
                    mutations[i].PDB_File_No,
                    mutations[i].Drug_PDB_ID,
                    drugs[i].PubChem_CID,
                    drugs[i].ChEMBL_ID,
                    interactions[i].Interaction_Distance_Ratio,
                    FindPredictedEffiency(mutations[i])
                };

                data.Add(dataRow);
            }

            Response.BinaryWrite(ExcelWriter.CreateAsStream(header, data).ToArray());

            Response.Flush();
            Response.SuppressContent = true;
            System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
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