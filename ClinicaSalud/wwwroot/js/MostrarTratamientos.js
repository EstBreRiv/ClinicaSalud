var dataTable;

let idPaciente;

$(document).ready(function () {
    idPaciente = $('#IdPaciente').val();

    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        ajax: {
            "url": "/Medicina/Paciente/getTratamientos/" + idPaciente
        },
        "columns": [
            { "data": "nombre", "width": "50%" }


        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
        "responsive": true
    });
}