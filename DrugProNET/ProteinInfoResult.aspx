<%@ Page Title="" Language="C#" MasterPageFile="~/BasePage.Master" AutoEventWireup="true" CodeBehind="ProteinInfoResult.aspx.cs" Inherits="DrugProNET.ProteinInfoResult" %>


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
        <div class="c-col body-content"></div>
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
            <p class="p-side">Protein Group:</p>
        </div>
        <div class="c-col body-content">
            <p id="protein_group" runat="server"></p>
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
            <p id="cell_component" runat="server"></p>
        </div>
         <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Molecular Function:</p>
        </div>
        <div class="c-col body-content">
            <p id="molecular_function" runat="server"></p>
        </div>
         <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Biological Process:</p>
        </div>
        <div class="c-col body-content">
            <p id="biological_process" runat="server"></p>
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
            <p id="uniprot_id" runat="server"></p>
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
            <p id="phosphonet_id" runat="server"></p>
        </div>
         <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">PhosphoSitePlus:</p>
        </div>
        <div class="c-col body-content">
            <p id="phosphositeplus" runat="server"></p>
        </div>
         <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">KinaseNET ID:</p>
        </div>
        <div class="c-col body-content">
            <p id="kineasenet_id" runat="server"></p>
        </div>
         <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">OncoNET ID:</p>
        </div>
        <div class="c-col body-content">
            <p id="onconet_id" runat="server"></p>
        </div>
         <div class="c-col advertisment-content"></div>
    </div>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">PDB Entries:</p>
        </div>
        <div class="c-col body-content">
            <p id="pdb_entries" runat="server"></p>
        </div>
         <div class="c-col advertisment-content"></div>
    </div>
</asp:Content>
