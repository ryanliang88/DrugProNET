
<%-- THIS PAGE IS INTENDED TO BE AS A TEMPLATE FOR CREATING OTHER PAGES WITH 3 COLUMNS, THIS PAGE SHOULD NEVER BE LINKED TO! --%>

<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/BasePage.Master" CodeBehind="Form 3 Column Template.aspx.cs" Inherits="DrugProNET.Form_3_Column_Template" %>

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
