<%@ Page Title="" Language="C#" MasterPageFile="~/BasePage.Master" AutoEventWireup="true" CodeBehind="SNVDrugResult.aspx.cs" Inherits="DrugProNET.SNVDrugResult" %>

<asp:Content runat="server" ContentPlaceHolderID="HeadContentPlaceHolder">
    <link rel="stylesheet" href="./css/3_column.css" />
    <link rel="stylesheet" href="./css/snv_drug_result.css" />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="BodyContentPlaceHolder">

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <h3 class="h3-side-title">General Info</h3>
        </div>
        <div class="c-col body-content">
            <h3 class="h3-body-title">SNV-based Identification of Protein-Drug Interactions</h3>
            <p>
                This query provides prediction of the effects of mutation of critical amino acids in proteins involved in drug
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
            <p class="p-white" id="snv_id" runat="server"></p>
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
            <p class="p-white" id="gene_name" runat="server"></p>
        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">UniProt ID:</p>
        </div>
        <div class="c-col body-content">
            <p class="p-white" id="uniprot_id" runat="server"></p>
        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">NCBI RefSeq ID:</p>
        </div>
        <div class="c-col body-content">
            <p class="p-white" id="ncbi_refseq_id" runat="server"></p>
        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Chromosome Location:</p>
        </div>
        <div class="c-col body-content">
            <p class="p-white" id="chromosome_location" runat="server"></p>
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
        <div class="c-col body-content-long">
            <div class="button-container">
                <asp:Button CssClass="download-button" ID="download_button" runat="server" Text="Download Table" OnClick="Download_Button_Click" />
            </div>
            <div>
                <p>Click to download MS-Excel file of predicted protein-drug interactions affected by mutation of this SNV</p>
            </div>
        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content"></div>
        <div class="c-col body-content-long">
            <asp:Table runat="server" ID="interaction_list">
            </asp:Table>
        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content"></div>
        <div class="c-col body-content-long">
            <p>
                "Importance for Drug" is based on the total number of atomic interactions of the amino acid residue 
                    specified by the nucleotide with the drug atoms divided by the average bond distance (in Angstroms) 
                    for all of the interactions. The higher the score, the more importance this amino acid residue may 
                    have for drug binding
            </p>
        </div>
    </div>

</asp:Content>

