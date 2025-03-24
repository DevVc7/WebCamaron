'use strict';

(function() {
    document.addEventListener('DOMContentLoaded', function() {
        if ($('#ComboEntePersonal').length) {
            const selectedId = $('#ComboEntePersonal').data('id');
            const selectedText = $('#ComboEntePersonal').data('text');

            $('#ComboEntePersonal').select2({
                theme: "bootstrap-5",
                language: "es",
                allowClear: true,
                placeholder: 'Selecciona un elemento',
                ajax: {
                    url: '/ComboEnte/ListarEnteSinRol',
                    dataType: 'json',
                    delay: 250,
                    data: function(params) {
                        return {
                            textoContiene: params.term,
                            rolExcluir: 'PERS'
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
                $('#ComboEntePersonal').append(preselectedOption).trigger('change');
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
    });
})();