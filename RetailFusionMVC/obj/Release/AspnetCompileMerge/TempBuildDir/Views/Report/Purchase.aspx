<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Purchase
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Content/Scripts/custom/purchase.js" type="text/javascript"></script>
    <h2 style="color: #003399">
        Purchase Management</h2>
    <p id="rData">
        <span id="msg" style="color: Green;" />
    </p>
    <table>
        <tr>
            <td>
                Party
            </td>
            <td>
                Invoice No
            </td>
            <td>
                Invoice Date
            </td>
            <td>
                Recieved Date
            </td>
            <td>
                Stock Quantity
            </td>
            <td>
                Cash Discount
            </td>
            <td>
                Invoice Amount (Exluding GST)
            </td>
            <td>
                GST
            </td>
            <td>
                Frieght Charges
            </td>
            <td>
                Total Invoice Amount (GST+Frieght+Invoice Amount)
            </td>
            <td>
                Remarks
            </td>
        </tr>
        <tr>
            <td>
                <select id="ddlPartyName">
                </select>
            </td>
            <td>
                <input type="text" id="tInvoiceNo" style="width: 80%" />
            </td>
            <td>
                <input type="text" id="tInvoiceDate" style="width: 80%" />
            </td>
            <td>
                <input type="text" id="tRecievedDate" style="width: 80%" />
            </td>
            <td>
                <input type="text" id="tStockQty" style="width: 80%" />
            </td>
            <td>
                <input type="text" id="tCashDiscount" style="width: 80%" />
            </td>
            <td>
                <input type="text" id="tInvoiceAmount" style="width: 80%" />
            </td>
            <td>
                <input type="text" id="tVat" style="width: 80%" />
            </td>
            <td>
                <input type="text" id="tFrieghtChgs" style="width: 80%" />
            </td>
            <td>
                <input type="text" id="tTotalInvoiceAmount" style="width: 80%" />
            </td>
            <td>
                <input type="text" id="tRemarks" style="width: 80%" />
            </td>
        </tr>
        <tr>
            <td colspan="11" align="center">
                <input type="button" value="Add Invoice" id="btnAddInvoice" />
            </td>
        </tr>
        <tr>
            <td colspan="11">
                <table width="100%">
                    <tr>
                        <td>
                            Select Pending Bill ID &nbsp;&nbsp;&nbsp;
                            <select id="ddlPedingBillNo" style="width: 70px;">
                            </select>
                            &nbsp;&nbsp;&nbsp;
                            <input type="text" id="txtPartialPayment" style="width: 70px;" />
                            <input type="checkbox" id="IsPartialPayment" />Partial Amount &nbsp;&nbsp;&nbsp;
                            <input type="button" value="Submit Paid Amount" id="btnCustCreditPaid" />
                        </td>
                        <td>
                            Total Pending Amount &nbsp;&nbsp;&nbsp; <span id="spnPendingAmount"></span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table id="gridCustomerCredit" class="scroll" cellpadding="0" cellspacing="0" width="100%">
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    Select Party &nbsp;&nbsp;&nbsp;
    <select id="ddlPartyList">
    </select>
    <input type="radio" name="aType" id="rdPendingAmountRpt" checked="checked" />
    Pending Amount Report &nbsp;&nbsp;&nbsp;
    <input type="radio" id="rdPaidAmountRpt" name="aType" />
    Paid Amount Report &nbsp;&nbsp;&nbsp;
    <input type="button" id="btnGetPartyPaymentDetail" value="Get Party Payment Details" />
    <br />
    <br />
    <table width="100%" class="PendingPurchase">
        <tr>
            <td colspan="2">
                <table id="gridPurchase" class="scroll" cellpadding="0" cellspacing="0">
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" class="PaidPurchase">
        <tr>
            <td colspan="2">
                <table id="gridPaidPurchase" class="scroll" cellpadding="0" cellspacing="0">
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
