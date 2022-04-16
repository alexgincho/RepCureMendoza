$(document).ready(function () {

    let TableProveedor = $("#TableProveedor");
    let btnAddProveedor = $("#btnAddProveedor");

    btnAddProveedor.on("click", function (e) {
        e.preventDefault();
        InvocarModal();
    });
    // Funcion Invocar un Modal
    function InvocarModal(id) {
        AbrirModal(`/Proveedor/MantenimientoProveedor/${id ? id : ""}`);
    }
    // Funcion para Abrir un Modal
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
    // Listado de Proveedor Consumiendo Datataables
    let DataTableProveedor = TableProveedor.DataTable({
        scrollY: 200,
        scrollX: true,
        paging: false,
        ordering: false,
        ajax: {
            url: '/Proveedor/GetAllProveedor',
        },
        columnDefs: [
            { targets: 0, width: 200 },
            { targets: 1, width: 210 },
            { targets: 2, width: 280 },
            { targets: 3, width: 340 },
            { targets: 4, width: 310 }
        ],
        columns: [
            { data: "ruc", title: "Ruc" },
            { data: "razonsocial", title: "Razon Social" },
            { data: "direccion", title: "Direccion" },
            { data: "email", title: "Email" },
            { data: "telefono", title: "Telefono" },
            {
                data: null,
                defaultContent: "<button type='button' id='btnEditar' class='btn btn-primary'><i class='fas fa-pen-square'></i></i></button>",
                orderable: false,
                searchable: false,
                width: "26px"
            },
            { data: null, defaultContent: "<button type='button' id='btnEliminar' class='btn btn-danger'><i class='fas fa-trash-alt'></i></i></button>" }
        ]
    });
    // Agregar Proveedor
    $(".modal-container").on("click", "#btnSave", function (e) {
        e.preventDefault();
        let Proveedor = {

            "Ruc": $("#Ruc").val(),
            "Razonsocial": $("#Razonsocial").val(),
            "Direccion": $("#Direccion").val(),
            "Email": $("#Email").val(),
            "Telefono": $("#Telefono").val(),
        }

        Swal.fire({
            title: '¿Desea Registrar a este Proveedor?',
            showDenyButton: true,
            confirmButtonText: 'Registrar',
            denyButtonText: `Cancelar`,
            denyButtonClass: 'button-cancel'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: '/Proveedor/CreateProveedor',
                    data: JSON.stringify(Proveedor),
                    type: 'POST',
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        console.log(data);
                        if (data.state == 200) {
                            console.log(data);
                            Swal.fire('Saved!', '', 'success')
                            $('#modal-default').modal('hide');
                            DataTableProveedor.ajax.reload();
                        }
                        else if (data.state == 404) {
                            console.log(data);
                            Swal.fire(`Upss! ${data.message}`, '', 'info')
                            $('#modal-default').modal('hide');
                        }
                    },
                    error: function (error) {
                        if (error.status === 400) {
                            Swal.fire('Upss! Ocurrio Algo.', '', 'info')
                        }
                    }
                });
            }
            else if (result.isDenied) {
                Swal.fire('Cambios no Registrados', '', 'info')
                $('#modal-default').modal('hide');
            }
        })
    });

    // Ingresando Update
    TableProveedor.on("click", "#btnEditar", function () {
        let id = DataTableProveedor.row($(this).parents("tr")).data().pkProveedor;
        console.log(id);
        InvocarModal(id);
    });
    $(".modal-container").on("click", "#btnUpdate", function (e) {
        e.preventDefault();
        let Proveedor = {
            "PkProveedor": $("#PkProveedor").val(),
            "Ruc": $("#Ruc").val(),
            "Razonsocial": $("#Razonsocial").val(),
            "Direccion": $("#Direccion").val(),
            "Email": $("#Email").val(),
            "Telefono": $("#Telefono").val(),
        }

        Swal.fire({
            title: 'Desea actualizar a este Proveedor?',
            showDenyButton: true,
            confirmButtonText: 'Actualizar',
            denyButtonText: `Cancelar`,
            denyButtonClass: 'button-cancel'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: '/Proveedor/UpdateProveedor',
                    data: JSON.stringify(Proveedor),
                    type: 'POST',
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        console.log(data);
                        if (data.state == 200) {
                            console.log(data);
                            Swal.fire('Update!', '', 'success')
                            $('#modal-default').modal('hide');
                            DataTableProveedor.ajax.reload();
                        }
                        else if (data.state == 404) {
                            console.log(data);
                            Swal.fire(`Upss! ${data.message}`, '', 'info')
                            $('#modal-default').modal('hide');
                        }
                    },
                    error: function (error) {
                        if (error.status === 400) {
                            Swal.fire('Upss! Ocurrio Algo.', '', 'info')
                        }
                    }
                });
            }
            else if (result.isDenied) {
                Swal.fire('Cambios no actualizados', '', 'info')
                $('#modal-default').modal('hide');
            }
        })
    });
    // Desactivar un Proveedor.
    TableProveedor.on("click", "#btnEliminar", function () {
        let id = DataTableProveedor.row($(this).parents("tr")).data().pkProveedor;
        let ruc = DataTableProveedor.row($(this).parents("tr")).data().ruc;
        console.log(id); console.log(ruc);
        // Ajax consumir rest metodo delete Proveedor.
        Swal.fire({
            title: 'Estas Seguro?',
            text: `que desea eliminar a este Proveedor : ${ruc} `,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok, Eliminar esto!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: `/Proveedor/DesactiveProveedor`,
                    data: JSON.stringify(id),
                    type: 'POST',
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        console.log(data);
                        if (data.state == 200) {
                            Swal.fire(
                                'Desactivado!',
                                `El Proveedor ${ruc} a sido Desactivado.`,
                                'success'
                            )
                            DataTableProveedor.ajax.reload();
                        }
                    },
                    error: function (error) {
                        if (error.status === 400) {
                            Swal.fire('Upss! Ocurrio Algo.', '', 'info')
                        }
                    }
                });
            }
        });
    });
});