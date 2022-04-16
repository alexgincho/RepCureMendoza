window.onload = function () {

    let btnAddProducto = document.getElementById("btnAddProducto");
    let btnBuscar = document.getElementById("btnBuscar");
    // Funcion Invocar un Modal
    function InvocarModal() {
        AbrirModal(`/Venta/PartialListProductos`);
    }
    // Funcion para Abrir un Modal
    function AbrirModal(url) {

        $.ajax({
            type: 'GET',
            url: url,
            dataType: "html",
            cache: false,
            success: function (data) {
                $('.modal-container-producto').html(data).find('.modal').modal({
                    show: true
                });
            }
        });
    }

    btnAddProducto.addEventListener("click", function (e) {
        e.preventDefault();
        InvocarModal();
    });

    // Tabla Productos

    let LstProductos = [];
    let Productos = {
        "pkProducto": 0,
        "codigo": "",
        "nombre": "",
        "precioventa": 0,
        "descuento":0
    };

    $(".modal-container-producto").on("show.bs.modal", function (e) {
        let AgregarProductos = $("#AgregarProductos")
        let TableProductos = $("#TableProductos");
        let DataTableProducto = TableProductos.DataTable({
            ordering: false,
            autoWidth: false,
            scrollCollapse: true,
            deferRender: true,
            scroller: true,
            lengthChange: false,
            destroy: true,
            pageLength: 5,
            language: {
                emptyTable: "No hay Productos Registrados",
            },
            ajax: {
                url: "/Venta/GetProductos"
            },
            columns: [
                {
                    data: null,
                    defaultContent: "<input type='checkbox' id='btnSelect' class='form-control form-control-sm' />",
                    orderable: false,
                    searchable: false,
                    width: 15,
                },
                { data: "codigo", title: "Codigo" },
                { data: "nombre", title: "Descripcion" },
                { data: "cantidad", title: "Cantidad" },
                { data: "precioventa", title: "Precio Unitario" },
                { data: "descuento", title: "Descuento" }
            ]
        });

        // Agregando Productos al Arreglo
        $("#TableProductos tbody").on('change', '#btnSelect', function (e) {
            if (this.checked) {
                let pkProducto = DataTableProducto.row($(this).parents("tr")).data().pkProducto;
                let codigo = DataTableProducto.row($(this).parents("tr")).data().codigo;
                let nombre = DataTableProducto.row($(this).parents("tr")).data().nombre;
                let precioventa = DataTableProducto.row($(this).parents("tr")).data().precioventa;
                let descuento = DataTableProducto.row($(this).parents("tr")).data().descuento;

                if (LstProductos.length >= 0) {

                    Productos = new Object(); // Cosas q no se deben de hacer :'c

                    Productos.pkProducto = pkProducto;
                    Productos.codigo = codigo;
                    Productos.nombre = nombre;
                    Productos.precioventa = precioventa;
                    Productos.descuento = descuento;

                    LstProductos.push(Productos);
                }             
            }
            else {
                let id = DataTableProducto.row($(this).parents("tr")).data().pkProducto;
                var index = LstProductos.find((v, i) => {
                    if (v.pkProducto === id) {
                        LstProductos.splice(i, 1);
                    }
                });
            }
        });
    });

    $(".modal-container-producto").on("click", "#AgregarProductos", function (e) {
            let TableCompraProducto = $("#TableCompraProducto");      
            LstProductos.forEach((v, i) => {
                $("#TableCompraProducto tbody").append(
                    `<tr>
                        <td> <button class='btn btn-danger'> <i class='fa fa-trash' ></i> </button> </td>                      
                        <td> <input name='pkProducto[]' id='pkProducto'  type='hidden' value='${v.pkProducto}' /> <input type='text' disabled value='${v.codigo}' class='form-control form-control-sm' /> </td>
                        <td> <input name='NombreProducto[]' id='NombreProducto'  type='text' disabled value='${v.nombre}' class='form-control form-control-sm' /> </td>
                        <td> <input name='Cantidad[]' id='Cantidad'  type='number' class='form-control form-control-sm' /> </td>
                        <td> <input name='PrecioVenta[]' id='PrecioVenta'  type='text' disabled value='${v.precioventa}' class='form-control form-control-sm' /> </td>
                        <td> <input name='Descuento[]' id='Descuento'  type='text' disabled value='${v.descuento}' class='form-control form-control-sm' /> </td>
                        <td> <input name='SubTotal[]' id='SubTotal'  type='text' class='form-control form-control-sm' disabeld /> </td>
                    </tr> `)
            });
            LstProductos = [];
            $('#modal-default').modal('hide');
    });

    // Eventos Suma y Multiplicacion Detalle Compras
    $(document).on("change", "#Cantidad", function (e) {
        let td = $(e.target).closest('tr');
        let m1 = parseFloat($(td).find("#Cantidad").val()) || 0;
        let m2 = parseFloat($(td).find("#PrecioVenta").val()) || 0;
        $(td).find("#SubTotal").val(m1 * m2);
        calcularTotal();
    });
    function calcularTotal() {
        let SubTotal = document.querySelectorAll("#SubTotal");
        let Total = 0;
        SubTotal.forEach((v, i) => {
            if (v.value == "") {
                v.value = 0;
            }
            Total = parseInt(Total) + parseInt(v.value);
        });
        $('#Total').val(Total);
    }

    // Tabla Clientes
    let LstCliente = [];
    let Cliente = {
        "pkUsuario": 0,
        "Nombre": "",
        "ApellidoPaterno": "",
        "ApellidoMaterno": ""
    };
    btnBuscar.addEventListener("click", function (e) {
        e.preventDefault();
        InvocarModalCliente();
    });
    // Funcion Invocar un Modal
    function InvocarModalCliente() {
        AbrirModalCliente(`/Venta/PartialListCliente`);
    }
    // Funcion para Abrir un Modal
    function AbrirModalCliente(url) {

        $.ajax({
            type: 'GET',
            url: url,
            dataType: "html",
            cache: false,
            success: function (data) {
                $('.modal-container-cliente').html(data).find('.modal').modal({
                    show: true
                });
            }
        });
    }
    $(".modal-container-cliente").on("show.bs.modal", function (e) {
        let TableCliente = $("#TableCliente");
        let DataTableCliente = TableCliente.DataTable({
            ordering: false,
            autoWidth: false,
            scrollCollapse: true,
            deferRender: true,
            scroller: true,
            lengthChange: false,
            destroy: true,
            pageLength: 5,
            language: {
                emptyTable: "No hay Clientes Registrados",
            },
            ajax: {
                url: "/Venta/GetClientes"
            },
            columns: [
                {
                    data: null,
                    defaultContent: "<input type='checkbox' id='btnSelect' class='form-control form-control-sm' />",
                    orderable: false,
                    searchable: false,
                    width: 15,
                },              
                { data: "nombre", title: "Nombre" },
                { data: "apellidopaterno", title: "Apellido Paterno" },
                { data: "apellidomaterno", title: "Apellido Materno" },         
            ]
        });

        // Agregando Clientes al Array
        $("#TableCliente tbody").on('change', '#btnSelect', function (e) {
            if (this.checked) {
                let pkUsuario = DataTableCliente.row($(this).parents("tr")).data().pkUsuario;
                let Nombre = DataTableCliente.row($(this).parents("tr")).data().nombre;
                let ApellidoPaterno = DataTableCliente.row($(this).parents("tr")).data().apellidomaterno;
                let ApellidoMaterno = DataTableCliente.row($(this).parents("tr")).data().apellidopaterno;

                if (LstCliente.length >= 1) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops..',
                        text: 'No puedes agregar 2 Clientes!',
                    }).then((result) => {
                        if (result.isConfirmed) {
                            this.checked = false;
                        }
                    });
                }
                else {
                    Cliente.pkUsuario = pkUsuario;
                    Cliente.Nombre = Nombre;
                    Cliente.ApellidoPaterno = ApellidoPaterno;
                    Cliente.ApellidoMaterno = ApellidoMaterno;
                    LstCliente.push(Cliente);
                }
            }
            else {
                let id = DataTableCliente.row($(this).parents("tr")).data().pkUsuario;
                var index = LstCliente.find((v, i) => {
                    if (v.pkUsuario === id) {
                        LstCliente.splice(i, 1);
                        LstCliente = [];
                    }
                });
            }
        });

    });
    let pkUsuarios = document.getElementById("pkUsuarios");
    let NombreCliente = document.getElementById("NombreCliente");
    $(".modal-container-cliente").on("click", "#AgregarCliente", function (e) {

        LstCliente.forEach((v, i) => {
            pkUsuarios.value = v.pkUsuario;
            NombreCliente.value = `${v.Nombre} ${v.ApellidoPaterno} ${v.ApellidoMaterno} `;
        });
        LstCliente = [];
        $('#modal-default').modal('hide');
    });

    // Create Venta

    let AgregarVentas = document.getElementById("AgregarVentas");
//https://es.stackoverflow.com/questions/128382/como-recorrer-una-tabla-y-obtener-los-valores-dentro-del-input-con-jquery
    AgregarVentas.addEventListener("click", function (e) {
        e.preventDefault();
        let TableCompraProducto = document.getElementById("TableCompraProducto");
        let tableRows = document.querySelectorAll('#TableCompraProducto tbody tr');

        let Ventas = {
            "Codigo": $("#CodigoV").val(),
            "Fechacrea": $("#FechaEntrega").val(),
            "Observacion": $("#Observacion").val(),
            "FkUsuario": $("#pkUsuarios").val(),
            "Totalventa": $("#Total").val(),
            "Detalleventa": [
                
            ]
        };
        let Detalleventa = {
            "FkProducto": 0,
            "Preciounitario": 0,
            "Cantidad": 0,
            "Descuento": 0,
            "Subtotal": 0
        }

        for (let i = 0; i < tableRows.length; i++) {
            let row = tableRows[i];
            let pkProducto = row.querySelectorAll("#pkProducto")
            let fkproducto = pkProducto.
            console.log(pkProducto)
        }
        console.log(Ventas);
    });

};