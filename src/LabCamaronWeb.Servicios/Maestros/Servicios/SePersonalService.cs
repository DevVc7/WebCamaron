using LabCamaronWeb.Dto.Maestros.Personal;
using LabCamaronWeb.Infraestructura.Modelo;
using LabCamaronWeb.Infraestructura.Utilidades.Http;
using LabCamaronWeb.Infraestructura.Utilidades.Logger;
using LabCamaronWeb.Servicios.Maestros.Interfaces;
using Microsoft.Extensions.Configuration;
using static LabCamaronWeb.Dto.Maestros.Personal.PersonalVm;

namespace LabCamaronWeb.Servicios.Maestros.Servicios
{
    internal class SePersonalService(IConfiguration configuration, IOperacionHttpServicio operacionHttp) : ISePersonalService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IOperacionHttpServicio _operacionHttp = operacionHttp;

        public async Task<RespuestaGenericaVm> Actualizar(ActualizarPersonal actualizar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<ActualizarPersonal, RespuestaGenericaVm>(
                        _configuration["Microservicios:ActualizarPersonal"]!, actualizar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, actualizar);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaConsultaGenericaVm<PersonalVm>> ConsultarPorId(ConsultarPersonal consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<ConsultarPersonal, RespuestaConsultaGenericaVm<PersonalVm>>(
                        _configuration["Microservicios:ConsultarPersonalCodigo"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaConsultasGenericaVm<PersonalVm>> ConsultarTodos(ConsultarTodosPersonal consultar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<ConsultarTodosPersonal, RespuestaConsultasGenericaVm<PersonalVm>>(
                        _configuration["Microservicios:ConsultarPersonals"]!, consultar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaVm.Excepcion());
            }
        }

        public async Task<RespuestaGenericaVm> Crear(CrearPersonal crear)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<CrearPersonal, RespuestaGenericaVm>(
                        _configuration["Microservicios:CrearPersonal"]!, crear);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, crear);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaGenericaVm> Eliminar(EliminarPersonal eliminar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<EliminarPersonal, RespuestaGenericaVm>(
                        _configuration["Microservicios:EliminarPersonal"]!, eliminar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, eliminar);
                return RespuestaGenericaVm.Excepcion();
            }
        }

        public async Task<RespuestaGenericaVm> Activar(ActivarPersonal activar)
        {
            try
            {
                var respuesta = await _operacionHttp
                    .EjecutarServicioAutenticado<ActivarPersonal, RespuestaGenericaVm>(
                        _configuration["Microservicios:ActivarPersonal"]!, activar);

                return respuesta;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, activar);
                return RespuestaGenericaVm.Excepcion();
            }
        }
    }
}