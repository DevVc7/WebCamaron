using LabCamaronWeb.Dto.Maestros.Categoria;
using LabCamaronWeb.Infraestructura.Modelo;
using LabCamaronWeb.Infraestructura.Utilidades.Http;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LabCamaronWeb.Servicios.Maestros.Servicios
{
    internal class SeCategoriaService(IConfiguration configuration, IOperacionHttpServicio operacionHttp) : ISeCategoriaService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IOperacionHttpServicio _operacionHttp = operacionHttp;

        public async Task<RespuestaGenericaVm> Actualizar(CategoriaVm.ActualizarCategoria actualizar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<CategoriaVm.ActualizarCategoria, RespuestaGenericaVm>(
                        _configuration["Microservicios:ActualizarCategoria"]!, actualizar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, actualizar);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaConsultaGenericaVm<CategoriaVm>> ConsultarPorId(CategoriaVm.ConsultarCategoria consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<CategoriaVm.ConsultarCategoria, RespuestaConsultaGenericaVm<CategoriaVm>>(
                        _configuration["Microservicios:ConsultarCategoriaCodigo"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaConsultasGenericaVm<CategoriaVm>> ConsultarTodos(CategoriaVm.ConsultarTodosCategoria consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<CategoriaVm.ConsultarTodosCategoria, RespuestaConsultasGenericaVm<CategoriaVm>>(
                        _configuration["Microservicios:ConsultarCategoria"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaGenericaVm> Crear(CategoriaVm.CrearCategoria crear)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<CategoriaVm.CrearCategoria, RespuestaGenericaVm>(
                        _configuration["Microservicios:CrearCategoria"]!, crear);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, crear);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaGenericaVm> Eliminar(CategoriaVm.EliminarCategoria eliminar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<CategoriaVm.EliminarCategoria, RespuestaGenericaVm>(
                        _configuration["Microservicios:EliminarCategoria"]!, eliminar);

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