'use strict';

(function() {
    const URL_ROLES_USUARIO = "/Usuario/EditarRolesUsuario";
    const URL_MODULO_LABORATORIO_USUARIO = "/Usuario/EditarModuloLaboratorioUsuario";
    function GuardarRolesUsuario() {
        let codigoUsuario = document.getElementById('CodigoUsuario').value;
        let checkboxes = document.getElementsByName('rolespermitidos');

        // Solo enviaremos los permisos activos del sistema
        let lsPermisos = [];
        for (let i = 0; i < checkboxes.length; i++) {
            let checkBox = checkboxes[i];
            lsPermisos.push({
                IdRol: Number(checkBox.id),
                TienePermiso: checkBox.checked
            });
        }

        // Datos para enviar
        const data = {
            Codigo: codigoUsuario,
            Roles: lsPermisos
        };

        // AJAX con jQuery
        $.ajax({
            url: URL_ROLES_USUARIO,
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
                }
            },
            error: function(xhr, status, error) {
                toastDangerShow("Error al procesar: " + error);
            }
        });
    }

    function GuardarModulosLaboratorioUsuario() {
        let codigoUsuario = document.getElementById('CodigoUsuario').value;
        let checkboxes = document.getElementsByName('modulosLaboratorio');

        // Solo enviaremos los permisos activos del sistema
        let lsPermisos = [];
        for (let i = 0; i < checkboxes.length; i++) {
            let checkBox = checkboxes[i];
            lsPermisos.push({
                IdModulo: Number(checkBox.id),
                TienePermiso: checkBox.checked
            });
        }

        // Datos para enviar
        const data = {
            Codigo: codigoUsuario,
            ModulosLaboratorio: lsPermisos
        };

        // AJAX con jQuery
        $.ajax({
            url: URL_MODULO_LABORATORIO_USUARIO,
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
                }
            },
            error: function(xhr, status, error) {
                toastDangerShow("Error al procesar: " + error);
            }
        });
    }

    window.GuardarRolesUsuario = GuardarRolesUsuario;
    window.GuardarModulosLaboratorioUsuario = GuardarModulosLaboratorioUsuario;
})();