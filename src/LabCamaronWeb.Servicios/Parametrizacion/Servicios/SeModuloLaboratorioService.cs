using LabCamaronWeb.Dto.Parametrizacion.ModuloLaboratorio;
using LabCamaronWeb.Infraestructura.Modelo;
using LabCamaronWeb.Infraestructura.Utilidades.Http;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Parametrizacion.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LabCamaronWeb.Servicios.Parametrizacion.Servicios
{
    internal class SeModuloLaboratorioService(IConfiguration configuration, IOperacionHttpServicio operacionHttp) : ISeModuloLaboratorioService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IOperacionHttpServicio _operacionHttp = operacionHttp;

        public async Task<RespuestaGenericaVm> Actualizar(ModuloLaboratorioVm.ActualizarModuloLaboratorio actualizar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<ModuloLaboratorioVm.ActualizarModuloLaboratorio, RespuestaGenericaVm>(
                        _configuration["Microservicios:ActualizarModuloLaboratorio"]!, actualizar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, actualizar);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaConsultaGenericaVm<ModuloLaboratorioVm>> ConsultarPorId(ModuloLaboratorioVm.ConsultarModuloLaboratorio consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<ModuloLaboratorioVm.ConsultarModuloLaboratorio, RespuestaConsultaGenericaVm<ModuloLaboratorioVm>>(
                        _configuration["Microservicios:ConsultarModuloLaboratorioCodigo"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaConsultasGenericaVm<ModuloLaboratorioVm>> ConsultarTodos(ModuloLaboratorioVm.ConsultarTodosModuloLaboratorio consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<ModuloLaboratorioVm.ConsultarTodosModuloLaboratorio, RespuestaConsultasGenericaVm<ModuloLaboratorioVm>>(
                        _configuration["Microservicios:ConsultarModuloLaboratorios"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaGenericaVm> Crear(ModuloLaboratorioVm.CrearModuloLaboratorio crear)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<ModuloLaboratorioVm.CrearModuloLaboratorio, RespuestaGenericaVm>(
                        _configuration["Microservicios:CrearModuloLaboratorio"]!, crear);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, crear);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaGenericaVm> Eliminar(ModuloLaboratorioVm.EliminarModuloLaboratorio eliminar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<ModuloLaboratorioVm.EliminarModuloLaboratorio, RespuestaGenericaVm>(
                        _configuration["Microservicios:EliminarModuloLaboratorio"]!, eliminar);

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