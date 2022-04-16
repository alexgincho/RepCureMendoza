$(document).ready(function () {

    let TablePersonal = $("#TablePersonal");
    let btnAddPersonal = $("#btnAddPersonal");

    btnAddPersonal.on("click", function (e) {
        e.preventDefault();
        InvocarModal();
    });
    // Funcion Invocar un Modal
    function InvocarModal(id) {
        AbrirModal(`/Personal/MantenimientoPersonal/${id ? id : ""}`);
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
    // Listado de Personal Consumiendo Datataables
    let DataTablePersonal = TablePersonal.DataTable({
        scrollY: 200,
        scrollX: true,
        paging: false,
        ordering:false,
        ajax: {
            url: '/Personal/GetAllPersonal',
        },
        columnDefs: [
            { targets: 0, width: 100},
            { targets: 1, width: 110},
            { targets: 2, width: 180 },
            { targets: 3, width: 180 },
            { targets: 4, width: 210 },
            { targets: 5, width: 100 },
            { targets: 6, width: 210 },
            { targets: 7, width: 150 },
            { targets: 8, width: 100 },
            { targets: 9, width: 110 }
        ],
        columns: [
            { data: "dni", title:"Dni" },
            { data: "nombre", title: "Nombre" },
            { data: "apellidopaterno", title: "Apellido Paterno" },
            { data: "apellidomaterno", title: "Apellido Materno" },
            { data: "direccion", title: "Direccion" },
            { data: "telefono", title:"Celular" },
            { data: "email", title: "Email" },
            { data: "fechacrea", title: "Fecha de Ingreso" },
            { data: "fkRol", title: "Cargo" },
            { data: "usuario", title:"Usuario Login" },
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
    // Agregar Personal Administrativo
    $(".modal-container").on("click", "#btnSave", function (e) {
        e.preventDefault();
        let Personal = {

            "Dni": $("#Dni").val(),
            "Nombre": $("#Nombre").val(),
            "Apellidopaterno": $("#Apellidopaterno").val(),
            "Apellidomaterno": $("#Apellidomaterno").val(),
            "Telefono": $("#Telefono").val(),
            "Direccion": $("#Direccion").val(),
            "Email": $("#Email").val(),
            "FkRol": $("#FkRol").val(),
            "Usuario": $("#Usuario").val(),
            "Passwords": $("#Passwords").val()
        }

        Swal.fire({
            title: 'Desea Registrar a este Personal?',
            showDenyButton: true,
            confirmButtonText: 'Registrar',
            denyButtonText: `Cancelar`,
            denyButtonClass: 'button-cancel'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: '/Personal/CreatePersonal',
                    data: JSON.stringify(Personal),
                    type: 'POST',
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        console.log(data);
                        if (data.state == 200) {
                            console.log(data);
                            Swal.fire('Saved!', '', 'success')
                            $('#modal-default').modal('hide');
                             DataTablePersonal.ajax.reload();
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
        TablePersonal.on("click", "#btnEditar", function () {
            let id = DataTablePersonal.row($(this).parents("tr")).data().pkPersonal;
            console.log(id);
            InvocarModal(id); 
        });
        $(".modal-container").on("click", "#btnUpdate", function (e) {
            e.preventDefault();
            let Personal = {
                "PkPersonal": $("#PkPersonal").val(),
                "Dni": $("#Dni").val(),
                "Nombre": $("#Nombre").val(),
                "Apellidopaterno": $("#Apellidopaterno").val(),
                "Apellidomaterno": $("#Apellidomaterno").val(),
                "Telefono": $("#Telefono").val(),
                "Direccion": $("#Direccion").val(),
                "Email": $("#Email").val(),
                "FkRol": $("#FkRol").val(),
                "Usuario": $("#Usuario").val(),
                "Passwords": $("#Passwords").val()
            }
    
            Swal.fire({
                title: 'Desea actualizar a este Personal?',
                showDenyButton: true,
                confirmButtonText: 'Actualizar',
                denyButtonText: `Cancelar`,
                denyButtonClass: 'button-cancel'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: '/Personal/UpdatePersonal',
                        data: JSON.stringify(Personal),
                        type: 'POST',
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            console.log(data);
                            if (data.state == 200) {
                                console.log(data);
                                Swal.fire('Update!', '', 'success')
                                $('#modal-default').modal('hide');
                                 DataTablePersonal.ajax.reload();
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
    // Desactivar un Personal.
    TablePersonal.on("click", "#btnEliminar", function () {
        let id = DataTablePersonal.row($(this).parents("tr")).data().pkPersonal;
        let nombre = DataTablePersonal.row($(this).parents("tr")).data().nombre;
        console.log(id); console.log(nombre);
        // Ajax consumir rest metodo delete personal.
        Swal.fire({
            title: 'Estas Seguro?',
            text: `que desea eliminar a este personal : ${nombre} `,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok, Eliminar esto!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: `/Personal/DesactivePersonal`,
                    data: JSON.stringify(id),                 
                    type: 'POST',
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        console.log(data);
                        if (data.state == 200) {
                            Swal.fire(
                                'Desactivado!',
                                `El Personal ${nombre} a sido Desactivado.`,
                                'success'
                            )
                            DataTablePersonal.ajax.reload();
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