<%@ Page Title="" Language="C#" MasterPageFile="~/BasePage.Master" AutoEventWireup="true" CodeBehind="ProteinInfoResults.aspx.cs" Inherits="DrugProNET.ProteinInfoResults" %>

<asp:Content runat="server" ContentPlaceHolderID="CSSContentPlaceHolder">
    <!-- css links -->
    <link rel="stylesheet" href="./css/protein_info.css">
    <link rel="stylesheet" href="./css/results_pages.css">

    <link rel="stylesheet" href="~/css/3_column.css" runat="server">
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="BodyContentPlaceHolder">

    <%-- First row --%>
    <div class="c-row">
        <div class="c-col side-content">
            <br />
            <p>General Info</p>
        </div>
        <div class="c-col body-content">
            <h3 class="h3-body-title">Protein Description</h3>
            <p>This report provides detailed information on one of over 485 proteins that have been experimentally identified as targets for inhibitory drugs and co-crystalized with these compounds. The data has been annotated from multiple sources including the US National Center for Biotechnology Information, UniProt, and Phosphosite Plos. URL links are provided to these and other SigNET KnowledgeBases.</p>
        </div>
        <div class="c-col advertisment-content">
            <asp:Timer ID="ad_refresh_timer" runat="server" Interval="3000" OnPreRender="RenewAdvertisement" OnTick="RenewAdvertisement"></asp:Timer>
            <asp:UpdatePanel ID="ad_update_panel" runat="server">
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
    <%-- First row end --%>

    <div class="c-row">
        <div class="c-col side-content">

            <h3 class="h3-body-title">Nomenclature</h3>

        </div>
        <div class="c-col body-content">
        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-content">

            <p>Protein short name</p>

        </div>
        <div class="c-col body-content">

            <p class="query-data" id="protein_short_name">ABL1</p>

        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-content">

            <p>Protein full name</p>

        </div>
        <div class="c-col body-content">

            <p class="query-data" id="protein_full_name">Abelson murine leukemia viral oncogene homologue 1</p>

        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-content">

            <p>Alias</p>

        </div>
        <div class="c-col body-content">

            <p class="query-data" id="alias">Abelson murine leukemia viral oncogene 1; ABL; C-ABL; EC 2.7.10.2; JTK7; P150; V-abl Abelson murine leukemia viral oncogene 1</p>

        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-content">

            <p>Protein type</p>

        </div>
        <div class="c-col body-content">

            <p class="query-data" id="protein_type">Protein kinase - Non-receptor-tyrosine kinase</p>

        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-content">

            <p>Kinase group</p>

        </div>
        <div class="c-col body-content">

            <p class="query-data" id="kinase_group">TK</p>

        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-content">

            <p>Kinase family</p>

        </div>
        <div class="c-col body-content">

            <p class="query-data" id="kinase_family">Abl</p>

        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-content">

            <p>Kinase subfamily</p>

        </div>
        <div class="c-col body-content">

            <p class="query-data" id="kinase_subfamily">N/A</p>

        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-content">

            <h3 class="h3-body-title">Gene Ontology Terms</h3>

        </div>
        <div class="c-col body-content">
        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-content">

            <p>Cell component</p>

        </div>
        <div class="c-col body-content">

            <p class="query-data" id="cell_components">
                <a class="query-data" id="cell_component_1" href="TODO">GO:0005887</a>&nbsp;
                    <a class="query-data" id="cell_component_2" href="TODO">GO:0005829</a>&nbsp;
                    <a class="query-data" id="cell_component_3" href="TODO">GO:0005730</a>&nbsp;
            </p>

        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-content">

            <p>Molecular function</p>

        </div>
        <div class="c-col body-content">

            <p class="query-data" id="mol_functions">
                <a class="query-data" id="mol_function_1" href="TODO">GO:0005524</a>&nbsp;
                    <a class="query-data" id="mol_function_2" href="TODO">GO:0003677</a>&nbsp;
                    <a class="query-data" id="mol_function_3" href="TODO">GO:0000287</a>&nbsp;
            </p>

        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-content">

            <p>Biological process</p>

        </div>
        <div class="c-col body-content">

            <p class="query-data" id="bio_processes">
                <a class="query-data" id="bio_process_1" href="TODO">GO:0008630</a>&nbsp;
                    <a class="query-data" id="bio_process_2" href="TODO">GO:0030036</a>&nbsp;
                    <a class="query-data" id="bio_process_3" href="TODO">GO:0007155</a>&nbsp;
            </p>

        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-content">

            <h3 class="h3-body-title">Specific Info</h3>

        </div>
        <div class="c-col body-content">
        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-content">

            <p>Mass (Da)</p>

        </div>
        <div class="c-col body-content">

            <p><a class="query-data" id="mass_da" href="TODO">122,873</a></p>

        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-content">

            <p>Number AA</p>

        </div>
        <div class="c-col body-content">

            <p><a class="query-data" id="number_aa" href="TODO">1149</a></p>

        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-content">

            <p>UniProt ID</p>

        </div>
        <div class="c-col body-content">

            <p><a class="query-data" id="uniprot_id" href="TODO">P00519<br />
                ABL1_HUMAN</a></p>

        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-content">

            <p>NCBI RefSeq ID</p>

        </div>
        <div class="c-col body-content">

            <p><a class="query-data" id="ncbi_refseq_id" href="TODO">NP_005148</a></p>

        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-content">

            <p>Int. Protein ID</p>

        </div>
        <div class="c-col body-content">

            <p><a class="query-data" id="int_protein_id" href="TODO">IPI00216969</a></p>

        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-content">

            <p>PhosphoNET ID</p>

        </div>
        <div class="c-col body-content">

            <p><a class="query-data" id="phosphonet_id" href="TODO">ABL</a></p>

        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-content">

            <p>PhosphoSitePlus</p>

        </div>
        <div class="c-col body-content">

            <p><a class="query-data" id="phosphosite_plus" href="TODO">4577</a></p>

        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-content">

            <p>KinaseNET ID</p>

        </div>
        <div class="c-col body-content">

            <p><a class="query-data" id="kinasenet_id" href="TODO">P00519</a></p>

        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-content">

            <p>OncoNET ID</p>

        </div>
        <div class="c-col body-content">

            <p><a class="query-data" id="" href="onconet_id">P00519</a></p>

        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-content">

            <p>PDB ENtries</p>

        </div>
        <div class="c-col body-content">

            <p><a class="query-data" id="pdb_entries" href="TODO">2E2B</a></p>

        </div>
    </div>
</asp:Content>
