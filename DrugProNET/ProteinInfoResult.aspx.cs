using DrugProNET.Advertisement;
using DrugProNET.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DrugProNET
{
    public partial class ProteinInfoResult : AdvertiseablePage
    {
        private const string QUERY_PAGE = "ProteinInfo.aspx";

        /// <summary>
        /// Author: Andy Tang
        /// </summary>
        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            string query = Request.QueryString["query_string"];

            Protein_Information protein = EF_Data.GetProteinsInfoQuery(query).FirstOrDefault();

            if (protein != null)
            {
                LoadData(protein);
            }
            else
            {
                Page.Master.FindControl("BodyContentPlaceHolder").Visible = false;
                ExceptionUtilities.DisplayAlert(this, QUERY_PAGE);
            }
        }

        /// <summary>
        /// Author: Ryan Liang
        /// </summary>
        private static void ProcessRow(Control control, Control textControl, string text, string url = null)
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
                if (control != null)
                {
                    ((HtmlGenericControl)control).Visible = false;
                }
            }
        }

        /// <summary>
        /// Author: Ryan Liang
        /// </summary>
        private static void ProcessControl(Control control, params string[] texts)
        {
            string[] arr = { "N/A" };

            bool allEmpty = true;

            foreach (string text in texts)
            {
                if (!(string.IsNullOrEmpty(text.Trim()) || Array.Exists(arr, element => element == text.Trim())))
                {
                    allEmpty = false;
                }
            }

            if (allEmpty)
            {
                control.Visible = false;
            }
        }

        /// <summary>
        /// Author: Ryan Liang
        /// </summary>
        private static void ProcessControls(Control control, params Control[] controls)
        {
            bool allEmpty = true;

            foreach (Control subControl in controls)
            {
                if (subControl.Visible)
                {
                    allEmpty = false;
                }
            }

            if (allEmpty)
            {
                control.Visible = false;
            }
        }

        /// <summary>
        /// Author: Ryan Liang
        /// </summary>
        private void LoadData(Protein_Information protein)
        {
            ProcessRow(protein_short_name_row, protein_short_name, protein.Protein_Short_Name);
            ProcessRow(protein_full_name_row, protein_full_name, protein.Protein_Full_Name);
            ProcessRow(gene_name_row, gene_name, protein.NCBI_Gene_Name);
            ProcessRow(alias_row, alias, protein.Protein_Alias);
            ProcessRow(protein_type_row, protein_type, protein.Protein_Type_Specific_);
            ProcessRow(kinase_group_row, kinase_group, protein.Kinase_Group);
            ProcessRow(kinase_family_row, kinase_family, protein.Kinase_Family);
            ProcessRow(kinase_subfamily_row, kinase_subfamily, protein.Kinase_Subfamily);

            ProcessRow(null, cell_component1, protein.GOCellComponent1, protein.GOCellComponent1URL);
            ProcessRow(null, cell_component2, protein.GOCellComponent2, protein.GOCellComponent2URL);
            ProcessRow(null, cell_component3, protein.GOCellComponent3, protein.GOCellComponent3URL);
            ProcessControl(cell_component_row, protein.GOCellComponent1, protein.GOCellComponent2, protein.GOCellComponent3);

            ProcessRow(null, mo1, protein.GOMolFunction1, protein.GOCellComponent1URL);
            ProcessRow(null, mo2, protein.GOMolFunction2, protein.GOCellComponent2URL);
            ProcessRow(null, mo3, protein.GOMolFunction3, protein.GOCellComponent3URL);
            ProcessControl(molecular_function_row, protein.GOMolFunction1, protein.GOMolFunction2, protein.GOMolFunction3);

            ProcessRow(null, bo1, protein.GOBioProcess1, protein.GOBioProcess1URL);
            ProcessRow(null, bo2, protein.GOBioProcess2, protein.GOBioProcess2URL);
            ProcessRow(null, bo3, protein.GOBioProcess3, protein.GOBioProcess3URL);
            ProcessControl(biological_process_row, protein.GOBioProcess1, protein.GOBioProcess2, protein.GOBioProcess3);

            ProcessControls(group_gene_ontology_terms, cell_component_row, molecular_function_row, biological_process_row);

            ProcessRow(mass_da_row, mass_da, protein.Protein_Mass);
            ProcessRow(number_aa_row, number_aa, protein.Protein_AA_Number);

            ProcessRow(null, uniprot_id, protein.Uniprot_ID, protein.UniProt_Entry_URL);
            ProcessRow(null, uniprot_entry, protein.Entry_ID, protein.UniProt_Entry_URL);
            ProcessControl(uniprot_row, protein.Uniprot_ID, protein.Entry_ID);

            ProcessRow(ncbi_refseq_id_row, ncbi_refseq_id, protein.NCBI_RefSeq_NP_ID, protein.NCBI_RefSeq_NP_ID_URL);
            ProcessRow(int_protein_id_row, int_protein_id, protein.International_Prot_ID);
            ProcessRow(phosphonet_id_row, phosphonet_id, protein.PhosphoNET_Name, protein.PhosphoNET_URL);
            ProcessRow(phosphositeplus_row, phosphositeplus, protein.PhosphoSite_Plus_Entry, protein.PhosphoSite_Plus_Entry_URL);
            ProcessRow(kinasenet_id_row, kinasenet_id, protein.Uniprot_ID, protein.KinaseNET_URL);
            kinasenet_id_row.Visible = !string.IsNullOrEmpty(protein.KinaseNET_URL?.Trim());

            ProcessRow(onconet_id_row, onconet_id, protein.Uniprot_ID, protein.OncoNET_URL);
            onconet_id_row.Visible = !string.IsNullOrEmpty(protein.OncoNET_URL?.Trim());
            ProcessRow(chromosome_no_row, chromosome_no, protein.Human_Chromosome_Number);
            ProcessRow(chromosome_location_row, chromosome_location, protein.Human_Chromosome_Location);
            ProcessRow(gene_location_row, gene_location, protein.Human_Gene_Location);
            ProcessRow(ncbi_nucleotide_id_row, ncbi_nucleotide_id, protein.NCBI_Nucleotide_ID, protein.NCBI_Nucleotide_ID_URL);
            ProcessRow(ncbi_gene_id_row, ncbi_gene_id, protein.NCBI_Gene_ID, protein.NCBI_Gene_URL);
        }
    }
}