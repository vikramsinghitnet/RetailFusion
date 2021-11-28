
function LinkExpense(data) {
    $("#ExpenseDetailsdialog").dialog("open");
}

function ReloadGrid(monthYear) {

    $("#gridEOD").jqGrid('setGridParam', {
        postData: { MonthYear: monthYear }
    }).trigger('reloadGrid');
    $("#gridEOD").trigger("reloadGrid");
   
}

function LinkDeposit(id) {
    var row = id.split("=");
    var row_ID = row[1];
    var EODDate = $("#gridEOD").getCell(row_ID, 'EODDate');
    var url = "CreatePartialView?EODDate=" + EODDate + "&ViewType=Deposit";; // sitename will be like google.com or yahoo.com
    window.open(url, "_blank", "location=1,status=0,scrollbars=1, resizable=0,  width=600, height=200");
}

function ReloadExpenseGrid(monthYear) {

    $("#gridExpanceDetail").jqGrid('setGridParam', {
        postData: { MonthYear: monthYear }
    }).trigger('reloadGrid');
}
function LoadSaleReport(monthYear) {

    $("#gridEOD").jqGrid({
        url: "/Home/GetEODDetail",
        datatype: 'json',
        postData: { MonthYear: "Jan 2000" },
        mtype: 'GET',
        colNames: ['EOD Submit Date', 'Total Sale', 'Card Payment', 'Total Discount', 'Total Party Payment', 'Total Deposit', 'Closing Balance (Counter Cash)', 'Total Expense', 'Total Employee Payment', 'Shortage Amount'],
        colModel: [
            { key: false, name: 'EODDate', sortable: false },
            { key: false, name: 'TotalSale', sortable: false },
            { key: false, name: 'CardPayment', sortable: false },
            { key: false, name: 'TotalDiscount', sortable: false },
            { key: false, name: 'TotapPartypayment', sortable: false, formatter: 'showlink', formatoptions: { baseLinkUrl: 'javascript:', showAction: "LinkPartyPayment('", addParam: "');" } },
            { key: false, name: 'TotalDeposit', sortable: false, formatter: 'showlink', formatoptions: { baseLinkUrl: 'javascript:', showAction: "LinkDeposit('", addParam: "');" } },
            { key: false, name: 'CounterCash', sortable: false },
            { key: false, name: 'TotalExpense', sortable: false, formatter: 'showlink', formatoptions: { baseLinkUrl: 'javascript:', showAction: "LinkExpense('", addParam: "');" } },
            { key: false, name: 'TotalAdvance', sortable: false, formatter: 'showlink', formatoptions: { baseLinkUrl: 'javascript:', showAction: "LinkAdvance('", addParam: "');" } },
            { key: false, name: 'ShortageAmount', sortable: false }
        ],
        height: '300px',
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
}

function LoadGrids() {

    var rptType = $('#ddlReportType option:selected').val();

    if (rptType == 1) {
        $(".PartyPendingReprot").hide();
        $(".ExpanceSummary").hide();
        $(".MonthlyReport").show();
        $("#gridWrapper").hide();
        $(".MonthlySaleDetail").hide();
        $(".ddlMonthYear").hide();
        $("#gridWrapperExpenseDetail").hide();
        $(".LedgerSummary").hide();
    }
    else if (rptType == 2) {

        $(".MonthlyReport").hide();
        $(".ExpanceSummary").hide();
        $(".PartyPendingReprot").show();
        //                $(".MonthlySaleDetail").hide();
        $("#gridWrapper").hide();
        $("#gridWrapperExpenseDetail").show();
        $(".ddlMonthYear").hide();
        $(".LedgerSummary").hide();
    }
    else if (rptType == 3) {
        $(".MonthlyReport").hide();
        $(".PartyPendingReprot").hide();
        $(".ExpanceSummary").show();
        $("#gridWrapper").hide();
        //                $(".MonthlySaleDetail").hide();
        $(".ddlMonthYear").hide();
        $(".LedgerSummary").hide();
        $("#gridWrapperExpenseDetail").hide();
    }//gridExpanceDetail
    else if (rptType == 4) {
        $(".MonthlyReport").hide();
        $(".PartyPendingReprot").hide();
        $(".ExpanceSummary").hide();
        if ($('#ddlMonthYear option:selected').text() != "") {
            ReloadGrid($('#ddlMonthYear option:selected').text());
            //                LoadSaleReport($('#ddlMonthYear option:selected').text());
        }
        $("#gridWrapper").show();
        //                $(".MonthlySaleDetail").show();
        $(".ddlMonthYear").show();
        $(".LedgerSummary").hide();
        $("#gridWrapperExpenseDetail").hide();
    }
    else if (rptType == 5) {
        $(".MonthlyReport").hide();
        $(".PartyPendingReprot").hide();
        $(".ExpanceSummary").hide();
        if ($('#ddlMonthYear option:selected').text() != "") {

            ReloadExpenseGrid($('#ddlMonthYear option:selected').text());
            //                LoadSaleReport($('#ddlMonthYear option:selected').text());
        }
        $("#gridWrapper").hide();
        $(".LedgerSummary").hide();
        $(".ddlMonthYear").show();
        $("#gridWrapperExpenseDetail").show();
        //                $(".MonthlySaleDetail").show();

    } else if (rptType == 6) {
        $(".PartyPendingReprot").hide();
        $(".ExpanceSummary").hide();
        $(".MonthlyReport").hide();
        $(".LedgerSummary").show();
        $("#gridWrapper").hide();
        $(".MonthlySaleDetail").hide();
        $(".ddlMonthYear").hide();
        $("#gridWrapperExpenseDetail").hide();
    }
    else {

        $(".PartyPendingReprot").hide();
        $(".MonthlyReport").hide();
        $(".ExpanceSummary").hide();
        //                $(".MonthlySaleDetail").hide();
        $("#gridWrapper").hide();
        $(".LedgerSummary").hide();
        $(".ddlMonthYear").hide();
        $("#gridWrapperExpenseDetail").hide();
    }
};

$(document).ready(function () {

    $.ajax({
        url: "/Report/GetEODMonths",
        type: "Get",
        success:
            function (data) {

                var optBlank = new Option('', '');
                $('#ddlMonthYear').append(optBlank);
                for (var i = 0; i < data.rows.length; i++) {
                    var opt = new Option(data.rows[i], data.rows[i]);
                    $('#ddlMonthYear').append(opt);
                }
            }
    });

    $("#ExpenseDetailsdialog").dialog({
        autoOpen: false,
        width: "80%",
        height: 400,
        modal: true,
        buttons: [
            {
                text: "Ok",
                click: function () {
                    $(this).dialog("close");
                }
            }
        ]
    }
    );

    $('#ddlReportType').change(function () {
        //                alert($('#ddlMonthYear option:selected').val());
        if ($('#ddlReportType option:selected').text() == "Month Sale Detail Report") {
            $(".ddlMonthYear").show();
            $("#gridWrapper").show();
            $("#gridWrapper").hide();
            $(".MonthlyReport").hide();
            $(".PartyPendingReprot").hide();
            $(".ExpanceSummary").hide();
            $("gridWrapperExpenseDetail").hide();
        }
        else if ($('#ddlReportType option:selected').text() == "Month Expense Detail Report") {
            $("gridWrapperExpenseDetail").show();
            $(".ddlMonthYear").show();
            $("#gridWrapper").hide();
            $("#gridWrapper").hide();
            $(".MonthlyReport").hide();
            $(".PartyPendingReprot").hide();
            $(".ExpanceSummary").hide();
            $("gridWrapperExpenseDetail").hide();

        }
        else {
            $(".ddlMonthYear").hide();
            $("#gridWrapper").hide();
            $(".MonthlyReport").hide();
            $(".PartyPendingReprot").hide();
            $(".ExpanceSummary").hide();
            $("gridWrapperExpenseDetail").hide();
        }
    });


    $("#gridExpanceSummary").jqGrid({
        url: "/Report/GetExpenseSummary",
        datatype: 'json',
        mtype: 'GET',
        colNames: ['Month Year', 'Vegetable', 'Alteration', 'Petrol', 'Kirana', 'Electricity', 'Buying', 'Stock Transport', 'Customer Credit', 'Shop Setup'],
        colModel: [
            { key: false, name: 'MonthYear', sortable: false },
            { key: false, name: 'Vegetable', sortable: false },
            { key: false, name: 'Alteration', sortable: false },
            { key: false, name: 'Petrol', sortable: false },
            { key: false, name: 'Kirana', sortable: false },
            { key: false, name: 'Electricity', sortable: false },
            { key: false, name: 'Buying', sortable: false },
            { key: false, name: 'StockTransport', sortable: false },
            { key: false, name: 'CustomerCredit', sortable: false },
            { key: false, name: 'ShopSetup', sortable: false }
        ],
        height: '300px',
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


    $("#gridEODAll").jqGrid({
        url: "/Report/GetMonthSummaryDL",
        datatype: 'json',
        mtype: 'GET',
        colNames: ['Month & Year', 'Total Gross Sale', 'Total Discount', 'Total Expense', 'Total Employee Payment', 'All Expenses', 'Shortage Amount', 'PFT'],
        colModel: [
            { key: false, name: 'EODDate', sortable: false },
            { key: false, name: 'TotalSale', sortable: false },
            { key: false, name: 'TotalDiscount', sortable: false },
            {
                key: false, name: 'TotalExpense', sortable: false /*, formatter: 'showlink' , formatoptions: { baseLinkUrl: 'javascript:', showAction: "LinkExpense('", addParam: "');" } */
            },
            { key: false, name: 'TotalAdvance', sortable: false },
            { key: false, name: 'TotapPartypayment', sortable: false },
            { key: false, name: 'ShortageAmount', sortable: false },
            { key: false, name: 'CounterCash', sortable: false }
        ],
        height: '300px',
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

    $("#gridLedgerSummary").jqGrid({
        url: "/Report/GetLedgerSummery",
        datatype: 'json',
        mtype: 'GET',
        colNames: ['Party', 'Closing Balance'],
        colModel: [
            { key: false, name: 'PartyName', sortable: false },
            { key: false, name: 'ClosingBalance', sortable: false }
        ],
        height: '300px',
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

    $("#gridPendingSummary").jqGrid({
        url: "/Report/GetPendingAmountSummary",
        datatype: 'json',
        mtype: 'GET',
        colNames: ['Party Name', 'Pending Amount', 'Pending VAT'],
        colModel: [
            { key: false, name: 'PartyName', sortable: false },
            { key: false, name: 'PendingAmount', sortable: false },
            { key: false, name: 'PendingVAT', sortable: false }
        ],
        height: '300px',
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

    $("#gridExpanceDetail").jqGrid({
        url: "/Report/GetMonthExpenses",
        datatype: 'json',
        mtype: 'GET',
        colNames: ['Date', 'Expense Name', 'Remarks', 'ExpenseAmount'],
        colModel: [
            { key: false, name: 'Date', sortable: false },
            { key: false, name: 'ExpName', sortable: false },
            { key: false, name: 'Remarks', sortable: false },
            { key: false, name: 'ExpenseAmount', sortable: false }
        ],
        height: '300px',
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
    LoadSaleReport("Jun 2016");

    LoadGrids();

});
