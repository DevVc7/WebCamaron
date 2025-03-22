'use strict';

(function() {
    const selectedPlacementType = "top-0 end-0";
    const toastPlacementExample = document.querySelector('.toast-placement-ex');
    let toastPlacement = null;

    function toastDispose(toast) {
        if (toast && toast._element !== null) {
            if (toastPlacementExample) {
                toastPlacementExample.classList.remove(toast._type);
                DOMTokenList.prototype.remove.apply(toastPlacementExample.classList, toast._placement || []);
            }
            toast.dispose();
        }
    }

    function toastDangerShow(mensaje) {
        let toastIcon = document.getElementById("toast-icon");
        while (toastIcon.classList.length > 0) {
            toastIcon.classList.remove(toastIcon.classList.item(0)); // Elimina la primera clase
        }

        document.getElementById('toast-icon').classList.add('bx', 'bx-error-circle');

        document.getElementById('toast-tittle').innerHTML = 'Error';
        document.getElementById('toast-message').innerHTML = mensaje;
        toastShow("bg-danger");
    }
    function toastSuccessShow(mensaje) {
        let toastIcon = document.getElementById("toast-icon");
        while (toastIcon.classList.length > 0) {
            toastIcon.classList.remove(toastIcon.classList.item(0)); // Elimina la primera clase
        }

        document.getElementById('toast-icon').classList.add('bx', 'bx-check-circle');

        document.getElementById('toast-tittle').innerHTML = 'Éxito';
        document.getElementById('toast-message').innerHTML = mensaje;
        toastShow("bg-success");
    }

    function toastShow(selectedType) {
        if (toastPlacement) {
            toastDispose(toastPlacement);
        }
        const selectedPlacement = selectedPlacementType.split(' ');

        toastPlacementExample.classList.add(selectedType);
        DOMTokenList.prototype.add.apply(toastPlacementExample.classList, selectedPlacement);

        // Asigna propiedades adicionales al objeto de la instancia para facilitar la limpieza
        toastPlacement = new bootstrap.Toast(toastPlacementExample);
        toastPlacement._type = selectedType;
        toastPlacement._placement = selectedPlacement;

        toastPlacement.show();
    }

    // Exponer funciones si se necesitan en otros scripts
    window.toastDangerShow = toastDangerShow;
    window.toastSuccessShow = toastSuccessShow;
    window.toastShow = toastShow;
})();