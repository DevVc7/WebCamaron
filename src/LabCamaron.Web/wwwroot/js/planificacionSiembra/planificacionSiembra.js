'use strict';
let cargarLaboratorio = () => {

    // Inicializa el combo de laboratorios
    const laboratorioSelectedId = $('#ComboLaboratorio').data('id');
    const laboratorioSelectedText = $('#ComboLaboratorio').data('text');
    $('#ComboLaboratorio').select2({
        theme: "bootstrap-5",
        language: "es",
        allowClear: true,
        placeholder: 'Selecciona un elemento',
        ajax: {
            url: '/ComboLaboratorio/ListarLaboratorios',
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

    if (laboratorioSelectedId && laboratorioSelectedText) {
        const preselectedOption = new Option(laboratorioSelectedText, laboratorioSelectedId, true, true);
        $('#ComboLaboratorio').append(preselectedOption).trigger('change');
    }


}

let cargarModulo = (laboratorioSelectedId) => {

    // Inicializar el combo de moduloLaboratorios
    const moduloModuloSelectedId = $('#ComboModulo').data('id');
    const moduloModuloSelectedText = $('#ComboModulo').data('text');

    $('#ComboModulo').select2({
        theme: "bootstrap-5",
        language: "es",
        allowClear: true,
        placeholder: 'Selecciona un elemento',
        dropdownParent: $('#DetallesModal'),
        ajax: {
            url: '/ComboModuloLaboratorio/ListarModuloLaboratorios',
            dataType: 'json',
            delay: 250,
            data: function (params) {
                return {
                    idLaboratorio: laboratorioSelectedId, // Usar el id de laboratorio inicial
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

    if (moduloModuloSelectedId && moduloModuloSelectedText) {
        const preselectedOption = new Option(moduloModuloSelectedText, moduloModuloSelectedId, true, true);
        $('#ComboModulo').append(preselectedOption).trigger('change');
    }


}

let cargarEnteTecnico = () => {

    // Inicializar el combo de moduloLaboratorios
    const moduloEnteSelectedId = $('#ComboEnteTecnico').data('id');
    const moduloEnteSelectedText = $('#ComboEnteTecnico').data('text');
    $('#ComboEnteTecnico').select2({
        theme: "bootstrap-5",
        language: "es",
        allowClear: true,
        placeholder: 'Selecciona un elemento',
        dropdownParent: $('#DetallesModal'),
        ajax: {
            url: '/ComboEnte/ListarEntesTecnicos',
            dataType: 'json',
            delay: 250,
            data: function (params) {
                //var selectedData = $('#ComboLaboratorio').select2('data')[0];
                //var idEmpresaSelection = selectedData ? selectedData.idEmpresa : null;
                const idModulo = $('#ComboModulo').val();
                return {
                    idModulo: idModulo,
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

    if (moduloEnteSelectedId && moduloEnteSelectedText) {
        const preselectedOption = new Option(moduloEnteSelectedText, moduloEnteSelectedId, true, true);
        $('#ComboEnteTecnico').append(preselectedOption).trigger('change');
    }

}

function actualizarModuloEnte(moduloId) {

    $('#ComboEnteTecnico').empty().trigger('change');
    cargarEnteTecnico();
}

function calcularRangoFecha() {
    // Obtener la fecha seleccionada del input
    let fechaInput = document.getElementById("FechaPlanificacion").value;
    if (!fechaInput) return; // Si no hay fecha seleccionada, salir

    // Convertir a objeto Date
    let fecha = new Date(fechaInput);
    let dia = fecha.getDate();
    let mesActual = fecha.toLocaleString('es-ES', { month: 'short' });

    // Calcular la fecha del próximo mes
    let fechaProxMes = new Date(fecha);
    fechaProxMes.setMonth(fechaProxMes.getMonth() + 1);
    let mesProx = fechaProxMes.toLocaleString('es-ES', { month: 'short' });

    // Formatear en el formato deseado
    let resultado = `${mesActual.charAt(0).toUpperCase() + mesActual.slice(1)}-${dia} a ${mesProx.charAt(0).toUpperCase() + mesProx.slice(1)}-${dia}`;

    // Mostrar el resultado en un elemento (puedes cambiarlo según necesites)
    document.getElementById("lblFechaPlanificaion").value = resultado;
}

function limpiarModal() {
    $('#ComboModulo').empty().trigger('change');
    $('#ComboEnteTecnico').empty().trigger('change');
    $('#CantidadFacturada').val('')

    const today = new Date().toISOString().split('T')[0];

    // Calcular fecha de cosecha (+20 días)
    const fechaCosecha = new Date();
    fechaCosecha.setDate(fechaCosecha.getDate() + 20);
    const fechaCosechaFormatted = fechaCosecha.toISOString().split('T')[0];

    // Asignar valores a los inputs
    $('#FechaSiembraDetail').val(today);
    $('#FechaCosecha').val(fechaCosechaFormatted);

}
function AbrirModal(data) {

    if (data != null) {

        $('#ComboModulo').val(data.IdModulo).trigger('change');
        $('#ComboEnteTecnico').val(data.IdEnteTecnico).trigger('change');
        $('#CantidadFacturada').val(data.CantidadFacturada)
        $('#FechaSiembraDetail').val(data.FechaSiembra);
        $('#FechaCosecha').val(data.FechaCosecha);

    } else {

        $('#ComboModulo').empty().trigger('change');
        $('#ComboEnteTecnico').empty().trigger('change');
        $('#CantidadFacturada').val('')

        const today = new Date().toISOString().split('T')[0];

        // Calcular fecha de cosecha (+20 días)
        const fechaCosecha = new Date();
        fechaCosecha.setDate(fechaCosecha.getDate() + 20);
        const fechaCosechaFormatted = fechaCosecha.toISOString().split('T')[0];

        // Asignar valores a los inputs
        $('#FechaSiembraDetail').val(today);
        $('#FechaCosecha').val(fechaCosechaFormatted);
    }

    $("#DetallesModal").modal("show");
}

function editarModulo() {

    $.ajax({
        url: "/PlanificacionSiembra/AgregarDetallePlanificacionSiembra", // Asegúrate de que la ruta sea correcta
        type: "POST",
        data: { detalleSiembraTable, agregar },
        success: function (data) {
            if (data.error) {
                var respuesta = data.data
                $('#ComboModulo').val('').trigger('change');
                $('#ComboEnteTecnico').val('').trigger('change');
                $('#CantidadFacturada').val('')

                const today = new Date().toISOString().split('T')[0];

                // Calcular fecha de cosecha (+20 días)
                const fechaCosecha = new Date();
                fechaCosecha.setDate(fechaCosecha.getDate() + 20);
                const fechaCosechaFormatted = fechaCosecha.toISOString().split('T')[0];

                // Asignar valores a los inputs
                $('#FechaSiembraDetail').val(today);
                $('#FechaCosecha').val(fechaCosechaFormatted);

                $("#DetallesModal").modal("show");
            } else {
                alert("Error " + data.message);

            }

        },
        error: function () {
            alert("Error al cargar el modal.");
        }
    });


}

function cargarTabla(detalleDataTable) {


    if (detalleSiembraTable.length > 0) {


        detalleDataTable.DataTable({
            destroy: true,
            data: detalleSiembraTable,
            columns: [
                { data: 'nombreModulo', className: 'text-center' },
                { data: 'fechaSiembra', className: 'text-start' },
                { data: 'fechaCosecha', className: 'text-start' },
                { data: 'cantidadFacturada', className: 'text-start' },
                { data: 'nombreEnteTecnico', className: 'text-start' },
                {
                    data: null,
                    className: 'text-center',
                    orderable: false,
                    render: function (data, type, row) {
                        return `
                    <button class="btn btn-warning btn-sm editar" data-id="${row.Index}">
                        <i class="fas fa-edit"></i> Editar
                    </button>
                    <button class="btn btn-danger btn-sm eliminar" data-id="${row.Index}">
                        <i class="fas fa-trash"></i> Eliminar
                    </button>
                `;
                    }
                }
            ],
            ordering: false,
            autoWidth: false,
            info: false,
            searching: false,
            paging: false,
            language: {
                decimal: "",
                emptyTable: "No hay información",
                loadingRecords: "Cargando...",
                processing: "Procesando...",
                zeroRecords: "Sin resultados encontrados",
            }
        });
    }

}

document.addEventListener("DOMContentLoaded", function () {
    // Inicializa el combo de empresas
    let detalleDataTable = $("#tbDetalles");

    cargarLaboratorio();

    //Asignar valores
    const fechaPlanificacion = $("#FechaPlanificacion").data('id');
    let fechaPlanificacionTemp;
    const today = new Date().toISOString().split('T')[0];
    if (fechaPlanificacion) {

        const [dia, mes, anio] = fechaPlanificacion.split(' ')[0].split('/');
        const fechaFormateada = `${anio}-${mes.padStart(2, '0')}-${dia.padStart(2, '0')}`;
        fechaPlanificacionTemp = fechaFormateada;
    } else {
        fechaPlanificacionTemp = today;
    }

    $("#FechaPlanificacion").val(fechaPlanificacionTemp);
    calcularRangoFecha();

    // Cambiar combo laboratorio
    $('#ComboModulo').on('change', function () {
        const moduloId = $(this).val(); // Obtener el id de la laboratorio seleccionada
        actualizarModuloEnte(moduloId); // Llamar a la función para actualizar las moduloLaboratorios
    });


    cargarTabla(detalleDataTable);

    $("#btnAgregarDetalle").click(function () {
        const idLaboratorio = $('#ComboLaboratorio').val();

        if (idLaboratorio == null || idLaboratorio == 0) {
            toastDangerShow('Laboratorio es obligatorio');
            return;
        }
        cargarModulo(idLaboratorio);
        cargarEnteTecnico();
        limpiarModal();
        const data = null;
        AbrirModal(data);
    });


    $("#btnGuardarDetalle").click(function () {
        const idModulo = $('#ComboModulo').val();
        var selectedDataModulo = $('#ComboModulo').select2('data')[0];
        var nombreModulo = selectedDataModulo ? selectedDataModulo.text : null;
        const idTecnico = $('#ComboEnteTecnico').val();
        var selectedDataTecnico = $('#ComboEnteTecnico').select2('data')[0];
        var nombreTecnico = selectedDataTecnico ? selectedDataTecnico.text : null;
        const fechaSiembra = $('#FechaSiembraDetail').val();
        const fechaCosecha = $('#FechaCosecha').val();
        const cantidadFacturada = $('#CantidadFacturada').val();


        const botones = "<button type='button' class='btn btn-sm btn-primary'>Editar</button>"
            + "<button type='button' class='btn btn-sm btn-danger'>Eliminar</button>";

        const agregarDetalle = {
            Index: 0,
            Id: 0,
            IdModulo: idModulo,
            NombreModulo: nombreModulo,
            IdEnteTecnico: idTecnico,
            NombreEnteTecnico: nombreTecnico,
            FechaSiembra: fechaSiembra,
            FechaCosecha: fechaCosecha,
            CantidadFacturada: cantidadFacturada,
            Activo: true,

        };
        detalleSiembraTable.push(agregarDetalle);

        // Reordenar y asignar índices secuenciales
        detalleSiembraTable = detalleSiembraTable.map((item, index) => ({
            ...item,
            Index: index + 1
        }));

        detalleDataTable.DataTable({
            destroy: true,
            data: detalleSiembraTable,
            columns: [
                { data: 'NombreModulo', className: 'text-center' },
                { data: 'FechaSiembra', className: 'text-start' },
                { data: 'FechaCosecha', className: 'text-start' },
                { data: 'CantidadFacturada', className: 'text-start' },
                { data: 'NombreEnteTecnico', className: 'text-start' },
                {
                    data: null,
                    className: 'text-center',
                    orderable: false,
                    render: function (data, type, row) {
                        return `<div>
                    <button class="btn btn-warning btn-sm editar" data-id="${row.Id}">
                        <i class="fas fa-edit"></i> Editar
                    </button>
                    <button class="btn btn-danger btn-sm eliminar" data-id="${row.Id}">
                        <i class="fas fa-trash"></i> Eliminar
                    </button></div>
                `;
                    }
                }
            ],
            language: {
                url: '//cdn.datatables.net/plug-ins/1.13.6/i18n/es-ES.json'
            }
        });
        //<button class="btn btn-warning btn-sm editar" data-id="${row.Id}" onclick="AbrirModal(${Json.Serialize(row.data)})">


        $("#DetallesModal").modal("hide");
    });

    //Crear


    if ($("#botonCrearNuevo").length) {
        document.getElementById("botonCrearNuevo").addEventListener("click", function (event) {
            let formData = {
                IdLaboratorio: Number($("#ComboLaboratorio").val()),
                FechaPlanificacion: $("#FechaPlanificacion").val(),
                PlanificacionSiembraDetalle: detalleSiembraTable,
            };

            if (formData.IdLaboratorio == 0 || formData.FechaPlanificacion == ''
            ) {
                toastDangerShow('Completa todos los campos obligatorios.');
                return;
            }

            if (formData.PlanificacionSiembraDetalle == undefined || formData.PlanificacionSiembraDetalle.length == 0) {
                toastDangerShow('Detalles de planificación de siembra son obligatorios.');
                return;
            }

            $.ajax({
                url: '/PlanificacionSiembra/CrearPlanificacionSiembra',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(formData),
                success: function (response) {
                    if (response.success) {
                        toastSuccessShow(response.message);
                        window.location.href = "/PlanificacionSiembra/Index?mostrarMensajeExito=true"; // Redirigir tras éxito
                    } else {
                        toastDangerShow(response.message);
                    }
                },
                error: function (xhr) {
                    console.error(xhr.responseText);
                    toastDangerShow('Hubo un error al guardar el cupo');
                }
            });
        });
    }


    // Escuchar cambios en el input de fecha
    document.getElementById("FechaPlanificacion").addEventListener("change", calcularRangoFecha);
});

