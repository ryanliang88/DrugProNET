using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DrugProNET
{
    public partial class ProteinQuery : Advertisement.AdvertiseablePage
    {
        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
        }

        protected void Reset_Click(object sender, EventArgs e)
        {
            search_pullDown.SelectedIndex = 0;
            search_textBox.Text = string.Empty;
            CheckBox1.Checked = false;
            CheckBox2.Checked = false;
            CheckBox3.Checked = false;
            CheckBox3.Checked = false;
            CheckBox4.Checked = false;
            CheckBox5.Checked = false;
            interaction_distance_dropdown.SelectedIndex = 0;
        }

        protected void Generate_Table_Click(object sender, EventArgs e)
        {
            Response.Redirect("QueryResult.aspx?query_string=" + search_textBox.Text
                + "&drug_spec=" + search_pullDown.SelectedValue
                + "&min_distance=" + interaction_distance_dropdown.SelectedValue
                + "&show_protein_chain=" + CheckBox1.Checked
                + "&show_protein_atoms=" + CheckBox2.Checked
                + "&show_protein_residues=" + CheckBox3.Checked
                + "&show_protein_residue_numbers=" + CheckBox4.Checked
                + "&show_drug_atoms=" + CheckBox5.Checked, false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}