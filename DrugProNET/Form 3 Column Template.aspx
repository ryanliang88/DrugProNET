<%-- THIS PAGE IS INTENDED TO BE AS A TEMPLATE FOR CREATING OTHER PAGES WITH 3 COLUMNS, THIS PAGE SHOULD NEVER BE LINKED TO! --%>

<%-- Use:
        To add content, the first thing you must do is the the <div class="c-row"> tag, this will declare a row in the webpage.
        Next you should have 3 <div class="c-col side-content"> tags, since this page is a 3 column page, you will add your content
        in between each <div class="c-col side-content"> tag --%>

<%-- Set the title of your page via the "Title" attribute! --%>
<%@ Page Language="C#" Title="Your title here" AutoEventWireup="true" MasterPageFile="~/BasePage.Master" CodeBehind="Form 3 Column Template.aspx.cs" Inherits="DrugProNET.Form_3_Column_Template" %>

<asp:Content runat="server" ContentPlaceHolderID="CSSContentPlaceHolder">
    <%-- Place your CSS link tags here, do NOT add the base_style.css in this tag, it is already included in the master page --%>

    <%-- This stylesheet should be included since this is a 3 column template --%>
    <link rel="stylesheet" href="./css/3_column.css">
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
                    <asp:HyperLink ID="ad_link" NavigateUrl="navigateurl" runat="server">
                        <asp:Image ImageUrl="imageUrl" runat="server" ID="ad_banner" AlternateText="" />
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
