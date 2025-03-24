'use strict';

(function() {
    document.addEventListener("DOMContentLoaded", function() {
        // Inicializa el combo de empresas

        if ($('#ComboEmpresa').length) {
            const selectedId = $('#ComboEmpresa').data('id');
            const selectedText = $('#ComboEmpresa').data('text');

            $('#ComboEmpresa').select2({
                theme: "bootstrap-5",
                language: "es",
                allowClear: true,
                placeholder: 'Selecciona un elemento',
                ajax: {
                    url: '/Laboratorio/ListarEmpresas',
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

            if (selectedId && selectedText) {
                const preselectedOption = new Option(selectedText, selectedId, true, true);
                $('#ComboEmpresa').append(preselectedOption).trigger('change');
            }
        }

        // Inicializa el combo de provincias
        const provinciaSelectedId = $('#ComboProvincia').data('id');
        const provinciaSelectedText = $('#ComboProvincia').data('text');

        $('#ComboProvincia').select2({
            theme: "bootstrap-5",
            language: "es",
            allowClear: true,
            placeholder: 'Selecciona un elemento',
            ajax: {
                url: '/ComboProvincia/ListarProvincias',
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

        if (provinciaSelectedId && provinciaSelectedText) {
            const preselectedOption = new Option(provinciaSelectedText, provinciaSelectedId, true, true);
            $('#ComboProvincia').append(preselectedOption).trigger('change');
        }

        // Cambiar combo provincia
        $('#ComboProvincia').on('change', function() {
            const provinciaId = $(this).val(); // Obtener el id de la provincia seleccionada
            actualizarCiudades(provinciaId); // Llamar a la función para actualizar las ciudades
        });

        // Inicializar el combo de ciudades
        const ciudadSelectedId = $('#ComboCiudad').data('id');
        const ciudadSelectedText = $('#ComboCiudad').data('text');

        $('#ComboCiudad').select2({
            theme: "bootstrap-5",
            language: "es",
            allowClear: true,
            placeholder: 'Selecciona un elemento',
            ajax: {
                url: '/ComboCiudad/ListarCiudades',
                dataType: 'json',
                delay: 250,
                data: function(params) {
                    return {
                        idProvincia: provinciaSelectedId, // Usar el id de provincia inicial
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

        if (ciudadSelectedId && ciudadSelectedText) {
            const preselectedOption = new Option(ciudadSelectedText, ciudadSelectedId, true, true);
            $('#ComboCiudad').append(preselectedOption).trigger('change');
        }

        // Función para actualizar las ciudades al cambiar de provincia
        function actualizarCiudades(provinciaId) {
            // Limpiar el combo de ciudades antes de cargar los nuevos valores
            $('#ComboCiudad').empty().trigger('change');

            // Actualizar el combo de ciudades con AJAX basado en la provincia seleccionada
            $('#ComboCiudad').select2('destroy').select2({
                theme: "bootstrap-5",
                language: "es",
                allowClear: true,
                placeholder: 'Selecciona un elemento',
                ajax: {
                    url: '/ComboCiudad/ListarCiudades',
                    dataType: 'json',
                    delay: 250,
                    data: function(params) {
                        return {
                            idProvincia: provinciaId, // Pasar la nueva provincia seleccionada
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
    });
})();