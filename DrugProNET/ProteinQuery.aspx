﻿<%@ Page Title="DrugProNET | Protein Query" Language="C#" MasterPageFile="~/BasePage.Master" AutoEventWireup="true" CodeBehind="ProteinQuery.aspx.cs" Inherits="DrugProNET.ProteinQuery" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content runat="server" ContentPlaceHolderID="HeadContentPlaceHolder">
    <link rel="stylesheet" href="./css/3_column.css" />
    <link rel="stylesheet" href="./css/protein_query.css">

    <%-- Needs to be inline for some reason --%>
    <script type="text/javascript">
        function RefreshUpdatePanel() {
            __doPostBack('<%= search_drop_down_UpdatePanel.ClientID %>', '');
        };
    </script>

</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="BodyContentPlaceHolder">

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <h3 class="h3-side-title">Query Type</h3>
        </div>
        <div class="c-col body-content">
            <h3 class="h3-body-title">Protein-focused Drug Identification</h3>
            <p>
                This query provides listings of atom-to-atom interaction pairs between a protein and a small drug molecule. Interactions are 
            defined by distance between the atoms, which are measured in Angstroms. The data in this database are extracted from coordinate 
            information of co-crystallization files in the RCSB PDB Protein Data Bank. The Protein Drug Interaction database currently 
            contains data from over 4500 co-crystallization files.
            </p>
        </div>
        <div class="c-col advertisment-content">
            <asp:UpdatePanel ID="ad_update_panel" runat="server" UpdateMode="Conditional">
                <contenttemplate>
                    <asp:Timer ID="ad_refresh_timer" runat="server" Interval="10000" OnPreRender="RenewAdvertisement" OnTick="RenewAdvertisement"></asp:Timer>
                    <asp:HyperLink ID="adLink" NavigateUrl="navigateurl" runat="server">
                        <asp:Image ImageUrl="imageUrl" runat="server" ID="adBanner" AlternateText="" />
                    </asp:HyperLink>
                </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="ad_refresh_timer" EventName="Tick" />
                </triggers>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <h3 class="h3-side-title">Filters</h3>
        </div>
        <div class="c-col body-content">
            <h3 class="h3-body-title">Step 1 - Protein Specification</h3>
            <p>
                Provide the gene name, protein name, UniProt ID, or NCBI RefSeq 
                ID of the target human protein as a search term.
            </p>

            <asp:TextBox CssClass="textBox" ID="search_textBox" runat="server" value="" placeholder="Type in at least 3 letters of the search term" OnTextChanged="Search_Textbox_Changed" onkeyup="RefreshUpdatePanel();" />
            <asp:AutoCompleteExtender ID="AutoCompleteExtender" runat="server" ServiceMethod="GetAutoCompleteData" TargetControlID="search_textBox" CompletionInterval="100" CompletionSetCount="5" MinimumPrefixLength="1" />
        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content"></div>
        <div class="c-col body-content">
            <h3 class="h3-body-title">Step 2 - Drug Specification</h3>
            <p>Use the pull-down menu below to select for the drug of interest.</p>

            <asp:UpdatePanel ID="search_drop_down_UpdatePanel" runat="server" UpdateMode="Conditional">
                <contenttemplate>
                    <asp:DropDownList CssClass="drop-down" ID="search_drop_down" runat="server">
                        <asp:ListItem Text="Select from list of output options" Value="0" />
                    </asp:DropDownList>
                </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="search_textBox" EventName="TextChanged" />
                </triggers>
            </asp:UpdatePanel>

        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content"></div>
        <div class="c-col body-content">
            <h3 class="h3-body-title">Step 3 - Interaction Distance Specification</h3>
            <p>Use the pull-down menu below to select the maximum value for interaction distance (in Angstroms) between atoms.</p>
            <asp:DropDownList CssClass="drop-down" ID="interaction_distance_drop_down" runat="server" value="">
                <asp:ListItem Text="5" Value="5" />
                <asp:ListItem Text="0.5" Value="0.5" />
                <asp:ListItem Text="1" Value="1" />
                <asp:ListItem Text="1.5" Value="1.5" />
                <asp:ListItem Text="2" Value="2" />
                <asp:ListItem Text="2.5" Value="2.5" />
                <asp:ListItem Text="3" Value="3" />
                <asp:ListItem Text="3.5" Value="3.5" />
                <asp:ListItem Text="4" Value="4" />
                <asp:ListItem Text="4.5" Value="4.5" />
                <asp:ListItem Text="5.5" Value="5.5" />
                <asp:ListItem Text="6" Value="6" />
                <asp:ListItem Text="6.5" Value="6.5" />
                <asp:ListItem Text="7" Value="7" />
                <asp:ListItem Text="7.5" Value="7.5" />
            </asp:DropDownList>
        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content"></div>
        <div class="c-col body-content">
            <h3 class="h3-body-title">Step 4 - Display Parameter Specification</h3>
            <p>Boxes that are marked are activated to display. Click on boxes to change status.</p>
            <asp:CheckBox CssClass="checkbox-toggle" ID="protein_chain_checkbox" runat="server" Text="Show protein chain" Checked="true" />
            <asp:CheckBox CssClass="checkbox-toggle" ID="protein_atoms_checkbox" runat="server" Text="Show protein atoms" Checked="true" />
            <asp:CheckBox CssClass="checkbox-toggle" ID="protein_residues_checkbox" runat="server" Text="Show protein residues" Checked="true" />
            <asp:CheckBox CssClass="checkbox-toggle" ID="protein_residue_number_checkbox" runat="server" Text="Show protein residue number" Checked="true" />
            <asp:CheckBox CssClass="checkbox-toggle" ID="drug_atoms_checkbox" runat="server" Text="Show drug atoms" Checked="true" />
        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content"></div>
        <div class="c-col body-content">
            <h3 class="h3-body-title">Step 5 - Report Generation</h3>
            <p>Click on the box below to produce custom tables with results or to reset the parameters.</p>
            <asp:Button ID="generate_table" CssClass="button" Text="Generate Table" runat="server" OnClick="Generate_Table_Button_Click" />
            <span>&emsp;&emsp;</span>
            <asp:Button ID="reset" CssClass="button" Text="Reset" runat="server" OnClick="Reset_Button_Click" />
        </div>
    </div>
</asp:Content>
