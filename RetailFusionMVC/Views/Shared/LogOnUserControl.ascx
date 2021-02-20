<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    if (Session["store"] != null)
    {
%>
Welcome <b>
    <%: Page.User.Identity.Name %></b>! [
<%: Html.ActionLink("Log Off", "LogOff", "Account")%>
]
<%
    }
    else
    {
%>
[
<%: Html.ActionLink("Log On", "Index", "Account")%>
]
<%
    }
%>
