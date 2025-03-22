'use strict';

(function() {
    document.addEventListener("DOMContentLoaded", function() {
        const selectedId = $('#IdLaboratorio').data('id');
        const selectedText = $('#IdLaboratorio').data('text');

        // Inicializa Select2 con la configuración de AJAX
        $('#IdLaboratorio').select2({
            theme: "bootstrap-5",
            language: "es",
            allowClear: true,
            placeholder: 'Selecciona un elemento',
            ajax: {
                url: '/ComboLaboratorio/ListarLaboratorios', // Endpoint para obtener los datos
                dataType: 'json',
                delay: 250,
                data: function(params) {
                    return {
                        textoContiene: params.term // Término de búsqueda
                    };
                },
                processResults: function(data) {
                    return {
                        results: data // Datos devueltos por el servidor
                    };
                }
            }
        });

        // Si hay valores preseleccionados, añádelos y selecciónalos
        if (selectedId && selectedText) {
            const preselectedOption = new Option(selectedText, selectedId, true, true);
            $('#IdLaboratorio').append(preselectedOption).trigger('change');
        }
    });
})();