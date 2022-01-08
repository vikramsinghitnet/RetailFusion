<%@ Page Title="EOD" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    EOD
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Content/Scripts/custom/eod.js" type="text/javascript"></script>
    <p id="rData">
        <span id="msg" style="color: Green;" />
    </p>
    <div id="tabs" style="width: 100%;">
        <ul>
            <li><a href="#tabs-1">Todays Transactions</a></li>
            <li><a href="#tabs-3">Month Summery</a></li>
        </ul>
        <div id="tabs-1">
            <b>EOD Date </b>
            <input type="text" id="tDate" style="width: 10%" />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <br />
            <br />
            <div id="accordion">
                <h3>Daily Expenses</h3>
                <div>
                    <table width="100%" border="0">
                        <tr>
                            <td>
                                <fieldset style="width: 90%; height: 215px;">
                                    <legend>Shop Expenses</legend>
                                    <table width="95%" style="height: 90%; border: none;">
                                        <tr>
                                            <td>Today Total Expanses
                    <br />
                                                <br />
                                                Expanse Type
                    <br />
                                                <br />
                                                Remarks
                                            </td>
                                            <td>
                                                <input type="text" id="tExpenses" style="width: 100%" />
                                                <br />
                                                <br />
                                                <% if (Session["store"] != null && Session["store"].ToString() == "0")
                                                    {%>
                                                <select id="ddlExpanse" style="width: 100%">
                                                    <option value=""></option>
                                                    <option value="1">Vegetable</option>
                                                    <option value="4">Kirana</option>
                                                    <option value="12">Bisleri</option>
                                                    <option value="16">GAS</option>
                                                    <option value="17">Woods</option>
                                                    <option value="18">Mutton</option>
                                                    <option value="19">Chicken</option>
                                                    <option value="20">Paneer</option>
                                                    <option value="21">Fish</option>
                                                    <option value="3">Petrol</option>
                                                    <option value="13">Shop Maintenence</option>
                                                    <option value="14">Advertising</option>
                                                    <option value="11">Customer Credit</option>
                                                    <option value="5">Electricity Bill</option>
                                                    <option value="8">Rent</option>
                                                    <option value="15">Other</option>
                                                </select>
                                                <%}
                                                    else
                                                    {
                                                %>
                                                <select id="ddlExpanse" style="width: 100%">
                                                    <option value=""></option>
                                                    <%--<option value="1">Vegetable</option>--%>
                                                    <option value="2">Alteration</option>
                                                    <option value="3">Petrol</option>
                                                    <%--<option value="4">Kirana</option>--%>
                                                    <option value="5">Electricity Bill</option>
                                                    <option value="6">Buying</option>
                                                    <option value="7">Stock Transport</option>
                                                    <option value="8">Shop Rent</option>
                                                    <option value="11">Customer Credit</option>
                                                    <%--<option value="12">Petrol</option>--%>
                                                    <option value="13">Shop Maintenence</option>
                                                    <option value="14">Advertising</option>
                                                    <option value="15">Other</option>
                                                </select>
                                                <%} %>
                                                <br />
                                                <br />
                                                <input type="text" id="tExpenseRemarks" style="width: 100%" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <br />
                                                <br />
                                                <br />

                                                <input type="button" value="Add Expanses" id="btnExpanse" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                            <td align="center">
                                <table id="gridExpanse" class="scroll" cellpadding="0" cellspacing="0" width="100%">
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <h3>Employee Payments</h3>
                <div>
                    <table width="100%" border="0">
                        <tr>
                            <td>
                                <fieldset style="width: 90%; height: 215px;">
                                    <legend>Employee Payments</legend>
                                    <table width="95%" style="height: 90%; border: none;">
                                        <tr>
                                            <td>Payment Amount
                    <br />
                                                <br />
                                                Employee Name
                    <br />
                                                <br />
                                                Payment Type
                    <br />
                                                <br />
                                                Remarks
              
                                            </td>
                                            <td>
                                                <input type="text" id="tAdvance" style="width: 100%" />
                                                <br />
                                                <br />
                                                <select id='ddlEmpName' style="width: 100%">
                                                </select>
                                                <br />
                                                <br />
                                                <select id="ddlAdvanceType" style="width: 100%">
                                                    <option value=""></option>
                                                    <option value="1">Advance</option>
                                                    <option value="2">Salary</option>
                                                    <option value="3">Incentive</option>
                                                </select>
                                                <br />
                                                <br />
                                                <input type="text" id="tRemarks" style="width: 100%" />
                                            </td>

                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <input type="button" value="Add Advance" id="btnAdvance" />
                                            </td>
                                        </tr>

                                    </table>
                                </fieldset>
                            </td>
                            <td align="center" style="width: 50%">
                                <table id="gridAdvance" class="scroll" cellpadding="0" cellspacing="0">
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <h3>Bank Deposits</h3>
                <div>
                    <table width="100%" border="0">
                        <tr>
                            <td>
                                <fieldset style="width: 90%; height: 215px;">
                                    <legend>Deposits</legend>
                                    <table style="border: none;">
                                        <tr>
                                            <td>Deposit Amount
                    <br />
                                                <br />
                                                Bank Name
                                            </td>
                                            <td>
                                                <input type="text" id="tDeposit" style="width: 100%" />
                                                <br />
                                                <br />
                                                <select id="ddlBank" style="width: 100%">
                                                    <option value=""></option>
                                                    <%--<option value="OBC">OBC</option>--%>
                                                    <option value="HDFC">HDFC</option>
                                                    <option value="HDFC Online">HDFC Online</option>
                                                    <option value="BOB MN">BOB MN</option>
                                                    <option value="BOB MN Online">BOB MN Online</option>
                                                    <option value="BOB AN">BOB AN</option>
                                                    <option value="BOB AN Online">BOB AN Online</option>
                                                    <option value="GPAY AN">GPAY AN</option>
                                                </select>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <input type="button" value="Add Deposit" id="btnDeposit" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                            <td align="center">
                                <table id="gridDeposit" class="scroll" cellpadding="0" cellspacing="0" width="50%">
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <h3>Party Payments</h3>
                <div>
                    <table width="100%" border="0">
                        <tr>
                            <td>
                                <fieldset style="width: 90%; height: 215px;">
                                    <legend>Party Payments</legend>
                                    <table style="border: none;">
                                        <tr>
                                            <td>Party Payment Amount
                    <br />
                                                <br />
                                                Party Name
                                            </td>
                                            <td>
                                                <input type="text" id="tParty" style="width: 80%" />
                                                <br />
                                                <br />
                                                <select id="ddlPartyName" style="width: 80%">
                                                </select>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <input type="button" value="Add Party Payment" id="btnPartyPayment" />
                                            </td>
                                        </tr>

                                    </table>
                                </fieldset>

                            </td>
                            <td align="center">
                                <table id="gridPartyPayment" class="scroll" cellpadding="0" cellspacing="0" width="50%">
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <h3>Save Eod</h3>
                <div>
                    <table style="width: 100%;">
                        <tr>
                            <th colspan="8" style="width: 100%;">
                                <b>Please Enter EOD Details</b>
                            </th>
                        </tr>
                        <tr>
                            <td>Sale (Gross)
                            </td>
                            <td>
                                <input type="text" id="tSale" style="width: 80%" />
                            </td>
                            <td>Discount
                            </td>
                            <td>
                                <input type="text" id="tDiscount" style="width: 80%" />
                            </td>
                            <td>Card Payment
                            </td>
                            <td>
                                <input type="text" id="tCardPayment" style="width: 80%" />
                            </td>
                            <td>Counter Cash
                    <br />
                                (Closing Balance)
                            </td>
                            <td>
                                <input type="text" id="tCounterCash" style="width: 80%" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div align="center">
                        <input type="button" value="Save EOD Detail" id="tSubmit" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <input type="button" value="EOD Calculator" id="eodCalculator" onclick="showCalculator()"/>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <input type="button" value="Delete Today EOD" id="tDeleteTodayEOD" />
                    </div>

                    <div style="overflow: scroll;">

                        <table width="100%">
                            <tr>
                                <th>
                                    <b>Last 2 Days Summary</b>
                                </th>
                            </tr>
                            <tr>
                                <td align="center" width="100%">
                                    <br />
                                    <table id="gridTodayEOD" width="100%">
                                    </table>
                                </td>
                            </tr>
                        </table>

                    </div>
                </div>
            </div>
        </div>
        <div id="tabs-3">
            <div style="overflow: scroll;">
                <table width="80%">
                    <tr>
                        <th>
                            <b>Month Summary</b>
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <table id="gridMonthSummary" class="scroll" cellpadding="0" cellspacing="0" width="100%">
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <br />
                            <table id="gridEOD" class="scroll" cellpadding="0" cellspacing="0" width="100%">
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>

    <div style="width: 100%;">
    </div>

    <br />

    <div style="overflow: scroll;">
        <table width="100%">
            <tr>
                <th>
                    <b>Customer Hold :</b> &nbsp;&nbsp;&nbsp;  Total Pending Amount &nbsp;- <span id="spnPendingAmount"></span>
                </th>
            </tr>
            <tr>
                <td width="100%">
                    <table id="gridCustomerCredit" class="scroll" cellpadding="0" cellspacing="0" width="100%">
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <br />

    <div id="dialog">
    </div>
</asp:Content>
