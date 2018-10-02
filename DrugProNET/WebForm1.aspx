<%@ Page Title="" Language="C#" MasterPageFile="~/BasePage.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="DrugProNET.WebForm1" %>

<asp:Content runat="server" ContentPlaceHolderID="CSSContentPlaceHolder">
    <%-- Place your CSS link tags here, do NOT add the base_style.css in this tag, it is already included in the master page --%>

    <%-- This stylesheet should be included since this is a 3 column template --%>
    <link rel="stylesheet" href="~/css/3_column.css" runat="server">
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="BodyContentPlaceHolder">

    <%-- First row --%>
    <div class="c-row">
        <div class="c-col side-content">
            <%-- Your side bar content here! --%>
        </div>
        <div class="c-col body-content">
            <%-- Your body conent here! --%>
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

    <%-- Second row --%>
    <div class="c-row">
        <div class="c-col side-content">
            <%-- Your side bar content here! --%>
        </div>
        <div class="c-col body-content">
            <%-- Your body content here! --%>
        </div>
    </div>
    <%-- Second row end --%>
</asp:Content>
