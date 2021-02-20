<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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

          

            $("#gridDeposit").jqGrid({
                url: "/Home/GetDepositList",
                datatype: 'json',
                mtype: 'GET',
                colNames: ['Deposit Amount', 'Bank','Deposit Date'],
                colModel: [
                        { key: false, name: 'DepositAmount', sortable: false },
                        { key: false, name: 'DepositBank', sortable: false },
                        { key: false, name: 'DepositDate', sortable: false }
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
                    EODDate: getUrlVars()["EODDate"]
                },
                viewrecords: true,
                emptyrecords: 'No records to display',
                autowidth: true,
                multiselect: false
            });
        });
    </script>
</head>
<body>
    <div class="page">
        <div id="main">
            <h2>
                <font color="gray">Deposit Details</font>
            </h2>
            <table id="gridDeposit" class="scroll" cellpadding="0" cellspacing="0" width="100%">
            </table>
        </div>
    </div>
</body>
</html>
