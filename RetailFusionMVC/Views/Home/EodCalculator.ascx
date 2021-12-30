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

        function Calculate() {
            var prvsCounterCash = $('#prvsCounterCash').val();// Total sale
            var tSale = $('#tSale').val();// Total sale
            var tCardPayment = $('#tCardPayment').val();//
            var tDiscount = $('#tDiscount').val();//
            var tAdvance = $('#tAdvance').val();//
            var tExpenses = $('#tExpenses').val();
            var tDeposit = $('#tDeposit').val();//
            var tParty = $('#tParty').val();//
            var todayCounterCash = $('#todayCounterCash').val();//

            if (prvsCounterCash == '') {
                //console.log('prvs cash setting 0..')
                prvsCounterCash = 0;
            }
            if (tSale == '') {
                //console.log('tSale cash setting 0..')
                tSale = 0;
            }
            if (tCardPayment == '') {
                //console.log('tCardPayment cash setting 0..')
                tCardPayment = 0;
            }
            if (tDiscount == '') {
                //console.log('tDiscount cash setting 0..')
                tDiscount = 0;
            }
            if (tAdvance == '') {
                //console.log('tAdvance cash setting 0..')
                tAdvance = 0;
            }
            if (tExpenses == '') {
                //console.log('tExpenses cash setting 0..')
                tExpenses = 0;
            }
            if (tDeposit == '') {
                //console.log('tDeposit cash setting 0..')
                tDeposit = 0;
            }
            if (tParty == '') {
                //console.log('tParty cash setting 0..')
                tParty = 0;
            }
            if (todayCounterCash == '') {
                todayCounterCash = 0;
            }

            var calculatedValue = (parseInt(prvsCounterCash) + parseInt(tSale)) -
                (parseInt(tCardPayment) + parseInt(tDiscount) + parseInt(tAdvance) + parseInt(tExpenses) + parseInt(tDeposit) + parseInt(tParty));
            console.log(calculatedValue)
            console.log('today counter cash' + todayCounterCash)
            //$("#lblMyValue").val(tSale);
            if (calculatedValue - todayCounterCash == 0) {
                $("#lblMyValue").html(calculatedValue - todayCounterCash);
            }
            if (calculatedValue - todayCounterCash > 0) {
                $("#lblMyValue").html((calculatedValue - todayCounterCash) + " Shortage");
            }
            if (calculatedValue - todayCounterCash < 0) {
                $("#lblMyValue").html((calculatedValue - todayCounterCash) + " Excess");
            }
        }

        $(document).ready(function () {

            //$("#tSale").keydown(function (e) {
            //    // Allow: backspace, delete, tab, escape, enter and .
            //    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            //        // Allow: Ctrl+A, Command+A
            //        (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            //        // Allow: home, end, left, right, down, up
            //        (e.keyCode >= 35 && e.keyCode <= 40)) {
            //        // let it happen, don't do anything
            //        return;
            //    }
            //    // Ensure that it is a number and stop the keypress
            //    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            //        e.preventDefault();
            //    }
            //});

            //$("#tCardPayment").keydown(function (e) {
            //    // Allow: backspace, delete, tab, escape, enter and .
            //    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            //        // Allow: Ctrl+A, Command+A
            //        (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            //        // Allow: home, end, left, right, down, up
            //        (e.keyCode >= 35 && e.keyCode <= 40)) {
            //        // let it happen, don't do anything
            //        return;
            //    }
            //    // Ensure that it is a number and stop the keypress
            //    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            //        e.preventDefault();
            //    }
            //});

            //$("#tDiscount").keydown(function (e) {
            //    // Allow: backspace, delete, tab, escape, enter and .
            //    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            //        // Allow: Ctrl+A, Command+A
            //        (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            //        // Allow: home, end, left, right, down, up
            //        (e.keyCode >= 35 && e.keyCode <= 40)) {
            //        // let it happen, don't do anything
            //        return;
            //    }
            //    // Ensure that it is a number and stop the keypress
            //    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            //        e.preventDefault();
            //    }
            //});

            //$("#tCounterCash").keydown(function (e) {
            //    // Allow: backspace, delete, tab, escape, enter and .
            //    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            //        // Allow: Ctrl+A, Command+A
            //        (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            //        // Allow: home, end, left, right, down, up
            //        (e.keyCode >= 35 && e.keyCode <= 40)) {
            //        // let it happen, don't do anything
            //        return;
            //    }
            //    // Ensure that it is a number and stop the keypress
            //    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            //        e.preventDefault();
            //    }
            //});

            //$("#tAdvance").keydown(function (e) {
            //    // Allow: backspace, delete, tab, escape, enter and .
            //    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            //        // Allow: Ctrl+A, Command+A
            //        (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            //        // Allow: home, end, left, right, down, up
            //        (e.keyCode >= 35 && e.keyCode <= 40)) {
            //        // let it happen, don't do anything
            //        return;
            //    }
            //    // Ensure that it is a number and stop the keypress
            //    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            //        e.preventDefault();
            //    }
            //});

            //$("#tExpenses").keydown(function (e) {
            //    // Allow: backspace, delete, tab, escape, enter and .
            //    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            //        // Allow: Ctrl+A, Command+A
            //        (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            //        // Allow: home, end, left, right, down, up
            //        (e.keyCode >= 35 && e.keyCode <= 40)) {
            //        // let it happen, don't do anything
            //        return;
            //    }
            //    // Ensure that it is a number and stop the keypress
            //    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            //        e.preventDefault();
            //    }
            //});

            //$("#tDeposit").keydown(function (e) {
            //    // Allow: backspace, delete, tab, escape, enter and .
            //    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            //        // Allow: Ctrl+A, Command+A
            //        (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            //        // Allow: home, end, left, right, down, up
            //        (e.keyCode >= 35 && e.keyCode <= 40)) {
            //        // let it happen, don't do anything
            //        return;
            //    }
            //    // Ensure that it is a number and stop the keypress
            //    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            //        e.preventDefault();
            //    }
            //});

            //$("#tParty").keydown(function (e) {
            //    // Allow: backspace, delete, tab, escape, enter and .
            //    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            //        // Allow: Ctrl+A, Command+A
            //        (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            //        // Allow: home, end, left, right, down, up
            //        (e.keyCode >= 35 && e.keyCode <= 40)) {
            //        // let it happen, don't do anything
            //        return;
            //    }
            //    // Ensure that it is a number and stop the keypress
            //    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            //        e.preventDefault();
            //    }
            //});

            //alert('Calculator Loaded');
        });
    </script>
</head>
<body>
    <table style="width: 100%;">
        <tr>
            <th colspan="8" style="width: 100%;">
                <b>Please Enter EOD Details</b>
            </th>
        </tr>
        <tr>
            <td>Previous day counter cash
            </td>
            <td>
                <input type="text" id="prvsCounterCash" style="width: 80%" />
            </td>
            <td>Total Gross Sale
            </td>
            <td>
                <input type="text" id="tSale" style="width: 80%" />
            </td>
            <td>Card Payment
            </td>
            <td>
                <input type="text" id="tCardPayment" style="width: 80%" />
            </td>
        </tr>
        <tr>
            <td>Discount
            </td>
            <td>
                <input type="text" id="tDiscount" style="width: 80%" />
            </td>
            <td>Total Deposit
            </td>
            <td>
                <input type="text" id="tDeposit" style="width: 80%" />
            </td>
            <td>Total Expenses
            </td>
            <td>
                <input type="text" id="tExpenses" style="width: 80%" />
            </td>
        </tr>
        <tr>
            <td>Total Employee Payments
            </td>
            <td>
                <input type="text" id="tAdvance" style="width: 80%" />
            </td>
            <td>Party Payment
            </td>
            <td>
                <input type="text" id="tParty" style="width: 80%" />
            </td>
            <td>Today Counter Cash
            </td>
            <td>
                <input type="text" id="todayCounterCash" style="width: 80%" />
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <input type="button" value="Calculate" id="eodCalculator" onclick="Calculate()" />
            </td>
            <td>
                <br />
                <b>Difference</b>
            </td>
            <td>
                <br />
                <b>
                    <span id="lblMyValue">0</span>
                </b>
            </td>
        </tr>
    </table>


</body>
</html>
