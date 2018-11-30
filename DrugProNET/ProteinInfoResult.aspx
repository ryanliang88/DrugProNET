<%@ Page Title="DrugProNET | Protein Information Result" Language="C#" MasterPageFile="~/BasePage.Master" AutoEventWireup="true" CodeBehind="ProteinInfoResult.aspx.cs" Inherits="DrugProNET.ProteinInfoResult" %>


<asp:Content runat="server" ContentPlaceHolderID="HeadContentPlaceHolder">
    <link rel="stylesheet" href="./css/3_column.css">
    <link rel="stylesheet" href="./css/protein_info_result.css">
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="BodyContentPlaceHolder">
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <h3 class="h3-side-title">General Info</h3>
        </div>
        <div class="c-col body-content">
            <h3 class="h3-body-title">Protein Description</h3>
            <p class="p-general-info">
                This report provides detailed information on one of over 485 proteins that have been experimentally identified as targets for inhibitory drugs and co-crystallized with these compounds. The data has been annotated from multiple sources, including the US National Center for Biotechnology Information, UniProt, and Phosphosite Plus, and url links are provided to these and other SigNET KnowledgeBases.
            </p>
        </div>
        <div class="c-col advertisment-content">
            <asp:UpdatePanel ID="ad_update_panel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Timer ID="ad_refresh_timer" runat="server" Interval="10000" OnPreRender="RenewAdvertisement" OnTick="RenewAdvertisement"></asp:Timer>
                    <asp:HyperLink ID="adLink" NavigateUrl="navigateurl" runat="server" Target="_blank">
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
            <h3 class="h3-side-title-gold">Nomenclature</h3>
        </div>
        <div class="c-col body-content"></div>
    </div>

    <div class="c-row" id="protein_short_name_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Protein Short Name:</p>
        </div>
        <div class="c-col body-content-long">
            <p id="protein_short_name" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="protein_full_name_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Protein Full Name:</p>
        </div>
        <div class="c-col body-content-long">
            <p id="protein_full_name" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="gene_name_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Gene Name:</p>
        </div>
        <div class="c-col body-content-long">
            <p id="gene_name" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="alias_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Alias:</p>
        </div>
        <div class="c-col body-content-long">
            <p id="alias" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="protein_type_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Protein Type:</p>
        </div>
        <div class="c-col body-content-long">
            <p id="protein_type" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="kinase_group_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Kinase Group:</p>
        </div>
        <div class="c-col body-content">
            <p id="kinase_group" runat="server"></p>
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row" id="kinase_family_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Kinase Family:</p>
        </div>
        <div class="c-col body-content">
            <p id="kinase_family" runat="server"></p>
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row" id="kinase_subfamily_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Kinase Subfamily:</p>
        </div>
        <div class="c-col body-content">
            <p id="kinase_subfamily" runat="server"></p>
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row" id="group_gene_ontology_terms" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <h3 class="h3-side-title-gold">Gene Ontology Terms</h3>
        </div>
        <div class="c-col body-content">
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row" id="cell_component_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Cell Component:</p>
        </div>
        <div class="c-col body-content">
            <asp:Table ID="cell_component_table" runat="server">
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:HyperLink CssClass="white-link" ID="cell_component1" NavigateUrl="navigateurl" runat="server" Target="_blank" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:HyperLink CssClass="white-link" ID="cell_component2" NavigateUrl="navigateurl" runat="server" Target="_blank" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:HyperLink CssClass="white-link" ID="cell_component3" NavigateUrl="navigateurl" runat="server" Target="_blank" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row" id="molecular_function_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Molecular Function:</p>
        </div>
        <div class="c-col body-content">
            <asp:Table ID="molecular_function_table" runat="server">
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:HyperLink CssClass="white-link" ID="mo1" NavigateUrl="navigateurl" runat="server" Target="_blank" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:HyperLink CssClass="white-link" ID="mo2" NavigateUrl="navigateurl" runat="server" Target="_blank" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:HyperLink CssClass="white-link" ID="mo3" NavigateUrl="navigateurl" runat="server" Target="_blank" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row" id="biological_process_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Biological Process:</p>
        </div>
        <div class="c-col body-content">
            <asp:Table ID="biological_process_table" runat="server">
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:HyperLink CssClass="white-link" ID="bo1" NavigateUrl="navigateurl" runat="server" Target="_blank" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:HyperLink CssClass="white-link" ID="bo2" NavigateUrl="navigateurl" runat="server" Target="_blank" />
                    </asp:TableCell><asp:TableCell>
                        <asp:HyperLink CssClass="white-link" ID="bo3" NavigateUrl="navigateurl" runat="server" Target="_blank" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
        <div class="c-col advertisment-content"></div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <h3 class="h3-side-title-gold">Specific Info</h3>
        </div>
        <div class="c-col body-content">
        </div>
        <div class="c-col advertisment-content"></div>
    </div>

    <div class="c-row" id="mass_da_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Mass(Da):</p>
        </div>
        <div class="c-col body-content">
            <p id="mass_da" runat="server"></p>
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row" id="number_aa_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Number AA:</p>
        </div>
        <div class="c-col body-content">
            <p id="number_aa" runat="server"></p>
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row" id="uniprot_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">UniProt ID:</p>
        </div>
        <div class="c-col body-content">
            <asp:HyperLink CssClass="white-link" ID="uniprot_id" NavigateUrl="navigateurl" runat="server" Target="_blank" />
            <br />
            <asp:HyperLink CssClass="white-link" ID="uniprot_entry" NavigateUrl="navigateurl" runat="server" Target="_blank" />
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row" id="ncbi_refseq_id_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">NCBI RefSeq ID:</p>
        </div>
        <div class="c-col body-content">
            <asp:HyperLink CssClass="white-link" ID="ncbi_refseq_id" NavigateUrl="navigateurl" runat="server" Target="_blank" />
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row" id="int_protein_id_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Int. Protein ID:</p>
        </div>
        <div class="c-col body-content">
            <p id="int_protein_id" runat="server"></p>
        </div>
        <div class="c-col advertisment-content"></div>
    </div>

    <div class="c-row" id="phosphonet_id_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">PhosphoNET ID:</p>
        </div>
        <div class="c-col body-content">
            <asp:HyperLink CssClass="white-link" ID="phosphonet_id" NavigateUrl="navigateurl" runat="server" Target="_blank" />
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row" id="phosphositeplus_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">PhosphoSitePlus:</p>
        </div>
        <div class="c-col body-content">
            <asp:HyperLink CssClass="white-link" ID="phosphositeplus" NavigateUrl="navigateurl" runat="server" Target="_blank" />
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row" id="kinasenet_id_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">KinaseNET ID:</p>
        </div>
        <div class="c-col body-content">
            <asp:HyperLink CssClass="white-link" ID="kinasenet_id" NavigateUrl="navigateurl" runat="server" Target="_blank" />
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row" id="onconet_id_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">OncoNET ID:</p>
        </div>
        <div class="c-col body-content">
            <asp:HyperLink CssClass="white-link" ID="onconet_id" NavigateUrl="navigateurl" runat="server" Target="_blank" />
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <h3 class="h3-side-title-gold">Gene Specific Info</h3>
        </div>
        <div class="c-col body-content">
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row" id="chromosome_no_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Chromosome No.:</p>
        </div>
        <div class="c-col body-content">
            <p id="chromosome_no" runat="server"></p>
        </div>
        <div class="c-col advertisment-content"></div>
    </div>

    <div class="c-row" id="chromosome_location_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Chromosome Location:</p>
        </div>
        <div class="c-col body-content">
            <p id="chromosome_location" runat="server"></p>
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row" id="gene_location_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Gene Location:</p>
        </div>
        <div class="c-col body-content">
            <p id="gene_location" runat="server"></p>
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row" id="ncbi_nucleotide_id_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">NCBI Nucleotide ID:</p>
        </div>
        <div class="c-col body-content">
            <asp:HyperLink CssClass="white-link" ID="ncbi_nucleotide_id" NavigateUrl="navigateurl" runat="server" Target="_blank" />
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row" id="ncbi_gene_id_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">NCBI Gene ID:</p>
        </div>
        <div class="c-col body-content">
            <asp:HyperLink CssClass="white-link" ID="ncbi_gene_id" NavigateUrl="navigateurl" runat="server" Target="_blank" />
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
</asp:Content>
