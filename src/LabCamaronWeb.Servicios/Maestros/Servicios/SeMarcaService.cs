using LabCamaronWeb.Dto.Maestros.Marca;
using LabCamaronWeb.Infraestructura.Modelo;
using LabCamaronWeb.Infraestructura.Utilidades.Http;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LabCamaronWeb.Servicios.Maestros.Servicios
{
    internal class SeMarcaService(IConfiguration configuration, IOperacionHttpServicio operacionHttp) : ISeMarcaService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IOperacionHttpServicio _operacionHttp = operacionHttp;

        public async Task<RespuestaGenericaVm> Actualizar(MarcaVm.ActualizarMarca actualizar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<MarcaVm.ActualizarMarca, RespuestaGenericaVm>(
                        _configuration["Microservicios:ActualizarMarca"]!, actualizar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, actualizar);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaConsultaGenericaVm<MarcaVm>> ConsultarPorId(MarcaVm.ConsultarMarca consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<MarcaVm.ConsultarMarca, RespuestaConsultaGenericaVm<MarcaVm>>(
                        _configuration["Microservicios:ConsultarMarcaCodigo"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaConsultasGenericaVm<MarcaVm>> ConsultarTodos(MarcaVm.ConsultarTodosMarca consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<MarcaVm.ConsultarTodosMarca, RespuestaConsultasGenericaVm<MarcaVm>>(
                        _configuration["Microservicios:ConsultarMarca"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaGenericaVm> Crear(MarcaVm.CrearMarca crear)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<MarcaVm.CrearMarca, RespuestaGenericaVm>(
                        _configuration["Microservicios:CrearMarca"]!, crear);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, crear);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaGenericaVm> Eliminar(MarcaVm.EliminarMarca eliminar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<MarcaVm.EliminarMarca, RespuestaGenericaVm>(
                        _configuration["Microservicios:EliminarMarca"]!, eliminar);

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