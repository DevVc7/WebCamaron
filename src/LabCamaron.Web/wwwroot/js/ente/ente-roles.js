'use strict';

(function() {
    document.addEventListener('DOMContentLoaded', function() {
        if ($('#FormularioVendedor').length) {
            // Transacciones del vendedor
            const checkVendedorActivo = document.getElementById('ActivoVendedor');
            const inputVendedorCodigo = document.getElementById('CodigoVendedor');
            const labelVendedor = document.querySelector('label[for="CodigoVendedor"]');

            // Función para manejar el cambio de estado
            function handleVendedorEstadoActivo() {
                // Si el checkbox está marcado
                if (checkVendedorActivo.checked) {
                    inputVendedorCodigo.removeAttribute('readonly');
                    inputVendedorCodigo.setAttribute('required', '');
                    labelVendedor.classList.add('required');
                } else {
                    inputVendedorCodigo.setAttribute('readonly', '');
                    inputVendedorCodigo.removeAttribute('required');
                    labelVendedor.classList.remove('required');
                }
            }

            // Ejecutar la función al cargar para establecer el estado inicial
            handleVendedorEstadoActivo();

            // Agregar el listener para el evento change del checkbox
            checkVendedorActivo.addEventListener('change', handleVendedorEstadoActivo);
        }

        if ($('#FormularioCliente').length) {
            // Transacciones del vendedor
            const checkClienteActivo = document.getElementById('ActivoCliente');
            const inputClienteCodigo = document.getElementById('CodigoCliente');
            const inputClienteContacto = document.getElementById('ContactoCliente');
            const labelCliente = document.querySelector('label[for="CodigoCliente"]');

            // Función para manejar el cambio de estado
            function handleClienteEstadoActivo() {
                // Si el checkbox está marcado
                if (checkClienteActivo.checked) {
                    inputClienteCodigo.removeAttribute('readonly');
                    inputClienteCodigo.setAttribute('required', '');
                    labelCliente.classList.add('required');

                    inputClienteContacto.removeAttribute('readonly');
                } else {
                    inputClienteCodigo.setAttribute('readonly', '');
                    inputClienteCodigo.removeAttribute('required');
                    labelCliente.classList.remove('required');

                    inputClienteContacto.setAttribute('readonly', '');
                }
            }

            // Ejecutar la función al cargar para establecer el estado inicial
            handleClienteEstadoActivo();

            // Agregar el listener para el evento change del checkbox
            checkClienteActivo.addEventListener('change', handleClienteEstadoActivo);
        }

        if ($('#FormularioTecnico').length) {
            // Transacciones del vendedor
            const labelTecnico = document.querySelector('label[for="CodigoTecnico"]');
            const inputTecnicoCodigo = document.getElementById('CodigoTecnico');
            const checkTecnicoActivo = document.getElementById('ActivoTecnico');

            // Función para manejar el cambio de estado
            function handleTecnicoEstadoActivo() {
                // Si el checkbox está marcado
                if (checkTecnicoActivo.checked) {
                    inputTecnicoCodigo.removeAttribute('readonly');
                    inputTecnicoCodigo.setAttribute('required', '');
                    labelTecnico.classList.add('required');

                    $('#ComboFuncionTecnico').removeAttr('readonly');
                    $('#ComboLaboratorio').removeAttr('readonly');
                    $('#ComboModulo').removeAttr('readonly');
                } else {
                    inputTecnicoCodigo.setAttribute('readonly', '');
                    inputTecnicoCodigo.removeAttribute('required');
                    labelTecnico.classList.remove('required');

                    $('#ComboFuncionTecnico').attr('readonly', true);
                    $('#ComboLaboratorio').attr('readonly', true);
                    $('#ComboModulo').attr('readonly', true);
                }
            }

            // Manejo de combos
            const laboratorioSelectedId = $('#ComboLaboratorio').data('id');
            const laboratorioSelectedText = $('#ComboLaboratorio').data('text');

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

            if (laboratorioSelectedId && laboratorioSelectedText) {
                const preselectedOption = new Option(laboratorioSelectedText, laboratorioSelectedId, true, true);
                $('#ComboLaboratorio').append(preselectedOption).trigger('change');
            }

            // Cambiar combo laboratorio
            $('#ComboLaboratorio').on('change', function() {
                const laboratorioId = $(this).val(); // Obtener el id de la laboratorio seleccionada
                actualizarModuloLaboratorios(laboratorioId); // Llamar a la función para actualizar las moduloLaboratorios
            });

            // Inicializar el combo de moduloLaboratorios
            const moduloLaboratorioSelectedId = $('#ComboModulo').data('id');
            const moduloLaboratorioSelectedText = $('#ComboModulo').data('text');

            $('#ComboModulo').select2({
                theme: "bootstrap-5",
                language: "es",
                allowClear: true,
                placeholder: 'Selecciona un elemento',
                ajax: {
                    url: '/ComboModuloLaboratorio/ListarModuloLaboratorios',
                    dataType: 'json',
                    delay: 250,
                    data: function(params) {
                        return {
                            idLaboratorio: laboratorioSelectedId, // Usar el id de laboratorio inicial
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
                const preselectedOption = new Option(moduloLaboratorioSelectedText, moduloLaboratorioSelectedId, true, true);
                $('#ComboModulo').append(preselectedOption).trigger('change');
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

            const funcionTecnicoSelectedId = $('#ComboFuncionTecnico').data('id');
            const funcionTecnicoSelectedText = $('#ComboFuncionTecnico').data('text');

            $('#ComboFuncionTecnico').select2({
                theme: "bootstrap-5",
                language: "es",
                allowClear: true,
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
                const preselectedOption = new Option(funcionTecnicoSelectedText, funcionTecnicoSelectedId, true, true);
                $('#ComboFuncionTecnico').append(preselectedOption).trigger('change');
            }

            // Ejecutar la función al cargar para establecer el estado inicial
            handleTecnicoEstadoActivo();

            // Agregar el listener para el evento change del checkbox
            checkTecnicoActivo.addEventListener('change', handleTecnicoEstadoActivo);
        }

        if ($('#FormularioPersonal').length) {
            // Transacciones del vendedor
            const labelPersonal = document.querySelector('label[for="CodigoPersonal"]');
            const inputPersonalCodigo = document.getElementById('CodigoPersonal');
            const checkPersonalActivo = document.getElementById('ActivoPersonal');

            // Función para manejar el cambio de estado
            function handlePersonalEstadoActivo() {
                // Si el checkbox está marcado
                if (checkPersonalActivo.checked) {
                    inputPersonalCodigo.removeAttribute('readonly');
                    inputPersonalCodigo.setAttribute('required', '');
                    labelPersonal.classList.add('required');

                    $('#ComboCargo').removeAttr('readonly');
                    $('#ComboLaboratorioPersonal').removeAttr('readonly');
                    $('#ComboModuloPersonal').removeAttr('readonly');
                } else {
                    inputPersonalCodigo.setAttribute('readonly', '');
                    inputPersonalCodigo.removeAttribute('required');
                    labelPersonal.classList.remove('required');

                    $('#ComboCargo').attr('readonly', true);
                    $('#ComboLaboratorioPersonal').attr('readonly', true);
                    $('#ComboModuloPersonal').attr('readonly', true);
                }
            }

            // Manejo de combos
            const laboratorioSelectedId = $('#ComboLaboratorioPersonal').data('id');
            const laboratorioSelectedText = $('#ComboLaboratorioPersonal').data('text');

            $('#ComboLaboratorioPersonal').select2({
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

            if (laboratorioSelectedId && laboratorioSelectedText) {
                const preselectedOption = new Option(laboratorioSelectedText, laboratorioSelectedId, true, true);
                $('#ComboLaboratorioPersonal').append(preselectedOption).trigger('change');
            }

            // Cambiar combo laboratorio
            $('#ComboLaboratorioPersonal').on('change', function() {
                const laboratorioId = $(this).val(); // Obtener el id de la laboratorio seleccionada
                actualizarModuloLaboratoriosPersonal(laboratorioId); // Llamar a la función para actualizar las moduloLaboratorios
            });

            // Inicializar el combo de moduloLaboratorios
            const moduloLaboratorioSelectedId = $('#ComboModuloPersonal').data('id');
            const moduloLaboratorioSelectedText = $('#ComboModuloPersonal').data('text');

            $('#ComboModuloPersonal').select2({
                theme: "bootstrap-5",
                language: "es",
                allowClear: true,
                placeholder: 'Selecciona un elemento',
                ajax: {
                    url: '/ComboModuloLaboratorio/ListarModuloLaboratorios',
                    dataType: 'json',
                    delay: 250,
                    data: function(params) {
                        return {
                            idLaboratorio: laboratorioSelectedId, // Usar el id de laboratorio inicial
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
                const preselectedOption = new Option(moduloLaboratorioSelectedText, moduloLaboratorioSelectedId, true, true);
                $('#ComboModuloPersonal').append(preselectedOption).trigger('change');
            }

            // Función para actualizar las moduloLaboratorios al cambiar de laboratorio
            function actualizarModuloLaboratoriosPersonal(laboratorioId) {
                // Limpiar el combo de moduloLaboratorios antes de cargar los nuevos valores
                $('#ComboModuloPersonal').empty().trigger('change');

                // Actualizar el combo de moduloLaboratorios con AJAX basado en la laboratorio seleccionada
                $('#ComboModuloPersonal').select2('destroy').select2({
                    theme: "bootstrap-5",
                    language: "es",
                    allowClear: true,
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

            const funcionPersonalSelectedId = $('#ComboCargo').data('id');
            const funcionPersonalSelectedText = $('#ComboCargo').data('text');

            $('#ComboCargo').select2({
                theme: "bootstrap-5",
                language: "es",
                allowClear: true,
                placeholder: 'Selecciona un elemento',
                ajax: {
                    url: '/ComboCargo/ListarCargos',
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

            if (funcionPersonalSelectedId && funcionPersonalSelectedText) {
                const preselectedOption = new Option(funcionPersonalSelectedText, funcionPersonalSelectedId, true, true);
                $('#ComboCargo').append(preselectedOption).trigger('change');
            }

            // Ejecutar la función al cargar para establecer el estado inicial
            handlePersonalEstadoActivo();

            // Agregar el listener para el evento change del checkbox
            checkPersonalActivo.addEventListener('change', handlePersonalEstadoActivo);
        }
    });
})();