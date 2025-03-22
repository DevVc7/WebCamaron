using LabCamaronWeb.Dto.Parametrizacion.Empresa;
using LabCamaronWeb.Infraestructura.Modelo;
using LabCamaronWeb.Infraestructura.Utilidades.Http;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Parametrizacion.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LabCamaronWeb.Servicios.Parametrizacion.Servicios
{
    internal class SeEmpresaService(IConfiguration configuration, IOperacionHttpServicio operacionHttp) : ISeEmpresaService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IOperacionHttpServicio _operacionHttp = operacionHttp;

        public async Task<RespuestaGenericaVm> Actualizar(EmpresaVm.ActualizarEmpresa actualizar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<EmpresaVm.ActualizarEmpresa, RespuestaGenericaVm>(
                        _configuration["Microservicios:ActualizarEmpresa"]!, actualizar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, actualizar);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaConsultaGenericaVm<EmpresaVm>> ConsultarPorId(EmpresaVm.ConsultarEmpresa consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<EmpresaVm.ConsultarEmpresa, RespuestaConsultaGenericaVm<EmpresaVm>>(
                        _configuration["Microservicios:ConsultarEmpresaCodigo"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaConsultasGenericaVm<EmpresaVm>> ConsultarTodos(EmpresaVm.ConsultarTodosEmpresa consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<EmpresaVm.ConsultarTodosEmpresa, RespuestaConsultasGenericaVm<EmpresaVm>>(
                        _configuration["Microservicios:ConsultarEmpresas"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaGenericaVm> Crear(EmpresaVm.CrearEmpresa crear)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<EmpresaVm.CrearEmpresa, RespuestaGenericaVm>(
                        _configuration["Microservicios:CrearEmpresa"]!, crear);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, crear);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaGenericaVm> Eliminar(EmpresaVm.EliminarEmpresa eliminar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<EmpresaVm.EliminarEmpresa, RespuestaGenericaVm>(
                        _configuration["Microservicios:EliminarEmpresa"]!, eliminar);

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