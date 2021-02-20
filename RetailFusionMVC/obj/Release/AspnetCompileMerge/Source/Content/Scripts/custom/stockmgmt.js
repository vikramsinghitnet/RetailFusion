function numericvalidation() {
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
    $("#txtMobileNo").keydown(function (e) {
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
    $("#txtValue").keydown(function (e) {
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
    $("#tValue").keydown(function (e) {
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

function loadFreshDispatch() {
    $('#ddlStockReturnId').empty();

    $.ajax({
        url: "/Report/GetDisptachIds",
        type: "Get",
        success:
            function (data) {
                var optParty = new Option('', '');
                $('#ddlStockReturnId').append(optParty);
                for (var i = 0; i < data.rows.length; i++) {
                    var opt = new Option(data.rows[i], data.rows[i]);
                    $('#ddlStockReturnId').append(opt);
                }
            }
    });

    $('#btnUpdateStatus').click(function () {
        var url = "/Report/SubmitDispatchStatus";
        var dispatchId = $('#ddlStockReturnId option:selected').val();
        var dispatchStatus = $('#ddlStatus option:selected').val();

        $.post(url, { DispatchId: dispatchId, DispatchStatus: dispatchStatus }, function (data) {
            $("#msg").html(data);
        });

        $.ajax({
            url: "/Report/GetStockReturnList",
            dataType: "json",
            data: {},
            type: "GET",
            contentType: 'application/json',
            success: function (data) {
                jQuery("#tblStockReturn").jqGrid('setGridParam', { data: data }).trigger('reloadGrid');
            }
        });

        loadFreshDispatch();
    });
}
$(document).ready(function () {

    loadFreshDispatch();
    $("#tabs").tabs();
    //checkRadio();
    $("input[name$='aType']").click(function () {
        checkRadio();
    });

    $("#btnSubmitReturn").click(function () {
        var returnType = $('#ddlReturnType option:selected').text();
        var partyId = $('#ddlPartyName option:selected').val();
        var qty = $('#tStockQty').val();
        var value = $('#tValue').val();
        var remark = $('#tRemark').val();
        var courierName = $('#tCourierName').val();
        var trackingId = $('#tTrackingId').val();

        if (returnType == '-Select-') {
            alert('Please select return type !');
            return;
        }
        else if (partyId == '') {
            alert('Please select party !');
            return;
        }
        else if (qty == '') {
            alert('Please enter quantity !');
            return;
        }
        else if (courierName == '') {
            alert('Please enter courier name !');
            return;
        }


        var url = "/Report/SubmitReturn";
        $.post(url, { Party: partyId, ReturnType: returnType, Quantity: qty, Value: value, Remark: remark, CourierName: courierName, TrackingId: trackingId }, function (data) {
            alert(data);
            $('#tStockQty').val('');
            $('#tValue').val('');
            $('#tRemark').val('');
            $('#tCourierName').val('');
            $('#tTrackingId').val('');
        });

        $.ajax({
            url: "/Report/GetStockReturnList",
            dataType: "json",
            type: "GET",
            contentType: 'application/json',
            success: function (data) {
                jQuery("#tblStockReturn").jqGrid('setGridParam', { data: data }).trigger('reloadGrid');
            }
        });
    });

    $("#btnSaveDefective").click(function () {
        var partyId = $('#txtCustomerName').val();
        var returnType = $('#txtMobileNo').val();
        var qty = $('#txtBrand').val();
        var value = $('#txtValue').val();
        var remark = $('#txtRemark').val();
        alert(partyId + '-' + returnType + '-' + qty + '-' + value + '-' + remark);
    });

    numericvalidation();
    $.ajax({
        url: "/Home/GetPartyList",
        type: "Get",
        success:
            function (data) {
                var optParty = new Option('-Select-', '');
                //                            var optParty1 = new Option('-Select-', '');
                $('#ddlPartyName').append(optParty);
                //                            $('#ddlPartyList').append(optParty1);
                for (var i = 0; i < data.rows.length; i++) {
                    var opt = new Option(data.rows[i].PartyName, data.rows[i].PartyId);
                    //                                var opt1 = new Option(data.rows[i].PartyName, data.rows[i].PartyId);
                    $('#ddlPartyName').append(opt);
                    //                                $('#ddlPartyList').append(opt1);
                }
            }
    });

    $("#tblStockReturn").jqGrid({
        url: "/Report/GetStockReturnList",
        datatype: 'json',
        width: '100%',
        autowidth: true,
        shrinktofit: true,
        mtype: 'GET',
        colNames: ['StockReturn ID', 'Party', 'ReturnType', 'Qty', 'ReturnValue', 'ReturnStatus', 'Courier Name', 'Tracking Id', 'Status Date', 'Remarks'],
        colModel: [
            { key: false, name: 'StockReturn_ID', sortable: false, width: '86px' },
            { key: false, name: 'Party', sortable: false, width: '86px' },
            { key: false, name: 'ReturnType', sortable: false, width: '86px' },

            { key: false, name: 'Qty', sortable: false, width: '86px' },
            { key: false, name: 'ReturnValue', sortable: false, width: '86px' },
            { key: false, name: 'ReturnStatus', sortable: false, width: '86px' },
            { key: false, name: 'CourierName', sortable: false, width: '86px' },
            { key: false, name: 'TrackingId', sortable: false, width: '86px' },
            { key: false, name: 'UpdatedDate', sortable: false, width: '96px' },
            { key: false, name: 'Remarks', sortable: false, width: '150px' }

        ],
        height: '270px',
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
        viewrecords: true,
        emptyrecords: 'No records to display',
        multiselect: false
    });

});