'use strict';

(function() {
    document.addEventListener("DOMContentLoaded", function() {
        // Inicializa el combo de laboratorios
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
    });
})();