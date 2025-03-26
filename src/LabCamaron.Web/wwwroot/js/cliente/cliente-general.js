'use strict';
let contador = 0;
var tablaDetalle;
let arrayDetalle = [];
(function() {
    document.addEventListener("DOMContentLoaded", function() {
        const inputTelefono = document.getElementById('Telefono');
        const inputCelular = document.getElementById('Celular');
        const inputCorreos = document.getElementById('Correos');
        const inputCedula = document.getElementById('Identificacion');
        const tipoIdentificacion = document.getElementById("TipoIdentificacion");

        function actualizarValidaciones() {
            ValidarIdentificacion(inputCedula);
        }

        // Ejecutar validación al cargar la página
        actualizarValidaciones();

        // Expresiones regulares para validar los campos
        const telRegex = /^(0[2-7])\d{7}$/; // Teléfono fijo (02-07 + 7 dígitos)
        const celRegex = /^(09)\d{8}$/; // Celular (09 + 8 dígitos)
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/; // Correo electrónico básico

        function validarTelefono() {
            const valor = inputTelefono.value.trim();
            if (valor === '' || telRegex.test(valor)) {
                inputTelefono.setCustomValidity('');
            } else {
                inputTelefono.setCustomValidity('Ingrese un número de teléfono válido.');
            }
            inputTelefono.reportValidity();
        }

        function validarCelular() {
            const valor = inputCelular.value.trim();
            if (valor === '' || celRegex.test(valor)) {
                inputCelular.setCustomValidity('');
            } else {
                inputCelular.setCustomValidity('Ingrese un número de celular válido.');
            }
            inputCelular.reportValidity();
        }

        function validarCorreos() {
            const textoCorreo = inputCorreos.value;
            debugger;
            const correos = textoCorreo.split(';').map(email => email.trim());
            const correosValidos = correos.every(email => emailRegex.test(email));

            if (correos.length === 0 || correos.some(email => email === '')) {
                inputCorreos.reportValidity();
            } else if (!correosValidos) {
                inputCorreos.setCustomValidity('Uno o más correos no tienen un formato válido.');
            } else {
                inputCorreos.setCustomValidity('');
            }
            inputCorreos.reportValidity();
        }

        function ValidarIdentificacion(ctrNumeroCedula) {
            if (tipoIdentificacion.value === "RUC") {
                ValidarRuc(ctrNumeroCedula);
            } else if (tipoIdentificacion.value === "CED") {
                ValidarNumeroCedula(ctrNumeroCedula);
            } else {
                ctrNumeroCedula.setCustomValidity("Tipo de identificación no reconocida");
                ctrNumeroCedula.reportValidity();
            }
        }
        function ValidarRuc(ctrNumeroCedula) {
            var ruc = ctrNumeroCedula.value.trim();

            const regex = /^[0-9]+$/;
            if (!regex.test(ruc)) {
                ctrNumeroCedula.setCustomValidity("Solo se permiten números");
                ctrNumeroCedula.reportValidity();
                return;
            }

            if (ruc.length !== 13) {
                ctrNumeroCedula.setCustomValidity("El ruc debe tener 13 dígitos");
                ctrNumeroCedula.reportValidity();
                return;
            }

            ctrNumeroCedula.setCustomValidity('');
            ctrNumeroCedula.reportValidity();
        }
        function ValidarNumeroCedula(ctrNumeroCedula) {
            var cedula = ctrNumeroCedula.value.trim();

            const regex = /^[0-9]+$/;
            if (!regex.test(cedula)) {
                ctrNumeroCedula.setCustomValidity("Solo se permiten números");
                ctrNumeroCedula.reportValidity();
                return;
            }

            if (cedula.length !== 10) {
                ctrNumeroCedula.setCustomValidity("La cédula debe tener 10 dígitos");
                ctrNumeroCedula.reportValidity();
                return;
            }

            var digito_region = parseInt(cedula.substring(0, 2));

            if (digito_region < 1 || digito_region > 24) {
                ctrNumeroCedula.setCustomValidity("No se ha ingresado un número de cédula válido");
                ctrNumeroCedula.reportValidity();
                return;
            }

            var ultimo_digito = parseInt(cedula.substring(9, 10));
            var pares = parseInt(cedula[1]) + parseInt(cedula[3]) + parseInt(cedula[5]) + parseInt(cedula[7]);

            var impares = 0;
            for (let i = 0; i < 9; i += 2) {
                let num = parseInt(cedula[i]) * 2;
                if (num > 9) num -= 9;
                impares += num;
            }

            var suma_total = pares + impares;
            var decena = Math.ceil(suma_total / 10) * 10;
            var digito_validador = decena - suma_total;

            if (digito_validador === 10) digito_validador = 0;

            if (digito_validador === ultimo_digito) {
                ctrNumeroCedula.setCustomValidity('');
            } else {
                ctrNumeroCedula.setCustomValidity("No se ha ingresado un número de cédula válido");
            }

            ctrNumeroCedula.reportValidity();
        }

        // Eventos de validación
        tipoIdentificacion.addEventListener("change", actualizarValidaciones);
        inputTelefono.addEventListener('input', validarTelefono);
        inputCelular.addEventListener('input', validarCelular);
        inputCorreos.addEventListener('input', validarCorreos);
        inputCedula.addEventListener('input', function() {
            ValidarIdentificacion(this);
        });

        // Métodos para la gestión del detalle
        document.getElementById("btnContacto").addEventListener("click", function() {
            let modal = new bootstrap.Modal(document.getElementById("modalDetalle"));
            modal.show();
        });

        // Manejo de los detalles
        if ($("#tbDetalles").length) {
            tablaDetalles = $("#tbDetalles").DataTable({
                columnDefs: [
                    { className: "text-start", targets: 0 },
                    { className: "text-center", targets: 1 },
                    { className: "text-start", targets: 2 },
                ],
                columns: [
                    { data: "orden" },
                    { data: "nombreContacto" },
                    { data: "acciones", orderable: false }
                ],
                ordering: false,
                autoWidth: false,
                info: false,
                searching: false,
                paging: false,
                language: {
                    decimal: "",
                    emptyTable: "No hay información",
                    loadingRecords: "Cargando...",
                    processing: "Procesando...",
                    zeroRecords: "Sin resultados encontrados",
                }
            });
        }

        function eliminarFila(button, orden) {
            tablaDetalles.row($(button).parents("tr")).remove().draw();
            arrayDetalle = arrayDetalle.filter(detalle => detalle.orden !== orden);


            let nombresUnidos = arrayDetalle.map(item => item.nombreContacto).join("|");
            $("#Contactos").val(nombresUnidos);
        }

        function editarFila(orden) {
            // Buscar el registro en arrayDetalle
            const detalle = arrayDetalle.find(item => item.orden === orden);
            if (!detalle) return;

            // Llenar los campos del modal con los valores existentes
            $("#nombreContacto").val(detalle.nombreContacto);

            // Guardar el índice para actualizarlo después
            $("#modalDetalle").data("ordenEditar", orden);

            // Mostrar el modal
            let modal = new bootstrap.Modal(document.getElementById("modalDetalle"));
            modal.show();
        }

        if (detalles) {
            contador = detalles.length;

            for (var i = 0; i < detalles.length; i++) {
                var detalle = detalles[i];
                let orden = i + 1;
                arrayDetalle.push({
                    orden: orden,
                    nombreContacto: detalle.nombreContacto,
                });

                tablaDetalles.row.add({
                    orden: `${orden}`,
                    nombreContacto: `${detalle.nombreContacto}`,
                    acciones: `<div class="d-flex gap-2">
                                    <a href="#" class="btn btn-sm btn-primary" onclick="editarFila(${orden})">Editar</a>
                                    <a href="#" class="btn btn-danger btn-sm" onclick="eliminarFila(this,${orden})">Eliminar</a>
                                </div>`
                }).draw();
            }
            let nombresUnidos = detalles.map(item => item.nombreContacto).join("|");
            $("#Contactos").val(nombresUnidos);
        }

        window.eliminarFila = eliminarFila;
        window.editarFila = editarFila;
    });
})();