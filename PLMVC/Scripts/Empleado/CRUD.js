
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
            alert('Error en la consulta.' + result.responseJSON.ErrorMessage);
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
                    '<option value="' + entidad.Id + '">'+entidad.Estado+'</option>';
                $("#ddlEntidad").append(filas);
            });
        },
        error: function (result) {
            alert('Error .' + result.responseJSON.ErrorMessage);
        }
    });
}

function Eliminar(Id) {

    if (confirm("¿Estas seguro de eliminar la SubCategoria seleccionada?")) {
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
           
           
            GetAll();
            
        },
        error: function (result) {
            alert('Error en la consulta.' + result.responseJSON.ErrorMessage);
        }
    });

};

