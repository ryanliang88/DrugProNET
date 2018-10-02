<%-- THIS PAGE IS INTENDED TO BE AS A TEMPLATE FOR CREATING OTHER PAGES WITH 3 COLUMNS, THIS PAGE SHOULD NEVER BE LINKED TO! --%>

<%-- Use:
        To add content, the first thing you must do is add the <div class="c-row"> tag, this will declare a row in the webpage.
        Next you should have 2 <div class="c-col side-content"> tags between the <div class="c-row"> tags since this page is a 
        2 column page, you will add your content in between each <div class="c-col side-content"> tag --%>

<%-- Set the title of your page via the "Title" attribute! --%>

<%@ Page Language="C#" Title="Your Title Here" AutoEventWireup="true" MasterPageFile="~/BasePage.Master" CodeBehind="Form 2 Column Template.aspx.cs" Inherits="DrugProNET.Credits" %>


<asp:Content runat="server" ContentPlaceHolderID="CSSContentPlaceHolder">
    <%-- Place your CSS link tags here, do NOT add the base_style.css in this tag, it is already included in the master page --%>
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
