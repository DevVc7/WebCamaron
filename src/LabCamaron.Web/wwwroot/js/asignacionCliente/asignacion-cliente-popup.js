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

            const tanquesSeleccionados = $("#ComboTanques").select2("data");
            const idsTanqueArray = tanquesSeleccionados.map(t => t.id);
            const nombresTanquesArray = tanquesSeleccionados.map(t => t.text); // Array de nombres

            const numeroLote = $("#Lote").val();
            const nombreSector = $("#Sector").val();
            const longitud = parseFloat($("#Longitud").val() || 0);
            const latitud = parseFloat($("#Latitud").val() || 0);
            const nombreContacto = $("#Contacto").val();
            const nombrePrecria = $("#Precria").val();
            const plGramo1 = parseFloat($("#PlGramo1").val() || 0);
            const plGramo2 = parseFloat($("#PlGramo2").val() || 0);
            const salinidad = parseFloat($("#Salinidad").val() || 0);
            const temperatura = parseFloat($("#Temperatura").val() || 0);
            const numeroCamiones = parseFloat($("#NumeroCamiones").val() || 0);
            const numeroTinas = parseFloat($("#NumeroTinas").val() || 0);
            const numeroChequeadores = parseFloat($("#NumeroChequeadores").val() || 0);
            const valorKilogramos = parseFloat($("#ValorKilogramos").val() || 0);

            let idsTanque = idsTanqueArray.join();
            let nombresTanques = nombresTanquesArray.join();
            if (!idsTanque) {
                toastDangerShow('Se debe seleccionar al menos un tanque');
                return;
            }

            // Procesamientos de tanques
            let idsTanquesExistentes = arrayDetalle
                .map(t => t.idsTanque.split(',')) 
                .flat();
            let hayDuplicados = idsTanqueArray.some(id => idsTanquesExistentes.includes(id));
            if (hayDuplicados) {
                toastDangerShow('Los tanques seleccionados ya ha sido ingresados en otros detalles');
                return;
            }

            // Procesamos los detalles
            if (ordenEditar !== undefined) {
                // Si es edición, actualizar el registro existente
                const index = arrayDetalle.findIndex(item => item.orden === ordenEditar);
                if (index !== -1) {
                    arrayDetalle[index] = {
                        orden: ordenEditar,
                        idsTanque,
                        nombreSector,
                        numeroLote,
                        longitud,
                        latitud,
                        nombreContacto,
                        nombrePrecria,
                        plGramo1,
                        plGramo2,
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
                        numeroLote: numeroLote,
                        nombreSector: nombreSector,
                        nombrePrecria: nombrePrecria,
                        nombresTanques: nombresTanques,
                        salinidad: salinidad,
                        temperatura: temperatura,
                        plGramo1: plGramo1,
                        plGramo2: plGramo2,
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
                const orden = contador + 1;
                tablaDetalles.row.add({
                    orden: `${orden}`,
                    numeroLote: `${numeroLote}`,
                    nombreSector: `${nombreSector}`,
                    nombrePrecria: `${nombrePrecria}`,
                    nombresTanques: `${nombresTanques}`,
                    salinidad: `${salinidad}`,
                    temperatura: `${temperatura}`,
                    plGramo1: `${plGramo1}`,
                    plGramo2: `${plGramo2}`,
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
                    nombresTanques: nombresTanques,
                    nombreSector: nombreSector,
                    numeroLote: numeroLote,
                    longitud: longitud,
                    latitud: latitud,
                    nombreContacto: nombreContacto,
                    nombrePrecria: nombrePrecria,
                    plGramo1: plGramo1,
                    plGramo2: plGramo2,
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
            $("#PlGramo1").val('');
            $("#PlGramo2").val('');
            $("#Salinidad").val('');
            $("#Temperatura").val('');
            $("#NumeroCamiones").val('');
            $("#NumeroTinas").val('');
            $("#NumeroChequeadores").val('');
            $("#ValorKilogramos").val('');
        }
    });
})();