'use strict';

(function () {
  document.addEventListener("DOMContentLoaded", function () {
    // Inicializa el combo de empresas

    if ($('#ComboEmpresa').length) {
      const selectedId = $('#ComboEmpresa').data('id');
      const selectedText = $('#ComboEmpresa').data('text');

      $('#ComboEmpresa').select2({
        theme: "bootstrap-5",
        language: "es",
        allowClear: true,
        placeholder: 'Selecciona un elemento',
        ajax: {
          url: '/ComboEmpresa/ListarEmpresas',
          dataType: 'json',
          delay: 250,
          data: function (params) {
            return {
              textoContiene: params.term
            };
          },
          processResults: function (data) {
            return {
              results: data
            };
          }
        }
      });

      if (selectedId && selectedText) {
        const preselectedOption = new Option(selectedText, selectedId, true, true);
        $('#ComboEmpresa').append(preselectedOption).trigger('change');
      }
    }
  });
})();