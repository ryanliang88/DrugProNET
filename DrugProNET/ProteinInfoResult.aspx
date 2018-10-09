<%@ Page Title="DrugProNET | Protein Information Result" Language="C#" MasterPageFile="~/BasePage.Master" AutoEventWireup="true" CodeBehind="ProteinInfoResult.aspx.cs" Inherits="DrugProNET.ProteinInfoResult" %>


<asp:Content runat="server" ContentPlaceHolderID="CSSContentPlaceHolder">
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
                This report provides detailed information on one of over 485 proteins
                that have been experimentally identified as targets for inhibitory drugs
                and co-crystalized with these compounds. The data has been annotated from
                multiple sources including the US National Center for Biotechnology Information,
                UniProt, and Phosphosite Plos. URL links are provided to these and other SigNET 
                KnowledgeBases.
            </p>
        </div>
        <div class="c-col advertisment-content">
            <asp:Timer ID="ad_refresh_timer" runat="server" Interval="3000" OnPreRender="RenewAdvertisement" OnTick="RenewAdvertisement"></asp:Timer>
            <asp:UpdatePanel ID="ad_update_panel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
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
            <h3 class="h3-side-title-gold">Nomenclature</h3>
        </div>
        <div class="c-col body-content"></div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Protein Short Name:</p>
        </div>
        <div class="c-col body-content">
            <p id="protein_short_name" runat="server"></p>
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Protein Full Name:</p>
        </div>
        <div class="c-col body-content">
            <p id="protein_full_name" runat="server"></p>
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Gene Name:</p>
        </div>
        <div class="c-col body-content">
            <p id="gene_name" runat="server"></p>
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Alias:</p>
        </div>
        <div class="c-col body-content">
            <p id="alias" runat="server"></p>
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Protein Type:</p>
        </div>
        <div class="c-col body-content">
            <p id="protein_type" runat="server"></p>
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Kinase Group:</p>
        </div>
        <div class="c-col body-content">
            <p id="kinase_group" runat="server"></p>
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Kinase Family:</p>
        </div>
        <div class="c-col body-content">
            <p id="kinase_family" runat="server"></p>
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Kinase Subfamily:</p>
        </div>
        <div class="c-col body-content">
            <p id="kinase_subfamilty" runat="server"></p>
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <h3 class="h3-side-title-gold">Gene Ontology Terms</h3>
        </div>
        <div class="c-col body-content">
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Cell Component:</p>
        </div>
        <div class="c-col body-content">
            <div>
                <asp:HyperLink class="white-link" ID="cell_component1" NavigateUrl="navigateurl" runat="server" />
                &nbsp;&nbsp;&nbsp;
                <asp:HyperLink class="white-link" ID="cell_component2" NavigateUrl="navigateurl" runat="server" />
                &nbsp;&nbsp;&nbsp;
                <asp:HyperLink class="white-link" ID="cell_component3" NavigateUrl="navigateurl" runat="server" />
                &nbsp;&nbsp;&nbsp;
            </div>
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Molecular Function:</p>
        </div>
        <div class="c-col body-content">
            <asp:HyperLink class="white-link" ID="mo1" NavigateUrl="navigateurl" runat="server" />
            &nbsp;&nbsp;&nbsp;
            <asp:HyperLink class="white-link" ID="mo2" NavigateUrl="navigateurl" runat="server" />
            &nbsp;&nbsp;&nbsp;
            <asp:HyperLink class="white-link" ID="mo3" NavigateUrl="navigateurl" runat="server" />
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Biological Process:</p>
        </div>
        <div class="c-col body-content">
            <asp:HyperLink class="white-link" ID="bo1" NavigateUrl="navigateurl" runat="server" />
            &nbsp;&nbsp;&nbsp;
            <asp:HyperLink class="white-link" ID="bo2" NavigateUrl="navigateurl" runat="server" />
            &nbsp;&nbsp;&nbsp;
            <asp:HyperLink class="white-link" ID="bo3" NavigateUrl="navigateurl" runat="server" />
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

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Mass(Da):</p>
        </div>
        <div class="c-col body-content">
            <p id="mass_da" runat="server"></p>
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Number AA:</p>
        </div>
        <div class="c-col body-content">
            <p id="number_aa" runat="server"></p>
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">UniProt ID:</p>
        </div>
        <div class="c-col body-content">
            <asp:HyperLink class="white-link" ID="uniprot_id" NavigateUrl="navigateurl" runat="server" value="" />
            <br />
            <asp:HyperLink class="white-link" ID="uniprot_entry" NavigateUrl="navigateurl" runat="server" />
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">NCBI RefSeq ID:</p>
        </div>
        <div class="c-col body-content">
            <asp:HyperLink class="white-link" ID="ncbi_refseq_id" NavigateUrl="navigateurl" runat="server" value="" />
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Int. Protein ID:</p>
        </div>
        <div class="c-col body-content">
            <p id="int_protein_id" runat="server"></p>
        </div>
        <div class="c-col advertisment-content"></div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">PhosphoNET ID:</p>
        </div>
        <div class="c-col body-content">
            <asp:HyperLink class="white-link" ID="phosphonet_id" NavigateUrl="navigateurl" runat="server" />
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">PhosphoSitePlus:</p>
        </div>
        <div class="c-col body-content">
            <asp:HyperLink class="white-link" ID="phosphositeplus" NavigateUrl="navigateurl" runat="server" />
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">KinaseNET ID:</p>
        </div>
        <div class="c-col body-content">
            <asp:HyperLink class="white-link" ID="kinasenet_id" NavigateUrl="navigateurl" runat="server" />
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">OncoNET ID:</p>
        </div>
        <div class="c-col body-content">
            <asp:HyperLink class="white-link" ID="onconet_id" NavigateUrl="navigateurl" runat="server" />
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
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Chromosome No.:</p>
        </div>
        <div class="c-col body-content">
            <p id="chromosome_no" runat="server"></p>
        </div>
        <div class="c-col advertisment-content"></div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Chromosome Location:</p>
        </div>
        <div class="c-col body-content">
            <p id="chromosome_location" runat="server"></p>
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Gene Location:</p>
        </div>
        <div class="c-col body-content">
            <p id="gene_location" runat="server"></p>
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">NCBI Nucleotide ID:</p>
        </div>
        <div class="c-col body-content">
            <asp:HyperLink class="white-link" ID="ncbi_nucleotide_id" NavigateUrl="navigateurl" runat="server" />
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">NCBI Gene ID:</p>
        </div>
        <div class="c-col body-content">
            <asp:HyperLink class="white-link" ID="ncbi_gene_id" NavigateUrl="navigateurl" runat="server" />
        </div>
        <div class="c-col advertisment-content"></div>
    </div>
</asp:Content>
