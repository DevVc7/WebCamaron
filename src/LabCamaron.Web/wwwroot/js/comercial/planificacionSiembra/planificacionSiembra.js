'use strict';
let isEditDetail = false;
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

    $('#ComboEnteTecnico').data('id', "");
    $('#ComboEnteTecnico').data('text', "");

    $('#ComboEnteTecnico').empty().trigger('change');
    cargarEnteTecnico();
}

function obtenerMesAnio(fecha) {
    let date = new Date(fecha);

    let mes = date.toLocaleString('es-ES', { month: 'short' }).replace('.', ''); // Obtener el mes abreviado en español
    let dia = date.getDate();

    return `${mes.charAt(0).toUpperCase() + mes.slice(1)}-${dia}`; // Asegurar mayúscula inicial
}

function calcularRangoFecha() {

    if (detalleSiembraTable.length > 0) {
        let fechaMinSiembra = new Date(Math.min(...detalleSiembraTable.map(item => new Date(item.fechaSiembra.includes("T") ? item.fechaSiembra : item.fechaSiembra + "T00:00:00"))));
        let fechaMaxCosecha = new Date(Math.max(...detalleSiembraTable.map(item => new Date(item.fechaCosecha.includes("T") ? item.fechaCosecha : item.fechaCosecha + "T00:00:00"))));

        if (fechaMinSiembra && fechaMaxCosecha) {
            let textoFormateado = `${obtenerMesAnio(fechaMinSiembra)} a ${obtenerMesAnio(fechaMaxCosecha)}`;
            document.getElementById("lblFechaPlanificaion").value = textoFormateado;
        }
    }
}

function calcularRangoFechaDetail() {
    let fechaImput = $('#FechaSiembraDetail').val()
    if (fechaImput != "" && fechaImput != null) {
        let fecha = new Date(fechaImput);
        fecha.setDate(fecha.getDate() + 20);
        let fechaFormateada = fecha.toISOString().split('T')[0];
        $('#FechaCosecha').val(fechaFormateada)
    } else {
        $('#FechaCosecha').val("")

    }

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
    $('#Index').val();
}

function eliminarFila(index) {
    detalleSiembraTable = detalleSiembraTable.filter((item, i) => item.index !== index);

    cargarTabla();
}

function abrirModal(idModulo, index) {

    if (idModulo != null) {
        isEditDetail = true;
        const data = detalleSiembraTable.find(item => item.index === index);

        if (data != null) {

            const idLaboratorio = $('#ComboLaboratorio').val();

            if (idLaboratorio == null || idLaboratorio == 0) {
                toastDangerShow('Laboratorio es obligatorio');
                return;
            }

            $('#ComboModulo').data('id', data.idModulo);
            $('#ComboModulo').data('text', data.nombreModulo);
            $('#ComboModulo').prop('disabled', true);

            cargarModulo(idLaboratorio);
            $('#ComboEnteTecnico').data('id', data.idEnteTecnico);
            $('#ComboEnteTecnico').data('text', data.nombreEnteTecnico);
            $('#ComboEnteTecnico').prop('disabled', true);

            cargarEnteTecnico();


            $('#CantidadFacturada').val(data.cantidadFacturada);

            $('#FechaSiembraDetail').val(data.fechaSiembra.toStringJsonFormatted());
            $('#FechaCosecha').val(data.fechaCosecha.toStringJsonFormatted());
            $('#Index').val(data.index);
        }
    } else {
        isEditDetail = false;
        $('#ComboModulo').prop('disabled', false);
        $('#ComboEnteTecnico').prop('disabled', false);

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
        $('#Index').val(index);

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

function cargarTabla() {

    // Reordenar y asignar �ndices secuenciales
    detalleSiembraTable = detalleSiembraTable.map((item, index) => ({
        ...item,
        index: index + 1
    }));

    if (detalleSiembraTable) {

        $("#tbDetalles").DataTable({
            destroy: true,
            data: detalleSiembraTable,
            columns: [
                { data: 'nombreModulo', className: 'text-center' },
                {
                    data: 'fechaSiembra', className: 'text-start',
                    render: function (data, type, row) {
                        return `<label>${row.fechaSiembra.toStringJsonFormatted()}</label>`
                    }
                },
                {
                    data: 'fechaCosecha', className: 'text-start',
                    render: function (data, type, row) {
                        return `<label>${row.fechaCosecha.toStringJsonFormatted()}</label>`
                    }
                },
                { data: 'cantidadFacturada', className: 'text-start' },
                { data: 'nombreEnteTecnico', className: 'text-start' },
                {
                    data: null,
                    className: 'text-center',
                    orderable: false,
                    render: function (data, type, row) {
                        return `<div>
                    <button class="btn btn-warning btn-sm editar" data-id="${row.id}" onclick="abrirModal(${row.idModulo},${row.index})">
                        <i class="fas fa-edit"></i> Editar
                    </button>
                    <button class="btn btn-danger btn-sm eliminar" data-id="${row.id}" onclick="eliminarFila(${row.index})">
                        <i class="fas fa-trash"></i> Eliminar
                    </button></div>
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
    //const fechaPlanificacion = $("#FechaPlanificacion").data('id');
    //let fechaPlanificacionTemp;
    //const today = new Date().toISOString().split('T')[0];
    //if (fechaPlanificacion) {

    //    const [dia, mes, anio] = fechaPlanificacion.split(' ')[0].split('/');
    //    const fechaFormateada = `${anio}-${mes.padStart(2, '0')}-${dia.padStart(2, '0')}`;
    //    fechaPlanificacionTemp = fechaFormateada;
    //} else {
    //    fechaPlanificacionTemp = today;
    //}

    //$("#FechaPlanificacion").val(fechaPlanificacionTemp);

    // Cambiar combo laboratorio
    $('#ComboModulo').on('change', function () {
        const moduloId = $(this).val(); // Obtener el id de la laboratorio seleccionada
        actualizarModuloEnte(moduloId); // Llamar a la función para actualizar las moduloLaboratorios
    });


    cargarTabla();
    calcularRangoFecha();

    $("#btnAgregarDetalle").click(function () {
        const idLaboratorio = $('#ComboLaboratorio').val();

        if (idLaboratorio == null || idLaboratorio == 0) {
            toastDangerShow('Laboratorio es obligatorio');
            return;
        }
        cargarModulo(idLaboratorio);
        cargarEnteTecnico();
        limpiarModal();
        abrirModal(null, null);
    });


    $("#btnGuardarDetalle").click(function () {
        const index = $('#Index').val();
        const idModulo = $('#ComboModulo').val();
        var selectedDataModulo = $('#ComboModulo').select2('data')[0];
        var nombreModulo = selectedDataModulo ? selectedDataModulo.text : null;
        const idTecnico = $('#ComboEnteTecnico').val();
        var selectedDataTecnico = $('#ComboEnteTecnico').select2('data')[0];
        var nombreTecnico = selectedDataTecnico ? selectedDataTecnico.text : null;
        const fechaSiembra = $('#FechaSiembraDetail').val();
        const fechaCosecha = $('#FechaCosecha').val();
        const cantidadFacturada = $('#CantidadFacturada').val();

        if (idModulo == 0 || idTecnico == 0 || idModulo == null || idTecnico == null || cantidadFacturada <= 0) {
            toastDangerShow('Completa todos los campos obligatorios.');
            return;
        }

        const dataIndex = detalleSiembraTable.findIndex(item => item.index == index);

        if (isEditDetail && dataIndex !== -1) {
            //Validamos si estpa dentro del ranfo permitido
            const dataValidIndex = detalleSiembraTable.findIndex(item =>
                item.idModulo === idModulo && item.index != index &&
                (
                    (new Date(fechaSiembra) >= new Date(item.fechaSiembra) && new Date(fechaSiembra) <= new Date(item.fechaCosecha)) ||
                    (new Date(fechaCosecha) >= new Date(item.fechaSiembra) && new Date(fechaCosecha) <= new Date(item.fechaCosecha)) ||
                    (new Date(fechaSiembra) <= new Date(item.fechaSiembra) && new Date(fechaCosecha) >= new Date(item.fechaCosecha))
                )
            );
            if (dataValidIndex !== -1) {
                toastDangerShow(`No se puede ingresar el Modulo ${nombreModulo} porque ya existe dentro del rango ${fechaSiembra} a ${fechaCosecha}`);
                return;
            }
        } else {
            //Validamos si estpa dentro del ranfo permitido
            const dataValidIndex = detalleSiembraTable.findIndex(item =>
                item.idModulo === idModulo &&
                (
                    (new Date(fechaSiembra) >= new Date(item.fechaSiembra) && new Date(fechaSiembra) <= new Date(item.fechaCosecha)) ||
                    (new Date(fechaCosecha) >= new Date(item.fechaSiembra) && new Date(fechaCosecha) <= new Date(item.fechaCosecha)) ||
                    (new Date(fechaSiembra) <= new Date(item.fechaSiembra) && new Date(fechaCosecha) >= new Date(item.fechaCosecha))
                )
            );
            if (dataValidIndex !== -1) {
                toastDangerShow(`No se puede ingresar el Modulo ${nombreModulo} porque ya existe dentro del rango ${fechaSiembra} a ${fechaCosecha}`);
                return;
            }
        }

        if (isEditDetail && dataIndex !== -1) {

            detalleSiembraTable[dataIndex] = {
                ...detalleSiembraTable[dataIndex],
                idEnteTecnico: idTecnico,
                nombreEnteTecnico: nombreTecnico,
                fechaSiembra: fechaSiembra,
                fechaCosecha: fechaCosecha,
                cantidadFacturada: cantidadFacturada
            };

        } else {
            const agregarDetalle = {
                index: 0,
                id: 0,
                idModulo: idModulo,
                nombreModulo: nombreModulo,
                idEnteTecnico: idTecnico,
                nombreEnteTecnico: nombreTecnico,
                fechaSiembra: fechaSiembra,
                fechaCosecha: fechaCosecha,
                cantidadFacturada: cantidadFacturada,
                activo: true,

            };

            detalleSiembraTable.push(agregarDetalle);

        }

        // Reordenar y asignar índices secuenciales
        detalleSiembraTable = detalleSiembraTable.map((item, index) => ({
            ...item,
            index: index + 1
        }));
        calcularRangoFecha();

        cargarTabla();

        $("#DetallesModal").modal("hide");
    });

    //Crear


    if ($("#botonCrearNuevo").length) {
        document.getElementById("botonCrearNuevo").addEventListener("click", function (event) {
            let formData = {
                IdLaboratorio: Number($("#ComboLaboratorio").val()),
                Codigo: $("#Codigo").val(),
                FechaPlanificacion: new Date(),//$("#FechaPlanificacion").val(),
                PlanificacionSiembraDetalle: detalleSiembraTable,
            };

            if (formData.IdLaboratorio == 0) {
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


    if ($("#botonEditar").length) {
        document.getElementById("botonEditar").addEventListener("click", function (event) {
            let formData = {
                Id: $("#id").val(),
                IdLaboratorio: Number($("#ComboLaboratorio").val()),
                Codigo: $("#Codigo").val(),
                FechaPlanificacion: new Date(),//$("#FechaPlanificacion").val(),
                //FechaPlanificacion: $("#FechaPlanificacion").val(),
                PlanificacionSiembraDetalle: detalleSiembraTable,
                Activo: true,
            };

            if (formData.IdLaboratorio == 0) {
                toastDangerShow('Completa todos los campos obligatorios.');
                return;
            }

            if (formData.PlanificacionSiembraDetalle == undefined || formData.PlanificacionSiembraDetalle.length == 0) {
                toastDangerShow('Detalles de planificación de siembra son obligatorios.');
                return;
            }

            $.ajax({
                url: '/PlanificacionSiembra/EditarPlanificacionSiembra',
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
    //document.getElementById("FechaPlanificacion").addEventListener("change", calcularRangoFecha);
    document.getElementById("FechaSiembraDetail").addEventListener("change", calcularRangoFechaDetail);
});

