<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    StockMgmt
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Content/Scripts/custom/stockmgmt.js" type="text/javascript"></script>
    <h2>Stock Management</h2>
    <p id="rData">
        <span id="msg" style="color: Green;" />
    </p>
    <div id="tabs" style="width: 100%;overflow: scroll;" >
        <ul>
            <li><a href="#tabs-1">Defective/Stock Return</a></li>
        </ul>
        <div id="tabs-1">
            <table>
                <tr>
                    <td>Select Stock Return Id &nbsp;&nbsp;&nbsp;
                <select id="ddlStockReturnId">
                </select>
                        Status&nbsp;&nbsp;&nbsp;
                <select id="ddlStatus">
                    <option value="CNPending">CNPending</option>
                    <option value="Disptached">Disptached</option>
                    <option value="CNReceived">CNReceived</option>
                </select>
                        &nbsp;&nbsp;&nbsp;
                <input type="button" value="Update Status" id="btnUpdateStatus" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <table width="100%">
                            <tr>
                                <td>ReturnType
                                </td>
                                <td>Party
                                </td>
                                <td>Quantity
                                </td>
                                <td>Value
                                </td>
                                <td>Courier Name
                                </td>
                                <td>Tracking Id
                                </td>
                                <td>Remark
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <select id="ddlReturnType">
                                        <option value="">-Select-</option>
                                        <option value="1">Defective</option>
                                        <option value="2">StockCorrrection</option>
                                        <option value="3">FreshReturn</option>
                                        <option value="4">EOSSReturn</option>
                                    </select>
                                </td>
                                <td>
                                    <select id="ddlPartyName">
                                    </select>
                                </td>
                                <td>
                                    <input type="text" id="tStockQty" style="width: 80%" />
                                </td>
                                <td>
                                    <input type="text" id="tValue" style="width: 80%" />
                                </td>
                                <td>
                                    <input type="text" id="tCourierName" style="width: 80%" />
                                </td>
                                <td>
                                    <input type="text" id="tTrackingId" style="width: 80%" />
                                </td>
                                <td>
                                    <input type="text" id="tRemark" style="width: 80%" />
                                </td>
                                <td>
                                    <input type="submit" id="btnSubmitReturn" value="Submit Return" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        <table id="tblStockReturn">
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>


    <br />
</asp:Content>
