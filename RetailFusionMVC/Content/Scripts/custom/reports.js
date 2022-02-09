
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

function ReloadExpenseGrid(monthYear,fromDate,toDate) {
    //alert(fromDate);
    $("#gridExpanceDetail").jqGrid('setGridParam', {
        postData: { MonthYear:"",FromDate: fromDate, ToDate: toDate }
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

function ReloadLedgerGrid() {

    if ($('#ddlLedgerType').val() == "0") {
        alert('Select ledger type ');
        return;
    }

    //$.ajax({
    //    url: "/Ledger/GetPurchaseSaleLedger",
    //    dataType: "json",
    //    data: { DrOrCr: $('option:selected', $("#ddlLedgerType")).val(), frmDate: $("#fromDate").val(), toDate: $("#toDate").val() },
    //    type: "GET",
    //    contentType: 'application/json',
    //    success: function (data) {
    //        jQuery("#gridPurchaseSale").jqGrid('setGridParam', { data: data }).trigger('reloadGrid');
    //    }
    //});

    jQuery("#gridPurchaseSale").jqGrid('setGridParam', { postData: { DrOrCr: $('option:selected', $("#ddlLedgerType")).val(), frmDate: $("#fromDate").val(), toDate: $("#toDate").val() } }).trigger('reloadGrid');

    //$("#gridPurchaseSale").jqGrid({
    //    url: "/Ledger/GetPurchaseSaleLedger",
    //    datatype: 'json',
    //    postData: { DrOrCr: $('option:selected', $("#ddlLedgerType")).val(), frmDate: $("#fromDate").val(), toDate: $("#toDate").val() },
    //    mtype: 'GET',
    //    colNames: [ 'Invoice Date', 'Invoice No', 'Brand', 'Remarks','Branch', 'Submitted By', 'Entry Date'],
    //    colModel: [
    //        { key: false, name: 'TransactionDate', sortable: false },
    //        { key: false, name: 'InvoiceNo', sortable: false },
    //        { key: false, name: 'BrandDesc', sortable: false },
    //        { key: false, name: 'Remarks', sortable: false },
    //        { key: false, name: 'Branch', sortable: false },
    //        { key: false, name: 'CreatedBy', sortable: false },
    //        { key: false, name: 'EntryDate', sortable: false }],
    //    height: '300px',
    //    width: '100%',
    //    rowNum: 0,
    //    overflow: scroll,
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
    //    pager: "#pager",
    //    multiselect: false
    //});

}

function LoadGrids() {

    var rptType = $('#ddlReportType option:selected').val();
    //alert(rptType);
    if (rptType == 1) {

        
        $("#trDuration").hide();
        $("#gridWrapper").hide();
        $("#gridWrapperPurchaseSale").hide();
        $("#trType").hide();
        $("#gridWrapperExpenseDetail").hide();

        $(".MonthlyReport").show();
        $(".PartyPendingReprot").hide();
        $(".ExpanceSummary").hide();
        //$(".MonthlySaleDetail").hide();
        $(".ddlMonthYear").hide();
        $(".LedgerSummary").hide();
       
    }
    else if (rptType == 2) {

        $("#gridWrapperExpenseDetail").show();              
        $("#trDuration").hide();
        $("#gridWrapperPurchaseSale").hide();
        $("#trType").hide();
        $("#gridWrapper").hide();
        
        $(".PartyPendingReprot").show();
        $(".MonthlyReport").hide();
        $(".ExpanceSummary").hide();
        $(".ddlMonthYear").hide();
        $(".LedgerSummary").hide();
       
    }
    else if (rptType == 3) {
       
        $("#trDuration").hide();
        $("#gridWrapper").hide();      
        $("#gridWrapperExpenseDetail").hide();
        $("#gridWrapperPurchaseSale").hide();
        $("#trType").hide();

        $(".ExpanceSummary").show();
        $(".MonthlyReport").hide();
        $(".PartyPendingReprot").hide();
        $(".ddlMonthYear").hide();
        $(".LedgerSummary").hide();
    }
    else if (rptType == 4) {
        if ($('#ddlMonthYear option:selected').text() != "") {
            ReloadGrid($('#ddlMonthYear option:selected').text());
        }
        $("#gridWrapper").show();
        //$(".MonthlySaleDetail").show();
        $("#gridWrapperExpenseDetail").hide();
        $("#gridWrapperPurchaseSale").hide();
        $("#trType").hide();
        $("#trDuration").hide();

        $(".ddlMonthYear").show();
        $(".ExpanceSummary").hide();           
        $(".LedgerSummary").hide();       
        $(".MonthlyReport").hide();
        $(".PartyPendingReprot").hide();
       
    }
    else if (rptType == 5) {
        ReloadExpenseGrid("", $("#fromDate").val(), $("#toDate").val());

        $("#trDuration").show();
        $("#gridWrapper").hide();
        $("#gridWrapperPurchaseSale").hide();
        $("#trType").hide();

        $("#gridWrapperExpenseDetail").show();
        $(".LedgerSummary").hide();
        $(".ddlMonthYear").hide();
        $(".MonthlyReport").hide();
        $(".PartyPendingReprot").hide();
        $(".ExpanceSummary").hide();
      

    }
    else if (rptType == 6) {
            
        $("#gridWrapper").hide();
        $("#gridWrapperExpenseDetail").hide();
        $("#trDuration").hide();
        $("#gridWrapperPurchaseSale").hide();
        $("#trType").hide();

        $(".LedgerSummary").show();
        $(".PartyPendingReprot").hide();
        $(".ExpanceSummary").hide();
        $(".MonthlyReport").hide();
        //$(".MonthlySaleDetail").hide();
        $(".ddlMonthYear").hide();
       
    }
    else if (rptType == 7) {

        ReloadLedgerGrid();

        $("#trType").show();
        $("#gridWrapperPurchaseSale").show();
        $("#trDuration").show();
        $("#gridWrapperExpenseDetail").hide();       
        $("#gridWrapper").hide();

        //$(".MonthlySaleDetail").hide();
        $(".ddlMonthYear").hide();
        $(".PartyPendingReprot").hide();
        $(".ExpanceSummary").hide();
        $(".MonthlyReport").hide();       
        $(".LedgerSummary").hide();
    }
    else {

        $("#gridWrapperExpenseDetail").hide();
        $("#trType").hide();
        $("#gridWrapperPurchaseSale").hide();
        $("#trDuration").hide();
        $("#gridWrapper").hide();

        $(".PartyPendingReprot").hide();
        $(".MonthlyReport").hide();       
        $(".ExpanceSummary").hide();        
        $(".LedgerSummary").hide();
        //$(".MonthlySaleDetail").hide();
        $(".ddlMonthYear").hide();       
    }
};

$(document).ready(function () {

    $("#fromDate").datepicker().datepicker('setDate', '+0');
    $("#toDate").datepicker().datepicker('setDate', '+0');

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
        if ($('#ddlReportType option:selected').text() == "Month Sale Detail Report") {
            
            $("#gridWrapper").show();
            $("#gridWrapper").hide();
            $(".MonthlyReport").hide();            
            $("#trDuration").hide();           
            $("#gridWrapperExpenseDetail").hide();

            $(".ddlMonthYear").show();
            $(".ExpanceSummary").hide();
            $(".PartyPendingReprot").hide();
            $("#trType").hide();
        }
        else if ($('#ddlReportType option:selected').text() == "Month Expense Detail Report") {
            //$("gridWrapperExpenseDetail").show();
            
            $("#trDuration").show();
            $("#trType").hide();
            $("#gridWrapper").hide();
            $("#gridWrapper").hide();           
            $("#gridWrapperExpenseDetail").hide();

            $(".ddlMonthYear").hide();
            $(".MonthlyReport").hide();
            $(".PartyPendingReprot").hide();
            $(".ExpanceSummary").hide();
        }
        else if ($('#ddlReportType option:selected').text() == "Sale/Purchase Report") {

            $("#trDuration").show();
            $("#gridWrapperPurchaseSale").hide();           
            $("#trType").show();
            $("#gridWrapper").hide();
            $("#gridWrapper").hide();
            $("#gridWrapperExpenseDetail").hide();

            $(".ddlMonthYear").hide();
            $(".MonthlyReport").hide();
            $(".PartyPendingReprot").hide();
            $(".ExpanceSummary").hide();
            
        }
        else {
           
            $("#gridWrapper").hide();           
            $("#gridWrapperExpenseDetail").hide();
            $("#trDuration").hide();
            $("#trType").hide();
            $("#gridWrapperPurchaseSale").hide();
            
            $(".ddlMonthYear").hide();
            $(".MonthlyReport").hide();
            $(".PartyPendingReprot").hide();
            $(".ExpanceSummary").hide();
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
        url: "/Ledger/GetLedgerSummery",
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
        width: '100%',
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

    $("#gridPurchaseSale").jqGrid({
        url: "/Ledger/GetPurchaseSaleLedger",
        datatype: 'json',
        mtype: 'GET',
        colNames: ['Invoice Date', 'Invoice No','Amount', 'Party', 'Brand', 'Remarks', 'Branch', 'Submitted By', 'Entry Date'],
        colModel: [
            { key: false, name: 'TransactionDate', sortable: false },
            { key: false, name: 'InvoiceNo', sortable: false },
            { key: false, name: 'Amount', sortable: false },
            { key: false, name: 'Party', sortable: false },
            { key: false, name: 'BrandDesc', sortable: false },
            { key: false, name: 'Remarks', sortable: false },
            { key: false, name: 'Branch', sortable: false },
            { key: false, name: 'CreatedBy', sortable: false },
            { key: false, name: 'EntryDate', sortable: false }],
        height: '300px',
        width: '100%',
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
