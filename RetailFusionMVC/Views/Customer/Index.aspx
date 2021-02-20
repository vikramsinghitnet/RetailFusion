<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Customer Credit
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script language="javascript" type="text/javascript">

        function loadFreshCustomerCreditDetails() {
            $('#ddlPedingCustomerCredit').empty();
            $.ajax({
                url: "/Customer/GetPedingCustomerCredit",
                type: "Get",
                success:
                    function (data) {
                        var optParty = new Option('', '');
                        $('#ddlPedingCustomerCredit').append(optParty);
                        for (var i = 0; i < data.rows.length; i++) {
                            var opt = new Option(data.rows[i], data.rows[i]);
                            $('#ddlPedingCustomerCredit').append(opt);
                        }
                    }
            });

            var url = "/Customer/GetCustomerPendingAmount";
            $.post(url, {}, function (data) {
                $("#spnPendingAmount").html(data);
            });
        }

        $(document).ready(function () {
            $("#tDate").datepicker().datepicker('setDate', new Date());
            loadFreshCustomerCreditDetails();
            $('#tDate').val('');
            $('#btnCustCreditPaid').click(function () {

                var tDate = $('#tDate').val();
                if (tDate == '') {
                    alert('Please select EOD date !!');
                }

                var url = "/Customer/SubmitPaidAmount";
                var advanceId = $('#ddlPedingCustomerCredit option:selected').val();;

                $.post(url, { AdvanceId: advanceId, CreditDate: tDate }, function (data) {
                    $("#msg").html(data);
                });

                $.ajax({
                    url: "/Customer/CustomerCreditDetails",
                    dataType: "json",
                    data: {},
                    type: "GET",
                    contentType: 'application/json',
                    success: function (data) {
                        jQuery("#gridCustomerCredit").jqGrid('setGridParam', { data: data }).trigger('reloadGrid');
                    }
                });

                loadFreshCustomerCreditDetails();
            });
            $("#gridCustomerCredit").jqGrid({
                url: "/Customer/CustomerCreditDetails",
                datatype: 'json',
                mtype: 'GET',
                colNames: ['Store', 'Credit Id', 'Credit Amount', 'Credit Date', 'Remarks','Received Date' ,'Payment Status'],
                colModel: [
                    { key: false, name: 'Store', sortable: false },
                    { key: true, name: 'ExpenseId', sortable: false },
                    { key: false, name: 'ExpenseAmount', sortable: false },
                    { key: false, name: 'ExpenseDate', sortable: false },
                    { key: false, name: 'Remarks', sortable: false },
                    { key: false, name: 'ReceivedDate', sortable: false },
                    { key: false, name: 'Status', sortable: false }
                ],
                height: '300px',
                overflow: scroll,
                rowNum: 0,
                jsonReader:
                {
                    root: "rows",
                    page: "page",
                    total: "records",
                    repeatitems: false,
                    id: "0"
                },
                loadComplete: function () {
                    var $grid = jQuery("#gridCustomerCredit"),
                        rows = $grid[0].rows,
                        cRows = rows.length,
                        iRow,
                        rowId,
                        row,
                        cellsOfRow;

                    for (iRow = 0; iRow < cRows; iRow++) {
                        row = rows[iRow];

                        if ($(row).hasClass("jqgrow")) {


                            cellsOfRow = row.cells;
                            //alert(cellsOfRow[8].innerHTML);

                            //if (cellsOfRow[9].innerHTML == "&nbsp;") {
                            //alert(cellsOfRow[14].innerHTML);
                            if (cellsOfRow[6].innerHTML != "Payment Pending") {
                                $("#gridCustomerCredit").jqGrid('setCell', cellsOfRow[1].innerHTML, "Status", "", { 'background-color': '#32CD32' });
                            }
                            else {
                                $("#gridCustomerCredit").jqGrid('setCell', cellsOfRow[1].innerHTML, "Status", "", { 'background-color': '#FFA500' });
                            }
                            //}
                            // 

                        }
                    }
                },
                viewrecords: true,
                emptyrecords: 'No records to display',
                autowidth: true,
                multiselect: false
            });

            //$("#gridCustomerCredit").jqGrid({
            //    url: "/Customer/CustomerCreditDetails",
            //    datatype: 'json',
            //    mtype: 'GET',
            //    colNames: ['Store', 'Credit Id', 'Credit Amount', 'Credit Date', 'Remarks', 'Payment Status'],
            //    colModel: [
            //        { key: false, name: 'Store', sortable: false },
            //        { key: false, name: 'ExpenseId', sortable: false },
            //        { key: false, name: 'ExpenseAmount', sortable: false },
            //        { key: false, name: 'ExpenseDate', sortable: false },
            //        { key: false, name: 'Remarks', sortable: false },
            //        { key: false, name: 'Status', sortable: false }
            //    ],
            //    height: '300px',
            //    overflow: scroll,
            //    rowNum: 0,
            //    jsonReader:
            //    {
            //        root: "rows",
            //        page: "page",
            //        total: "records",
            //        repeatitems: false,
            //        id: "0"
            //    },
            //    viewrecords: true,
            //    emptyrecords: 'No records to display',
            //    autowidth: true,
            //    multiselect: false,
            //    loadComplete: function () {
            //        var $grid = jQuery("#gridCustomerCredit"),
            //            rows = $grid[0].rows,
            //            cRows = rows.length,
            //            iRow,
            //            rowId,
            //            row,
            //            cellsOfRow;

            //        for (iRow = 0; iRow < cRows; iRow++) {
            //            row = rows[iRow];

            //            if ($(row).hasClass("jqgrow")) {


            //                cellsOfRow = row.cells;
            //                //alert(cellsOfRow[8].innerHTML);

            //                //if (cellsOfRow[9].innerHTML == "&nbsp;") {
            //                //alert(cellsOfRow[14].innerHTML);

            //                //if (cellsOfRow[5].innerHTML != "Payment Pending") {
            //                //    $("#gridCustomerCredit").jqGrid('setCell', cellsOfRow[1].innerHTML, "Status", "", { 'background-color': '#32CD32' });
            //                //}
            //                //else {
            //                //    $("#gridCustomerCredit").jqGrid('setCell', cellsOfRow[1].innerHTML, "Status", "", { 'background-color': '#FFA500' });
            //                //}
            //                //}
            //                // 

            //            }
            //        }
            //    }
            //});
        });
    </script>
    <h2>
        <font style="color: #003399">Customer Credit Report</font>
    </h2>
    <p id="rData">
        <span id="msg" style="color: Green;" />
    </p>
    <div style="overflow: scroll;">
        <table width="100%" border="1">
            <tr>
                <td>Select Pending Credit Id &nbsp;&nbsp;&nbsp;
                <select id="ddlPedingCustomerCredit">
                </select>
                    &nbsp;&nbsp;&nbsp;Cash Received Date&nbsp;&nbsp;&nbsp;<input type="text" id="tDate" style="width: 10%" />
                    &nbsp;&nbsp;&nbsp;<input type="button" value="Submit Paid Amount" id="btnCustCreditPaid" />
                </td>
                <td>Total Pending Amount &nbsp;&nbsp;&nbsp; <span id="spnPendingAmount"></span>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table id="gridCustomerCredit" class="scroll" cellpadding="0" cellspacing="0" width="100%">
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
