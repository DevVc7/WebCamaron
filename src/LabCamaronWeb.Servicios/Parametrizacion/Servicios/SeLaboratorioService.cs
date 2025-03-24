using LabCamaronWeb.Dto.Parametrizacion.Laboratorio;
using LabCamaronWeb.Infraestructura.Modelo;
using LabCamaronWeb.Infraestructura.Utilidades.Http;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Parametrizacion.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LabCamaronWeb.Servicios.Parametrizacion.Servicios
{
    internal class SeLaboratorioService(IConfiguration configuration, IOperacionHttpServicio operacionHttp) : ISeLaboratorioService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IOperacionHttpServicio _operacionHttp = operacionHttp;

        public async Task<RespuestaGenericaVm> Actualizar(LaboratorioVm.ActualizarLaboratorio actualizar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<LaboratorioVm.ActualizarLaboratorio, RespuestaGenericaVm>(
                        _configuration["Microservicios:ActualizarLaboratorio"]!, actualizar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, actualizar);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaConsultaGenericaVm<LaboratorioVm>> ConsultarPorId(LaboratorioVm.ConsultarLaboratorio consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<LaboratorioVm.ConsultarLaboratorio, RespuestaConsultaGenericaVm<LaboratorioVm>>(
                        _configuration["Microservicios:ConsultarLaboratorioCodigo"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaConsultasGenericaVm<LaboratorioVm>> ConsultarTodos(LaboratorioVm.ConsultarTodosLaboratorio consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<LaboratorioVm.ConsultarTodosLaboratorio, RespuestaConsultasGenericaVm<LaboratorioVm>>(
                        _configuration["Microservicios:ConsultarLaboratorios"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaGenericaVm> Crear(LaboratorioVm.CrearLaboratorio crear)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<LaboratorioVm.CrearLaboratorio, RespuestaGenericaVm>(
                        _configuration["Microservicios:CrearLaboratorio"]!, crear);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, crear);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaGenericaVm> Eliminar(LaboratorioVm.EliminarLaboratorio eliminar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<LaboratorioVm.EliminarLaboratorio, RespuestaGenericaVm>(
                        _configuration["Microservicios:EliminarLaboratorio"]!, eliminar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, eliminar);
                return RespuestaGenericaVm.Excepcion();
            }
        }
    }
}