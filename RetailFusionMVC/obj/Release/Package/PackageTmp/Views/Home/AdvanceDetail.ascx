<%@ Control Language="C#"  Inherits="System.Web.Mvc.ViewUserControl" %>
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

    $(document).ready(function () {

        $("#gridAdvance").jqGrid({
            url: "/Home/GetAdvanceDetail",
            datatype: 'json',
            mtype: 'GET',
            colNames: ['Employee Name', 'Advance Amount', 'Payment Type','Advance Date','Remarks'],
            colModel: [
                        { key: false, name: 'EmpName', sortable: false },
                        { key: false, name: 'AdvanceAmount', sortable: false },
                         { key: false, name: 'PaymentType', sortable: false },
                         { key: false, name: 'AdvanceDate', sortable: false },
                         { key: false, name: 'Remarks', sortable: false }
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
