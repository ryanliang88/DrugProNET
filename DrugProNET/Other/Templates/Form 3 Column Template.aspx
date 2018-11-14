<%-- THIS PAGE IS INTENDED TO BE AS A TEMPLATE FOR CREATING OTHER PAGES WITH 3 COLUMNS, THIS PAGE SHOULD NEVER BE LINKED TO! --%>

<%-- 
#######################################################################
#                                Usage                                #
#######################################################################

For each row that you need add the following to the BodyContentPlaceHolder:

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
        </div>
        <div class="c-col body-content">
        </div>
    </div>

If you need to add CSS, scripts or other tags that belong in the head section,
place the tags in the HeadContentPlaceHolder section. By default the base_style.css
us added within the master page, so you do not need to include it in this template

--%>

<%-- Set the title of your page via the "Title" attribute! --%>

<%@ Page Language="C#" Title="Your Title Here" AutoEventWireup="true" MasterPageFile="~/BasePage.Master" CodeBehind="Form 3 Column Template.aspx.cs" Inherits="DrugProNET.Form_3_Column_Template" %>

<asp:Content runat="server" ContentPlaceHolderID="HeadContentPlaceHolder">

    <%-- This stylesheet should be included since this is a 3 column template --%>
    <link rel="stylesheet" href="./css/3_column.css">
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="BodyContentPlaceHolder">

    <div class="c-row">
        <div class="c-col side-spacing"></div>
        <div class="c-col side-content">
            <%-- Your side bar content here! --%>
        </div>
        <div class="c-col body-content">
            <%-- Your body conent here! --%>
        </div>
        <div class="c-col advertisment-content">
            <asp:UpdatePanel ID="ad_update_panel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Timer ID="ad_refresh_timer" runat="server" Interval="10000" OnPreRender="RenewAdvertisement" OnTick="RenewAdvertisement"></asp:Timer>
                    <asp:HyperLink ID="adLink" NavigateUrl="navigateurl" runat="server">
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
            <%-- Your side bar content here! --%>
        </div>
        <div class="c-col body-content">
            <%-- Your body content here! --%>
        </div>
    </div>
</asp:Content>
