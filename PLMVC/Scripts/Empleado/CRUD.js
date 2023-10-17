
$(document).ready(function () { //click
    GetAll();
    GetAllEntidad();
});

function showModal() {
    $('#modal').modal("show");
}

function modalClose() {
    $('#modal').modal("hide");
}

function limpiarModal() {
    $('#txtNumeroNomina').val('');
    $('#txtNombre').val('');
    $('#txtApPaterno').val('');
    $('#txtApMaterno').val('');
    $('#ddlEntidad option[value="0"]').attr("selected", true);
}


function GetAll() {
    $.ajax({

        url: 'http://localhost:50952/api/empleado',
        type: 'GET',
        success: function (result) {
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
            alert('Error en la consulta.' + result.responseJSON.ErrorMessage); //es un error de JSON, y es porque puede venir vacio o null
        }
    });
};

function GetAllEntidad() {
    $.ajax({

        url: 'http://localhost:50952/api/entidad',
        type: 'GET',
        success: function (result) {
            console.log(result)
            $.each(result.Objects, function (i, entidad) {
                var filas =
                    '<option value="' + entidad.Id + '">' + entidad.Estado + '</option>';
                $("#ddlEntidad").append(filas);
            });
        },
        error: function (result) {
            alert('Error .' + result.responseJSON.ErrorMessage);
        }
    });
}

function Add() {


    var json = {
        NumeroNomina: $('#txtNumeroNomina').val(),
        Nombre: $('#txtNombre').val(),
        ApellidoPaterno: $('#txtApPaterno').val(),
        ApellidoMaterno: $('#txtApMaterno').val(),
        Entidad: {
            "Id": Math.floor($('#ddlEntidad').val())
        }
    }


    $.ajax({

        url: 'http://localhost:50952/api/empleado',
        type: 'POST',
        datatype: 'JSON',
        data: json,
        success: function (result) {
            console.log(result);
            alert("Empleado dado de alta correctamente");
            modalClose();
            limpiarModal();
            GetAll();
        },
        error: function (result) {
            console.log(result);
            alert('Error en la consulta.' + result.responseJSON.ErrorMessage); //es un error de JSON, y es porque puede venir vacio o null
        }
    });

}