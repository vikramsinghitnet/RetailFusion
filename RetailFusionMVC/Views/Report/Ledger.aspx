<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Ledger
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Content/Scripts/custom/ledger.js" type="text/javascript"></script>
    <h2 style="color: #003399">Ledger</h2>
    <p id="rData">
        <span id="msg" style="color: Green;" />
    </p>
    <div id="pager"></div>
    <table>
        <tr>
            <td>Select Party
            </td>
            <td>
                <select id="ddlPartyName">
                </select>
            </td>
            <td>Transaction Type
            </td>
            <td>
                <select id="ddlTranType">
                    <option value="">-Select-</option>
                    <option value="Purchase">Purchase</option>
                    <option value="Payment">Payment</option>
                </select>
            </td>
             <td>Branch &nbsp;
            </td>
            <td>
                <select id="ddlBranch">
                    <option value="">-Select-</option>
                    <option value="Waidhan">Waidhan Men</option>
                    <option value="Shahdol">Shahdol</option>
                    <option value="Women">Waidhan Women</option>
                    <option value="Singrauli">Singrauli</option>
                </select>
            </td>
        </tr>
    </table>
    <br />
    <table>
        <tr>
            <td>Date
            </td>
            <td>
                <input type="text" id="tDate" style="width: 80%" />
            </td>
            <td>Invoice No
            </td>
            <td>
                <input type="text" id="tInvoiceNo" style="width: 80%" />
            </td>
            <td>Amount
            </td>
            <td>
                <input type="text" id="tAmount" style="width: 80%" />
            </td>
            <td>Brand
            </td>
            <td>
                <input type="text" id="tBrand" style="width: 100%" />
            </td>
            <td>Remarks
            </td>
            <td>
                <input type="text" id="tRemarks" style="width: 100%" />
            </td>
        </tr>
    </table>
    <br />
    <input type="button" id="save" value="Save" />
    &nbsp;&nbsp;&nbsp;&nbsp;
    <input type="button" value="Delete Today's Ledger" id="tDeleteLedger" />
    <br />
    <br />
    <table id="gridLedger" class="scroll" cellpadding="0" cellspacing="0">
    </table>
</asp:Content>
