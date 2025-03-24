'use strict';
let contador = 0;
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

            debugger;
            // Buscar el registro en arrayDetalle
            const detalle = arrayDetalle.find(item => item.orden === orden);
            if (!detalle) return;

            // Esperamos que se cargue el modal, antes de asignar el combo
            $('#modalDetalle').on('shown.bs.modal', function() {
                const selectedIds = detalle.idsTanque ? detalle.idsTanque.split(',') : [];
                const selectedTexts = detalle.nombresTanques ? detalle.nombresTanques.split(',') : [];
                $('#ComboTanques').val(null).trigger('change');  // Reinicia Select2  
                $('#ComboTanques').empty();

                selectedIds.forEach((id, index) => {
                    const option = new Option(selectedTexts[index], id, true, true);
                    $('#ComboTanques').append(option);
                });

                $('#ComboTanques').trigger('change');
            });            

            // Llenar los campos del modal con los valores existentes
            $("#Lote").val(detalle.numeroLote);
            $("#Sector").val(detalle.nombreSector);
            $("#Longitud").val(detalle.longitud);
            $("#Latitud").val(detalle.latitud);
            $("#Contacto").val(detalle.nombreContacto);
            $("#Precria").val(detalle.nombrePrecria);
            $("#PlGramoRequerido").val(detalle.plGramoRequerido);
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


        if ($("#botonCrearNuevo").length) {
            document.getElementById("botonCrearNuevo").addEventListener("click", function(event) {
                let formData = {
                    IdLaboratorio: Number($("#ComboLaboratorio").val()),
                    IdModulo: Number($("#ComboModulo").val()),
                    IdPlanificacion: Number($("#ComboPlanificacionSiembra").val()),
                    IdEnteCliente: Number($("#ComboCliente").val()),
                    IdEnteVendedor: Number($("#ComboVendedor").val()),
                    DetallesLote: arrayDetalle,
                };

                if (formData.IdLaboratorio == 0 || formData.IdModulo == 0
                    || formData.IdPlanificacion == 0 || formData.IdEnteCliente == 0
                    || formData.IdEnteVendedor == 0
                ) {
                    toastDangerShow('Completa todos los campos obligatorios.');
                    return;
                }
                if (formData.DetallesLote == undefined || formData.DetallesLote.length == 0) {
                    toastDangerShow('Detalles de lote son obligatorios.');
                    return;

                }


                $.ajax({
                    url: '/AsignacionCliente/CrearAsignacionCliente',
                    type: 'POST',
                    contentType: 'application/json',
                    data: JSON.stringify(formData),
                    success: function(response) {
                        debugger;
                        if (response.success) {
                            toastSuccessShow(response.message);
                            window.location.href = "/AsignacionCliente/Index?mostrarMensajeExito=true"; // Redirigir tras éxito
                        } else {
                            toastDangerShow(response.message);
                        }
                    },
                    error: function(xhr) {
                        console.error(xhr.responseText);
                        toastDangerShow('Hubo un error al guardar el cupo');
                    }
                });
            });
        }

        if ($("#botonEditar").length) {
            document.getElementById("botonEditar").addEventListener("click", function(event) {
                let formData = {
                    Id: Number($("#id").val()),
                    IdLaboratorio: Number($("#ComboLaboratorio").val()),
                    IdModulo: Number($("#ComboModulo").val()),
                    IdPlanificacion: Number($("#ComboPlanificacionSiembra").val()),
                    IdEnteCliente: Number($("#ComboCliente").val()),
                    IdEnteVendedor: Number($("#ComboVendedor").val()),
                    DetallesLote: arrayDetalle,
                };


                if (formData.IdLaboratorio == 0 || formData.IdModulo == 0
                    || formData.IdPlanificacion == 0 || formData.IdEnteCliente == 0
                    || formData.IdEnteVendedor == 0
                ) {
                    toastDangerShow('Completa todos los campos obligatorios.');
                    return;
                }
                if (formData.DetallesLote == undefined || formData.DetallesLote.length == 0) {
                    toastDangerShow('Detalles de lote son obligatorios.');
                    return;

                }


                $.ajax({
                    url: '/AsignacionCliente/EditarAsignacionCliente',
                    type: 'POST',
                    contentType: 'application/json',
                    data: JSON.stringify(formData),
                    success: function(response) {
                        debugger;
                        if (response.success) {
                            toastSuccessShow(response.message);
                        } else {
                            toastDangerShow(response.message);
                        }
                    },
                    error: function(xhr) {
                        console.error(xhr.responseText);
                        toastDangerShow('Hubo un error al guardar el cupo');
                    }
                });
            });
        }

        if (detalles) {
            contador = detalles.length;

            for (var i = 0; i < detalles.length; i++) {
                var detalle = detalles[i];
                let orden = i + 1;
                arrayDetalle.push({
                    orden: orden,
                    idsTanque: detalle.idsTanque,
                    nombresTanques: detalle.nombresTanques,
                    nombreSector: detalle.nombreSector,
                    numeroLote: detalle.numeroLote,
                    longitud: detalle.longitud,
                    latitud: detalle.latitud,
                    nombreContacto: detalle.nombreContacto,
                    nombrePrecria: detalle.nombrePrecria,
                    plGramoRequerido: detalle.plGramoRequerido,
                    salinidad: detalle.salinidad,
                    temperatura: detalle.temperatura,
                    numeroCamiones: detalle.numeroCamiones,
                    numeroTinas: detalle.numeroTinas,
                    numeroChequeadores: detalle.numeroChequeadores,
                    valorKilogramos: detalle.valorKilogramos,
                });

                tablaDetalles.row.add({
                    orden: `${orden}`,
                    numeroLote: `${detalle.numeroLote}`,
                    nombreSector: `${detalle.nombreSector}`,
                    nombrePrecria: `${detalle.nombrePrecria}`,
                    nombresTanques: `${detalle.nombresTanques}`,
                    salinidad: `${detalle.salinidad}`,
                    temperatura: `${detalle.temperatura}`,
                    plGramoRequerido: `${detalle.plGramoRequerido}`,
                    numeroTinas: `${detalle.numeroTinas}`,
                    numeroCamiones: `${detalle.numeroCamiones}`,
                    acciones: `<div class="d-flex gap-2">
                                    <a href="#" class="btn btn-sm btn-primary" onclick="editarFila(${orden})">Editar</a>
                                    <a href="#" class="btn btn-danger btn-sm" onclick="eliminarFila(this,${orden})">Eliminar</a>
                                </div>`
                }).draw();
            }
        }


        window.eliminarFila = eliminarFila;
        window.editarFila = editarFila;
    });
})();