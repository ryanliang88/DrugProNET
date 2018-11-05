<%@ Page Title="" Language="C#" MasterPageFile="~/BasePage.Master" AutoEventWireup="true" CodeBehind="SNVDrugResult.aspx.cs" Inherits="DrugProNET.SNVDrugResult" %>
<asp:Content runat="server" ContentPlaceHolderID="CSSContentPlaceHolder">
    <link rel="stylesheet" href="./css/3_column.css" />
    <link rel="stylesheet" href="./css/drug_info.css" />
    <link rel="stylesheet" href="./css/query_result.css" />

    <script src="Scripts/3DViewer/ngl.js"></script>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="BodyContentPlaceHolder">

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <h3 class="h3-side-title">General Info</h3>
        </div>
        <div class="c-col body-content">
            <h3 class="h3-body-title">SNV-based Identification of Protein-Drug Interactions</h3>
            <p>This query provides prediction of the effects of mutation of critical amino acids in proteins involved in drug
                interactions starting with a defined single nucleotide variant (SNV) in the human genome. The interaction of 
                multiple drugs with a particular amino acid residue in a target protein of interest may be identified.
            </p>
        </div>
        <div class="c-col advertisment-content">
            <asp:UpdatePanel ID="ad_update_panel" runat="server">
                <ContentTemplate>
                    <asp:Timer ID="ad_refresh_timer" runat="server" Interval="10000" OnPreRender="RenewAdvertisement" OnTick="RenewAdvertisement"></asp:Timer>
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
        <div class="c-col side-content"></div>
        <div class="c-col body-content">
            <h3 class="h3-body-title">Single Nucleotide Variant Identification</h3>
        </div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">SNV ID:</p>
        </div>
        <div class="c-col body-content">
            <p id="snv_id" runat="server"></p>
        </div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content"></div>
        <div class="c-col body-content">
            <h3 class="h3-body-title">Target Gene Identification</h3>
        </div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Gene Name:</p>
        </div>
        <div class="c-col body-content">
            <p id="gene_name" runat="server"></p>
        </div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">UniProt ID:</p>
        </div>
        <div class="c-col body-content">
            <p id="uniprot_id" runat="server"></p>
        </div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">NCBI RefSeq ID:</p>
        </div>
        <div class="c-col body-content">
            <p id="ncbi_refseq_id" runat="server"></p>
        </div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Chromosome Location:</p>
        </div>
        <div class="c-col body-content">
            <p id="chromosome_location" runat="server"></p>
        </div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content"></div>
        <div class="c-col body-content">
            <h3 class="h3-body-title">Identification of Protein-Drug Interactions Linked to the SNV</h3>
        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content"></div>
        <div class="c-col body-content longDiv" style="padding-top: 2em">
            <div style="display: inline-block; float: left; padding-right: 2em">
                <asp:Button ID="Button1" runat="server" Text="Download Table" />
            </div>
            <div>
                <p>Click to download MS-Excel file of predicted protein-drug interactions affected by mutation of this SNV</p>
            </div>
        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content"></div>
        <div class="c-col body-content longDiv" style="padding-top: 2em">
            <asp:Table runat="server" ID="interaction_list">
            </asp:Table>
        </div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content"></div>
        <div class="c-col body-content longDiv" style="padding-top: 2em">
            <div>
                <p>"Importance for Drug" is based on the total number of atomic interactions of the amino acid residue 
                    specified by the nucleotide with the drug atoms divided by the average bond distance (in Angstroms) 
                    for all of the interactions. The higher the score, the more importance this amino acid residue may 
                    have for drug binding</p>
            </div>
        </div>

    </div>

</asp:Content>

