'use strict';

(function() {
    const URL_PERMISOS = "/Rol/EditarPermisos";
    function GuardarRolesPermisos() {
        let codigoRol = document.getElementById('CodigoRol').value;
        let checkboxes = document.getElementsByName('permisos');

        // Solo enviaremos los permisos activos del sistema
        let lsPermisos = [];
        for (let i = 0; i < checkboxes.length; i++) {
            let checkBox = checkboxes[i];
            lsPermisos.push({
                IdMenuAccion: Number(checkBox.id),
                TienePermiso: checkBox.checked
            });
        }

        // Datos para enviar
        const data = {
            Codigo: codigoRol,
            DetallePermisos: lsPermisos
        };

        // AJAX con jQuery
        $.ajax({
            url: URL_PERMISOS,
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(data),
            success: function(response, textStatus, jqXHR) {
                if (response.esFatal) {
                    window.location.href = "/Home/ErrorMantenimiento";
                }
                else if (response.esError) {
                    toastDangerShow(response.mensaje);
                }
                else {
                    toastSuccessShow(response.mensaje);

                    // Se recarga la pagina actual después de 1 segundo para ver la alerta
                    setTimeout(() => {
                        window.location.reload(); // Reloads the current page
                    }, 1000);
                }
            },
            error: function(xhr, status, error) {
                toastDangerShow("Error al procesar: " + error);
            }
        });
    }

    window.GuardarRolesPermisos = GuardarRolesPermisos;
})();