var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({

        ajax: {
            "url": "/Especialidad/getall"
        },
        "columns": [
            { "data": "nombre", "width": "30%" },
            {
                "data": "ID",
                "render": function (data) {
                    return `
                            <a href="/VehicleModel/upsert/${data}" class="btn btn-primary mx-2">
                                <i class="bi bi-pencil-square"></i> Edit
                            </a>

                            <a onClick=Delete(${data}) class="btn btn-danger mx-2">
                                <i class="bi bi-trash"></i> Delete
                            </a>
                          `
                }
            }
            
        ]
    });
}

//    function Delete(_id) {
//        Swal.fire({
//            title: "Are you sure?",
//            text: "You won't be able to revert this!",
//            icon: "warning",
//            showCancelButton: true,
//            confirmButtonColor: "#3085d6",
//            cancelButtonColor: "#d33",
//            confirmButtonText: "Yes, delete it!"
//        }).then((result) => {
//            if (result.isConfirmed) {

//                //metodo que permite hacer el delete sin tener que hacer un httpget
//                $.ajax({
//                    url: "/VehicleModel/delete/" + _id,
//                    type: 'DELETE',
//                    success: function (data) {
//                        if (data.success) {
//                            dataTable.ajax.reload();
//                            alert("Eliminado");
//                        }
//                        else {
//                            alert("Error");
//                        }
//                    },
//                    error: function () {
//                        alert("Error");
//                    }
//                });
//            }
//        });

//}