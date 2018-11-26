<%@ Page Title="DrugProNET | SNV ID Query" Language="C#" MasterPageFile="~/BasePage.Master" AutoEventWireup="true" CodeBehind="SNVIDQuery.aspx.cs" Inherits="DrugProNET.SNVIDQuery" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content runat="server" ContentPlaceHolderID="HeadContentPlaceHolder">
    <link rel="stylesheet" href="./css/3_column.css" />
    <link rel="stylesheet" href="./css/snv_id_query.css" />
    <link rel="stylesheet" href="./css/loading_icon.css" />

    <%-- Needs to be inline for some reason --%>
    <script type="text/javascript">
        function RefreshUpdatePanel() {
            __doPostBack('<%= drug_drop_down_UpdatePanel.ClientID %>', '');
        };
    </script>

    <script type="text/javascript">
        function RefreshUpdatePanel2() {
            __doPostBack('<%= amino_acid_specification_updatePanel.ClientID %>', '');
        };
    </script>

    <script type="text/javascript">
        function selectedItem(sender, args) {
            __doPostBack(sender.get_element().name, '');
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
            <h3 class="h3-body-title">Protein-Drug Interaction SNV Identification</h3>
            <p>
                This query provides identification of single nucleotide variants (SNVs) in the human genome that 
                may affect specific protein drug interactions. Using critical amino acid residues in a target 
                protein implicated in a specific drug binding from a previous DrugProNET query, SNVs of these 
                amino acids are identified for their locations in the human genome and the consequence of their 
                mutation on binding of a specific drug is predicted
            </p>
        </div>
        <div class="c-col advertisment-content">

            <asp:Timer ID="ad_refresh_timer" runat="server" Interval="10000" OnPreRender="RenewAdvertisement" OnTick="RenewAdvertisement"></asp:Timer>
            <asp:UpdatePanel ID="ad_update_panel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:HyperLink ID="adLink" NavigateUrl="navigateurl" runat="server">
                        <asp:Image ImageUrl="imageUrl" runat="server" ID="adBanner" AlternateText="" />
                    </asp:HyperLink>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ad_refresh_timer" EventName="Tick" />
                </Triggers>
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
                Provide the gene name, protein name, UniProt ID, or NCBI RefSeq ID of the target human
                protein as a search term.
            </p>

            <asp:TextBox CssClass="textBox" ID="search_textBox" runat="server" value="" placeholder="Type in at least 3 letters of the search term" OnTextChanged="Search_Textbox_Changed" onkeyup="RefreshUpdatePanel();" />
            <asp:AutoCompleteExtender ID="AutoCompleteExtender" runat="server" ServiceMethod="GetAutoCompleteData" TargetControlID="search_textBox" CompletionInterval="1000" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="listItemHighlighted" EnableCaching="true"
                OnClientItemSelected="selectedItem" />
        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
        </div>
        <div class="c-col body-content">
            <h3 class="h3-body-title">Step 2 - Drug Specification</h3>
            <p>Use the pull-down menu below to select for the drug of interest.</p>

            <asp:UpdatePanel ID="drug_drop_down_UpdatePanel" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:DropDownList CssClass="drop-down" ID="drug_specification_drop_down" runat="server" onchange="RefreshUpdatePanel2();" OnSelectedIndexChanged="LoadAminoAcidDropDown">
                    </asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="search_textBox" EventName="TextChanged" />
                </Triggers>
            </asp:UpdatePanel>

        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
        </div>
        <div class="c-col body-content">

            <h3 class="h3-body-title">Step 3 - Protein Amino Acid Specification</h3>
            <p>
                Use the pull-down menu below to select the amino acid in the proteins for which you wish to 
                identify SNPs that affect the specified drug binding.
            </p>

            <asp:UpdatePanel ID="amino_acid_specification_updatePanel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:DropDownList CssClass="drop-down" ID="amino_acid_specification_drop_down" runat="server">
                    </asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="drug_specification_drop_down" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>

        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
        </div>
        <div class="c-col body-content">
            <h3 class="h3-body-title">Step 4 - Report Generation</h3>
            <p>Click on the box below to produce custom tables with results or to reset the parameters.</p>

            <asp:Button ID="generate_table_button" CssClass="button" Text="Generate Table" runat="server" OnClick="Generate" OnClientClick="target='_blank'" />
            <span>&emsp;&emsp;</span>
            <asp:Button ID="reset_button" CssClass="button" Text="Reset" runat="server" />
        </div>
    </div>

</asp:Content>
