<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Reports
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <font color="gray">Reports</font>
    </h2>
    Select Report Type
    <br />
    <select id="ddlExpanse">
        <option value=""></option>
        <option value="1">Monthly</option>
        <option value="2">Daly</option>
    </select>
</asp:Content>
