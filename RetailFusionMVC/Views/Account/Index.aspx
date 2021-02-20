<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $('#btnLogon').click(function () {
                var url = "/Account/Logon";
                var userName = $('#tUserName').val();
                var password = $('#tPassword').val();
                var store = $('#ddlStore option:selected').val();
                if (userName == "" || password == "") {
                    alert("Please enter user name / password !!");
                }
                else {
                    if (store == "") {
                        alert("Please select Store.");
                    }
                    else {
                        $.post(url, { UserName: userName, Password: password, Store: store }, function (data) {
                            if (data = 'true') {
                                window.location.href = "/Home/EOD";
                            }
                            else {
                                alert("Please enter user name / password !!");
                                return false;
                            }
                        });
                    }
                }
            });
        });
    </script>
    <h2>
        <font style="color: #003399">Welcome to Retail Fusion</font>
    </h2>
    <p id="rData">
        <span id="msg" style="color: Green;" />
    </p>
    <table width="100%">
        <tr>
            <td align="right">
                <fieldset style="width: 25%">
                    <legend>Account Information</legend>
                    <div class="editor-label" align="left">
                        User name
                    </div>
                    <div class="editor-field">
                        <input type="text" id="tUserName" style="width: 100%;" />
                    </div>
                    <div class="editor-label" align="left">
                        Password
                    </div>
                    <div class="editor-field">
                        <input type="password" id="tPassword" style="width: 100%;" />
                    </div>
                    <div class="editor-label" align="left">
                        Store
                    </div>
                    <div class="editor-field">
                        <select id="ddlStore" style="width: 100%;">
                            <option value="-Select Store-"></option>
                            <option value="2">Fashion Fusion</option>
                            <option value="4">Fashion Fusion Women</option>
                            <option value="1">FF Singrauli</option>
                            <option value="3">Sarovar</option>
                            <option value="5">Test Store</option>
                        </select>
                    </div>
                    <p>
                        <input type="submit" id="btnLogon" value="Log On" />
                    </p>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
