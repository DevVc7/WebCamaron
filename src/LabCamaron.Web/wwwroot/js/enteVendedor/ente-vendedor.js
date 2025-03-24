'use strict';

(function() {
    document.addEventListener('DOMContentLoaded', function() {
        if ($('#ComboEnteVendedor').length) {
            const selectedId = $('#ComboEnteVendedor').data('id');
            const selectedText = $('#ComboEnteVendedor').data('text');

            $('#ComboEnteVendedor').select2({
                theme: "bootstrap-5",
                language: "es",
                allowClear: true,
                placeholder: 'Selecciona un elemento',
                ajax: {
                    url: '/ComboEnte/ListarEnteSinRol',
                    dataType: 'json',
                    delay: 250,
                    data: function(params) {
                        return {
                            textoContiene: params.term,
                            rolExcluir: 'VEND'
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
                $('#ComboEnteVendedor').append(preselectedOption).trigger('change');
            }
        }
    });
})();