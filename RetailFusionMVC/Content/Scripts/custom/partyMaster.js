$(document).ready(function () {



    $('#btnAddParty').click(function () {

        var url = "/Home/AddParty";
        var partyName = $('#tPartyName').val();
        var partyAddress = $('textarea#tPartyAddress').val();
        var partyContacts = $('textarea#tPartyContacts').val();
        var partyBankDetails = $('textarea#tPartyBankDetails').val();
        var brands = $('textarea#tBrands').val();

        $.post(url, { PartyName: partyName, PartyAddress: partyAddress, PartyContacts: partyContacts, PartyBankDetails: partyBankDetails, Brands: brands }, function (data) {
            $("#msg").html(data);
        });

        $.ajax({
            url: "/Home/GetPartyList",
            dataType: "json",
            data: {},
            type: "GET",
            contentType: 'application/json',
            success: function (data) {
                jQuery("#gridPartyList").jqGrid('setGridParam', { data: data }).trigger('reloadGrid');
            }
        });

        var depositAmount = $('#tPartyName').val('');
        var depositAmount = $('#tPartyAddress').val('');
        var depositAmount = $('#tPartyContacts').val('');
        var depositAmount = $('#tPartyBankDetails').val('');
        var depositAmount = $('#tBrands').val('');

    });

    $('#btnAddEmployee').click(function () {

        var url = "/Home/AddEmployee";
        var empName = $('#tEmpName').val();
        var employeeAddress = $('textarea#tEmployeeAddress').val();
        var employeeProofType = $('#tEmployeeProofType').val();
        var employeeProofId = $('#tEmployeeProofId').val();
        var employeeMobile = $('#tEmployeeMobile').val();

        $.post(url, { EmpName: empName, EmployeeAddress: employeeAddress, EmployeeProofType: employeeProofType, EmployeeProofId: employeeProofId, EmployeeMobile: employeeMobile },
            function (data) {
                $("#msg").html(data);
            });

        $.ajax({
            url: "/Home/GetEmployeeList",
            dataType: "json",
            data: {},
            type: "GET",
            contentType: 'application/json',
            success: function (data) {
                jQuery("#gridEmployeeList").jqGrid('setGridParam', { data: data }).trigger('reloadGrid');
            }
        });



        var depositAmount = $('#tEmpName').val('');
        var depositAmount = $('#tEmployeeAddress').val('');
        var depositAmount = $('#tEmployeeProofType').val('');
        var depositAmount = $('#tEmployeeProofId').val('');
        var depositAmount = $('#tEmployeeMobile').val('');

    });

    $("#gridPartyList").jqGrid({
        url: "/Home/GetPartyList",
        datatype: 'json',
        mtype: 'GET',
        colNames: ['Party Name', 'Party Address', 'Contact Details', 'Brands Available', 'Party Bank Details'],
        colModel: [
            { key: false, name: 'PartyName', sortable: false },
            { key: false, name: 'PartyAddress', sortable: false },
            { key: false, name: 'PartyContact', sortable: false },
            { key: false, name: 'PartyProduct', sortable: false },
            { key: false, name: 'PartyBankname', sortable: false }
        ],
        height: '200px',
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

    $("#gridEmployeeList").jqGrid({
        url: "/Home/GetEmployeeList",
        datatype: 'json',
        mtype: 'GET',
        colNames: ['Employee Name', 'Employee Role', 'Contact No', 'Proof Id', 'Poof Type'],
        colModel: [
            { key: false, name: 'EmpName', sortable: false },
            { key: false, name: 'EmpRole', sortable: false },
            { key: false, name: 'EmpMobile', sortable: false },
            { key: false, name: 'EmpProofID', sortable: false },
            { key: false, name: 'EmpProofType', sortable: false }
        ],
        height: '200px',
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
});