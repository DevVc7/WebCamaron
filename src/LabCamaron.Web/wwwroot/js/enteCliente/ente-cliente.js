'use strict';

(function() {
    document.addEventListener('DOMContentLoaded', function() {
        if ($('#ComboEnteCliente').length) {
            const selectedId = $('#ComboEnteCliente').data('id');
            const selectedText = $('#ComboEnteCliente').data('text');

            $('#ComboEnteCliente').select2({
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
                            rolExcluir: 'CLIE'
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
                $('#ComboEnteCliente').append(preselectedOption).trigger('change');
            }
        }
    });
})();