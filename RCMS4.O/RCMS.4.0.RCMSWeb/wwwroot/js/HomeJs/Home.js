
$(function () {
    $.ajax({
        type: "POST",
        url: "/Home/IndexJson",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess,
        failure: function (response) {
            alert(response.d);
        },
        error: function (response) {
            alert(response.d);
        }
    });
});
function OnSuccess(response) {
    debugger;
    $.noConflict();
    $("#example").DataTable(
        {
            bLengthChange: true,
            lengthMenu: [[5, 10, -1], [5, 10, "All"]],
            bFilter: true,
            bSort: true,
            bPaginate: true,
            data: response,
            columns: [{ "data": "id", "name": "Id", "autoWidth": true },
            { "data": "name", "name": "Name", "autoWidth": true },
            { "data": "address", "name": "Address", "autoWidth": true },
            { "data": "city", "name": "City", "autoWidth": true },
            { "data": "region", "name": "Region", "autoWidth": true },
            { "data": "postalCode", "name": "PostalCode", "autoWidth": true },
            { "data": "country", "name": "Country", "autoWidth": true },
            { "data": "phone", "name": "Phone", "autoWidth": true },
            { "data": "email", "name": "Email", "autoWidth": true },],
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'excelHtml5',
                    title: 'Excel',
                    text: 'Export to excel'
                },
                {
                    extend: 'pdfHtml5',
                    title: 'PDF',
                    text: 'Export to PDF',
                }
                ,
                {
                    extend: 'csvHtml5',
                    title: 'CSV',
                    text: 'Export to CSV',
                },
                {
                    text: 'Export to JSON',
                    action: function (e, dt, button, config) {
                        debugger;
                        var data = dt.buttons.exportData();

                        $.fn.dataTable.fileSave(
                            new Blob([JSON.stringify(data)]),
                            'Export.json'
                        );
                    }
                },
                {
                    text: 'Export to XML',
                    action: function exporttoxml() {
                        debugger;
                        $("#example").tabletoxml({
                            rootnode: "Employee",
                            childnode: "EmployeeDetails",
                            filename: 'EmployeeList'
                        });
                    }
                }
            ]

        });
};


