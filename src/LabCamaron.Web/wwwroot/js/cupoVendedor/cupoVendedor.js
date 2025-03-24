'use strict';

let contador = 1;
let tablaVendedores;
let arrayVendedores = [];
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

        tablaVendedores = $("#tbVendedores").DataTable({
            columnDefs: [
                { className: "text-start", targets: 0 },
                { className: "text-center", targets: 1 },
                { className: "text-start", targets: 2 },
                { className: "text-start", targets: 3 },
            ],
            columns: [
                { data: "nombreVendedor" },
                { data: "porcentajeCupo" },
                {
                    data: "color", render: function(data, type, row) {
                        return `<div style="width: 100%; height: 30px; background-color: ${data}; border: 1px solid #000;"></div>`;
                    }
                },
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

        $('#btnEditar').on('click', function() {
            const idModulo = $("#IdModuloLaboratorio").val();

            if (idModulo == undefined || idModulo == 0) {
                toastDangerShow('Modulo es obligatorio');
                return;
            }
            if (arrayVendedores.length === 0) {
                toastDangerShow('Se debe tener al menos un cupo de vendedor asignado al módulo');
                return;
            }

            let formData = {
                IdModuloLaboratorio: idModulo,
                DetalleCupoVendedores: arrayVendedores,
                IdLaboratorio: $("#IdLaboratorio").val(),
                CodigoModuloLaboratorio: $("#CodigoModuloLaboratorio").val(),
            };

            $.ajax({
                url: '/CupoVendedor/EditarCupoVendedor',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(formData),
                success: function(response) {
                    if (response.success) {
                        toastDangerShow(response.message);
                        window.location.href = "/CupoVendedor/Index?mostrarMensajeExito=true"; // Redirigir tras éxito
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

        //Cargar si viene lleno lista de vendedores
        if (cupoVendedor) {
            cupoVendedor.detallesCupoVendedor.forEach((e) => {
                tablaVendedores.row.add({
                    nombreVendedor: e.tipoEntidad == "JUR" ? e.razonSocialEnteVendedor : e.nombresCompletosEnteVendedor,
                    porcentajeCupo: `${e.porcentajeCupo} %`,
                    color: e.codigoHexadecimal,
                    acciones: `<button class="btn btn-danger btn-sm" onclick="eliminarFila(this,${e.idEnteVendedor})">Eliminar</button>`
                }).draw();

                arrayVendedores.push({
                    "IdEnteVendedor": e.idEnteVendedor,
                    "IdColor": e.idColor,
                    "PorcentajeCupo": e.porcentajeCupo,
                    "Activo": true,
                });
            });
        }

        document.getElementById("Cupo").addEventListener("input", function() {
            let valor = parseInt(this.value, 10);

            if (valor < 0) {
                this.value = 0;
            } else if (valor > 100) {
                this.value = 100;
            }
        });

        function eliminarFila(button, idVendedor) {
            tablaVendedores.row($(button).parents("tr")).remove().draw();
            //arrayVendedores = arrayVendedores.filter(e => e.IdEnteVendedor != idVendedor);

            arrayVendedores = arrayVendedores.map((e) => {
                if (e.IdEnteVendedor == idVendedor) {
                    return {
                        "IdEnteVendedor": e.IdEnteVendedor,
                        "IdColor": e.IdColor,
                        "PorcentajeCupo": e.PorcentajeCupo,
                        "Activo": false,
                    };
                } else {
                    return {
                        "IdEnteVendedor": e.IdEnteVendedor,
                        "IdColor": e.IdColor,
                        "PorcentajeCupo": e.PorcentajeCupo,
                        "Activo": true,
                    };
                }
            });
        }

        window.eliminarFila = eliminarFila;
    });
})();