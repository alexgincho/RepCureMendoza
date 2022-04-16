window.onload = function () {

    let ContentProducto = document.getElementById("content-productos-carrito");
    let tableBody = document.createElement("tbody");
    let ProductosList = [];

    // Obteneos los Productos Agregados 
    function GetCarritoCompras() {

        if (localStorage.getItem("DetalleCarrito")) {
            let CarritoJson = localStorage.getItem("DetalleCarrito");
            let JsonCarrito = JSON.parse(CarritoJson);

            for (let i = 0; i < JsonCarrito.length; i++) {
                ProductosList.push(JsonCarrito[i]);
            }
            console.log("Carrito tiene estos Productos : ", ProductosList)
        }
        else {
            console.log("Carrito Vacio")
        }
    };

    function LLenarTablaCarrito() {
        LimpiarData();
        ContentProducto.appendChild(tableBody);
        ProductosList.forEach((v, i) => {
            let row = document.createElement("tr");
            row.innerHTML = `
                <td> <input id='idproducto' type='hidden' value='${v.pkProducto}'  />  <strong> ${v.nombre} </strong>   </td>
                <td>
                     <button id='DesCant' value='0' class='btn btn-primary btn-sm Restar' style='width: 30px;'> - </button>
                     <input  name='cantidad[${i}]' id='cantidad' class='form-control form-control-sm' style='text-align: center; width: 30px; display: inline-block;' value='1' type='text' />
                     <button id='AunCant' value='1' class='btn btn-primary btn-sm Sumar' data-id='1' style='width: 30px;'> + </button>
                </td>
                <td> <input type='text'  id="precio" style='text-align: center; width: 80px; display: inline-block;' value='${v.precioventa}' /> </td>
                <td> <input type='text' name='subTotal[${i}]'  id="subTotal" style='text-align: center; width: 80px; display: inline-block;' /> </td>
                <td> <button class='btn btn-danger btn-sm Eliminar' data-id='${v.pkProducto}'> <i class="far fa-trash-alt"></i> </button>  </td>
            `;
            tableBody.appendChild(row);
        });

    }
    function EliminarDataCarrito(e) {
        e.preventDefault();
        if (e.target.classList.contains('Eliminar')) {

            const id = e.target.dataset.id;
            ProductosList.forEach((catalogo, index, objecto) => {
                if (catalogo.pkProducto == id) {
                    objecto.splice(index, 1);
                    localStorage.setItem("DetalleCarrito", JSON.stringify(ProductosList));
                    recargar();
                    LLenarTablaCarrito();
                    CalcularTotal();
                    ActualizarStock();
                }
            });

        }
    }
    function LimpiarData() {
        tableBody.innerHTML = '';
    }
    tableBody.addEventListener("click", EliminarDataCarrito);

    // Funciones
    GetCarritoCompras();
    LLenarTablaCarrito();

    let AuntCant = document.querySelectorAll("#AunCant");
    let DesCant = document.querySelectorAll("#DesCant");
    let cantidad = document.querySelectorAll("#cantidad");
    let subTotal = document.querySelectorAll("#subTotal");
    let precio = document.querySelectorAll("#precio");
    let totalPagar = document.getElementById("totalPagar");
    let idproducto = document.querySelectorAll("#idproducto");

    function Suma() {
        AuntCant.forEach((v, i) => {
            AuntCant[i].addEventListener("click", function (event, ite) {

                event.preventDefault();
                ite = i;
                let span = document.querySelector("#AunCant");
                let classes = span.classList;
                if (classes.contains('Sumar')) {
                    /*const id = span.target.dataset.id;*/
                    const id = span.value;
                    if (id == 1) {
                        cantidad.forEach((v, i) => {
                            if (ite == i) {
                                let valor = parseInt(cantidad[i].value);
                                let precios = parseFloat(precio[i].value);
                                let subtotals = parseFloat(subTotal[i].value);

                                valor = parseInt(valor) + 1;
                                cantidad[i].value = valor;

                                subTotal[i].value = precios * cantidad[i].value;

                                CalcularTotal();
                            }
                        });
                    }

                }
            })
        });
    }
    DesCant.forEach((v, i) => {
        DesCant[i].addEventListener("click", function (event, ite) {

            event.preventDefault();
            ite = i;
            let span = document.querySelector("#DesCant");
            let classes = span.classList;
            if (classes.contains('Restar')) {
                /*const id = span.target.dataset.id;*/
                const id = span.value;
                if (id == 0) {
                    cantidad.forEach((v, i) => {
                        if (ite == i) {
                            let valor = parseInt(cantidad[i].value);
                            let precios = parseFloat(precio[i].value);
                            let subtotals = parseFloat(subTotal[i].value);
                            valor = parseInt(valor) - 1;                         
                            cantidad[i].value = valor;

                            subTotal[i].value = precios * cantidad[i].value;

                            CalcularTotal();
                        }
                    });
                }

            }
        })
    });

    // Calcular Subtotal
    function CalcularTotal() {
        let suma = 0;
        subTotal.forEach((v, i) => {
            if (subTotal[i].value == "") {
                subTotal[i].value = 0;
            }
            suma = parseFloat(suma) + parseFloat(subTotal[i].value);
            totalPagar.textContent = suma;
        });
    }
    function ActualizarStock() {
        if (localStorage.getItem("DetalleCarrito")) {
            let CarritoJson = localStorage.getItem("DetalleCarrito");
            let JsonCarrito = JSON.parse(CarritoJson);
            let ArrayCard = [];
            for (let i = 0; i < JsonCarrito.length; i++) {
                ArrayCard.push(JsonCarrito[i]);
            }
            $(".contador-carrito").text(ArrayCard.length);
            /*            $('#toast-carrito').toast('show');*/
            console.log(ArrayCard)

        }
        else {
            console.log("Carrito Vacio")
        }
    }
    function recargar() {
        document.location.reload();
    }
    Suma();

    let btnProcesarPago = document.getElementById("btnProcesarPago");


    btnProcesarPago.addEventListener("click", function (e) {
        e.preventDefault();

        let CarritoCompra = {
            Codigo: "",
            Total: totalPagar.textContent,
            Detalles: [],
            Metodo: {
                Propietario: $("#trj_nombre").val(),
                Numeroccv: $("#trj_cvv").val(),
                Numerotarjeta: $("#trj_numero").val(),
                Tipotarjeta: $("#card").val()
            }
        }
        let Detalle =  {
            pkProducto: 0,
            PrecioUnitario: 0,
            Cantidad: 0,
            SubTotal:0
        }
        // Recuperamos Datos de la Tabla

        var myRows = [];
        var $headers = $("th");
        var $rows = $("tbody tr").each(function (index) {
            $cells = $(this).find("td");
            myRows[index] = {};
            $cells.each(function (cellIndex) {
                myRows[index][$($headers[cellIndex]).html()] = $(this).html();
            });
        });

        myRows.forEach((v, i) => {
            Detalle = new Object();
            if ($(v.Descripcion).val()) {
                console.log(idproducto[i].value)
                Detalle.pkProducto = idproducto[i].value;
            }

            if ($(v.Precio).val()) {
                console.log(precio[i].value);
                Detalle.PrecioUnitario = precio[i].value;
            }
            if ($(v.Cantidad).val()) {
                console.log(cantidad[i].value)
                Detalle.Cantidad = cantidad[i].value;
            }
            CarritoCompra.Detalles.push(Detalle);
        });

        console.log(CarritoCompra)
        let url = "/Home/RegistrarCarrito"
        let config = {
            method: 'POST',
            body: JSON.stringify(CarritoCompra),
            headers: { 'Content-Type': 'application/json' },
            dataType: "json",
        }

        fetch(url, config)
            .then(response => {
                console.log(response);
                if (response.status == 400) {
                    Swal.fire({
                        title: 'Inicia Sesion',
                        showClass: {
                            popup: 'animate__animated animate__fadeInDown'
                        },
                        hideClass: {
                            popup: 'animate__animated animate__fadeOutUp'
                        }
                    })
                }
                else {
                    Swal.fire('Compra Exitosa!', '', 'success')
                    localStorage.removeItem('DetalleCarrito');
                    setTimeout(function () { recargar(); }, 3000);
                   
                }
            })
            .catch(error => {

                console.log(error)
            });

        //Swal.fire({
        //    title: 'Deseas Realizar esta Compra?',
        //    showDenyButton: true,
        //    showCancelButton: true,
        //    confirmButtonText: 'Si',
        //}).then((result) => {
        //    /* Read more about isConfirmed, isDenied below */
        //    if (result.isConfirmed) {

                
                
        //    } else if (result.isDenied) {

        //        Swal.fire('Esperamos tu Compra', '', 'info')
        //    }
        //})

       

    });

};