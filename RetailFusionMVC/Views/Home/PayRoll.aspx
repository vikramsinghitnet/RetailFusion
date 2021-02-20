<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    PayRoll
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script language="javascript" type="text/javascript">
        function LinkAdvance(id) {

            var row = id.split("=");
            var row_ID = row[1];

            var EmpName = $("#gridPaymentReport").getCell(row_ID, 'EmpName');
            var EODDate = $("#gridPaymentReport").getCell(row_ID, 'AdvanceDate');

            var url = "CreatePartialView?EODDate=" + EODDate + "&ViewType=Advance" + "&EmpName=" + EmpName; // sitename will be like google.com or yahoo.com
            window.open(url, "_blank", "location=1,status=0,scrollbars=1, resizable=0,  width=600, height=200");
        }

        $(document).ready(function () {
            $("#gridPaymentReport").jqGrid({
                url: "/Home/GetMonthWiseEmployeePaymentDetail",
                datatype: 'json',
                mtype: 'GET',
                colNames: ['id','Employee Name', 'Paid Amount', 'Advance Month'],
                colModel: [
                        { name: 'id', key: true, hidden: true },
                        { key: false, name: 'EmpName', sortable: false },
                        { key: false, name: 'AdvanceAmount', sortable: false, formatter: 'showlink', formatoptions: { baseLinkUrl: 'javascript:', showAction: "LinkAdvance('", addParam: "');"} },
                //                         { key: false, name: 'PaymentType', sortable: false },
                         {key: false, name: 'AdvanceDate', sortable: false }
                    ],
                height: '400px',
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
                autowidth: true,
                multiselect: false
            });
        });
    </script>
    <h2>
        <font style="color: #003399">Employee Month Wise Payment Report</font>
    </h2>
    <div style="overflow: scroll;">
        <table id="gridPaymentReport" class="scroll" cellpadding="0" cellspacing="0" width="100%">
        </table>
    </div>
</asp:Content>
