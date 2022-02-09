
function deleteRecords(rowData, inv) {
    var url = "/Ledger/DeleteTodayLedger";

    var result = confirm("Want to delete? Invoice no: " + inv.toString());

    if (result) {
        $.post(url, { LedgerId: rowData }, function (data) {
            $("#msg").html(data);
            $.ajax({
                url: "/Ledger/GetLedger",
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

}

function JSONToCSVConvertor(JSONData, ReportTitle, ShowLabel) {
    //If JSONData is not an object then JSON.parse will parse the JSON string in an Object
    var arrData = typeof JSONData != 'object' ? JSON.parse(JSONData) : JSONData;


    var CSV = '';
    //Set Report title in first row or line

    CSV += ReportTitle + '\r\n\n';

    //This condition will generate the Label/Header
    if (ShowLabel) {
        var row = "";

        //This loop will extract the label from 1st index of on array
        for (var index in arrData[0]) {

            //Now convert each value to string and comma-seprated
            row += index + ',';
        }

        row = row.slice(0, -1);

        //append Label row with line break
        CSV += row + '\r\n';
    }

    //1st loop is to extract each row
    for (var i = 0; i < arrData.length; i++) {
        var row = "";

        //2nd loop will extract each column and convert it in string comma-seprated
        for (var index in arrData[i]) {
            row += '"' + arrData[i][index] + '",';
        }

        row.slice(0, row.length - 1);

        //add a line break after each row
        CSV += row + '\r\n';
    }

    if (CSV == '') {
        alert("Invalid data");
        return;
    }

    //Generate a file name
    var fileName = "MyReport_";
    //this will remove the blank-spaces from the title and replace it with an underscore
    fileName += ReportTitle.replace(/ /g, "_");

    //Initialize file format you want csv or xls
    var uri = 'data:text/csv;charset=utf-8,' + escape(CSV);

    // Now the little tricky part.
    // you can use either>> window.open(uri);
    // but this will not work in some browsers
    // or you will not get the correct file extension    

    //this trick will generate a temp <a /> tag
    var link = document.createElement("a");
    link.href = uri;

    //set the visibility hidden so it will not effect on your web-layout
    link.style = "visibility:hidden";
    link.download = fileName + ".csv";

    //this part will append the anchor tag and remove it after automatic click
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

$(document).ready(function () {

    $(function () {
        $("#tDate").datepicker().datepicker('setDate', '+0');

        var today = new Date();
        var dd = String(today.getDate());//.padStart(2, '0');
        var mm = String(today.getMonth() + 1);//.padStart(2, '0'); //January is 0!
        //alert(mm);
        var yyyy = today.getFullYear();
        if (mm == 1 || mm == 2 || mm == 3) {
            yyyy = yyyy - 1;
        }
        $("#fromDate").datepicker().datepicker('setDate', new Date(yyyy, 03, 01));

        $("#toDate").datepicker().datepicker('setDate', '+0');
    });
    
    $("#ddlPartyName").change(function () {
        if ($('option:selected', $(this)).text() == "-Select-") {
            $("#gridLedger").jqGrid("GridUnload");
        }
        else {

            $("#gridLedger").jqGrid("GridUnload");
            $("#gridLedger").jqGrid({
                url: "/Ledger/GetLedger",
                datatype: 'json',
                postData: { PartyId: $('option:selected', $(this)).val(),frmDate: $("#fromDate").val(), toDate: $("#toDate").val()},
                mtype: 'GET',
                colNames: ['LedgerId', 'Invoice Date', 'Invoice No', 'Debit', 'Credit', 'Brand', 'Remarks', 'Closing Balance', 'Branch', 'Submitted By', 'Entry Date', 'Delete Entry'],
                colModel: [
                    { key: true, name: 'LedgerId', sortable: false, hidden: true },
                    { key: false, name: 'TransactionDate', sortable: false },
                    { key: false, name: 'InvoiceNo', sortable: false },
                    { key: false, name: 'Debit', sortable: false },
                    { key: false, name: 'Credit', sortable: false },
                    { key: false, name: 'BrandDesc', sortable: false },
                    { key: false, name: 'Remarks', sortable: false },
                    { key: false, name: 'ClosingBalance', sortable: false },
                    { key: false, name: 'Branch', sortable: false },
                    { key: false, name: 'CreatedBy', sortable: false },
                    { key: false, name: 'EntryDate', sortable: false },
                    {
                        key: false, name: 'actions', sortable: false, formatter: function (rowId, cellval, colpos, rwdat, _act) {
                            var rowInterviewId = colpos.LedgerId.toString();
                            var invNo = colpos.InvoiceNo.toString();
                            //alert(colpos.EntryDate);
                            var today = new Date();
                            var dd = String(today.getDate());//.padStart(2, '0');
                            var mm = String(today.getMonth() + 1);//.padStart(2, '0'); //January is 0!
                            var yyyy = today.getFullYear();

                            today = mm + '/' + dd + '/' + yyyy + ' 12:00:00 AM';
                            //alert(today);
                            if (colpos.EntryDate) {
                                //alert('@Session["store"]');
                                if (sessionStorage.getItem("UserSession") == 'admin') {
                                    //alert("&nbsp;&nbsp;&nbsp;&nbsp;<input type='button' id='" + rowInterviewId + "' value='delete' class='btn' onClick = 'deleteRecords(" + rowInterviewId.toString() + ",\"'" + invNo + "\"');' /> ");
                                    return "&nbsp;&nbsp;&nbsp;&nbsp;<input type='button' id='" + rowInterviewId + "' value='delete' class='btn' onClick = 'deleteRecords(" + rowInterviewId.toString() + ",\"" + invNo + "\");' /> ";
                                }
                                else {
                                    if (today == colpos.EntryDate) {
                                        return "&nbsp;&nbsp;&nbsp;&nbsp;<input type='button' id='" + rowInterviewId + "' value='delete' class='btn' onClick = 'deleteRecords(" + rowInterviewId.toString() + ",\"" + invNo + "\");' /> ";
                                    }
                                }
                            }
                            else {
                                return "&nbsp;";
                            }
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
                pager: "#pager",
                multiselect: false
            });

            grid = jQuery("#gridLedger");

            grid.jqGrid('navGrid', '#pager', { add: false, edit: false, del: false });
            grid.jqGrid('navButtonAdd', '#pager', {
                caption: "Export to Excel",
                onClickButton: function () {
                    //jQuery("#grid").jqGrid('excelExport', { tag: 'excel', url: 'grid.php' });
                    //jQuery("#gridLedger").excelExport();
                    var gridData = $("#gridLedger").jqGrid('getRowData');
                    JSONToCSVConvertor(gridData, "Data", true);
                }
            });

            //jQuery("#gridLedger").jqGrid('navGrid', '#pager', { add: false, edit: false, del: false });
            //jQuery("#gridLedger").jqGrid('navButtonAdd', '#pager', {
            //    caption: "",
            //    onClickButton: function () {
            //        var gridData = $("#gridLedger").jqGrid('getRowData');
            //        var dataToSend = JSON.stringify(gridData);
            //        jQuery("#gridLedger").jqGrid('excelExport', dataToSend);
            //        //jQuery("#gridLedger").excelExport();
            //    }
            //});
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

    $('#tDeleteLedger').click(function () {

        var partyName = $('#ddlPartyName').val();
        if (partyName.trim() == '') {
            alert('Please select party ');
            return;
        }

        var url = "/Ledger/DeleteTodayLedger";
        $.post(url, { PartyId: partyName }, function (data) {
            $("#msg").html(data);

            $.ajax({
                url: "/Ledger/GetLedger",
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

        var url = "/Ledger/SaveLedger";
        var partyName = $('#ddlPartyName').val();
        var branch = $('#ddlBranch').val();
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

        if ($('#ddlBranch').val() == "") {
            alert('Select Branch ');
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
            Remarks: remarks, Brand: brand, Branch: branch
        }, function (data) {
            $("#msg").html(data);
            $.ajax({
                url: "/Ledger/GetLedger",
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