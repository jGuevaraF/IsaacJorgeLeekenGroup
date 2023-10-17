﻿function showModal() {
    $('#modal').modal("show");
}

function modalClose() {
    $('#modal').modal("hide");
}

$(document).ready(function () { //click
    GetAll();
});

function GetAll() {
    $.ajax({

        url: 'http://localhost:50952/api/empleado',
        type: 'GET',
        success: function (result) { //200 OK 
            console.log(result)
            $('#tablaEmpleado tbody').empty();
            $.each(result.Objects, function (i, empleado) {
                var filas =
                    '<tr>'
                    + '<td class="text-center"> '
                    + '<button class="btn" onclick="GetById(' + empleado.IdSubCategoria + ')">'
                    + '<i class="fa-solid fa-pen-to-square fa-xl"></i>'
                    + '</button> '
                    + '</td>'
                    + "<td  id='id' class='text-center'>" + empleado.NumeroNomina + "</td>"
                    + "<td class='text-center'>" + empleado.Nombre + "</td>"
                    + "<td class='text-center'>" + empleado.ApellidoPaterno + "</ td>"
                    + "<td class='text-center'>" + empleado.ApellidoMaterno + "</ td>"
                    + "<td class='text-center'>" + empleado.Entidad.Estado + "</td>"
                    + '<td class="text-center"> <button class="btn btn-danger" onclick="Eliminar(' + empleado.IdSubCategoria + ')"><i class="fa-solid fa-ban fa-xl" style="color: #ffffff;"></i></button></td>'

                    + "</tr>";
                $("#tablaEmpleado tbody").append(filas);
            });
        },
        error: function (result) {
            alert('Error en la consulta.' + result.responseJSON.ErrorMessage);
        }
    });
};