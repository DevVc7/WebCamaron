'use strict';
(function() {
    document.addEventListener("DOMContentLoaded", function() {
        //Combro Ente
        $('#modalFormulario').on('shown.bs.modal', function() {
            $('#ComboEnteVendedor').select2({
                theme: "bootstrap-5",
                language: "es",
                allowClear: true,
                placeholder: 'Selecciona un elemento',
                dropdownParent: $('#modalFormulario'),
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

            //Combo Color

            $('#ComboColor').select2({
                theme: "bootstrap-5",
                language: "es",
                allowClear: true,
                placeholder: 'Selecciona un elemento',
                dropdownParent: $('#modalFormulario'),
                ajax: {
                    url: '/ComboColor/ListarColor',
                    dataType: 'json',
                    delay: 250,
                    data: function(params) {
                        return {
                            textoContiene: params.term
                        };
                    },
                    processResults: function(data) {
                        return {
                            results: data.map(item => ({
                                id: item.id,
                                text: item.text,
                                codigoHexadecimal: item.codigoHexadecimal // Agrega el código hexadecimal
                            }))
                        };
                    }
                },
                templateResult: function(data) {
                    if (!data.id) {
                        return data.text;
                    }

                    // Crea un elemento con el color de fondo correspondiente
                    var $color = $(
                        `<div style="display: flex; align-items: center; ">
                <span style="width: 20px; height: 20px; background-color: ${data.codigoHexadecimal}; display: inline-block; border-radius: 4px; margin-right: 8px;"></span>
                ${data.text}
            </div>`
                    );

                    return $color;
                },
                templateSelection: function(data) {
                    if (!data.id) {
                        return data.text;
                    }

                    // Muestra el color también en la opción seleccionada
                    return $(
                        `<div style="display: flex; align-items: center;">
                <span style="width: 20px; height: 20px; background-color: ${data.codigoHexadecimal}; display: inline-block; border-radius: 4px; margin-right: 8px;"></span>
                ${data.text}
            </div>`
                    );
                }
            });
        });

        // Agregar datos a la tabla
        $("#btnAgregarVendedor").click(function() {
            const vendedorSeleccionado = $("#ComboEnteVendedor option:selected");
            const idVendedor = vendedorSeleccionado.val();
            const nombreVendedor = vendedorSeleccionado.text();

            const colorSeleccionado = $('#ComboColor').select2('data')[0];
            if (!colorSeleccionado) {
                toastDangerShow("Color es obligatorio.");
                return;
            }
            const idColor = colorSeleccionado.id;
            const codigoHexadecimal = colorSeleccionado.codigoHexadecimal; // Recupera el código hexadecimal

            const porcentajeCupo = parseFloat($("#Cupo").val()) || 0;

            if (!idVendedor || !porcentajeCupo || !idColor) {
                toastDangerShow("Todos los campos son obligatorios.");
                return;
            }

            if (porcentajeCupo < 0 || porcentajeCupo > 100) {
                toastDangerShow("El cupo del vendedor debe ser entre 0% y 100%.");
                return;
            }

            let existeVendedor = arrayVendedores.filter(e => e.IdEnteVendedor == idVendedor && e.Activo);
            if (existeVendedor.length > 0) {
                toastDangerShow("Ya existe un cupo para el vendedor seleccionado.");
                return;
            }
            let existeColor = arrayVendedores.filter(e => e.IdColor == idColor && e.Activo);
            if (existeColor.length > 0) {
                toastDangerShow("Ya existe un color asignado para un cupo de vendedor.");
                return;
            }

            let sumaCupo = arrayVendedores.filter(e => e.Activo).reduce((a, b) => a + (parseFloat(b.PorcentajeCupo) || 0), porcentajeCupo);
            if (sumaCupo > 100) {
                toastDangerShow('La suma de cupo de los vendedores no debe ser mayor a 100%');
                return;
            }

            let isEnteExist = arrayVendedores.filter(e => e.IdEnteVendedor == idVendedor);

            if (isEnteExist.length == 0) {
                arrayVendedores.push({
                    "IdEnteVendedor": idVendedor,
                    "IdColor": idColor,
                    "PorcentajeCupo": porcentajeCupo,
                    "Activo": true,
                });
            } else {
                arrayVendedores = arrayVendedores.map((e) => {
                    if (e.IdEnteVendedor == idVendedor) {
                        return {
                            "IdEnteVendedor": idVendedor,
                            "IdColor": idColor,
                            "PorcentajeCupo": porcentajeCupo,
                            "Activo": true,
                        };
                    } else {
                        return e;
                    }
                });
            }

            // Agregar fila a la tabla
            tablaVendedores.row.add({
                nombreVendedor: `${nombreVendedor}`,
                porcentajeCupo: `${porcentajeCupo} %`,
                color: codigoHexadecimal,
                acciones: `<button class="btn btn-danger btn-sm" onclick="eliminarFila(this,${idVendedor})">Eliminar</button>`
            }).draw();

            //limpamos los campos
            $('#ComboEnteVendedor').empty().trigger('change');
            $('#ComboColor').empty().trigger('change');
            $("#Cupo").val('');

            // Ocultamos el modal
            let modal = bootstrap.Modal.getInstance(document.getElementById("modalFormulario"));
            modal.hide(); // Cierra el modal
        });

        document.getElementById("Cupo").addEventListener("input", function() {
            let valor = parseInt(this.value, 10);

            if (valor < 0) {
                this.value = 0;
            } else if (valor > 100) {
                this.value = 100;
            }
        });

        $('#modalFormulario').on('hidden.bs.modal', function() {
            $('#ComboEnteVendedor').select2('destroy');
            $('#ComboColor').select2('destroy');
        });
    });
})();