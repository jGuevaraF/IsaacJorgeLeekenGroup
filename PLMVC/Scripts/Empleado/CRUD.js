
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

function guardar() {
    var idEmpleado = $('#txtid').val()
    if (idEmpleado == 0) {
        Add();
    } else {
        Update();
    }
}

function limpiarModal() {
    $('#txtid').val('');
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
                    + '<button class="btn" onclick="GetById(' + empleado.id + ')">'
                    + '<i class="fa-solid fa-pen-to-square fa-xl"></i>'
                    + '</button> '
                    + '</td>'
                    + "<td  id='id' class='text-center'>" + empleado.NumeroNomina + "</td>"
                    + "<td class='text-center'>" + empleado.Nombre + "</td>"
                    + "<td class='text-center'>" + empleado.ApellidoPaterno + "</ td>"
                    + "<td class='text-center'>" + empleado.ApellidoMaterno + "</ td>"
                    + "<td class='text-center'>" + empleado.Entidad.Estado + "</td>"
                   
                    + '<td class="text-center"> <button class="btn btn-danger" onclick="Eliminar(' + empleado.id + ')"><i class="fa-solid fa-ban fa-xl" style="color: #ffffff;"></i></button></td>'

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

function Eliminar(Id) {

    if (confirm("¿Estas seguro de eliminar el Empleado?")) {
        $.ajax({
            type: 'DELETE',
            url: 'http://localhost:50952/api/empleado/' + Id,
            success: function (result) {
                $('#myModal').modal();
                GetAll();
            },
            error: function (result) {
                alert('Error en la consulta.' + result.responseJSON.ErrorMessage);
            }
        });

    };
};

function GetById(id) {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:50952/api/empleado/' + id,
        success: function (result) {
            limpiarModal()

            $('#txtid').val(result.Object.id);
            $('#txtNumeroNomina').val(result.Object.NumeroNomina);
            $('#txtNombre').val(result.Object.Nombre);
            $('#txtApPaterno').val(result.Object.ApellidoPaterno);
            $('#txtApMaterno').val(result.Object.ApellidoMaterno);
            //$('#ddlEntidad').append(result.Object.Entidad.id);
            $("#ddlEntidad option[value='" + result.Object.Entidad.Id + "']").attr("selected", true);
            $('#modal').modal('show');
        },
        error: function (result) {
            alert('Error en la consulta.' + result.responseJSON.ErrorMessage);
        }


    });

}

function Update() {

    var Empleado = {
        
        NumeroNomina: $('#txtNumeroNomina').val(),
        Nombre: $('#txtNombre').val(),
        ApellidoPaterno: $('#txtApPaterno').val(),
        ApellidoMaterno: $('#txtApMaterno').val(),
        Entidad: {
            Id: $('#ddlEntidad').val()
        }
    }
    var idEmpleado = {
        id: $('#txtid').val()
    }

    $.ajax({
        type: 'PUT',
        url: 'http://localhost:50952/api/empleado/ ' + idEmpleado.id,
        datatype: 'json',
        data: Empleado,
        success: function (result) {
            alert("Empleado se ha actualizado correctamente");
            modalClose();
            limpiarModal();
            GetAll();
            
        },
        error: function (result) {
            alert('Error en la consulta.' + result.responseJSON.ErrorMessage);
        }
    });

};



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