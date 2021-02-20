
function deleteRecords(rowData) {
    var url = "/Report/DeleteTodayLedger";
    $.post(url, { LedgerId: rowData }, function (data) {
        $("#msg").html(data);
        $.ajax({
            url: "/Report/GetLedger",
            dataType: "json",
            data: { PartyId: $("#ddlPartyName").val() },
            type: "GET",
            contentType: 'application/json',
            success: function (data) {
                jQuery("#gridLedger").jqGrid('setGridParam', { data: data }).trigger('reloadGrid');
            }
        });

    });
}

$(document).ready(function () {

    $(function () {
        $("#tDate").datepicker().datepicker('setDate', '+0');;
    });



    $("#ddlPartyName").change(function () {
        if ($('option:selected', $(this)).text() == "-Select-") {
            $("#gridLedger").jqGrid("GridUnload");
        }
        else {
            $("#gridLedger").jqGrid("GridUnload");
            $("#gridLedger").jqGrid({
                url: "/Report/GetLedger",
                datatype: 'json',
                postData: { PartyId: $('option:selected', $(this)).val() },
                mtype: 'GET',
                colNames: ['LedgerId', 'Date', 'Invoice No', 'Debit', 'Credit', 'Brand', 'Remarks', 'Closing Balance', 'Entry Date', 'Delete Entry'],
                colModel: [
                    { key: true, name: 'LedgerId', sortable: false, hidden: true },
                    { key: false, name: 'TransactionDate', sortable: false },
                    { key: false, name: 'InvoiceNo', sortable: false },
                    { key: false, name: 'Debit', sortable: false },
                    { key: false, name: 'Credit', sortable: false },
                    { key: false, name: 'BrandDesc', sortable: false },
                    { key: false, name: 'Remarks', sortable: false },
                    { key: false, name: 'ClosingBalance', sortable: false },
                    { key: false, name: 'EntryDate', sortable: false },
                    {
                        key: false, name: 'actions', sortable: false, formatter: function (rowId, cellval, colpos, rwdat, _act) {
                            var rowInterviewId = colpos.LedgerId.toString();
                            return "&nbsp;&nbsp;&nbsp;&nbsp;<input type='button' id='" + rowInterviewId + "' value='delete' class='btn' onClick = 'deleteRecords(" + rowInterviewId + ");' /> ";
                        }
                    }
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
        }
    });

    $.ajax({
        url: "/Home/GetPartyList",
        type: "Get",
        success:
            function (data) {
                var optParty = new Option('-Select-', '');
                $('#ddlPartyName').append(optParty);
                for (var i = 0; i < data.rows.length; i++) {
                    var opt = new Option(data.rows[i].PartyName, data.rows[i].PartyId);
                    var opt1 = new Option(data.rows[i].PartyName, data.rows[i].PartyId);
                    $('#ddlPartyName').append(opt);
                }
            }
    });
    $("#tAmount").keydown(function (e) {
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

    function deleteTodaysLedger() {
        alert();
    }

    $('#tDeleteLedger').click(function () {

        var partyName = $('#ddlPartyName').val();
        if (partyName.trim() == '') {
            alert('Please select party ');
            return;
        }

        var url = "/Report/DeleteTodayLedger";
        $.post(url, { PartyId: partyName }, function (data) {
            $("#msg").html(data);

            $.ajax({
                url: "/Report/GetLedger",
                dataType: "json",
                data: { PartyId: $("#ddlPartyName").val() },
                type: "GET",
                contentType: 'application/json',
                success: function (data) {
                    jQuery("#gridLedger").jqGrid('setGridParam', { data: data }).trigger('reloadGrid');
                }
            });

        });
    });

    $('#save').click(function () {

        var url = "/Report/SaveLedger";
        var partyName = $('#ddlPartyName').val();
        var invoiceNo = $('#tInvoiceNo').val();
        var amount = $('#tAmount').val();
        var remarks = $('#tRemarks').val();
        var date = $('#tDate').val();
        var brand = $('#tBrand').val();
        var drOrCr = "";



        if (partyName.trim() == '') {
            alert('Please select party ');
            return;
        }

        if ($('#ddlTranType').val() == "") {
            alert('Select transaction type ');
            return;
        }
        if ($('#ddlTranType').val() == "Purchase") {
            drOrCr = "Dr";
        }
        else {
            drOrCr = "Cr";
        }

        if (date.trim() == '') {
            alert('Enter Date .');
            return;
        }
        if (invoiceNo.trim() == '') {
            alert('Enter Invoice No.');
            return;
        }

        if (amount.trim() == '' || amount.trim() == '0') {
            alert('Enter amount greater than 0');
            return;
        }


        $.post(url, {
            PartyId: partyName, InvoiceNo: invoiceNo, InvoiceAmount: amount, DrOrCr: drOrCr, Date: date,
            Remarks: remarks, Brand: brand
        }, function (data) {
            $("#msg").html(data);
            $.ajax({
                url: "/Report/GetLedger",
                dataType: "json",
                data: { PartyId: $("#ddlPartyName").val() },
                type: "GET",
                contentType: 'application/json',
                success: function (data) {
                    jQuery("#gridLedger").jqGrid('setGridParam', { data: data }).trigger('reloadGrid');
                }
            });

            $('#tInvoiceNo').val('');
            $('#tAmount').val('');
            $('#tRemarks').val('');
            $('#tDate').val('');
        });
    });
});