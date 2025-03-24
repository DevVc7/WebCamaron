'use strict';

(function() {
    const inputTelefono = document.getElementById('Telefono');
    const inputCelular = document.getElementById('Celular');
    const inputCorreos = document.getElementById('Correos');
    const inputCedula = document.getElementById('Identificacion');

    const tipoEntidad = document.getElementById("TipoEntidad");

    const nombres = document.getElementById("Nombres");
    const apellidos = document.getElementById("Apellidos");
    const razonSocial = document.getElementById("RazonSocial");

    const labelNombres = document.querySelector("label[for='Nombres']");
    const labelApellidos = document.querySelector("label[for='Apellidos']");
    const labelRazonSocial = document.querySelector("label[for='RazonSocial']");

    function actualizarValidaciones() {
        if (tipoEntidad.value === "JUR") {
            // Quitar "required" de nombres y apellidos
            nombres.removeAttribute("required");
            apellidos.removeAttribute("required");
            labelNombres.classList.remove("required");
            labelApellidos.classList.remove("required");

            // Agregar "required" a razón social
            razonSocial.setAttribute("required", "required");
            labelRazonSocial.classList.add("required");
        } else {
            // Agregar "required" a nombres y apellidos
            nombres.setAttribute("required", "required");
            apellidos.setAttribute("required", "required");
            labelNombres.classList.add("required");
            labelApellidos.classList.add("required");

            // Quitar "required" de razón social
            razonSocial.removeAttribute("required");
            labelRazonSocial.classList.remove("required");
        }

        ValidarIdentificacion(inputCedula);
    }

    // Ejecutar validación al cargar la página
    actualizarValidaciones();

    // Expresiones regulares para validar los campos
    const telRegex = /^(0[2-7])\d{7}$/; // Teléfono fijo (02-07 + 7 dígitos)
    const celRegex = /^(09)\d{8}$/; // Celular (09 + 8 dígitos)
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/; // Correo electrónico básico

    function validarTelefono() {
        const valor = inputTelefono.value.trim();
        if (valor === '' || telRegex.test(valor)) {
            inputTelefono.setCustomValidity('');
        } else {
            inputTelefono.setCustomValidity('Ingrese un número de teléfono válido.');
        }
        inputTelefono.reportValidity();
    }

    function validarCelular() {
        const valor = inputCelular.value.trim();
        if (valor === '' || celRegex.test(valor)) {
            inputCelular.setCustomValidity('');
        } else {
            inputCelular.setCustomValidity('Ingrese un número de celular válido.');
        }
        inputCelular.reportValidity();
    }

    function validarCorreos() {
        const textoCorreo = inputCorreos.value;
        debugger;
        const correos = textoCorreo.split(';').map(email => email.trim());
        const correosValidos = correos.every(email => emailRegex.test(email));

        if (correos.length === 0 || correos.some(email => email === '')) {
            inputCorreos.reportValidity();
        } else if (!correosValidos) {
            inputCorreos.setCustomValidity('Uno o más correos no tienen un formato válido.');
        } else {
            inputCorreos.setCustomValidity('');
        }
        inputCorreos.reportValidity();
    }

    function ValidarIdentificacion(ctrNumeroCedula) {
        if (tipoEntidad.value === "JUR") {
            ValidarRuc(ctrNumeroCedula);
        } else if (tipoEntidad.value === "NAT") {
            ValidarNumeroCedula(ctrNumeroCedula);
        } else {
            ctrNumeroCedula.setCustomValidity("Tipo de identificación no reconocida");
            ctrNumeroCedula.reportValidity();
        }
    }
    function ValidarRuc(ctrNumeroCedula) {
        var ruc = ctrNumeroCedula.value.trim();

        const regex = /^[0-9]+$/;
        if (!regex.test(ruc)) {
            ctrNumeroCedula.setCustomValidity("Solo se permiten números");
            ctrNumeroCedula.reportValidity();
            return;
        }

        if (ruc.length !== 13) {
            ctrNumeroCedula.setCustomValidity("El ruc debe tener 13 dígitos");
            ctrNumeroCedula.reportValidity();
            return;
        }

        ctrNumeroCedula.setCustomValidity('');
        ctrNumeroCedula.reportValidity();
    }
    function ValidarNumeroCedula(ctrNumeroCedula) {
        var cedula = ctrNumeroCedula.value.trim();

        const regex = /^[0-9]+$/;
        if (!regex.test(cedula)) {
            ctrNumeroCedula.setCustomValidity("Solo se permiten números");
            ctrNumeroCedula.reportValidity();
            return;
        }

        if (cedula.length !== 10) {
            ctrNumeroCedula.setCustomValidity("La cédula debe tener 10 dígitos");
            ctrNumeroCedula.reportValidity();
            return;
        }

        var digito_region = parseInt(cedula.substring(0, 2));

        if (digito_region < 1 || digito_region > 24) {
            ctrNumeroCedula.setCustomValidity("No se ha ingresado un número de cédula válido");
            ctrNumeroCedula.reportValidity();
            return;
        }

        var ultimo_digito = parseInt(cedula.substring(9, 10));
        var pares = parseInt(cedula[1]) + parseInt(cedula[3]) + parseInt(cedula[5]) + parseInt(cedula[7]);

        var impares = 0;
        for (let i = 0; i < 9; i += 2) {
            let num = parseInt(cedula[i]) * 2;
            if (num > 9) num -= 9;
            impares += num;
        }

        var suma_total = pares + impares;
        var decena = Math.ceil(suma_total / 10) * 10;
        var digito_validador = decena - suma_total;

        if (digito_validador === 10) digito_validador = 0;

        if (digito_validador === ultimo_digito) {
            ctrNumeroCedula.setCustomValidity('');
        } else {
            ctrNumeroCedula.setCustomValidity("No se ha ingresado un número de cédula válido");
        }

        ctrNumeroCedula.reportValidity();
    }

    // Eventos de validación
    tipoEntidad.addEventListener("change", actualizarValidaciones);
    inputTelefono.addEventListener('input', validarTelefono);
    inputCelular.addEventListener('input', validarCelular);
    inputCorreos.addEventListener('input', validarCorreos);
    inputCedula.addEventListener('input', function() {
        ValidarIdentificacion(this);
    });
})();