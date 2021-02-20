<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    StoreSetup
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            //validation for numerics
            $("#tExpAmount").keydown(function (e) {
                // Allow: backspace, delete, tab, escape, enter and .
                if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                // Allow: Ctrl+A, Command+A
            (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
                    // let it happen, don't do anything
                    return;
                }
                // Ensure that it is a number and stop the keypress
                if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
            });

            $("#gridStoreSetupSummary").jqGrid({
                url: "/Home/GetStoreSetupSummary",
                datatype: 'json',
                mtype: 'GET',
                colNames: [ 'Expense By','Expense Amount'],
                colModel: [

                        { key: false, name: 'UserName', sortable: false },
                        { key: false, name: 'ExpenseAmount', sortable: false }
                    ],
                height: '100px',
                overflow: scroll,
                jsonReader:
                {
                    root: "rows",
                    page: "page",
                    total: "records",
                    repeatitems: false,
                    id: "0"
                },
                viewrecords: true,
                emptyrecords: 'No records to display',
                autowidth: true,
                multiselect: false
            });

            $("#gridStoreSetupExpanse").jqGrid({
                url: "/Home/GetStoreSetupExpanseDetail",
                datatype: 'json',
                mtype: 'GET',
                colNames: ['Expense Description', 'Expense Amount', 'Expense By', 'Expense Date'],
                colModel: [
                        { key: false, name: 'ExpenseDesc', sortable: false },
                        { key: false, name: 'ExpenseAmount', sortable: false },
                        { key: false, name: 'UserName', sortable: false },
                        { key: false, name: 'ExpenseDate', sortable: false }
                    ],
                height: '500px',
                rowNum: 0,
                overflow: scroll,
                jsonReader:
                {
                    root: "rows",
                    page: "page",
                    total: "records",
                    repeatitems: false,
                    id: "0"
                },
                viewrecords: true,
                emptyrecords: 'No records to display',
                autowidth: true,
                multiselect: false
            });

            $('#btnSubmit').click(function () {
                var url = "/Home/SubmitStoreExpense";
                var expenseAmount = $('#tExpAmount').val();
                var expenseDesc = $('#tExpDesc').val();

                $.post(url, { ExpenseDesc: expenseDesc, ExpenseAmount: expenseAmount }, function (data) {
                    $("#msg").html(data);
                });


                $.ajax({
                    url: "/Home/GetStoreSetupExpanseDetail",
                    dataType: "json",
                    data: {},
                    type: "GET",
                    contentType: 'application/json',
                    success: function (data) {
                        jQuery("#gridStoreSetupExpanse").jqGrid('setGridParam', { data: data }).trigger('reloadGrid');
                    }
                });

            });

        });        
    </script>
    <p id="rData">
        <span id="msg" style="color: Green;" />
    </p>
    <table width="100%">
        <tr>
            <th >
                <b>Submit Expense</b>
            </th>
            <th >
                <b>Expense Summary</b>
            </th>
        </tr>
        <tr>
            <td width="50%">
                <fieldset style="width: 50%">
                    <legend>Please enter store expense details</legend>
                    <div class="editor-label" align="left">
                        Expense Descpription
                    </div>
                    <div class="editor-field">
                        <input type="text" id="tExpDesc" />
                    </div>
                    <div class="editor-label" align="left">
                        Expense Amount
                    </div>
                    <div class="editor-field">
                        <input type="text" id="tExpAmount" />
                    </div>
                    <p>
                        <input disabled="disabled" type="submit" id="btnSubmit" value="Submit Expense" />
                    </p>
                </fieldset>
            </td>
            <td width="50%">
                <div>
                    <table id="gridStoreSetupSummary" class="scroll" cellpadding="0" cellspacing="0"
                        width="100%">
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <div>
        <table id="gridStoreSetupExpanse" class="scroll" cellpadding="0" cellspacing="0"
            width="100%">
        </table>
    </div>
</asp:Content>
