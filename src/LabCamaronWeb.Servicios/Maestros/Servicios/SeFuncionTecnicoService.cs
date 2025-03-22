using LabCamaronWeb.Dto.Maestros.FuncionTecnico;
using LabCamaronWeb.Infraestructura.Modelo;
using LabCamaronWeb.Infraestructura.Utilidades.Http;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LabCamaronWeb.Servicios.Maestros.Servicios
{
    internal class SeFuncionTecnicoService(IConfiguration configuration, IOperacionHttpServicio operacionHttp) : ISeFuncionTecnicoService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IOperacionHttpServicio _operacionHttp = operacionHttp;

        public async Task<RespuestaGenericaVm> Actualizar(FuncionTecnicoVm.ActualizarFuncionTecnico actualizar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<FuncionTecnicoVm.ActualizarFuncionTecnico, RespuestaGenericaVm>(
                        _configuration["Microservicios:ActualizarFuncionTecnico"]!, actualizar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, actualizar);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaConsultaGenericaVm<FuncionTecnicoVm>> ConsultarPorId(FuncionTecnicoVm.ConsultarFuncionTecnico consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<FuncionTecnicoVm.ConsultarFuncionTecnico, RespuestaConsultaGenericaVm<FuncionTecnicoVm>>(
                        _configuration["Microservicios:ConsultarFuncionTecnicoCodigo"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaConsultasGenericaVm<FuncionTecnicoVm>> ConsultarTodos(FuncionTecnicoVm.ConsultarTodosFuncionTecnico consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<FuncionTecnicoVm.ConsultarTodosFuncionTecnico, RespuestaConsultasGenericaVm<FuncionTecnicoVm>>(
                        _configuration["Microservicios:ConsultarFuncionesTecnico"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaGenericaVm> Crear(FuncionTecnicoVm.CrearFuncionTecnico crear)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<FuncionTecnicoVm.CrearFuncionTecnico, RespuestaGenericaVm>(
                        _configuration["Microservicios:CrearFuncionTecnico"]!, crear);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, crear);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaGenericaVm> Eliminar(FuncionTecnicoVm.EliminarFuncionTecnico eliminar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<FuncionTecnicoVm.EliminarFuncionTecnico, RespuestaGenericaVm>(
                        _configuration["Microservicios:EliminarFuncionTecnico"]!, eliminar);

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