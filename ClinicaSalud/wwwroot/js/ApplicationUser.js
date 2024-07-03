var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        ajax: {
            "url": "/Administracion/ApplicationUser/getall"
        },
        "columns": [
            { "data": "nombre", "width": "25%" },
            { "data": "apellidos", "width": "25%" },
            { "data": "cedula", "width": "25%" },
            { "data": "isBlocked", "width": "25%" },
            {
                "data": "cedula",
                "render": function (data) {
                    return `
                        <div class="d-flex justify-content-center align-items-center">
                            <div class="btn-group" role="group" aria-label="Acciones">
                                <a href="/Administracion/ApplicationUser/ToggleBlock/${data}" class="btn btn-info btn-sm mx-1">
                                    <i class="bi bi-info-circle"></i> Bloquear/Desbloquear
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

function ToggleBlock(_cedula) {
    console.log("Cedula: ", _cedula); // Verifica que el valor no sea nulo aquí
    Swal.fire({
        title: "¿Está seguro de bloquear o desbloquear?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Bloquear/Desbloquear"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/Administracion/ApplicationUser/ToggleBlock/" + _cedula,
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        alert("Bloqueado/Desbloqueado");
                    } else {
                        alert("ErrorSuccess");
                    }
                },
                error: function () {
                    alert("ErrorNoSuccess");
                }
            });
        }
    });
}