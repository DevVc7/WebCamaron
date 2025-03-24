'use strict';

(function() {
    document.addEventListener("DOMContentLoaded", function() {
        // Inicializa el combo de empresas

        if ($('#ComboTipoUnidadMedida').length) {
            const selectedId = $('#ComboTipoUnidadMedida').data('id');
            const selectedText = $('#ComboTipoUnidadMedida').data('text');

            $('#ComboTipoUnidadMedida').select2({
                theme: "bootstrap-5",
                language: "es",
                allowClear: true,
                placeholder: 'Selecciona un elemento',
                ajax: {
                    url: '/ComboTipoUnidadMedida/ListarTiposUnidadMedida',
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

            if (selectedId && selectedText) {
                const preselectedOption = new Option(selectedText, selectedId, true, true);
                $('#ComboTipoUnidadMedida').append(preselectedOption).trigger('change');
            }
        }
    });
})();