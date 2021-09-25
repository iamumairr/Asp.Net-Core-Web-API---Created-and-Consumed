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
                }, "width": "30%"
            },
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure you want to Delete?",
        text: "You will not be able to revert the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "Delete",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    });
}