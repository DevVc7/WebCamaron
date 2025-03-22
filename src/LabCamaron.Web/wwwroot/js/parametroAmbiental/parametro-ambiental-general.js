'use strict';

(function() {
    document.addEventListener("DOMContentLoaded", function() {
        // Inicializa el combo de empresas

        if ($('#ComboLaboratorio').length) {
            const selectedId = $('#ComboLaboratorio').data('id');
            const selectedText = $('#ComboLaboratorio').data('text');

            $('#ComboLaboratorio').select2({
                theme: "bootstrap-5",
                language: "es",
                allowClear: true,
                placeholder: 'Selecciona un elemento',
                ajax: {
                    url: '/ComboLaboratorio/ListarLaboratorios',
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
                $('#ComboLaboratorio').append(preselectedOption).trigger('change');
            }
        }

        const inputMinimo = document.getElementById('ValorMinimo');
        const inputMaximo = document.getElementById('ValorMaximo');

        // Función para validar los valores
        function validarValores() {
            const valorMinimo = parseFloat(inputMinimo.value);
            const valorMaximo = parseFloat(inputMaximo.value);

            if (isNaN(valorMinimo) || isNaN(valorMaximo)) {
                // Si alguno de los valores no es un número, no mostrar error
                return;
            }

            if (valorMinimo >= valorMaximo) {
                // Mostrar un mensaje de error
                inputMinimo.setCustomValidity('El valor mínimo debe ser menor que el valor máximo.');
                inputMaximo.setCustomValidity('El valor máximo debe ser mayor que el valor mínimo.');
            } else {
                // Limpiar el mensaje de error si los valores son válidos
                inputMinimo.setCustomValidity('');
                inputMaximo.setCustomValidity('');
            }

            // Actualizar la validación del formulario
            inputMinimo.reportValidity();
            inputMaximo.reportValidity();
        }

        // Escuchar eventos de cambio en los inputs
        inputMinimo.addEventListener('input', validarValores);
        inputMaximo.addEventListener('input', validarValores);
    });
})();