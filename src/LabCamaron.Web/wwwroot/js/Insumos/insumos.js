'use strict';

let contador = 1;
let dataUnidadMedida;
(function() {
    document.addEventListener("DOMContentLoaded", function() {
        const skuInput = document.getElementById("Sku");

        skuInput.addEventListener("input", function() {
            this.value = this.value.replace(/[^a-zA-Z0-9]/g, '');
        });

        // Inicializa el combo de laboratorios
        const laboratorioSelectedId = $('#ComboLaboratorio').data('id');
        const laboratorioSelectedText = $('#ComboLaboratorio').data('text');
        const idLaboratorio = $('#IdLaboratorioEdit').val();

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

        if (idLaboratorio) {
            $.ajax({
                url: '/ComboLaboratorio/ListarLaboratoriosEdit',
                dataType: 'json',
                data: { idLaboratorio: idLaboratorio },
                success: function(data) {
                    if (data && data.length > 0) {
                        let laboratorio = data[0]; // Suponiendo que el servicio devuelve un array
                        let option = new Option(laboratorio.text, laboratorio.id, true, true);
                        $('#ComboLaboratorio').append(option).trigger('change');
                    }
                }
            });
        }

        // Inicializa el combo de Categoria
        const categoriaSelectedId = $('#ComboCategoria').data('id');
        const categoriaSelectedText = $('#ComboCategoria').data('text');
        const idCategoria = $('#IdCategoriaEdit').val();

        $('#ComboCategoria').select2({
            theme: "bootstrap-5",
            language: "es",
            allowClear: true,
            placeholder: 'Selecciona un elemento',
            ajax: {
                url: '/ComboCategoria/ListarCategoria',
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

        if (categoriaSelectedId && laboratorioSelectedText) {
            const preselectedOption = new Option(laboratorioSelectedText, categoriaSelectedId, true, true);
            $('#ComboCategoria').append(preselectedOption).trigger('change');
        }

        if (idCategoria) {
            $.ajax({
                url: '/ComboCategoria/ListarCategoriaEdit',
                dataType: 'json',
                data: { id: idCategoria },
                success: function(data) {
                    if (data && data.length > 0) {
                        let categoria = data[0]; // Suponiendo que el servicio devuelve un array
                        let option = new Option(categoria.text, categoria.id, true, true);
                        $('#ComboCategoria').append(option).trigger('change');
                    }
                }
            });
        }

        // Inicializa el combo de laboratorios
        const marcaSelectedId = $('#ComboMarca').data('id');
        const marcaSelectedText = $('#ComboMarca').data('text');
        const idMarca = $('#IdMarcaEdit').val();

        $('#ComboMarca').select2({
            theme: "bootstrap-5",
            language: "es",
            allowClear: true,
            placeholder: 'Selecciona un elemento',
            ajax: {
                url: '/ComboMarca/ListarMarca',
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

        if (marcaSelectedId && laboratorioSelectedText) {
            const preselectedOption = new Option(laboratorioSelectedText, marcaSelectedId, true, true);
            $('#ComboMarca').append(preselectedOption).trigger('change');
        }

        if (idMarca) {
            $.ajax({
                url: '/ComboMarca/ListarMarcaEdit',
                dataType: 'json',
                data: { id: idMarca },
                success: function(data) {
                    if (data && data.length > 0) {
                        let categoria = data[0]; // Suponiendo que el servicio devuelve un array
                        let option = new Option(categoria.text, categoria.id, true, true);
                        $('#ComboMarca').append(option).trigger('change');
                    }
                }
            });
        }

        const idUnidadMedida = $('#UnidadMedida').val();

        if ($('#ComboTipoUnidadMedida').length) {
            const selectedId = $('#ComboTipoUnidadMedida').data('id');
            const selectedText = $('#ComboTipoUnidadMedida').data('text');

            $('#ComboTipoUnidadMedida').select2({
                theme: "bootstrap-5",
                language: "es",
                allowClear: true,
                placeholder: 'Selecciona un elemento',
                ajax: {
                    url: '/ComboTipoUnidadMedida/ListarTiposUnidadMedida',
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
                $('#ComboTipoUnidadMedida').append(preselectedOption).trigger('change');
            }

            // Cambiar combo laboratorio
            $('#ComboTipoUnidadMedida').on('change', function() {
                const id = $(this).val(); // Obtener el id de la laboratorio seleccionada
                actualizarUnidadMedida(id, idUnidadMedida); // Llamar a la función para actualizar las moduloLaboratorios
            });
        }

        if (idUnidadMedida) {
            let tipoUnidadTemp = "";
            $.ajax({
                url: '/ComboTipoUnidadMedida/ListarUnidadesMedida',
                dataType: 'json',
                data: { codigoUnidadMedida: idUnidadMedida }
            }).then(function(data) {
                if (data && data.length > 0) {
                    let result = data[0]; // Suponiendo que el servicio devuelve un array
                    dataUnidadMedida = result;
                    tipoUnidadTemp = result.tipoUnidad;
                    return $.ajax({
                        url: '/ComboTipoUnidadMedida/ListarTiposUnidadMedida',
                        dataType: 'json',
                        data: { codigoTipoUnidad: result.tipoUnidad }
                    });
                }
            }).then(function(data) {
                if (data && data.length > 0) {
                    let result = data[0]; // Suponiendo que el servicio devuelve un array
                    let option = new Option(result.text, result.id, true, true);
                    $('#ComboTipoUnidadMedida').append(option).trigger('change');
                }
            });
        }

        // Función para actualizar las moduloLaboratorios al cambiar de laboratorio
        function actualizarUnidadMedida(tipoUnidad, idUnidadMedida) {
            // Limpiar el combo de moduloLaboratorios antes de cargar los nuevos valores
            $('#ComboUnidadMedida').empty().trigger('change');

            // Actualizar el combo de moduloLaboratorios con AJAX basado en la laboratorio seleccionada

            const selectedUnidadMedidaId = $('#ComboUnidadMedida').data('id');
            const selectedUnidadMedidaText = $('#ComboUnidadMedida').data('text');

            $('#ComboUnidadMedida').select2({
                theme: "bootstrap-5",
                language: "es",
                allowClear: true,
                placeholder: 'Selecciona un elemento',
                ajax: {
                    url: '/ComboTipoUnidadMedida/ListarUnidadesMedida',
                    dataType: 'json',
                    delay: 250,
                    data: function(params) {
                        return {
                            codigoTipoUnidad: tipoUnidad,
                            textoContiene: params.term,
                        };
                    },
                    processResults: function(data) {
                        return {
                            results: data
                        };
                    }
                }
            });

            if (dataUnidadMedida) {
                let option = new Option(dataUnidadMedida.text, dataUnidadMedida.id, true, true);
                $('#ComboUnidadMedida').append(option).trigger('change');
                dataUnidadMedida = null;
            }
            if (selectedUnidadMedidaId && selectedUnidadMedidaText) {
                const preselectedOption = new Option(selectedUnidadMedidaText, selectedUnidadMedidaId, true, true);
                $('#ComboUnidadMedida').append(preselectedOption).trigger('change');
            }
        }

        //Change

        // Cambiar combo laboratorio
        $('#ComboCategoria').on('change', function() {
            const id = $(this).val(); // Obtener el id de la laboratorio seleccionada
            if (isActualizar) {
                $('#IdCategoriaEdit').val(id);
            }
        });
        // Cambiar combo laboratorio
        $('#ComboMarca').on('change', function() {
            const id = $(this).val(); // Obtener el id de la laboratorio seleccionada
            if (isActualizar) {
                $('#IdMarcaEdit').val(id);
            }
        });
        // Cambiar combo laboratorio
        $('#ComboUnidadMedida').on('change', function() {
            const id = $(this).val(); // Obtener el id de la laboratorio seleccionada
            if (isActualizar) {
                $('#UnidadMedida').val(id);
            }
        });
    });
})();