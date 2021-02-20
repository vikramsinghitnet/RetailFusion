<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Master
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Content/Scripts/custom/partyMaster.js" type="text/javascript"></script>
    <h2>
        <font style="color: #003399">Master Management</font>
    </h2>
    <p id="rData">
        <span id="msg" style="color: Green;" />
    </p>
    <div>
        <table width="100%">
            <tr>
                <td width="50%" align="center">
                    <table>
                        <tr>
                            <th colspan="2">
                                <b>Enter Party details</b>
                            </th>
                        </tr>
                        <tr>
                            <td>
                                Party Name
                            </td>
                            <td>
                                <input type="text" id="tPartyName" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Address
                            </td>
                            <td>
                                <textarea id="tPartyAddress" cols="30" rows="3"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Contacts
                            </td>
                            <td>
                                <textarea id="tPartyContacts" cols="30" rows="3"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Bank Details
                            </td>
                            <td>
                                <textarea id="tPartyBankDetails" cols="30" rows="3"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Brands Available
                            </td>
                            <td>
                                <textarea id="tBrands" cols="30" rows="3"></textarea>
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%" align="center">
                    <table>
                        <tr>
                            <th colspan="2">
                                <b>Enter Employee details</b>
                            </th>
                        </tr>
                        <tr>
                            <td>
                                Employee Name
                            </td>
                            <td>
                                <input type="text" id="tEmpName" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Address
                            </td>
                            <td>
                                <textarea type="text" id="tEmployeeAddress" cols="30" rows="3"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Mobile
                            </td>
                            <td>
                                <input type="text" id="tEmployeeMobile" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Proof Type
                            </td>
                            <td>
                                <input type="text" id="tEmployeeProofType" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Proof ID No.
                            </td>
                            <td>
                                <input type="text" id="tEmployeeProofId" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <div align="center">
                        <input type="button" value="Add Party" id="btnAddParty" />
                    </div>
                    <br />
                </td>
                <td>
                    <br />
                    <div align="center">
                        <input type="button" value="Add Employee" id="btnAddEmployee" />
                    </div>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <table id="gridPartyList" class="scroll" cellpadding="0" cellspacing="0">
                    </table>
                </td>
                <td>
                    <table id="gridEmployeeList" class="scroll" cellpadding="0" cellspacing="0">
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
