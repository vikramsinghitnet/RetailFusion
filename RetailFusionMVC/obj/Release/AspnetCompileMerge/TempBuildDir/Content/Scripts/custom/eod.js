function ClearAll() {
    $('#tSale').val('');
    $('#tDiscount').val('');
    $('#tCardPayment').val('');
    $('#tCounterCash').val('');
    $('#tAdvance').val('');
    $('#tExpenses').val('');
    $("#tExpenseRemarks").val('');
    $('#tParty').val('');
    $('#tDeposit').val('');
    $("#ddlPartyName option[value='']").attr('selected', true);
    $("#ddlBank option[value='']").attr('selected', true);
    $("#ddlEmpName option[value='']").attr('selected', true);
    $("#ddlExpanse option[value='']").attr('selected', true);
    $("#ddlAdvanceType option[value='']").attr('selected', true);
    $('#tDate').val('');
}
jQuery("#gridAdvance").trigger('reloadGrid');

function deleteRecords(rowData, type) {
    var url = "/Home/DeleteTodayTransaction";
    $.post(url, { Id: rowData, Type: type }, function (data) {
        $("#msg").html(data);
    });
    var tDate = $('#tDate').val();
    jQuery("#gridAdvance").jqGrid('setGridParam', { postData: { EODDate: tDate } }).trigger('reloadGrid');
    jQuery("#gridDeposit").jqGrid('setGridParam', { postData: { EODDate: tDate } }).trigger('reloadGrid');
    jQuery("#gridExpanse").jqGrid('setGridParam', { postData: { EODDate: tDate } }).trigger('reloadGrid');
    jQuery("#gridPartyPayment").jqGrid('setGridParam', { postData: { EODDate: tDate } }).trigger('reloadGrid');
    jQuery("#gridTodayEOD").trigger('reloadGrid');
    jQuery("#gridEOD").trigger('reloadGrid');
}

function LinkAdvance(id) {

    var row = id.split("=");
    var row_ID = row[1];

    var EODDate = $("#gridEOD").getCell(row_ID, 'EODDate');
    var url = "CreatePartialView?EODDate=" + EODDate + "&ViewType=Advance"; // sitename will be like google.com or yahoo.com
    window.open(url, "_blank", "location=1,status=0,scrollbars=1, resizable=0,  width=600, height=200");
}

function LinkExpense(id) {
    var row = id.split("=");
    var row_ID = row[1];
    var EODDate = $("#gridEOD").getCell(row_ID, 'EODDate');
    var url = "CreatePartialView?EODDate=" + EODDate + "&ViewType=Expense";; // sitename will be like google.com or yahoo.com
    window.open(url, "_blank", "location=1,status=0,scrollbars=1, resizable=0,  width=800, height=400");
}

function LinkDeposit(id) {
    var row = id.split("=");
    var row_ID = row[1];
    var EODDate = $("#gridEOD").getCell(row_ID, 'EODDate');
    var url = "CreatePartialView?EODDate=" + EODDate + "&ViewType=Deposit";; // sitename will be like google.com or yahoo.com
    window.open(url, "_blank", "location=1,status=0,scrollbars=1, resizable=0,  width=600, height=200");
}

function LinkPartyPayment(id) {
    var row = id.split("=");
    var row_ID = row[1];
    var EODDate = $("#gridEOD").getCell(row_ID, 'EODDate');
    var url = "CreatePartialView?EODDate=" + EODDate + "&ViewType=PartyPayment";; // sitename will be like google.com or yahoo.com
    window.open(url, "_blank", "location=1,status=0,scrollbars=1, resizable=0,  width=600, height=200");
}

function loadEODGrid() {

    $("#tabs").tabs();

    $("#gridAdvance").jqGrid({
        url: "/Home/GetAdvanceDetail", cache: false,
        datatype: 'json',
        caption: 'Advcance',
        mtype: 'GET',
        colNames: ['Id', 'Employee Name', 'Advance Amount', 'Payment Type', 'Remarks', 'Date', 'Delete Entry'],
        colModel: [
            { key: true, name: 'Advance_ID', sortable: false, hidden:true },
            { key: false, name: 'EmpName', sortable: false },
            { key: false, name: 'AdvanceAmount', sortable: false },
            { key: false, name: 'PaymentType', sortable: false },
            { key: false, name: 'Remarks', sortable: false },
            { key: false, name: 'AdvanceDate', sortable: false },
            {
                key: false, name: 'actions', sortable: false, formatter: function (rowId, cellval, colpos, rwdat, _act) {
                    var rowInterviewId = colpos.Id;
                    var advance = '"Advance"';
                    var rtrn = "&nbsp;&nbsp;&nbsp;&nbsp;<input type='button' id='" + rowInterviewId + "' value='delete' class='btn' onClick = 'deleteRecords(" + rowInterviewId + "," + advance + ");' /> ";
                    return rtrn;
                }
            }

        ],
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
        autowidth: false,
        multiselect: false
    });


    $("#gridPartyPayment").jqGrid({
        url: "/Home/GetPartyPaymentList", cache: false,
        datatype: 'json',
        mtype: 'GET',
        colNames: ['Id','Party Name', 'Payment Amount', 'Date','Delete Entry'],
        colModel: [
            { key: true, name: 'Id', sortable: false, hidden: true },
            { key: false, name: 'PartyName', sortable: false },
            { key: false, name: 'PaymentAmount', sortable: false },
            { key: false, name: 'PaymentDate', sortable: false },
            {
                key: false, name: 'actions', sortable: false, formatter: function (rowId, cellval, colpos, rwdat, _act) {
                    var rowInterviewId = colpos.Id;
                    var advance = '"PartyPayment"';
                    var rtrn = "&nbsp;&nbsp;&nbsp;&nbsp;<input type='button' id='" + rowInterviewId + "' value='delete' class='btn' onClick = 'deleteRecords(" + rowInterviewId + "," + advance + ");' /> ";
                    return rtrn;
                }
            }
        ],
        overflow: scroll,
        jsonReader:
        {
            root: "rows",
            page: "page",
            total: "records",
            repeatitems: false,
            id: "0"
        },
        caption: 'Party Payments',
        viewrecords: true,
        emptyrecords: 'No records to display',
        autowidth: true,
        multiselect: false
    });

    $("#gridDeposit").jqGrid({
        url: "/Home/GetDepositList", cache: false,
        datatype: 'json',
        mtype: 'GET',
        colNames: ['Id','Deposit Amount', 'Bank', 'Date','Delete Entry'],
        colModel: [
            { key: true, name: 'Id', sortable: false, hidden: true },
            { key: false, name: 'DepositAmount', sortable: false },
            { key: false, name: 'DepositBank', sortable: false },
            { key: false, name: 'DepositDate', sortable: false },
            {
                key: false, name: 'actions', sortable: false, formatter: function (rowId, cellval, colpos, rwdat, _act) {
                    var rowInterviewId = colpos.Id;
                    var advance = '"Deposit"';
                    var rtrn = "&nbsp;&nbsp;&nbsp;&nbsp;<input type='button' id='" + rowInterviewId + "' value='delete' class='btn' onClick = 'deleteRecords(" + rowInterviewId + "," + advance + ");' /> ";
                    return rtrn;
                }
            }
        ],
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
        caption: 'Deposits',
        multiselect: false
    });

    $("#gridExpanse").jqGrid({
        url: "/Home/GetExpanseDetail", cache: false,
        caption: 'Expenses',
        datatype: 'json',
        mtype: 'GET',
        colNames: ['Id','Expense Type', 'Expense Amount', 'Remarks', 'Date','Delete Entry'],
        colModel: [
            { key: true, name: 'Id', sortable: false, hidden: true },
            { key: false, name: 'ExpenseType', sortable: false },
            { key: false, name: 'ExpenseAmount', sortable: false },
            { key: false, name: 'Remarks', sortable: false },
            { key: false, name: 'ExpenseDate', sortable: false },
            {
                key: false, name: 'actions', sortable: false, formatter: function (rowId, cellval, colpos, rwdat, _act) {
                    var rowInterviewId = colpos.Id;
                    var advance = '"Expense"';
                    var rtrn = "&nbsp;&nbsp;&nbsp;&nbsp;<input type='button' id='" + rowInterviewId + "' value='delete' class='btn' onClick = 'deleteRecords(" + rowInterviewId + "," + advance + ");' /> ";
                    return rtrn;
                }
            }
        ],
        
        jsonReader:
        {
            root: "rows",
            page: "page",
            total: "records",
            repeatitems: false,
            id: "0"
        },
        rowNum:0,
        viewrecords: true,
        emptyrecords: 'No records to display',
        autowidth: true,
        multiselect: false
    });

    var url = "/Customer/GetCustomerPendingAmount";
    $.post(url, {}, function (data) {
        $("#spnPendingAmount").html(data);
    });

    $("#gridCustomerCredit").jqGrid({
        url: "/Customer/CustomerCreditDetails",
        datatype: 'json',
        postData: { filterType: "Pending" },
        mtype: 'GET',
        colNames: ['Store', 'Credit Id', 'Credit Amount', 'Credit Date', 'Remarks', 'Payment Status'],
        colModel: [
            { key: false, name: 'Store', sortable: false },
            { key: true, name: 'ExpenseId', sortable: false },
            { key: false, name: 'ExpenseAmount', sortable: false },
            { key: false, name: 'ExpenseDate', sortable: false },
            { key: false, name: 'Remarks', sortable: false },
            { key: false, name: 'Status', sortable: false }
        ],
        height: '150px',
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
                    if (cellsOfRow[5].innerHTML != "Payment Pending") {
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

    $("#gridMonthSummary").jqGrid({
        url: "/Home/GetMonthSummaryDL",
        datatype: 'json',
        mtype: 'GET',
        colNames: ['Total Gross Sale', 'Total Discount', 'Net Sale', 'Total Expense', 'Total Employee Payment', 'All Expenses', 'Shortage Amount', 'PFT'],
        colModel: [
            { key: false, name: 'TotalSale', sortable: false },
            { key: false, name: 'TotalDiscount', sortable: false },
            { key: false, name: 'EODDate', sortable: false },
            { key: false, name: 'TotalExpense', sortable: false },
            { key: false, name: 'TotalAdvance', sortable: false },
            { key: false, name: 'TotapPartypayment', sortable: false },
            { key: false, name: 'ShortageAmount', sortable: false },
            { key: false, name: 'CounterCash', sortable: false }
        ],
        height: '70px',
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


    $("#gridTodayEOD").jqGrid({
        url: "/Home/GetEODDetail",
        datatype: 'json',
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
        height: '70px',
        rowNum: 2,
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

    $("#gridEOD").jqGrid({
        url: "/Home/GetEODDetail",
        datatype: 'json',
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

function numericvalidation() {

    //validation for numerics
    $("#tSale").keydown(function (e) {
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

    $("#tCardPayment").keydown(function (e) {
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

    $("#tDiscount").keydown(function (e) {
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

    $("#tCounterCash").keydown(function (e) {
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

    $("#tAdvance").keydown(function (e) {
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

    $("#tExpenses").keydown(function (e) {
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

    $("#tDeposit").keydown(function (e) {
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

    $("#tParty").keydown(function (e) {
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

}

function setGridWidth() {
    $('#gridMonthSummary').setGridWidth($(window).width() - 280, true);
    $('#gridAdvance').setGridWidth($(window).width() - 600, true);
    $('#gridEOD').setGridWidth($(window).width() - 280, true); //Back to original width
    $('#gridTodayEOD').setGridWidth($(window).width() - 260, true);

}

// ]]>
$(document).ready(function () {

    $("#tDate").datepicker().datepicker('setDate', new Date());
    $("#accordion").accordion();
    numericvalidation();
    loadEODGrid();

    setGridWidth();

    $.ajax({
        url: "/Home/GetEmployeeList",
        type: "Get",
        success:
            function (data) {

                var optBlank = new Option('', '');
                $('#ddlEmpName').append(optBlank);
                for (var i = 0; i < data.rows.length; i++) {
                    var opt = new Option(data.rows[i].EmpName, data.rows[i].EmpId);
                    $('#ddlEmpName').append(opt);
                }
            }
    });

    $.ajax({
        url: "/Home/GetPartyList",
        type: "Get",
        success:
            function (data) {
                var optParty = new Option('', '');
                $('#ddlPartyName').append(optParty);
                for (var i = 0; i < data.rows.length; i++) {
                    var opt = new Option(data.rows[i].PartyName, data.rows[i].PartyId);
                    $('#ddlPartyName').append(opt);
                }
            }
    });

    $('#tDeleteTodayEOD').click(function () {

        var url = "/Home/DeleteTodayEOD";
        var tDate = $('#tDate').val();
        if (tDate == '') {
            alert('Please select EOD date !!');
        }
        $.post(url, { EodDate: tDate }, function (data) {
            if (data == '1') {
                alert('EOD Deleted Successfully.');
            }
            else {
                alert('Error....');
            }
        });
        jQuery("#gridEOD").jqGrid('setGridParam', { postData: { EODDate: tDate } }).trigger('reloadGrid');
        jQuery("#gridTodayEOD").jqGrid('setGridParam', { postData: { EODDate: tDate } }).trigger('reloadGrid');
        jQuery("#gridExpanse").jqGrid('setGridParam', { postData: { EODDate: tDate } }).trigger('reloadGrid');
        jQuery("#gridAdvance").jqGrid('setGridParam', { postData: { EODDate: tDate } }).trigger('reloadGrid');
        jQuery("#gridPartyPayment").jqGrid('setGridParam', { postData: { EODDate: tDate } }).trigger('reloadGrid');
        jQuery("#gridDeposit").jqGrid('setGridParam', { postData: { EODDate: tDate } }).trigger('reloadGrid');
     

        ClearAll();

    });

    $('#tSubmit').click(function () {

        var url = "/Home/SubmitEODDetail";
        var totalSale = $('#tSale').val();
        var totalDiscount = $('#tDiscount').val();
        var totalCardPayment = $('#tCardPayment').val();
        var totalCounterCash = $('#tCounterCash').val();
        var tDate = $('#tDate').val();
        if (tDate == '') {
            alert('Please select EOD Date!!');
            return;
        }
        $.post(url, {
            EodDate: tDate, TotalSale: totalSale, TotalDiscount: totalDiscount,
            TotalCardPayment: totalCardPayment, TotalCounterCash: totalCounterCash
        }, function (data) {
            if (data == "1") {
                alert("Thank you EOD submitted .");
                jQuery("#gridEOD").jqGrid('setGridParam', { postData: { EODDate: tDate } }).trigger('reloadGrid');
               
                jQuery("#gridTodayEOD").jqGrid('setGridParam', { postData: { EODDate: tDate } }).trigger('reloadGrid');
               

                ClearAll();
            }
            else if (data == "-1") {
                alert("Error ...");
            }
            else if (data == "-2") {
                alert("Please complete the form..");
            }
            else if (data == "-3") {
                alert("Oops!! You already submitted EOD for this Date .");
            }
        });

    });

    $('#btnDeposit').click(function () {

        var url = "/Home/SubmitDeposit";
        var depositAmount = $('#tDeposit').val();
        var bankName = $('#ddlBank option:selected').val();
        var tDate = $('#tDate').val();
        if (tDate == '') {
            alert('Please select EOD Date!!');
            return;
        }
        $.post(url, { EodDate: tDate, DepositAmount: depositAmount, BankName: bankName }, function (data) {
            $("#msg").html(data);
        });
        jQuery("#gridDeposit").jqGrid('setGridParam', { postData: { EODDate: tDate } }).trigger('reloadGrid');
        jQuery("#gridTodayEOD").trigger('reloadGrid');
        jQuery("#gridEOD").trigger('reloadGrid');

        $("#ddlBank option[value='']").attr('selected', true);
        $("#tDeposit").val('');


    });

    $('#btnPartyPayment').click(function () {

        var url = "/Home/SubmitPartyPayment";
        var paymentAmount = $('#tParty').val();
        var partyId = $('#ddlPartyName option:selected').val();
        var tDate = $('#tDate').val();
        if (tDate == '') {
            alert('Please select EOD Date!!');
            return;
        }
        $.post(url, { EodDate: tDate, PaymentAmount: paymentAmount, PartyID: partyId }, function (data) {
            $("#msg").html(data);
        });
        jQuery("#gridPartyPayment").jqGrid('setGridParam', { postData: { EODDate: tDate } }).trigger('reloadGrid');
        jQuery("#gridTodayEOD").trigger('reloadGrid');
        jQuery("#gridEOD").trigger('reloadGrid');

        $("#ddlPartyName option[value='']").attr('selected', true);
        $("#tParty").val('');
    });

    $('#btnAdvance').click(function () {

        var url = "/Home/SubmitAdvance";
        var advanceAmount = $('#tAdvance').val();
        var empID = $('#ddlEmpName option:selected').val();
        var advanceTypeId = $('#ddlAdvanceType option:selected').val();
        var remarks = $('#tRemarks').val();
        var tDate = $('#tDate').val();
        if (tDate == '') {
            alert('Please select EOD Date!!');
            return;
        }
        $.post(url, { EodDate: tDate, AdvanceAmount: advanceAmount, EmpID: empID, AdvanceTypeId: advanceTypeId, Remarks: remarks }, function (data) {
            $("#msg").html(data);
        });
        jQuery("#gridAdvance").jqGrid('setGridParam', { postData: { EODDate: tDate } }).trigger('reloadGrid');
       
        jQuery("#gridTodayEOD").trigger('reloadGrid');
        jQuery("#gridEOD").trigger('reloadGrid');

        $("#ddlEmpName option[value='']").attr('selected', true);
        $("#tAdvance").val('');
        $('#tRemarks').val('');
        $("#ddlAdvanceType option[value='']").attr('selected', true);
    });

    $('#btnExpanse').click(function () {
        var url = "/Home/SubmitExpense";
        var expenseType = $('#ddlExpanse option:selected').val();
        var expenseAmount = $('#tExpenses').val();
        var remarks = $('#tExpenseRemarks').val();
        var tDate = $('#tDate').val();
        if (tDate == '') {
            alert('Please select EOD Date!!');
            return;
        }
        $.post(url, { EodDate: tDate, ExpenseType: expenseType, ExpenseAmount: expenseAmount, Remarks: remarks }, function (data) {
            $("#msg").html(data);
        });


        $("#ddlExpanse option[value='']").attr('selected', true);
        $("#tExpenses").val('');
        $("#tExpenseRemarks").val('');
        jQuery("#gridExpanse").jqGrid('setGridParam', { postData: { EODDate: tDate } }).trigger('reloadGrid');
       

    });

});
