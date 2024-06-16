var dataTable;

$(document).ready(function () {
    alert("Hola")
    loadDataTable();
});

function loadDataTable() {
    //busque un documento #tbldata y conviertalo en un datatable
    dataTable = $('#tblData').DataTable({
        //utilice ajax para consumir una url que se encuentra en "/VehicleModel/getall"
        ajax: {
            "url": "/Especialidad/getall"
        },
        //el match de las columnas indica como se ordenan los datos en sus respectivas columnas
        "columns": [
            { "data": "nombre", "width": "30%" }
            
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