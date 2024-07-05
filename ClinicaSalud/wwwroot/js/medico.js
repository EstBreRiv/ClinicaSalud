var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        ajax: {
            "url": "/Administracion/Medico/getall"
        },
        "columns": [
            { "data": "nombre", "width": "25%" },
            { "data": "apellidos", "width": "25%" },
            { "data": "numeroColegiado", "width": "25%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="d-flex justify-content-center align-items-center">
                            <div class="btn-group" role="group" aria-label="Acciones">
                                <a href="/Administracion/Medico/Upsert/${data}" class="btn btn-primary btn-sm mx-1">
                                    <i class="bi bi-pencil-square"></i> Editar
                                </a>
                                <a onClick=Delete(${data}) class="btn btn-danger btn-sm mx-1">
                                    <i class="bi bi-trash"></i> Borrar
                                </a>
                                <a href="/Administracion/Medico/Details/${data}" class="btn btn-info btn-sm mx-1">
                                    <i class="bi bi-info-circle"></i> Detalles
                                </a>
                            </div>
                        </div>
                    `;
                },
                "width": "25%",
                "orderable": false, 
                "title": "Acciones",
                "className": "text-center"
            }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
        "responsive": true
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
                url: "/Administracion/Medico/delete/" + _id,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                },
                error: function () {
                    alert("Error");
                }
            });
        }
    });

}