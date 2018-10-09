using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DrugProNET
{
    public partial class DrugInfoResult : Advertisement.AdvertiseablePage
    {
        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            string query = Request.QueryString["query_string"];

            C18OC3_DrugProNET_B_Drug_Info drug = GetDrug(query);
            LoadData(drug);
        }

        private C18OC3_DrugProNET_B_Drug_Info GetDrug(string query)
        {
            C18OC3_DrugProNET_B_Drug_Info drug;
            using (SampleDatabaseEntities context = new SampleDatabaseEntities())
            {
                DbSet<C18OC3_DrugProNET_B_Drug_Info> dbSet = context.C18OC3_DrugProNET_B_Drug_Info;
                drug = dbSet.Where(d => d.ChEMBL_ID == query).ToList()[0];
            }

            return drug;
        }

        private void LoadData(C18OC3_DrugProNET_B_Drug_Info drug)
        {
            compound_structure.Src = "./Images/Compound Structure Images/" + drug.Compound_Structure_png;
            compound_structure.Alt = "No image found in database";

            compound_name.InnerText = drug.Drug_Common_Name;
            chemical_name.InnerText = drug.Drug_Chemical_Name;
            compound_alias.InnerText = drug.Other_Drug_Name_Alias;
            compound_inchl_id.InnerText = drug.Drug_InChl;
            drug_formula.InnerText = drug.Drug_Formula;
            molecular_mass.InnerText = drug.Molecular_Mass;

            pdb_drug_id.Text = drug.Drug_PDB_ID;
            pdb_drug_id.NavigateUrl = drug.Drug_PDB_ID_URL;

            compound_cas_id.InnerText = drug.Compound_CAS_ID;

            pubchem_cid.Text = drug.PubChem_CID;
            pubchem_cid.NavigateUrl = drug.PubChem_Compound_Link;

            chembl_id.Text = drug.ChEMBL_ID;
            chembl_id.NavigateUrl = drug.ChEMBL_Link;

            kinase_sarfair.Text = drug.ChEMBL_ID;
            kinase_sarfair.NavigateUrl = drug.CHEMBL_Kinase_SARFari_Link;

            pubchem_sid.Text = drug.PubChem_SID;
            pubchem_sid.NavigateUrl = drug.PubChem_Substance_Link;

            chemspider_sid.Text = drug.ChemSpider_ID;
            chemspider_sid.Text = drug.ChemSpider_Link;

            chebi_id.Text = drug.ChEBI_ID;
            chebi_id.NavigateUrl = drug.ChEBI_Link;

            bindingdb_id.Text = drug.BindingDB_ID;
            bindingdb_id.NavigateUrl = drug.BindingDB_Link;

            drugbank_id.Text = drug.DrugBank_ID;
            drugbank_id.NavigateUrl = drug.DrugBank_Link;

            drug_registration.InnerText = drug.Drug_Registration_Number;

            kegg_drug_id.Text = drug.KEGG_Drug_ID;
            kegg_drug_id.NavigateUrl = drug.KEGG_Drug_Link;

            therapeutic_target_id.Text = drug.Therapeutic_Targets_ID;
            therapeutic_target_id.NavigateUrl = drug.Therapeutic_Targets_Link;

            pharmgkb_id.Text = drug.PharmGKB_ID;
            pharmgkb_id.NavigateUrl = drug.PharmGKB_Link;

            het_id.Text = drug.HET_ID;
            het_id.NavigateUrl = drug.HET_Link;

            drug_product_id.Text = drug.Drug_Product_ID;
            drug_product_id.NavigateUrl = drug.Drug_Product_ID_Link;

            rxlist_id.Text = drug.RxList_ID;
            rxlist_id.NavigateUrl = drug.RxList_Link;

            drugs_com_id.Text = drug.Drugs_com_ID;
            drugs_com_id.NavigateUrl = drug.Drugs_com_Link;

            wikipedia.Text = drug.Wikipedia;
            wikipedia.NavigateUrl = drug.Wikipedia_Link;

            general_targets.InnerText = drug.General_Targets;
            general_activity.InnerText = drug.General_Activity;

            commentary.InnerText = drug.Commentary;

            source_type.InnerText = drug.Source_Type;
            living_source1.InnerText = drug.Living_Source1;
            living_source2.InnerText = drug.Living_Source2;

            clinically_approved.InnerText = drug.Clinically_Approved;

            latest_stage_trials.InnerText = drug.Latest_Stage_Trials;
            producer.InnerText = drug.Producer;
            disease_applications.InnerText = drug.Disease_Applications;
            toxic_effects.InnerText = drug.Toxic_Effects;
            reference_1.InnerText = drug.Reference_1;
            reference_2.InnerText = drug.Reference_2;
        }
    }
}