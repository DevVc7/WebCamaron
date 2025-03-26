'use strict';
(function() {
    document.addEventListener("DOMContentLoaded", function() {

        // Agregamos el detalle
        $("#btnCerrarDetalle").on("click", function() {
            LimpiarControles();
        });

        $("#btnAgregarDetalle").on("click", function() {
            const ordenEditar = $("#modalDetalle").data("ordenEditar");

            const nombreContacto = $("#nombreContacto").val();           

            // Procesamos los detalles
            if (ordenEditar !== undefined) {
                // Si es edición, actualizar el registro existente
                const index = arrayDetalle.findIndex(item => item.orden === ordenEditar);
                if (index !== -1) {
                    arrayDetalle[index] = {
                        nombreContacto: nombreContacto,
                    };

                    // Actualizar la fila en la tabla
                    const row = tablaDetalles.row(index);
                    row.data({
                        orden: ordenEditar,
                        nombreContacto: nombreContacto,
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
                    nombreContacto: `${nombreContacto}`,
                    acciones: `<div class="d-flex gap-2">
                                    <a href="#" class="btn btn-sm btn-primary" onclick="editarFila(${orden})">Editar</a>
                                    <a href="#" class="btn btn-danger btn-sm" onclick="eliminarFila(this,${orden})">Eliminar</a>
                                </div>`
                }).draw();

                arrayDetalle.push({
                    orden: orden,
                    nombreContacto: nombreContacto,
                });
            }

            let nombresUnidos = arrayDetalle.map(item => item.nombreContacto).join("|");
            $("#Contactos").val(nombresUnidos);

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
            $("#nombreContacto").val('');
        }
    });
})();