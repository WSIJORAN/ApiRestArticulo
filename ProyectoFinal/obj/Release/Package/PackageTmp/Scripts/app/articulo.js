$(document).on("ready", function () {
    $('#btnSearch').on('click', function () {
        GetArticuloById($('#txtIdSearch').val());
    })
    $('#btnDelete').on('click', function () {
        DeleteArticuloById($('#txtIdSearch').val());
    })
    $('#btnUpdate').on('click', function () {
        var articulo = new Object();
        articulo.id = $('#txtIdSearch').val();
        articulo.Producto = $('#txtProducto').val();
        articulo.Costo = $('#txtCosto').val();
        articulo.Referencia = $('#txtReferencia').val();
        UpdateArticulo(articulo.id, JSON.stringify(articulo));
    })
    $('#btnCreate').on('click', function () {
        var articulo = new Object();
        articulo.Producto = $('#txtProducto').val();
        articulo.Costo = $('#txtCosto').val();
        articulo.Referencia = $('#txtReferencia').val();
        CreateArticulo(JSON.stringify(articulo));
    })
    GetAll();
})

//Get all Articulos
function GetAll() {
    var item = "";
    $('#tblList tbody').html('');
    $.getJSON('/api/Articulo', function (data) {
        $.each(data, function (key, value) {
            item += "<tr><td>" + value.Producto + "</td><td>" + value.Costo + "</td><td>" + value.Referencia + "</td></tr>";
        });
        $('#tblList tbody').append(item);
    });
};

//Get Articulo by id
function GetArticuloById(idArticulo) {
    var url = '/api/Articulo/' + idArticulo;
    $.getJSON(url)
        .done(function (data) {
            $('#txtProducto').val(data.Producto);
            $('#txtCosto').val(data.Costo);
            $('#txtReferencia').val(data.Referencia);
        })
        .fail(function (erro) {
            ClearForm();
        });
};

//Delete Articulo by id
function DeleteArticuloById(idArticulo) {
    var url = '/api/Articulo/' + idArticulo;
    $.ajax({
        url: url,
        type: 'DELETE',
        contentType: "application/json;chartset=utf-8",
        statusCode: {
            200: function () {
                GetAll();
                ClearForm();
                alert('Articulo con id: ' + idArticulo + ' se elimino');
            },
            404: function () {
                alert('Articulo con id: ' + idArticulo + ' no se elimino');
            }
        }
    });
}

//Update Articulo
function UpdateArticulo(idArticulo, articulo) {
    var url = '/api/Articulo/' + idArticulo;
    $.ajax({
        url: url,
        type: 'PUT',
        data: articulo,
        contentType: "application/json;chartset=utf-8",
        statusCode: {
            200: function () {
                GetAll();
                ClearForm();
                alert('Articulo con id: ' + idArticulo + ' se actualizo');
            },
            404: function () {
                ClearForm();
                alert('Articulo con id: ' + idArticulo + ' no se actualizo');
            },
            400: function () {
                ClearForm();
                alert('Error');
            }
        }
    });
}

//Create a new Articulo
function CreateArticulo(articulo) {
    var url = '/api/Articulo/';
    $.ajax({
        url: url,
        type: 'POST',
        data: articulo,
        contentType: "application/json;chartset=utf-8",
        statusCode: {
            201: function () {
                GetAll();
                ClearForm();
                alert('Articulo con id: ' + articulo + ' se actualizo');
            },
            400: function () {
                ClearForm();
                alert('Error');
            }
        }
    });
}

//Clear form
function ClearForm() {
    $('#txtIdSearch').val('');
    $('#txtProducto').val('');
    $('#txtCosto').val('');
    $('#txtReferencia').val('');
}