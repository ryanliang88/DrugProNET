<%@ Page Language="C#" Title="DrugProNET | Query Result" AutoEventWireup="true" MasterPageFile="~/BasePage.Master" CodeBehind="QueryResult.aspx.cs" Inherits="DrugProNET.QueryResult" %>

<asp:Content runat="server" ContentPlaceHolderID="HeadContentPlaceHolder">
    <link rel="stylesheet" href="./css/3_column.css" />
    <link rel="stylesheet" href="./css/query_result.css" />

    <script src="Scripts/3DViewer/ngl.js"></script>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="BodyContentPlaceHolder">

    <div class="c-row ad_row_min_height">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <h3 class="h3-side-title">General Info</h3>
        </div>
        <div class="c-col body-content">
            <h3 class="h3-body-title">Protein-Drug Interaction Report</h3>
            <p class="descriptive">
                This report provides a listing of atom-to-atom interactions between a protein of interest and a small drug molecule that have been documented in co-crystallization files retrieved from the RCSB PDB Protein Data Bank. In the second table, each row represents a unique interaction between an atom on the protein and an atom on the drug molecule. The distances between the atom pairs are measured in Angstroms, and the shorter the distance, the more important the atom pairs are likely to be for drug binding. Based on this data, the most important amino acid residues involved in drug binding are identified in the first table.
            </p>
        </div>
        <div class="c-col advertisment-content">
            <asp:UpdatePanel ID="ad_update_panel" runat="server" class="ad_banner">
                <ContentTemplate>
                    <asp:Timer ID="ad_refresh_timer" runat="server" Interval="10000" OnPreRender="RenewAdvertisement" OnTick="RenewAdvertisement"></asp:Timer>
                    <asp:HyperLink ID="adLink" NavigateUrl="navigateurl" runat="server" Target="_blank">
                        <asp:Image ImageUrl="imageUrl" runat="server" ID="adBanner" AlternateText="" Width="288px" Style="float: right" />
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
            <h3 class="h3-body-title">Specified Protein Information</h3>
        </div>
    </div>
    <div class="c-row" id="protein_name_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Protein Name:</p>
        </div>
        <div class="c-col body-content">
            <p id="protein_name" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="protein_full_name_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Protein Full Name:</p>
        </div>
        <div class="c-col body-content">
            <p id="protein_full_name" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="p_alias_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Alias:</p>
        </div>
        <div class="c-col body-content">
            <p id="p_alias" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="uniprot_ID_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">UniProt ID:</p>
        </div>
        <div class="c-col body-content">
            <p id="uniprot_ID" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="NCBI_ID_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">NCBI ID:</p>
        </div>
        <div class="c-col body-content">
            <p id="NCBI_ID" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="protein_type_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Protein Type:</p>
        </div>
        <div class="c-col body-content">
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
    </div>
    <div class="c-row" id="kinase_family_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Kinase Family:</p>
        </div>
        <div class="c-col body-content">
            <p id="kinase_family" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="number_aa_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Number AA:</p>
        </div>
        <div class="c-col body-content">
            <p id="number_aa" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="protein_mass_da_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Mass (Da):</p>
        </div>
        <div class="c-col body-content">
            <p id="protein_mass_da" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="protein_information_result_url_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content"></div>
        <div class="c-col body-content">
            <asp:HyperLink ID="protein_information_result_url" runat="server" NavigateUrl="navigateurl" Target="_blank" />
        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content"></div>
        <div class="c-col body-content">
            <h3 class="h3-body-title">Specified Drug Information</h3>
        </div>
    </div>
    <div class="c-row" id="PDB_drug_ID_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">PDB Drug ID:</p>
        </div>
        <div class="c-col body-content">
            <p id="PDB_drug_ID" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="drug_name_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Drug Name:</p>
        </div>
        <div class="c-col body-content">
            <p id="drug_name" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="drug_chemical_name_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Drug Chemical Name:</p>
        </div>
        <div class="c-col body-content">
            <p id="drug_chemical_name" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="drug_alias_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Drug Alias:</p>
        </div>
        <div class="c-col body-content">
            <p id="drug_alias" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="drug_formula_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Drug Formula:</p>
        </div>
        <div class="c-col body-content">
            <p id="drug_formula" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="drug_mass_da_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Mass (Da):</p>
        </div>
        <div class="c-col body-content">
            <p id="drug_mass_da" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="potency_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Potency:</p>
        </div>
        <div class="c-col body-content">
            <p id="potency" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="drug_information_result_url_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content"></div>
        <div class="c-col body-content">
            <asp:HyperLink ID="drug_information_result_url" runat="server" NavigateUrl="navigateurl" Target="_blank" />
        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content"></div>
        <div class="c-col body-content">
            <h3 class="h3-body-title">Specified PDB Information</h3>
        </div>
    </div>
    <div class="c-row" id="PDB_File_ID_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">PDB File ID:</p>
        </div>
        <div class="c-col body-content">
            <asp:HyperLink ID="PDB_File_ID" runat="server" NavigateUrl="navigateurl" Target="_blank" />
        </div>
    </div>
    <div class="c-row" id="release_date_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Release Date:</p>
        </div>
        <div class="c-col body-content">
            <p id="release_date" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="resolution_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Resolution:</p>
        </div>
        <div class="c-col body-content">
            <p id="resolution" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="title_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Title:</p>
        </div>
        <div class="c-col body-content">
            <p id="title" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="authors_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Authors:</p>
        </div>
        <div class="c-col body-content">
            <p id="authors" runat="server"></p>
        </div>
    </div>
    <div class="c-row" id="reference_row" runat="server">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Reference:</p>
        </div>
        <div class="c-col body-content">
            <p id="reference" runat="server"></p>
        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content"></div>
        <div class="c-col body-content-long">
            <div style="display: inline-block; float: left;">
                <h3 class="h3-body-title">Drug Atom Numbering</h3>
                <asp:Image ID="selected_amino_acid_residue_atom_numbering" runat="server"
                    class="display-square" ImageUrl="~/Images/placeholder.jpg" />
            </div>
            <div style="display: inline-block;">
                <h3 class="h3-body-title">Drug 3D Structure</h3>
                <div id="D_3DViewer_viewport" class="display-square"></div>
                <script src="Scripts/3DViewer/D_3DViewer.js"></script>
                <!-- Uses ngl.js -->
            </div>
        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content"></div>
        <div class="c-col body-content-long">
            <div style="display: inline-block; float: left;">
                <h3 class="h3-body-title">Selected Amino Acid Residue Atom Numbering</h3>
                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Image ID="drug_atom_numbering" runat="server"
                            ImageUrl="~/Images/AminoAcidImages/Default Starting.jpg" class="display-square" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="amino_acid_dropdown" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div style="display: inline-block;">
                <h3 class="h3-body-title">Protein-Drug 3D Stucture</h3>
                <div id="viewport" class="display-square"></div>
                <!-- // MUST BE NAMED "viewport" for unknown reasons in ngl, see PDB_3DViewer.js -->
                <script src="Scripts/3DViewer/PDB_3DViewer.js"></script>
                <!-- Uses ngl.js -->
            </div>
        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content"></div>
        <div class="body-content-long amino-acid-container">
            <asp:DropDownList runat="server" CssClass="amino-acid-dropdown" ID="amino_acid_dropdown"
                AutoPostBack="true" OnSelectedIndexChanged="AminoAcidImage_Change">
                <asp:ListItem Text="Select an amino acid" Value="Default Starting" />
                <asp:ListItem Text="Alanine-Ala-A" Value="Alanine-Ala-A" />
                <asp:ListItem Text="Arginine-Arg-R" Value="Arginine-Arg-R" />
                <asp:ListItem Text="Asparagine-Asn-N" Value="Asparagine-Asn-N" />
                <asp:ListItem Text="AsparticAcid-Asp-D" Value="AsparticAcid-Asp-D" />
                <asp:ListItem Text="Cysteine-Cys-C" Value="Cysteine-Cys-C" />
                <asp:ListItem Text="GlutamicAcid-Glu-E" Value="GlutamicAcid-Glu-E" />
                <asp:ListItem Text="Glutamine-Gln-Q" Value="Glutamine-Gln-Q" />
                <asp:ListItem Text="Glycine-Gly-G" Value="Glycine-Gly-G" />
                <asp:ListItem Text="Histidine-His-H" Value="Histidine-His-H" />
                <asp:ListItem Text="Isoleucine-Ile-I" Value="Isoleucine-Ile-I" />
                <asp:ListItem Text="Leucine-Leu-L" Value="Leucine-Leu-L" />
                <asp:ListItem Text="Lysine-Lys-K" Value="Lysine-Lys-K" />
                <asp:ListItem Text="Methionine-Met-M" Value="Methionine-Met-M" />
                <asp:ListItem Text="Phenylalanine-Phe-F" Value="Phenylalanine-Phe-F" />
                <asp:ListItem Text="Proline-Pro-P" Value="Proline-Pro-P" />
                <asp:ListItem Text="Serine-Ser-S" Value="Serine-Ser-S" />
                <asp:ListItem Text="Threonine-Thr-T" Value="Threonine-Thr-T" />
                <asp:ListItem Text="Tryptophan-Trp-W" Value="Tryptophan-Trp-W" />
                <asp:ListItem Text="Tyrosine-Tyr-Y" Value="Tyrosine-Tyr-Y" />
                <asp:ListItem Text="Valine-Val-V" Value="Valine-Val-V" />
            </asp:DropDownList>
            <p class="amino-acid-instructions descriptive">
                Click to show above one of 20 selected amino acid residues with the standard numbering of all of the atoms in the amino acid residue
            </p>
        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content"></div>
        <div class="c-col body-content-long top-spacing">
            <h3 class="h3-body-title">Interaction Summary of Critical Amino Acid Residues Implicated in Drug Binding</h3>
        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content"></div>
        <div class="body-content-long">
            <asp:Button CssClass="download-button" ID="download_interaction_summary" runat="server" Text="Download Table" OnClick="Download_Summary_Click" />
            <p class="descriptive download-description">Click to download MS-Excel file of interaction summary table below</p>
        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content"></div>
        <div class="c-col body-content-long interaction-summary-container">
            <asp:Table runat="server" ID="interaction_summary" CssClass="interaction-summary">
            </asp:Table>
        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content"></div>
        <div class="c-col body-content-long top-spacing">
            <h3 class="h3-body-title">Interaction List of Critical Amino Acid Residues Atoms Implicated in Drug Atom Binding</h3>
        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content"></div>
        <div class="c-col body-content-long">
            <div class="button-container">
                <asp:Button CssClass="download-button" ID="Button1" runat="server" Text="Download Table" OnClick="Download_List_Click" />
            </div>
            <div>
                <p class="descriptive download-description">Click to download MS-Excel file of the atom-to-atom interaction table below</p>
            </div>
        </div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content"></div>
        <div class="c-col body-content-long interaction_list_container">
            <asp:Table runat="server" ID="interaction_list" CssClass="interaction-list">
            </asp:Table>
        </div>
    </div>
</asp:Content>
