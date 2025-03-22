'use strict';
(function() {
    document.addEventListener("DOMContentLoaded", function() {
        //Combro de tanques
        $('#modalDetalle').on('shown.bs.modal', function() {
            let moduloSeleccionado = document.getElementById("ComboModulo").value;
            $('#ComboTanques').select2({
                theme: "bootstrap-5",
                language: "es",
                allowClear: true,
                multiple: true,
                placeholder: 'Selecciona un elemento',
                dropdownParent: $('#modalDetalle'),
                ajax: {
                    url: '/ComboTanque/ListarTanques',
                    dataType: 'json',
                    delay: 250,
                    data: function(params) {
                        return {
                            idModulo: moduloSeleccionado,
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

            // Inicializar el combo de combos clientes
            const comboTanquesSelectedId = $('#ComboTanques').data('id');
            const comboTanquesSelectedText = $('#ComboTanques').data('text');

            if (comboTanquesSelectedId && comboTanquesSelectedText) {
                const selectedIds = comboTanquesSelectedId.split(','); // Manejo de múltiples valores
                const selectedTexts = comboTanquesSelectedText.split(',');

                selectedIds.forEach((id, index) => {
                    const option = new Option(selectedTexts[index], id, true, true);
                    $('#ComboTanques').append(option);
                });

                $('#ComboTanques').trigger('change');
            }
        });

        // Agregamos el detalle
        $("#btnCerrarDetalle").on("click", function() {
            LimpiarControles();
        });

        $("#btnAgregarDetalle").on("click", function() {
            const ordenEditar = $("#modalDetalle").data("ordenEditar");

            const tanquesSeleccionados = $("#ComboTanques option:selected");
            const idsTanque = tanquesSeleccionados.val();
            const nombresTanque = tanquesSeleccionados.text();

            const lote = $("#Lote").val();
            const sector = $("#Sector").val();
            const longitud = parseFloat($("#Longitud").val() || 0);
            const latitud = parseFloat($("#Latitud").val() || 0);
            const contacto = $("#Contacto").val();
            const precria = $("#Precria").val();
            const plGramosRequerido = parseFloat($("#PlGramoRequerido").val() || 0);
            const salinidad = parseFloat($("#Salinidad").val() || 0);
            const temperatura = parseFloat($("#Temperatura").val() || 0);
            const numeroCamiones = parseFloat($("#NumeroCamiones").val() || 0);
            const numeroTinas = parseFloat($("#NumeroTinas").val() || 0);
            const numeroChequeadores = parseFloat($("#NumeroChequeadores").val() || 0);
            const valorKilogramos = parseFloat($("#ValorKilogramos").val() || 0);
            const orden = contador;

            if (!idsTanque) {
                toastDangerShow('Se debe seleccionar al menos un tanque');
                return;
            }


            if (ordenEditar !== undefined) {
                // Si es edición, actualizar el registro existente
                const index = arrayDetalle.findIndex(item => item.orden === ordenEditar);
                if (index !== -1) {
                    arrayDetalle[index] = {
                        orden: ordenEditar,
                        idsTanque,
                        sector,
                        lote,
                        longitud,
                        latitud,
                        contacto,
                        precria,
                        plGramosRequerido,
                        salinidad,
                        temperatura,
                        numeroCamiones,
                        numeroTinas,
                        numeroChequeadores,
                        valorKilogramos,
                    };

                    // Actualizar la fila en la tabla
                    const row = tablaDetalles.row(index);
                    row.data({
                        orden: ordenEditar,
                        numeroLote: lote,
                        nombreSector: sector,
                        nombrePrecria: precria,
                        nombresTanques: nombresTanque,
                        salinidad: salinidad,
                        temperatura: temperatura,
                        plGramoRequerido: plGramosRequerido,
                        numeroTinas: numeroTinas,
                        numeroCamiones: numeroCamiones,
                        acciones: `<div class="d-flex gap-2">
                                        <a href="#" class="btn btn-sm btn-primary" onclick="editarFila(${ordenEditar})">Editar</a>
                                        <a href="#" class="btn btn-danger btn-sm" onclick="eliminarFila(this,${ordenEditar})">Eliminar</a>
                                   </div>`

                    }).draw();
                }
            }
            else {
                tablaDetalles.row.add({
                    orden: `${orden}`,
                    numeroLote: `${lote}`,
                    nombreSector: `${sector}`,
                    nombrePrecria: `${precria}`,
                    nombresTanques: `${nombresTanque}`,
                    salinidad: `${salinidad}`,
                    temperatura: `${temperatura}`,
                    plGramoRequerido: `${plGramosRequerido}`,
                    numeroTinas: `${numeroTinas}`,
                    numeroCamiones: `${numeroCamiones}`,
                    acciones: `<div class="d-flex gap-2">
                                    <a href="#" class="btn btn-sm btn-primary" onclick="editarFila(${orden})">Editar</a>
                                    <a href="#" class="btn btn-danger btn-sm" onclick="eliminarFila(this,${orden})">Eliminar</a>
                                </div>`
                }).draw();

                arrayDetalle.push({
                    orden: orden,
                    idsTanque: idsTanque,
                    sector: sector,
                    lote: lote,
                    longitud: longitud,
                    latitud: latitud,
                    contacto: contacto,
                    precria: precria,
                    plGramosRequerido: plGramosRequerido,
                    salinidad: salinidad,
                    temperatura: temperatura,
                    numeroCamiones: numeroCamiones,
                    numeroTinas: numeroTinas,
                    numeroChequeadores: numeroChequeadores,
                    valorKilogramos: valorKilogramos,
                });
            }
            // Limpiar el identificador de edición
            $("#modalDetalle").removeData("ordenEditar");

            // Limpiamos los campos y cerramos el modal
            LimpiarControles();

            // Ocultamos el modal
            let modal = bootstrap.Modal.getInstance(document.getElementById("modalDetalle"));
            modal.hide(); // Cierra el modal

            contador++;
        });

        function LimpiarControles() {
            $('#ComboTanques').empty().trigger('change');
            $("#Lote").val('');
            $("#Sector").val('');
            $("#Longitud").val('');
            $("#Latitud").val('');
            $("#Contacto").val('');
            $("#Precria").val('');
            $("#PlGramoRequerido").val('');
            $("#Salinidad").val('');
            $("#Temperatura").val('');
            $("#NumeroCamiones").val('');
            $("#NumeroTinas").val('');
            $("#NumeroChequeadores").val('');
            $("#ValorKilogramos").val('');
        }
    });
})();