'use strict';

(function() {
    document.addEventListener("DOMContentLoaded", function() {
        const selectedId = $('#IdLaboratorio').data('id');
        const selectedText = $('#IdLaboratorio').data('text');

        $.ajax({
            url: '/ComboLaboratorio/ListarLaboratorios', // Tu API que devuelve los laboratorios
            dataType: 'json',
            success: function(data) {
                let $select = $('#IdLaboratorio');
                $select.empty().append('<option value="">Selecciona un laboratorio</option>');

                data.forEach(item => {
                    let newOption = new Option(item.text, item.id, false, false);
                    $select.append(newOption);
                });

                $select.select2({
                    theme: "bootstrap-5",
                    language: "es",
                    allowClear: true,
                    placeholder: 'Selecciona un laboratorio'
                });

                // Seleccionar automáticamente el primer elemento después de cargar los datos
                if (data.length > 0) {
                    $select.val(data[0].id).trigger('change');
                }
            },
            error: function() {
                toastDangerShow("Error al cargar los datos del laboratorio.")
            }
        });

        // Si hay valores preseleccionados, añádelos y selecciónalos
        if (selectedId && selectedText) {
            const preselectedOption = new Option(selectedText, selectedId, true, true);
            $('#IdLaboratorio').append(preselectedOption).trigger('change');
        }
    });
})();