<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<link href="../../Content/themes/base/jquery-ui.css" rel="Stylesheet" />
<link href="../../Content/jquery.jqGrid/ui.jqgrid.css" rel="Stylesheet" />
<script src="../../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
<script src="../../Scripts/i18n/grid.locale-en.js" type="text/javascript"></script>
<script src="../../Scripts/jquery.jqGrid.min.js" type="text/javascript"></script>
<script src="../../Scripts/jQuery.jqGrid.dynamicLink.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">

    function getUrlVars() {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    }

    function deleteRecords(rowData, type) {
        var url = "/Home/DeleteTodayTransaction";
        $.post(url, { Id: rowData, Type: "Advance" }, function (data) {
            alert("Record deleted");
        });

        jQuery("#gridAdvance").jqGrid().trigger('reloadGrid');
    }

    $(document).ready(function () {

        $("#gridAdvance").jqGrid({
            url: "/Home/GetAdvanceDetail",
            datatype: 'json',
            mtype: 'GET',
            colNames: ['Id', 'Employee Name', 'Advance Amount', 'Payment Type', 'Advance Date', 'Remarks', 'Delete Entry'],
            colModel: [{ key: true, name: 'Id', sortable: false, hidden: true },
            { key: false, name: 'EmpName', sortable: false },
            { key: false, name: 'AdvanceAmount', sortable: false },
            { key: false, name: 'PaymentType', sortable: false },
            { key: false, name: 'AdvanceDate', sortable: false },
            { key: false, name: 'Remarks', sortable: false },
            {
                key: false, name: 'actions', sortable: false, formatter: function (rowId, cellval, colpos, rwdat, _act) {
                    //alert(colpos.Id);
                    var rowInterviewId = colpos.Id.toString();
                    if (sessionStorage.getItem("UserSession") == 'admin') {
                        return "&nbsp;&nbsp;&nbsp;&nbsp;<input type='button' id='" + rowInterviewId + "' value='delete' class='btn' onClick = 'deleteRecords(" + rowInterviewId + ");' /> ";
                    }
                }
            }
            ],
            height: '200px',
            width: '200px',
            overflow: scroll,
            jsonReader:
            {
                root: "rows",
                page: "page",
                total: "records",
                repeatitems: false,
                id: "0"
            },
            postData: {

                EmpName: getUrlVars()["EmpName"],
                EODDate: getUrlVars()["EODDate"]
            },
            viewrecords: true,
            emptyrecords: 'No records to display',
            autowidth: true,
            multiselect: false
        });
    });
</script>

<div class="page">
    <div id="main">
        <h2>
            <font color="gray">Advance Details</font>
        </h2>
        <table id="gridAdvance" class="scroll" cellpadding="0" cellspacing="0" width="100%">
        </table>
    </div>
</div>
