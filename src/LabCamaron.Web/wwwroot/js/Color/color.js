'use strict';

(function () {
  document.addEventListener("DOMContentLoaded", function () {
    const colorPicker = document.getElementById("colorPicker");
    const hexCode = document.getElementById("CodigoHexadecimal");
    //const rgbCode = document.getElementById("rgbCode");
    function updateColorValues() {
      const hex = colorPicker.value;
      hexCode.value = hex;

      // Convertir Hex a RGB
      const r = parseInt(hex.substring(1, 3), 16);
      const g = parseInt(hex.substring(3, 5), 16);
      const b = parseInt(hex.substring(5, 7), 16);
      //rgbCode.value = `rgb(${r}, ${g}, ${b})`;
    }

    colorPicker.addEventListener("input", updateColorValues);
    updateColorValues(); // Para establecer los valores iniciales
  });
})();