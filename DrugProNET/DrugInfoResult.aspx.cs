using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
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

        private void ProcessRow(Control control, Control textControl, string text, string url = null)
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
                ((HtmlGenericControl)control).Visible = false;
            }
        }

        private void LoadData(C18OC3_DrugProNET_B_Drug_Info drug)
        {
            compound_structure.Src = "./Images/Compound Structure Images/" + drug.Compound_Structure_png;
            compound_structure.Alt = "No image found in database";

            ProcessRow(compound_name_row, compound_name, drug.Drug_Common_Name);
            ProcessRow(chemical_name_row, chemical_name, drug.Drug_Chemical_Name);
            ProcessRow(compound_alias_row, compound_alias, drug.Other_Drug_Name_Alias);
            ProcessRow(compound_inchl_row, compound_inchl_id, drug.Drug_InChl);
            ProcessRow(drug_formula_row, drug_formula, drug.Drug_Formula);
            ProcessRow(molecular_mass_row, molecular_mass, drug.Molecular_Mass);
            ProcessRow(pdb_drug_id_row, pdb_drug_id, drug.Drug_PDB_ID, drug.Drug_PDB_ID_URL);
            ProcessRow(compound_cas_id_row, compound_cas_id, drug.Compound_CAS_ID);
            ProcessRow(chembl_id_row, chembl_id, drug.ChEMBL_ID, drug.ChEMBL_Link);
            ProcessRow(kinase_sarfair_row, kinase_sarfair, drug.ChEMBL_ID, drug.CHEMBL_Kinase_SARFari_Link);
            ProcessRow(pubchem_sid_row, pubchem_sid, drug.PubChem_SID, drug.PubChem_Substance_Link);
            ProcessRow(pubchem_cid_row, pubchem_cid, drug.PubChem_CID, drug.PubChem_Compound_Link);
            ProcessRow(chemspider_sid_row, chemspider_sid, drug.ChemSpider_ID, drug.ChemSpider_Link);
            ProcessRow(chebi_id_row, chebi_id, drug.ChEBI_ID, drug.ChEBI_Link);
            ProcessRow(bindingdb_id_row, bindingdb_id, drug.BindingDB_ID, drug.BindingDB_Link);
            ProcessRow(drugbank_id_row, drugbank_id, drug.DrugBank_ID, drug.DrugBank_Link);
            ProcessRow(drug_reg_row, drug_registration, drug.Drug_Registration_Number);
            ProcessRow(kegg_drug_id_row, kegg_drug_id, drug.KEGG_Drug_ID, drug.KEGG_Drug_Link);
            ProcessRow(therapeautic_target_row, therapeutic_target_id, drug.Therapeutic_Targets_ID, drug.Therapeutic_Targets_Link);
            ProcessRow(pharmgkb_id_row, pharmgkb_id, drug.PharmGKB_ID, drug.PharmGKB_Link);
            ProcessRow(het_id_row, het_id, drug.HET_ID, drug.HET_Link);
            ProcessRow(drug_product_row, drug_product_id, drug.Drug_Product_ID, drug.Drug_Product_ID_Link);
            ProcessRow(rxlist_id_row, rxlist_id, drug.RxList_ID, drug.RxList_Link);
            ProcessRow(drugs_com_id_row, drugs_com_id, drug.Drugs_com_ID, drug.Drugs_com_Link);
            ProcessRow(wikipedia_row, wikipedia, drug.Wikipedia, drug.Wikipedia_Link);
            ProcessRow(general_activity_row, general_activity, drug.General_Activity);
            ProcessRow(general_targets_row, general_targets, drug.General_Targets);
            ProcessRow(commentary_row, commentary, drug.Commentary);
            ProcessRow(source_type_row, source_type, drug.Source_Type);
            ProcessRow(living1_source_row, living_source1, drug.Living_Source1);
            ProcessRow(living2_source_row, living_source2, drug.Living_Source2);
            ProcessRow(approved_row, clinically_approved, drug.Clinically_Approved);
            ProcessRow(latest_stage_trial_row, latest_stage_trials, drug.Latest_Stage_Trials);
            ProcessRow(producer_row, producer, drug.Producer);
            ProcessRow(disease_applications_row, disease_applications, drug.Disease_Applications);
            ProcessRow(toxic_effects_row, toxic_effects, drug.Toxic_Effects);
            ProcessRow(reference1_row, reference_1, drug.Reference_1);
            ProcessRow(reference2_row, reference_2, drug.Reference_2);
        }
    }
}