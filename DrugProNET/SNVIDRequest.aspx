<%@ Page Title="" Language="C#" MasterPageFile="~/BasePage.Master" AutoEventWireup="true" CodeBehind="SNVIDRequest.aspx.cs" Inherits="DrugProNET.SNVIDRequest" %>

<asp:Content runat="server" ContentPlaceHolderID="HeadContentPlaceHolder">
    <link rel="stylesheet" href="./css/3_column.css" />
    <link rel="stylesheet" href="./css/drug_info.css" />
    <link rel="stylesheet" href="./css/query_page.css" />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="BodyContentPlaceHolder">

    <%-- First row --%>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <h3 class="h3-side-title">Query Type</h3>
        </div>
        <div class="c-col body-content">
            <h3 class="h3-body-title">Protein-Drug Interaction SNV Identification</h3>
            <p>This query provides identification of single nucleotide variants (SNVs) in the human genome that 
                may affect specific protein drug interactions. Using critical amino acid residues in a target 
                protein implicated in a specific drug binding from a previous DrugProNET query, SNVs of these 
                amino acids are identified for their locations in the human genome and the consequence of their 
                mutation on binding of a specific drug is predicted</p>
        </div>
        <div class="c-col advertisment-content">
            <asp:UpdatePanel ID="ad_update_panel" runat="server">
                <ContentTemplate>
                    <asp:Timer ID="ad_refresh_timer" runat="server" Interval="3000" OnPreRender="RenewAdvertisement" OnTick="RenewAdvertisement"></asp:Timer>
                    <asp:HyperLink ID="adLink" NavigateUrl="navigateurl" runat="server">
                        <asp:Image ImageUrl="imageUrl" runat="server" ID="adBanner" AlternateText="" />
                    </asp:HyperLink>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ad_refresh_timer" EventName="Tick"></asp:AsyncPostBackTrigger>
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
            <p>Provide the gene name, protein name, UniProt ID, or NCBI RefSeq ID of the target human
                protein as a search term.</p>

            <asp:TextBox CssClass="textBox" ID="search_textBox" runat="server" value="" placeholder="Type in at least 3 letters of the search term" />

            
        </div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
        </div>
        <div class="c-col body-content">

            <h3 class="h3-body-title">Step 2 - Drug Specification</h3>
            <p>Use the pull-down menu below to select for the drug of interest.</p>

            <asp:DropDownList CssClass="pulldown" ID="DropDownList1" runat="server" value="">
                <asp:ListItem Text="Select from list of output options" Value="-1"></asp:ListItem>
            </asp:DropDownList>

        </div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
        </div>
        <div class="c-col body-content">

            <h3 class="h3-body-title">Step 3 - Protein Amino Acid Specification</h3>
            <p>Use the pull-down menu below to select the amino acid in the proteins for which you wish to 
                identify SNPs that affect the specified drug binding.</p>

            <asp:DropDownList CssClass="pulldown" ID="DropDownList2" runat="server" value="">
                <asp:ListItem Text="Select from list of output options" Value="-1"></asp:ListItem>
            </asp:DropDownList>

        </div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
        </div>
        <div class="c-col body-content">

            <h3 class="h3-body-title">Step 4 - Report Generation</h3>
            <p>Click on the box below to produce custom tables with results or to reset the parameters.</p>

            <asp:Button ID="generate_table_button" CssClass="button" Text="Generate Table" runat="server" />
            <span>&emsp;&emsp;</span>
            <asp:Button ID="reset_button" CssClass="button" Text="Reset" runat="server" />

        </div>
    </div>

</asp:Content>