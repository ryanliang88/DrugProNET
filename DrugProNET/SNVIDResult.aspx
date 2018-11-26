<%@ Page Title="DrugProNET | SNV ID Result" Language="C#" MasterPageFile="~/BasePage.Master" AutoEventWireup="true" CodeBehind="SNVIDResult.aspx.cs" Inherits="DrugProNET.SNVIDResult" %>

<asp:Content runat="server" ContentPlaceHolderID="HeadContentPlaceHolder">
    <link rel="stylesheet" href="./css/3_column.css" />
    <link rel="stylesheet" href="./css/snv_id_result.css" />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="BodyContentPlaceHolder">

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <h3 class="h3-side-title">General Info</h3>
        </div>
        <div class="c-col body-content">
            <h3 class="h3-body-title">Protein-Drug Interaction SNV Identification</h3>
            <p class="description">
                This query provides identification of single nucleotide variants (SNVs) in 
                the human genome that may affect specific protein-drug interactions. Using 
                critical amino acid residues in a target protein implicated in a specific 
                drug binding from a previous DrugProNET query. SNVs of these amino acids 
                are identified for their locations in the human genome and the consequence 
                of their mutation on binding of a speciic drug is predicted.
            </p>
        </div>
        <div class="c-col advertisment-content">
            <asp:UpdatePanel ID="ad_update_panel" runat="server" UpdateMode="Conditional">
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
        <div class="c-col body-content-long">
            <h3 class="h3-body-title">Protein/Gene Specifications</h3>
        </div>
    </div>
    <div class="c-row" id="gene_name_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Gene Name:</p>
        </div>
        <div class="c-col body-content-long">
            <p class="p-white" id="gene_name" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="uniprot_id_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">UniProt ID:</p>
        </div>
        <div class="c-col body-content-long">
            <p class="p-white" id="uniprot_id" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="refseq_id_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">RefSeq ID:</p>
        </div>
        <div class="c-col body-content-long">
            <p class="p-white" id="refseq_id" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="nucleotide_id_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Nucleotide ID:</p>
        </div>
        <div class="c-col body-content-long">
            <p class="p-white" id="nucleotide_id" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="gene_id_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Gene ID:</p>
        </div>
        <div class="c-col body-content-long">
            <p class="p-white" id="gene_id" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="chromosome_location_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Chromosome Location:</p>
        </div>
        <div class="c-col body-content-long">
            <p class="p-white" id="chromosome_location" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="gene_location_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Gene Location:</p>
        </div>
        <div class="c-col body-content-long">
            <p class="p-white" id="gene_location" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="aa_residue_no_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">AA Residue No:</p>
        </div>
        <div class="c-col body-content-long">
            <p class="p-white" id="aa_residue_no" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="aa_residue_type_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">AA Residue Type:</p>
        </div>
        <div class="c-col body-content-long">
            <p class="p-white" id="aa_residue_type" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="atomic_interactions_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">No. Atomic Interactions:</p>
        </div>
        <div class="c-col body-content-long">
            <p class="p-white" id="atomic_interactions" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="avg_atom_distance_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Average Atom Distance (Angstroms):</p>
        </div>
        <div class="c-col body-content-long">
            <p class="p-white" id="avg_atom_distance" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="interaction_distance_ratio_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Interaction / Distance Ratio:</p>
        </div>
        <div class="c-col body-content-long">
            <p class="p-white" id="interaction_distance_ratio" runat="server"></p>
        </div>
    </div>


    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content"></div>
        <div class="c-col body-content-long">
            <h3 class="h3-body-title">Drug Specifications</h3>
        </div>
    </div>
    <div class="c-row" id="PDB_drug_ID_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">PDB Drug ID:</p>
        </div>
        <div class="c-col body-content-long">
            <p class="p-white" id="PDB_drug_ID" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="drug_name_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Drug Name:</p>
        </div>
        <div class="c-col body-content-long">
            <p class="p-white" id="drug_name" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="pubchem_cid_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">PubChem CID:</p>
        </div>
        <div class="c-col body-content-long">
            <p class="p-white" id="pubchem_cid" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="chembl_id_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">ChEMBL ID:</p>
        </div>
        <div class="c-col body-content-long">
            <p class="p-white" id="chembl_id" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="chemspider_id_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">ChemSpider ID:</p>
        </div>
        <div class="c-col body-content-long">
            <p class="p-white" id="chemspider_id" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="drugbank_id_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">DrugBank ID:</p>
        </div>
        <div class="c-col body-content-long">
            <p class="p-white" id="drugbank_id" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="drug_information_result_url_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content"></div>
        <div class="c-col body-content">
            <asp:HyperLink ID="drug_information_result_url" runat="server" NavigateUrl="navigateurl" />
        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content"></div>
        <div class="c-col body-content-long">
            <h3 class="h3-body-title">PDB Information</h3>
        </div>
    </div>
    <div class="c-row" id="PDB_entry_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">PDB Entry:</p>
        </div>
        <div class="c-col body-content-long">
            <p class="p-white" id="PDB_entry" runat="server"></p>
        </div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content"></div>
        <div class="c-col body-content-long">
            <h3 class="h3-body-title">Single Nucleotide Variant Identification</h3>
        </div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content"></div>
        <div class="c-col body-content-long longDiv">
            <div class="button-container">
                <asp:Button CssClass="download-button" ID="download_button" runat="server" Text="Download Table" OnClick="Download_SNV_Identification_Click" />
            </div>
            <div>
                <p class="descriptive download-description">Click to download MS-Excel file of predicted SNVs</p>
            </div>
        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content"></div>
        <div class="c-col body-content-long longDiv" style="padding-top: 2em">
            <asp:Table runat="server" ID="SNV_Identification_Table">
            </asp:Table>
        </div>
    </div>

</asp:Content>
