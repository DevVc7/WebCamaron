//scripts para la ejecución de la aplicación web
document.addEventListener("DOMContentLoaded", function() {
    document.querySelectorAll("input[type='text'], textarea").forEach(input => {
        input.addEventListener("input", function() {
            this.value = this.value.toUpperCase();
        });
    });
});