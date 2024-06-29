var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        ajax: {
            "url": "/Medicina/Tratamiento/getall"
        },
        "columns": [
            { "data": "nombre", "width": "30%" },
            { "data": "descripcion", "width": "30%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="d-flex justify-content-center align-items-center">
                            <div class="btn-group" role="group" aria-label="Acciones">
                                <a href="/Medicina/Tratamiento/Upsert/${data}" class="btn btn-primary btn-sm mx-1">
                                    <i class="bi bi-pencil-square"></i> Editar
                                </a>

                                <a onClick=Delete(${data}) class="btn btn-danger btn-sm mx-1">
                                    <i class="bi bi-trash"></i> Borrar
                                </a>
                            </div>
                        </div>
                    `;
                },
                "width": "50%",  // Ajusta el ancho para que los botones ocupen el 50% de la columna
                "className": "text-center",
                "orderable": false,  // Evita que esta columna sea ordenable
                "title": "Acciones"  // Agrega el título "Acciones" al encabezado de la columna 
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
                url: "/Medicina/Tratamiento/delete/" + _id,
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