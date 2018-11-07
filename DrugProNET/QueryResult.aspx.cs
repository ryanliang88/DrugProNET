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
                PDB_Information PDB_Info = null;

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


                drug = EF_Data.GetDrug(drug_specification);
                protein = EF_Data.GetProtein(protein_specification);

                PDB_Info = EF_Data.GetPDBInfo(protein.Uniprot_ID, drug.Drug_PDB_ID);

                LoadProtein(protein);
                LoadDrug(drug, PDB_Info);
                LoadPDB_Info(PDB_Info);

                GetDrugAtomNumberingImage(drug);

                ScriptManager.RegisterStartupScript(Page, GetType(), "D_3DViewer", "javascript:loadDrugLigand('" + drug.Drug_PDB_ID + "');", true);
                ScriptManager.RegisterStartupScript(Page, GetType(), "PDB_3DViewer", "javascript:loadStage('" + drug.PDB_File_ID + "', '" + drug.Drug_PDB_ID + "');", true);
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
            number_aa.InnerText = protein.Protein_AA_Number.ToString();
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