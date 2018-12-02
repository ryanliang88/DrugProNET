<%@ Page Language="C#" Title="DrugProNET | Drug Information" AutoEventWireup="true" MasterPageFile="~/BasePage.Master" CodeBehind="DrugInfo.aspx.cs" Inherits="DrugProNET.DrugInfo" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content runat="server" ContentPlaceHolderID="HeadContentPlaceHolder">
    <link rel="stylesheet" href="./css/3_column.css">
    <link rel="stylesheet" href="./css/drug_info.css">
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="BodyContentPlaceHolder">
    <div class="c-row ad_row_min_height">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <h3 class="h3-side-title">Query Type</h3>
        </div>
        <div class="c-col body-content">
            <h3 class="h3-body-title">Drug Information</h3>
            <p>
                This query provides detailed information on over 2000 compounds that have been experimentally identified as inhibitors of one or more human proteins. Follow the instructions below to retrieve information on a specific drug of interest.
            </p>
        </div>
        <div class="c-col advertisment-content">
            <asp:UpdatePanel ID="ad_update_panel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Timer ID="ad_refresh_timer" runat="server" Interval="10000" OnPreRender="RenewAdvertisement" OnTick="RenewAdvertisement"></asp:Timer>
                    <asp:HyperLink ID="adLink" NavigateUrl="navigateurl" runat="server" Target="_blank">
                        <asp:Image ImageUrl="imageUrl" runat="server" ID="adBanner" AlternateText="" Width="288px"/>
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
            <h3 class="h3-side-title">Filters</h3>
        </div>
        <div class="c-col body-content">
            <h3 class="h3-body-title">Step 1 - Drug Specification</h3>
            <p>
                Enter the first few characters for a compound name, CAS ID, PubChem ID or ChEMBL ID and then select the desired search term from the Drop Down List.
            </p>
            <asp:UpdatePanel ID="search_textBox_UpdatePanel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:TextBox CssClass="textBox" ID="search_textBox" runat="server" placeholder="Type in at least 3 letters of the search term" />
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender" runat="server" ServiceMethod="GetAutoCompleteData" TargetControlID="search_textBox" CompletionInterval="1000" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="listItemHighlighted" EnableCaching="true" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <h3 class="h3-body-title">Step 2 - Retrieve Compound Information</h3>
            <p>
                Click on the buttons below to retrieve information on the compound of interest or to reset the parameters for a new query.
            </p>
            <asp:Button ID="retrieve_button" CssClass="button" Text="Retrieve Information" runat="server" OnClick="RetrieveData" OnClientClick="target='_blank'" />
            <span>&emsp;&emsp;</span>
            <asp:Button ID="reset_button" CssClass="button" Text="Reset" runat="server" OnClientClick="window.location.reload(); return false;"  />
        </div>
    </div>
</asp:Content>
