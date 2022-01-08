<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Reports
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Content/Scripts/custom/reports.js" type="text/javascript"></script>
    <h2>
        <font style="color: #003399">Reports</font>
    </h2>
    <table>
        <tr>
            <td>Select Report Type
            </td>
            <td>
                <select id="ddlReportType">
                    <option value="0">--Select--</option>
                    <option value="1">Month Wise Sale Summary Report</option>
                    <option value="2">Party Pending Payment Report</option>
                    <option value="3">Monthly Expense Detail Report</option>
                    <option value="4">Month Sale Detail Report</option>
                    <option value="5">Month Expense Detail Report</option>
                    <option value="6">Ledger Summary</option>
                </select>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <select id="ddlMonthYear" class="ddlMonthYear">
                </select>
            </td>
        </tr>
        <tr id="trDuration">
            <td>From
            </td>
            <td>
                <input type="text" id="fromDate" style="width: 80%" />
            </td>
            <td>To
            </td>
            <td>
                <input type="text" id="toDate" style="width: 80%" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <br />
                <input type="button" id="btnGetReport" value="Get Report" onclick="LoadGrids();" />
            </td>
        </tr>
    </table>
    <div style="overflow: scroll;">
        <table width="100%" id="tblMonthlyReport" class="MonthlyReport">
            <tr>
                <th colspan="2">
                    <b>Monthly Sale Summary</b>
                </th>
            </tr>
            <tr>
                <td colspan="2">
                    <table id="gridEODAll" class="scroll" cellpadding="0" cellspacing="0" width="100%">
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div style="overflow: scroll;">
        <table width="100%" id="tblLedgerSummary" class="LedgerSummary">
            <tr>
                <th colspan="2">
                    <b>Ledger Summary</b>
                </th>
            </tr>
            <tr>
                <td colspan="2">
                    <table id="gridLedgerSummary" class="scroll" cellpadding="0" cellspacing="0" width="100%">
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div style="overflow: scroll;">
        <table id="tblPartyPendingReprot" class="PartyPendingReprot" width="100%">
            <tr>
                <th colspan="2">
                    <b>Party Pending Amount Summary</b>
                </th>
            </tr>
            <tr>
                <td colspan="2">
                    <table id="gridPendingSummary" class="scroll" cellpadding="0" cellspacing="0" width="100%">
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div style="overflow: scroll;">
        <table id="tblExpanceSummary" class="ExpanceSummary" width="100%">
            <tr>
                <th colspan="2">
                    <b>Expense Detailed Summary</b>
                </th>
            </tr>
            <tr>
                <td colspan="2">
                    <table id="gridExpanceSummary" class="scroll" cellpadding="0" cellspacing="0" width="100%">
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="gridWrapper" style="overflow: scroll;">
        <table>
            <tr>
                <th>
                    <b>Monthly Sale Detail</b>
                </th>
            </tr>
            <tr>
                <td>
                    <table id="gridEOD" class="MonthlySaleDetail" cellpadding="0" cellspacing="0" width="100%">
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="gridWrapperExpenseDetail" style="overflow: scroll;">
        <table width="100%">
            <tr>
                <th>
                    <b>Monthly Expense Detail</b>
                </th>
            </tr>
            <tr>
                <td>
                    <table id="gridExpanceDetail" class="scroll" cellpadding="0" cellspacing="0" width="100%">
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
