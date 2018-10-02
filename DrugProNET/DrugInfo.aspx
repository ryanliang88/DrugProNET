<%@ Page Language="C#" Title="DrugProNET | Drug Information" AutoEventWireup="true" MasterPageFile="~/BasePage.Master" CodeBehind="DrugInfo.aspx.cs" Inherits="DrugProNET.DrugInfo" %>

<asp:Content runat="server" ContentPlaceHolderID="CSSContentPlaceHolder">
    <link rel="stylesheet" href="./css/3_column.css">
    <link rel="stylesheet" href="./css/drug_info.css">
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="BodyContentPlaceHolder">
    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <h3 class="h3-side-title">Query Type</h3>
        </div>
        <div class="c-col body-content">
            <h3 class="h3-body-title">Drug Information</h3>
            <p>
                This query provides detailed information on over 2000 compounds
                    that have been experimentally identified as inhibitors of one or
                    more human proteins. Follow the instructions below to retrieve
                    information on a specific drug of interest.
            </p>
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
            <h3 class="h3-side-title">Filters</h3>
        </div>
        <div class="c-col body-content">
            <h3 class="h3-body-title">Step 1 - Drug Specification</h3>
            <p>
                Enter the first few characters for a compound name, CAS ID,
                    PubChem ID or ChEMBL ID and then select the desired search
                    term from the Drop Down List.
            </p>
            <asp:TextBox CssClass="textBox" ID="search_textBox" runat="server" value="" placeholder="Type in at least 3 letters of the search term" />
            <h3 class="h3-body-title">Step 2 - Retrieve Compound Information</h3>
            <p>
                Click on the buttons below to retrieve information on the
                    compound of interest or to reset the parameters for a new query.
            </p>

            <asp:Button ID="retrieve_button" CssClass="button" Text="Retrieve Information" runat="server" />
            <span>&emsp;&emsp;</span>
            <asp:Button ID="reset_button" CssClass="button" Text="Reset" runat="server" />
        </div>
    </div>
</asp:Content>
