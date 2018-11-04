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

<%@ Page Language="C#" Title="Your Title Here" AutoEventWireup="true" MasterPageFile="~/BasePage.Master" CodeBehind="Form 2 Column Template.aspx.cs" Inherits="DrugProNET.Credits" %>


<asp:Content runat="server" ContentPlaceHolderID="HeadContentPlaceHolder">
    
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
    </div>
</asp:Content>
