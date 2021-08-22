var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $("#tableData").DataTable({
        "ajax": {
            "url": "/NationalParks/GetAllNationalParks",
            "type": "GET",
            "dataType": "json",
        },
        "columns": [
            { "data": "name", "width": "50%" },
            { "data": "state", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/NationalParks/Upsert/${data}" class="btn btn-success text-white pe-auto">
                                    <i class="far fa-edit"></i>
                                </a>
                                &nbsp;
                                <a onclick=Delete("/NationalParks/Delete/${data}") class="btn btn-danger text-white pe-auto">
                                    <i class="far fa-trash-alt"></i>
                                </a>
                            </div>`
                }, "width":"30%"
            },
        ]
    });
}