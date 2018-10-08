using DrugProNET.Advertisement;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DrugProNET
{
    public partial class ProteinInfoResult : AdvertiseablePage
    {
        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            string query = Request.QueryString["query_string"];

            C18OC3_DrugProNET_A_Protein_Info protein = GetProtein(query);
            LoadData(protein);
        }

        private C18OC3_DrugProNET_A_Protein_Info GetProtein(string query)
        {
            C18OC3_DrugProNET_A_Protein_Info theProteinImLookingFor;
            using (SampleDatabaseEntities context = new SampleDatabaseEntities())
            {
                DbSet<C18OC3_DrugProNET_A_Protein_Info> dbSet = context.C18OC3_DrugProNET_A_Protein_Info;
                theProteinImLookingFor = dbSet.Where(protein => protein.Uniprot_ID == query).ToList()[0];
            }

            return theProteinImLookingFor;
        }

        private void LoadData(C18OC3_DrugProNET_A_Protein_Info protein)
        {
            protein_short_name.InnerText = protein.Protein_Short_Name;
            protein_full_name.InnerText = protein.Protein_Full_Name;

            alias.InnerText = protein.Protein_Alias;

            protein_type.InnerText = protein.Protein_Type_Specific_;

            kinase_group.InnerText = protein.Kinase_Group;

            kinase_family.InnerText = protein.Kinase_Family;
            kinase_subfamilty.InnerText = protein.Kinase_Subfamily;

            cell_component1.Text = protein.GOCellComponent1;
            cell_component1.NavigateUrl = protein.GOCellComponent1URL;
            cell_component2.Text = protein.GOCellComponent2;
            cell_component2.NavigateUrl = protein.GOCellComponent2URL;
            cell_component3.Text = protein.GOCellComponent3;
            cell_component3.NavigateUrl = protein.GOCellComponent3URL;

            mo1.Text = protein.GOMolFunction1;
            mo1.NavigateUrl = protein.GOMolFunction1URL;
            mo2.Text = protein.GOMolFunction2;
            mo2.NavigateUrl = protein.GOMolFunction2URL;
            mo3.Text = protein.GOMolFunction3;
            mo3.NavigateUrl = protein.GOMolFunction3URL;

            bo1.Text = protein.GOBioProcess1;
            bo1.NavigateUrl = protein.GOBioProcess1URL;
            bo2.Text = protein.GOBioProcess2;
            bo2.NavigateUrl = protein.GOBioProcess2URL;
            bo3.Text = protein.GOBioProcess3;
            bo3.NavigateUrl = protein.GOBioProcess3URL;

            mass_da.InnerText = protein.Protein_Mass;
            number_aa.InnerText = protein.Protein_AA_Number.ToString();

            uniprot_id.Text = protein.Uniprot_ID;
            uniprot_id.NavigateUrl = protein.UniProt_Entry_URL;
            uniprot_entry.Text = protein.Entry_ID;
            uniprot_entry.NavigateUrl = protein.UniProt_Entry_URL;

            ncbi_refseq_id.Text = protein.NCBI_RefSeq_NP_ID;
            ncbi_refseq_id.NavigateUrl = protein.NCBI_RefSeq_NP_ID_URL;

            int_protein_id.InnerText = protein.International_Prot_ID;

            phosphonet_id.Text = protein.PhosphoNET_Name;
            phosphonet_id.NavigateUrl = protein.PhosphoNET_URL;

            phosphositeplus.Text = protein.PhosphoSIte_Plus_Entry.ToString();
            phosphositeplus.NavigateUrl = protein.PhosphoSIte_Plus_Entry_URL;

            kinasenet_id.Text = protein.Uniprot_ID;
            kinasenet_id.NavigateUrl = protein.KinaseNET_URL;

            onconet_id.Text = protein.Uniprot_ID;
            onconet_id.NavigateUrl = protein.OncoNET_URL;

            // PDB entry doesn't exist in the database!
            //pdb_entries.Text = protein.;
            //pdb_entries.NavigateUrl = "http://www.rcsb.org/structure/" + protein.PDB_Protein_Name;
        }
    }
}