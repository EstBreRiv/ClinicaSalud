var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        ajax: {
            "url": "/Administracion/Especialidad/getall"
        },
        "columns": [
            { "data": "nombre", "width": "30%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <a href="/Administracion/Especialidad/Upsert/${data}" class="btn btn-primary mx-2">
                                <i class="bi bi-pencil-square"></i> Editar
                            </a>

                            <a onClick=Delete(${data}) class="btn btn-danger mx-2">
                                <i class="bi bi-trash"></i> Borrar
                            </a>
                          `
                }
            }
            
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        }
    });
}

    function Delete(_id) {
        Swal.fire({
            title: "¿Esta seguro de eliminar?",
            text: "No se podran recuperar los datos borrados",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Borrar"
        }).then((result) => {
            if (result.isConfirmed) {

                //metodo que permite hacer el delete sin tener que hacer un httpget
                $.ajax({
                    url: "/Administracion/Especialidad/delete/" + _id,
                    type: 'DELETE',
                    success: function (data) {
                        if (data.success) {
                            dataTable.ajax.reload();
                            alert("Eliminado");
                        }
                        else {
                            alert("Error");
                        }
                    },
                    error: function () {
                        alert("Error");
                    }
                });
            }
        });

}


