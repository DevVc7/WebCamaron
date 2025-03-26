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


        // Preparar el combo de laboratorio
        const selectedId = $('#ComboLaboratorio').data('id');
        const selectedText = $('#ComboLaboratorio').data('text');

        // El combo tiene datos
        if (selectedId) {
            $('#ComboLaboratorio').select2({
                theme: "bootstrap-5",
                language: "es",
                allowClear: true,
                placeholder: 'Selecciona un elemento',
                ajax: {
                    url: '/ComboLaboratorio/ListarLaboratorios',
                    dataType: 'json',
                    delay: 250,
                    data: function(params) {
                        return {
                            textoContiene: params.term
                        };
                    },
                    processResults: function(data) {
                        return {
                            results: data
                        };
                    }
                }
            });
        } else {
            $.ajax({
                url: '/ComboLaboratorio/ListarLaboratorios', // Tu API que devuelve los laboratorios
                dataType: 'json',
                success: function(data) {
                    let $select = $('#ComboLaboratorio');
                    $select.empty().append('<option value="">Selecciona un laboratorio</option>');

                    data.forEach(item => {
                        let newOption = new Option(item.text, item.id, false, false);
                        $select.append(newOption);
                    });

                    $select.select2({
                        theme: "bootstrap-5",
                        language: "es",
                        allowClear: true,
                        placeholder: 'Selecciona un laboratorio'
                    });

                    // Seleccionar automáticamente el primer elemento después de cargar los datos
                    if (data.length > 0) {
                        $select.val(data[0].id).trigger('change');
                    }
                },
                error: function() {
                    toastDangerShow("Error al cargar los datos del laboratorio.");
                }
            });
        }

        // Si hay valores preseleccionados, añádelos y selecciónalos
        if (selectedId && selectedText) {
            const preselectedOption = new Option(selectedText, selectedId, true, true);
            $('#ComboLaboratorio').append(preselectedOption).trigger('change');
        }

        // Cambiar combo laboratorio
        $('#ComboLaboratorio').on('change', function() {
            const laboratorioId = $(this).val(); // Obtener el id de la laboratorio seleccionada
            actualizarModuloLaboratorios(laboratorioId); // Llamar a la función para actualizar las moduloLaboratorios
        });

        // Inicializar el combo de moduloLaboratorios
        const moduloLaboratorioSelectedId = String($('#ComboModulo').data('id'));
        const moduloLaboratorioSelectedText = $('#ComboModulo').data('text');

        $('#ComboModulo').select2({
            theme: "bootstrap-5",
            language: "es",
            allowClear: true,
            multiple: true,
            placeholder: 'Selecciona un elemento',
            ajax: {
                url: '/ComboModuloLaboratorio/ListarModuloLaboratorios',
                dataType: 'json',
                delay: 250,
                data: function(params) {
                    return {
                        idLaboratorio: selectedId, // Usar el id de laboratorio inicial
                        textoContiene: params.term
                    };
                },
                processResults: function(data) {
                    return {
                        results: data
                    };
                }
            }
        });

        if (moduloLaboratorioSelectedId && moduloLaboratorioSelectedText) {
            const selectedIds = moduloLaboratorioSelectedId.split(','); // Manejo de múltiples valores
            const selectedTexts = moduloLaboratorioSelectedText.split(',');

            selectedIds.forEach((id, index) => {
                const option = new Option(selectedTexts[index], id, true, true);
                $('#ComboModulo').append(option);
            });

            $('#ComboModulo').trigger('change');
        }

        // Función para actualizar las moduloLaboratorios al cambiar de laboratorio
        function actualizarModuloLaboratorios(laboratorioId) {
            // Limpiar el combo de moduloLaboratorios antes de cargar los nuevos valores
            $('#ComboModulo').empty().trigger('change');

            // Actualizar el combo de moduloLaboratorios con AJAX basado en la laboratorio seleccionada
            $('#ComboModulo').select2('destroy').select2({
                theme: "bootstrap-5",
                language: "es",
                allowClear: true,
                multiple: true,
                placeholder: 'Selecciona un elemento',
                ajax: {
                    url: '/ComboModuloLaboratorio/ListarModuloLaboratorios',
                    dataType: 'json',
                    delay: 250,
                    data: function(params) {
                        return {
                            idLaboratorio: laboratorioId, // Pasar la nueva laboratorio seleccionada
                            textoContiene: params.term
                        };
                    },
                    processResults: function(data) {
                        return {
                            results: data
                        };
                    }
                }
            });
        }

        const funcionTecnicoSelectedId = String($('#ComboFuncionTecnico').data('id'));
        const funcionTecnicoSelectedText = $('#ComboFuncionTecnico').data('text');

        $('#ComboFuncionTecnico').select2({
            theme: "bootstrap-5",
            language: "es",
            allowClear: true,
            multiple: true,
            placeholder: 'Selecciona un elemento',
            ajax: {
                url: '/ComboFuncionTecnico/ListarFuncionTecnicos',
                dataType: 'json',
                delay: 250,
                data: function(params) {
                    return {
                        textoContiene: params.term
                    };
                },
                processResults: function(data) {
                    return {
                        results: data
                    };
                }
            }
        });

        if (funcionTecnicoSelectedId && funcionTecnicoSelectedText) {
            const selectedIds = funcionTecnicoSelectedId.split(','); // Manejo de múltiples valores
            const selectedTexts = funcionTecnicoSelectedText.split(',');

            selectedIds.forEach((id, index) => {
                const option = new Option(selectedTexts[index], id, true, true);
                $('#ComboFuncionTecnico').append(option);
            });

            $('#ComboFuncionTecnico').trigger('change');
        }
    });
})();