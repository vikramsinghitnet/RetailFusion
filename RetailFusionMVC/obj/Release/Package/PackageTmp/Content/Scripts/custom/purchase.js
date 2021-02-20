function PartyPayment(id) {
    var row = id.split("=");
    var row_ID = row[1];
    var BillId = $("#gridPurchase").getCell(row_ID, 'InvoiceID');
    var url = "PartPaymentView?BillId=" + BillId;  // sitename will be like google.com or yahoo.com      
    window.open(url, "_blank", "location=1,status=0,scrollbars=1, resizable=0,  width=600, height=200");
}

function PaidPartyPayment(id) {
    var row = id.split("=");
    var row_ID = row[1];
    var BillId = $("#gridPaidPurchase").getCell(row_ID, 'InvoiceID');
    var url = "PartPaymentView?BillId=" + BillId;  // sitename will be like google.com or yahoo.com      
    window.open(url, "_blank", "location=1,status=0,scrollbars=1, resizable=0,  width=600, height=200");
}
function loadPendingPurchaseGrid() {
    $("#gridPurchase").jqGrid({
        url: "/Report/GetInvoices",
        datatype: 'json',
        postData: { ReportType: "PEND" },
        mtype: 'GET',
        colNames: ['Bill Id', 'Invoice No', 'Party', 'Invoice Date', 'Recieved Date', 'Stock Qty', 'Total Invoice Amount (GST+Frieght+Invoice Amount)', 'Pending Amount', 'Paid Ammount', 'Paid Date', 'Days Left', 'Remarks', 'Payment Due Date', 'Cash Discount', 'Invoice Amount (Exluding GST)', 'GST', 'Frieght Charges'],
        colModel: [
            { key: true, name: 'InvoiceID', sortable: false },
            { key: true, name: 'InvoiceNo', sortable: false },
            { key: false, name: 'PartyName', sortable: false },
            { key: false, name: 'InvoiceDate', sortable: false },
            { key: false, name: 'RecievedDate', sortable: false },
            { key: false, name: 'StockQty', sortable: false },
            { key: false, name: 'TotalInvoiceAmount', sortable: false },
            { key: false, name: 'PendingAmount', sortable: false },
            { key: false, name: 'PaidAmount', sortable: false, formatter: 'showlink', formatoptions: { baseLinkUrl: 'javascript:', showAction: "PartyPayment('", addParam: "');" } },
            { key: false, name: 'PaymentDate', sortable: false },
            { key: false, name: 'DaysLeft', sortable: false },
            { key: false, name: 'Remarks', sortable: false },
            { key: false, name: 'DewDate', sortable: false },
            { key: false, name: 'CashDiscount', sortable: false },
            { key: false, name: 'InvoiceAmount', sortable: false },
            { key: false, name: 'Vat', sortable: false },
            { key: false, name: 'FrieghtChgs', sortable: false }
        ],
        height: '300px',
        width: '100%',
        rowNum: 0,
        loadComplete: function () {
            var $grid = jQuery("#gridPurchase"), rows = $grid[0].rows, cRows = rows.length, iRow, rowId, row, cellsOfRow;

            for (iRow = 0; iRow < cRows; iRow++) {
                row = rows[iRow];

                if ($(row).hasClass("jqgrow")) {


                    cellsOfRow = row.cells;
                    //alert(cellsOfRow[8].innerHTML);

                    if (cellsOfRow[9].innerHTML == "&nbsp;") {
                        //alert(cellsOfRow[14].innerHTML);
                        if (cellsOfRow[10].innerHTML > 0) {
                            $("#gridPurchase").jqGrid('setCell', cellsOfRow[0].innerHTML, "DaysLeft", "", { 'background-color': '#32CD32' });
                        }
                        else {

                            $("#gridPurchase").jqGrid('setCell', cellsOfRow[0].innerHTML, "DaysLeft", "", { 'background-color': '#FFA500' });
                        }
                    }
                    // 

                }
            }
        },
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

    $('#gridPurchase').jqGrid('setGridWidth', '1150');
}

function loadPaidPurchaseGrid(partyId) {
    $("#gridPaidPurchase").jqGrid({
        url: "/Report/GetInvoices",
        datatype: 'json',
        postData: { ReportType: "PAID", },
        mtype: 'GET',
        colNames: ['Bill Id', 'Invoice No', 'Party', 'Invoice Date', 'Recieved Date', 'Stock Qty', 'Cash Discount', 'Total Invoice Amount (GST+Frieght+Invoice Amount)', 'Paid Ammount', 'Paid Date', 'Remarks', 'Payment Due Date', 'Invoice Amount (Exluding GST)', 'GST', 'Frieght Charges'],
        colModel: [
            { key: true, name: 'InvoiceID', sortable: false },
            { key: true, name: 'InvoiceNo', sortable: false },
            { key: false, name: 'PartyName', sortable: false },
            { key: false, name: 'InvoiceDate', sortable: false },
            { key: false, name: 'RecievedDate', sortable: false },
            { key: false, name: 'StockQty', sortable: false },
            { key: false, name: 'CashDiscount', sortable: false },
            { key: false, name: 'TotalInvoiceAmount', sortable: false },
            { key: false, name: 'PaidAmount', sortable: false, formatter: 'showlink', formatoptions: { baseLinkUrl: 'javascript:', showAction: "PaidPartyPayment('", addParam: "');" } },
            { key: false, name: 'PaymentDate', sortable: false },
            { key: false, name: 'Remarks', sortable: false },
            //{ key: false, name: 'DaysLeft', sortable: false },
            { key: false, name: 'DewDate', sortable: false },
            { key: false, name: 'InvoiceAmount', sortable: false },
            { key: false, name: 'Vat', sortable: false },
            { key: false, name: 'FrieghtChgs', sortable: false }
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

    $('#gridPaidPurchase').jqGrid('setGridWidth', '1150');
}

function loadFreshBillsToPay() {

    $('#ddlPedingBillNo').empty();
    $.ajax({
        url: "/Report/GetPedingBills",
        type: "Get",
        success:
            function (data) {

                var optParty = new Option('-Select-', '');
                $('#ddlPedingBillNo').append(optParty);
                for (var i = 0; i < data.rows.length; i++) {
                    var opt = new Option(data.rows[i], data.rows[i]);
                    $('#ddlPedingBillNo').append(opt);
                }
            }
    });

    var url = "/Report/GetPedingCustomerCredit";
    $.post(url, {}, function (data) {
        $("#spnPendingAmount").html(data);
    });
}


function numericvalidation() {

    $("#tCashDiscount").keydown(function (e) {
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
    $("#tTotalInvoiceAmount").keydown(function (e) {
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
    $("#txtPartialPayment").keydown(function (e) {
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
    $("#tFrieghtChgs").keydown(function (e) {
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

    $("#tStockQty").keydown(function (e) {
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

    $("#tInvoiceAmount").keydown(function (e) {
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

    $("#tVat").keydown(function (e) {
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

$(function () {
    $("#tInvoiceDate").datepicker();
});
$(function () {
    $("#tRecievedDate").datepicker();
});

$(document).ready(function () {

    $("#txtPartialPayment").prop("disabled", !$("#IsPartialPayment").is(':checked'));

    $('#IsPartialPayment').change(function () {
        $("#txtPartialPayment").prop("disabled", !$(this).is(':checked'));
    });

    $(".PaidPurchase").hide();

   

    loadPendingPurchaseGrid();
    loadPaidPurchaseGrid();
    loadFreshBillsToPay();

    $('#btnGetPartyPaymentDetail').click(function () {

        var partyId = $('#ddlPartyList option:selected').val();;

        if ($('#rdPendingAmountRpt').is(':checked')) {
            $(".PaidPurchase").hide();
            $(".PendingPurchase").show();
           

            jQuery("#gridPurchase").jqGrid('setGridParam', {
                postData: { ReportType: "PEND", partyId: partyId }
            }).trigger('reloadGrid');
        }
        else {
            $(".PaidPurchase").show();
            $(".PendingPurchase").hide();

            jQuery("#gridPaidPurchase").jqGrid('setGridParam', {
                postData: { ReportType: "PAID", partyId: partyId }
            }).trigger('reloadGrid');
        }
    });

    $('#btnCustCreditPaid').click(function () {

        var url = "/Report/SubmitPaidAmount";
        var advanceId = $('#ddlPedingBillNo option:selected').val();
        var partialPayment = 0;
        if ($("#IsPartialPayment").is(':checked') && $('#txtPartialPayment').val() != '') {
            partialPayment = $('#txtPartialPayment').val();
            $('#IsPartialPayment').attr('checked', false);
            $("#txtPartialPayment").prop("disabled", true);

        }
        if (advanceId != '') {
            $.post(url, { AdvanceId: advanceId, PartialPayment: partialPayment }, function (data) {
                $("#msg").html(data);
                $('#txtPartialPayment').val('');
            });

            $.ajax({
                url: "/Report/GetInvoices",
                dataType: "json",
                data: { ReportType: "PEND" },
                type: "GET",
                contentType: 'application/json',
                success: function (data) {
                    jQuery("#gridPurchase").jqGrid('setGridParam', { data: data }).trigger('reloadGrid');
                }
            });

            $.ajax({
                url: "/Report/GetInvoices",
                dataType: "json",
                data: { ReportType: "PAID" },
                type: "GET",
                contentType: 'application/json',
                success: function (data) {
                    jQuery("#gridPaidPurchase").jqGrid('setGridParam', { data: data }).trigger('reloadGrid');
                }
            });

            loadFreshBillsToPay();
        }
        else {
            alert('Please select Bill ID !!');
        }
    });

    numericvalidation();

    $('#btnAddInvoice').click(function () {
        var url = "/Report/AddInvoice";
        var invoiceNo = $('#tInvoiceNo').val();
        var invoiceDate = $('#tInvoiceDate').val();
        var invoiceAmount = $('#tInvoiceAmount').val();
        var vat = $('#tVat').val();
        var stockQty = $('#tStockQty').val();
        var recievedDate = $('#tRecievedDate').val();
        var partyName = $('#ddlPartyName').val();
        var cashDiscount = $('#tCashDiscount').val();
        var frieghtChgs = $('#tFrieghtChgs').val();
        var totalInvoiceAmount = $('#tTotalInvoiceAmount').val();
        var remarks = $('#tRemarks').val();
        if (invoiceNo.trim() == '') {
            alert('Enter Invoice No.');
            return;
        }
        if (invoiceDate.trim() == '') {
            alert('Enter Invoice Date');
            return;
        }
        if (invoiceAmount.trim() == '' || invoiceAmount.trim() == '0') {
            alert('Enter Invoice amount greater than 0');
            return;
        }
        if (stockQty.trim() == '') {
            alert('Enter Stock Qty.');
            return;
        }
        if (partyName.trim() == '') {
            alert('Please select Party Name');
            return;
        }
        if (totalInvoiceAmount.trim() == '' || totalInvoiceAmount.trim() == '0') {
            alert('Enter Total Invoice amount greater than 0');
            return;
        }

        $.post(url, {
            InvoiceNo: invoiceNo, InvoiceDate: invoiceDate, InvoiceAmount: invoiceAmount, Vat: vat
            , StockQty: stockQty, RecievedDate: recievedDate, PartyName: partyName, TotalInvoiceAmount: totalInvoiceAmount,
            FrieghtChgs: frieghtChgs, CashDiscount: cashDiscount, Remarks: remarks
        }, function (data) {
            $("#msg").html(data);

        });

        loadFreshBillsToPay();

        $.ajax({
            url: "/Report/GetInvoices",
            dataType: "json",
            data: { ReportType: "PEND" },
            type: "GET",
            contentType: 'application/json',
            success: function (data) {
                jQuery("#gridPurchase").jqGrid('setGridParam', { data: data }).trigger('reloadGrid');
            }
        });

        $('#tInvoiceNo').val('');
        $('#tInvoiceDate').val('');
        $('#tInvoiceAmount').val('');
        $('#tVat').val('');
        $('#tStockQty').val('');
        $('#tRecievedDate').val('');
        $("#ddlPartyName option[value='']").attr('selected', true);
        $('#tCashDiscount').val('');
        $('#tFrieghtChgs').val('');
        $('#tTotalInvoiceAmount').val('');
        $('#tRemarks').val('');

    });

    $.ajax({
        url: "/Home/GetPartyList",
        type: "Get",
        success:
            function (data) {
                var optParty = new Option('-Select-', '');
                var optParty1 = new Option('-Select-', '');
                $('#ddlPartyName').append(optParty);
                $('#ddlPartyList').append(optParty1);
                for (var i = 0; i < data.rows.length; i++) {
                    var opt = new Option(data.rows[i].PartyName, data.rows[i].PartyId);
                    var opt1 = new Option(data.rows[i].PartyName, data.rows[i].PartyId);
                    $('#ddlPartyName').append(opt);
                    $('#ddlPartyList').append(opt1);
                }
            }
    });





});