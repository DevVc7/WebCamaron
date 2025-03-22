'use strict';
let contador = 1;
var tablaDetalle;
let arrayDetalle = [];
(function() {
    document.addEventListener('DOMContentLoaded', function() {
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

        // Inicializar el combo de combos clientes
        const comboClienteSelectedId = $('#ComboCliente').data('id');
        const comboClienteSelectedText = $('#ComboCliente').data('text');

        $('#ComboCliente').select2({
            theme: "bootstrap-5",
            language: "es",
            allowClear: true,
            placeholder: 'Selecciona un elemento',
            ajax: {
                url: '/ComboEnte/ListarEntesClientes',
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

        if (comboClienteSelectedId && comboClienteSelectedText) {
            const preselectedOption = new Option(comboClienteSelectedText, comboClienteSelectedId, true, true);
            $('#ComboCliente').append(preselectedOption).trigger('change');
        }

        // Inicializar el combo de combos clientes
        const comboVendedorSelectedId = $('#ComboVendedor').data('id');
        const comboVendedorSelectedText = $('#ComboVendedor').data('text');

        $('#ComboVendedor').select2({
            theme: "bootstrap-5",
            language: "es",
            allowClear: true,
            placeholder: 'Selecciona un elemento',
            ajax: {
                url: '/ComboEnte/ListarEntesVendedor',
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

        if (comboVendedorSelectedId && comboVendedorSelectedText) {
            const preselectedOption = new Option(comboVendedorSelectedText, comboVendedorSelectedId, true, true);
            $('#ComboVendedor').append(preselectedOption).trigger('change');
        }

        // Inicializar el combo de combos planificacion siembra
        const comboPlanificacionSiembraSelectedId = $('#ComboPlanificacionSiembra').data('id');
        const comboPlanificacionSiembraSelectedText = $('#ComboPlanificacionSiembra').data('text');

        $('#ComboPlanificacionSiembra').select2({
            theme: "bootstrap-5",
            language: "es",
            allowClear: true,
            placeholder: 'Selecciona un elemento',
            ajax: {
                url: '/ComboPlanificacionSiembra/ListarPlanificacionSiembra',
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

        if (comboPlanificacionSiembraSelectedId && comboPlanificacionSiembraSelectedText) {
            const preselectedOption = new Option(comboPlanificacionSiembraSelectedText, comboPlanificacionSiembraSelectedId, true, true);
            $('#ComboPlanificacionSiembra').append(preselectedOption).trigger('change');
        }


        // Métodos para la gestión del detalle
        document.getElementById("btnAgregarLote").addEventListener("click", function() {
            let moduloSeleccionado = document.getElementById("ComboModulo").value;
            if (!moduloSeleccionado) {
                toastDangerShow('Se debe seleccionar un modulo');
            } else {
                // Si hay un vendedor seleccionado, abre la modal manualmente
                let modal = new bootstrap.Modal(document.getElementById("modalDetalle"));
                modal.show();
            }
        });


        // Manejo de los detalles
        if ($("#tbDetalles").length) {
            tablaDetalles = $("#tbDetalles").DataTable({
                columnDefs: [
                    { className: "text-start", targets: 0 },
                    { className: "text-center", targets: 1 },
                    { className: "text-start", targets: 2 },
                    { className: "text-start", targets: 3 },
                    { className: "text-start", targets: 4 },
                    { className: "text-start", targets: 5 },
                    { className: "text-start", targets: 6 },
                    { className: "text-start", targets: 7 },
                    { className: "text-start", targets: 8 },
                    { className: "text-start", targets: 9 },
                    { className: "text-start", targets: 10 },
                ],
                columns: [
                    { data: "orden" },
                    { data: "numeroLote" },
                    { data: "nombreSector" },
                    { data: "nombrePrecria" },
                    { data: "nombresTanques" },
                    { data: "salinidad" },
                    { data: "temperatura" },
                    { data: "plGramoRequerido" },
                    { data: "numeroTinas" },
                    { data: "numeroCamiones" },
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
        }

        function editarFila(orden) {
            // Buscar el registro en arrayDetalle
            const detalle = arrayDetalle.find(item => item.orden === orden);
            if (!detalle) return;

            // Llenar los campos del modal con los valores existentes
            $("#Lote").val(detalle.lote);
            $("#Sector").val(detalle.sector);
            $("#Longitud").val(detalle.longitud);
            $("#Latitud").val(detalle.latitud);
            $("#Contacto").val(detalle.contacto);
            $("#Precria").val(detalle.precria);
            $("#PlGramoRequerido").val(detalle.plGramosRequerido);
            $("#Salinidad").val(detalle.salinidad);
            $("#Temperatura").val(detalle.temperatura);
            $("#NumeroCamiones").val(detalle.numeroCamiones);
            $("#NumeroTinas").val(detalle.numeroTinas);
            $("#NumeroChequeadores").val(detalle.numeroChequeadores);
            $("#ValorKilogramos").val(detalle.valorKilogramos);

            // Guardar el índice para actualizarlo después
            $("#modalDetalle").data("ordenEditar", orden);

            // Mostrar el modal
            let modal = new bootstrap.Modal(document.getElementById("modalDetalle"));
            modal.show();
        }

        window.eliminarFila = eliminarFila;
        window.editarFila = editarFila;
    });
})();