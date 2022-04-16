$(document).ready(function() {

    let TableProducto = $("#TableProducto");
    let btnAddProducto = $("#btnAddProducto");
    

    btnAddProducto.on("click", function (e) {
        e.preventDefault();
        InvocarModal();
    });
    
    //Función Invocar un modal 
    function InvocarModal(id) {
        AbrirModal(`/Producto/MantenimientoProductos/${id ? id : ""}`);
    }
    
    //Función para abrir un modal
    function AbrirModal(url) {

        $.ajax({
            type: 'GET',
            url: url,
            dataType: "html", 
            cache: false,
            success: function (data) {
                $('.modal-container').html(data).find('.modal').modal({
                    show: true
                });
            }
        });
    }

    //Listadao de Producto-revisar getallproducto
    let DataTableProducto = TableProducto.DataTable({
        scrollY:200,
        scrollX: true,
        paging: false,
        ordering: false,
        ajax: {
            url: '/Producto/GetAllProductoP',
        },
        columnDefs: [
            { targets: 0, width:100},
            { targets: 1, width: 110},
            { targets: 2, width: 180 },
            { targets: 3, width: 180 },
            { targets: 4, width: 210 },
            { targets: 5, width: 100 },
            { targets: 6, width: 210 },
            { targets: 7, width: 150 },
            { targets: 8, width: 100 }
        ],
        columns: [
            { data: "codigo", title:"Codigo" },
            { data: "nombre", title: "Nombre" },
            { data: "descripcion", title: "Descripcion" },
            { data: "precioventa", title: "Precio de Venta" },
            { data: "preciocompra", title: "Precio de Compra" },
            { data: "cantidad", title:"Cantidad" },
            { data: "descuento", title: "Descuento" },
            { data: "fkCategoria", title: "Categoria" },
            { data: "fechavencimiento", title: "Fecha de Vencimiento" },
            {
                data: null,
                defaultContent: "<button type='button' id='btnEditar' class='btn btn-primary'><i class='fas fa-pen-square'></i></i></button>",
                orderable: false,
                searchable: false,
                width: "26px"
            },
            { data: null, defaultContent: "<button type='button' id='btnEliminar' class='btn btn-danger'><i class='fas fa-trash-alt'></i></i></button>"}
        ]
    });

    //Agregar Producto 
    $(".modal-container").on("click", "#btnSave", function (e) {
        e.preventDefault();
        let Producto = {

            "Codigo": $("#Codigo").val(),
            "Nombre": $("#Nombre").val(),
            "Descripcion": $("#Descripcion").val(),
            "Precioventa": $("#Precioventa").val(),
            "Preciocompra": $("#Preciocompra").val(),
            "Cantidad": $("#Cantidad").val(),
            "Descuento" : $("#Descuento").val(),
            "FkCategoria": $("#FkCategoria").val(),
            "Fechavencimiento": $("#Fechavencimiento").val(),
            "Imagen": $("#Imagen").val()
        }

        Swal.fire({
            title: 'Desea Registrar este producto?',
            showDenyButton: true,
            confirmButtonText: 'Registrar',
            denyButtonText: `Cancelar`,
            denyButtonClass: 'button-cancel'
        }).then((result) =>{
            if(result.isConfirmed){
                $.ajax({
                    url: '/Producto/CreateProducto',
                    data: JSON.stringify(Producto),
                    type: 'POST',
                    contentType: 'application/json;charset=utf-8',
                    dataType: "json",
                    success: function (data) {
                        console.log(data);
                        if(data.state == 200){
                            console.log(data);
                            Swal.fire('Saved!', '', 'success')
                            $('#modal-default').modal('hide');
                            DataTableProducto.ajax.reload();
                        }
                        else if(data.state == 404){
                            console.log(data);
                            Swal.fire(`Upss! ${data.message}`, '', 'info')
                            $('#modal-default').modal('hide');
                        }
                    },
                    error: function(error) {
                        if (error.status === 400) {
                            Swal.fire('Upss! Ocurrio Algo.....', '', 'info')
                        }
                    }
                });
            }
            else if (result.isDenied){
                Swal.fire('Los cambios no se registraron', '', 'info')
                $('#modal-default').modal('hide');   
            }
        })
    });


    //Ingresando Actualizar

    TableProducto.on("click", "#btnEditar", function() {
        let id = DataTableProducto.row($(this).parents("tr")).data().pkProducto;
        console.log(id);
        InvocarModal(id);
    });
    $(".modal-container").on("click", "#btnUpdate", function (e){
        e.preventDefault();
        let Producto = {
            "PkProducto": $("#PkProducto").val(),
            "Codigo": $("#Codigo").val(),
            "Nombre": $("#Nombre").val(),
            "Descripcion": $("#Descripcion").val(),
            "Precioventa": $("#Precioventa").val(),
            "Preciocompra": $("#Preciocompra").val(),
            "Cantidad": $("#Cantidad").val(),
            "Descuento": $("#Descuento").val(),
            "FkCategoria": $("#FkCategoria").val(),
            "Fechavencimiento": $("#Fechavencimiento").val()
        }

        Swal.fire({
            title: 'Desea actualizar este Producto?',
            showDenyButton: true,
            confirmButtonText: 'Actualizar',
            denyButtonText: `Cancelar`,
            denyButtonClass: 'button-cancel'
        }).then((result) => {
            if(result.isConfirmed){
                $.ajax({
                    url: '/Producto/UpdateProducto',
                    data: JSON.stringify(Producto),
                    type: 'POST',
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        console.log(data);
                        if(data.state == 200){
                            console.log(data);
                            Swal.fire('Update!', '', 'success')
                            $('#modal-default').modal('hide');
                            DataTableProducto.ajax.reload();          
                        }
                        else if (data.state == 404){
                            console.log(data);
                            Swal.fire(`Ups! ${data.message}`, '', 'info')
                                $('#modal-default').modal('hide');
                        }
                    },
                    error: function(error){
                        if(error.status === 400){
                            Swal.fire('Ups! Ocurrio Algo.', '', 'info')
                        }
                    }
                })
            }
            else if (result.isDenied){
                Swal.fire('Cambios no actualizados', '', 'info')
                $('#modal-default').modal('hide');
            }
        })
    });

    //Desactiva un Producto
    
    TableProducto.on("click", "#btnEliminar", function (){
        let id = DataTableProducto.row($(this).parents("tr")).data().pkProducto;
        let nombre = DataTableProducto.row($(this).parents("tr")).data().nombre;
        console.log(id); console.log(nombre);
        
        //Ajax

        Swal.fire({
            title: 'Estas completamente Seguro?',
            text: `que desea eliminar a este producto : ${nombre} `,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok, Eliminar esto!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: `/Producto/DesactiveProducto`,
                    data: JSON.stringify(id),                 
                    type: 'POST',
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        console.log(data);
                        if (data.state == 200) {
                            Swal.fire(
                                'Desactivado!',
                                `El Producto ${nombre} a sido Desactivado.`,
                                'success'
                            )
                            DataTablePersonal.ajax.reload();
                        }
                    },
                    error: function (error) {
                        if (error.status === 400) {
                            Swal.fire('Ups! Ocurrio Algo.', '', 'info')
                        }
                    }
                });
            }
        });
   
    });
    

});
