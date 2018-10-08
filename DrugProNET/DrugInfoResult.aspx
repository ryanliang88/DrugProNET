<%@ Page Title="" Language="C#" MasterPageFile="~/BasePage.Master" AutoEventWireup="true" CodeBehind="DrugInfoResult.aspx.cs" Inherits="DrugProNET.DrugInfoResult" %>

<asp:Content runat="server" ContentPlaceHolderID="CSSContentPlaceHolder">
    <link href="./css/drug_info_result.css" rel="stylesheet" />
    <link rel="stylesheet" href="./css/3_column.css">
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="BodyContentPlaceHolder">

    <%-- First row --%>
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <h3 class="h3-side-title">General Info</h3>
        </div>
        <div class="c-col body-content">
            <h3 class="h3-body-title">Compound Information</h3>
            <p class="p-general-info">This report provides detailed information on one selected compound from over 800 compounds that have been experimentally identified as protein kinase inhibitors.</p>
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

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <br />
        </div>
        <div class="c-col body-content">
        </div>
        <div class="c-col advertisment-content"></div>
    </div>

    <%-- ------- --%>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col body-content-long-full">
            <div class="flex-container">
                <div class="flex-row">
                    <div class="flex-col">
                        <div class="flex-row">
                            <div class="side">
                                <p class="p-side">Compound Alias:</p>
                            </div>
                            <div class="body">
                                <p id="compound_alias" runat="server"></p>
                            </div>
                        </div>
                        <div class="flex-row">
                            <div class="side">
                                <p class="p-side">Drug Formula</p>
                            </div>
                            <div class="body">
                                <p id="drug_formula" runat="server"></p>
                            </div>
                        </div>
                        <div class="flex-row">
                            <div class="side">
                                <p class="p-side">Molecular Mass</p>
                            </div>
                            <div class="body">
                                <p id="molecular_mass" runat="server"></p>
                            </div>
                        </div>
                        <div class="flex-row">
                            <div class="side">
                                <p class="p-side">Compound CAS ID</p>
                            </div>
                            <div class="body">
                                <p id="compound_cas_id" runat="server"></p>
                            </div>
                        </div>
                        <div class="flex-row">
                            <div class="side">
                                <p class="p-side">PubChem ID</p>
                            </div>
                            <div class="body">
                                <p id="pubchem_id" runat="server"></p>
                            </div>
                        </div>

                        <div class="flex-row">
                            <div class="side">
                                <p class="p-side">ChEMBL ID</p>
                            </div>
                            <div class="body">
                                <p id="chembl_id" runat="server"></p>
                            </div>
                        </div>

                        <div class="flex-row">
                            <div class="side">
                                <p class="p-side">Kinase SARfair</p>
                            </div>
                            <div class="body">
                                <p id="kinas_sarfari" runat="server"></p>
                            </div>
                        </div>

                        <div class="flex-row">
                            <div class="side">
                                <p class="p-side">PubChem SID</p>
                            </div>
                            <div class="body">
                                <p id="pubchem_sid" runat="server"></p>
                            </div>
                        </div>

                        <div class="flex-row">
                            <div class="side">
                                <p class="p-side">ChemSpider SID</p>
                            </div>
                            <div class="body">
                                <p id="chemspider_sid" runat="server"></p>
                            </div>
                        </div>

                        <div class="flex-row">
                            <div class="side">
                                <p class="p-side">ChEBI ID</p>
                            </div>
                            <div class="body">
                                <p id="chebi_id" runat="server"></p>
                            </div>
                        </div>

                        <div class="flex-row">
                            <div class="side">
                                <p class="p-side">BindingDB ID</p>
                            </div>
                            <div class="body">
                                <p id="bindingdb_id" runat="server"></p>
                            </div>
                        </div>

                        <div class="flex-row">
                            <div class="side">
                                <p class="p-side">DrugBank ID</p>
                            </div>
                            <div class="body">
                                <p id="drugbank_id" runat="server"></p>
                            </div>
                        </div>

                        <div class="flex-row">
                            <div class="side">
                                <p class="p-side">Drug Registration</p>
                            </div>
                            <div class="body">
                                <p id="drug_registration" runat="server"></p>
                            </div>
                        </div>

                        <div class="flex-row">
                            <div class="side">
                                <p class="p-side">KEGG Drug ID</p>
                            </div>
                            <div class="body">
                                <p id="kegg_drug_id" runat="server"></p>
                            </div>
                        </div>

                        <div class="flex-row">
                            <div class="side">
                                <p class="p-side">Therapeutic Targets ID</p>
                            </div>
                            <div class="body">
                                <p id="therapeutic_target_id" runat="server"></p>
                            </div>
                        </div>

                        <div class="flex-row">
                            <div class="side">
                                <p class="p-side">PharmGKB ID</p>
                            </div>
                            <div class="body">
                                <p id="pharmgkd_id" runat="server"></p>
                            </div>
                        </div>

                        <div class="flex-row">
                            <div class="side">
                                <p class="p-side">HET ID</p>
                            </div>
                            <div class="body">
                                <p id="het_id" runat="server"></p>
                            </div>
                        </div>

                        <div class="flex-row">
                            <div class="side">
                                <p class="p-side">Drug Product ID</p>
                            </div>
                            <div class="body">
                                <p id="drug_product_id" runat="server"></p>
                            </div>
                        </div>

                        <div class="flex-row">
                            <div class="side">
                                <p class="p-side">RxList ID</p>
                            </div>
                            <div class="body">
                                <p id="rxlist_id" runat="server"></p>
                            </div>
                        </div>

                        <div class="flex-row">
                            <div class="side">
                                <p class="p-side">Drugs.com ID</p>
                            </div>
                            <div class="body">
                                <p id="drugs_com_id" runat="server"></p>
                            </div>
                        </div>

                        <div class="flex-row">
                            <div class="side">
                                <p class="p-side">Wikipedia</p>
                            </div>
                            <div class="body">
                                <p id="wikipedia" runat="server"></p>
                            </div>
                        </div>

                        <div class="flex-row">
                            <div class="side">
                                <p class="p-side">General Targets</p>
                            </div>
                            <div class="body">
                                <p id="general_targets" runat="server"></p>
                            </div>
                        </div>

                        <div class="flex-row">
                            <div class="side">
                                <p class="p-side">General Activity</p>
                            </div>
                            <div class="body">
                                <p id="general_activity" runat="server"></p>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="flex-col">
                    <div class="image-label">
                        <p class="p-side">Compound Structure</p>
                    </div>
                    <div class="image-content">
                        <img src="./Images/placeholder.jpg" alt="Alternate Text" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%-- ---------- --%>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Commentary</p>
        </div>
        <div class="c-col body-content-long">
            <p id="commentary" runat="server"></p>
        </div>
        <div class="c-col advertisment-content"></div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Clinically Approved</p>
        </div>
        <div class="c-col body-content-long">
            <p id="clinically_approved" runat="server"></p>
        </div>
        <div class="c-col advertisment-content"></div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Lastest Stage Trials</p>
        </div>
        <div class="c-col body-content-long">
            <p id="latest_stage_trials" runat="server"></p>
        </div>
        <div class="c-col advertisment-content"></div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Producer</p>
        </div>
        <div class="c-col body-content-long">
            <p id="producer" runat="server"></p>
        </div>
        <div class="c-col advertisment-content"></div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Disease Applications</p>
        </div>
        <div class="c-col body-content-long">
            <p id="disease_applications" runat="server"></p>
        </div>
        <div class="c-col advertisment-content"></div>
    </div>

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <p class="p-side">Toxic Effects</p>
        </div>
        <div class="c-col body-content-long">
            <p id="toxic_effects" runat="server"></p>
        </div>
        <div class="c-col advertisment-content"></div>
    </div>

    <%-- Copy drug info query below! --%>
</asp:Content>
