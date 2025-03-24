namespace LabCamaronWeb.Infraestructura.Constantes
{
    public static class Servidor
    {
        public const string ExitoComun = "00000";
        public const string ErrorComun = "00099";
        public const string ErrorBaseDatos = "00088";
        public const string Excepcion = "99999";

        public const string CodigoSesionInvalida = "00077";
        public const string CodigoRespuestaVacia = "00080";
        public const string CodigoRespuestaMicroservicio = "00081";
        public const string CodigoRespuestaTokenNoEncontrado = "00082";
        public const string CodigoMetodoNoAutorizado = "00083";
        public const string CodigoServicioNoDisponible = "00084";
        public const string CodigoExcepcionServicio = "00085";

        public const string MensajeRespuestaVacia = "El microservicio se ejecuto exitosamente, pero su respuesta está vacía";
        public const string MensajeRespuestaMicroservicio = "Error en la respuesta del microservicio";
        public const string MensajeExcepcion = "Ha ocurrido una excepción";
        public const string MensajeTokenNoEncontrado = "Error al recuperar el token desde sesión";
        public const string MensajeMetodoNoAutorizado = "Método de consulta no autorizado";
        public const string MensajeServicioNoDisponible = "El servicio solicitado no está disponible, por favor comuniquese con su administrador de sistemas";
        public const string MensajeExcepcionServicio = "Ocurrio una excepcion en el servicio solicitado, por favor comuniquese con su administrador de sistemas";

        public static readonly string[] CodigosNoLog =
        [
            ExitoComun,
            Excepcion
        ];

        public static readonly string[] CodigosErrorServicio =
        [
            CodigoRespuestaVacia,
            CodigoRespuestaMicroservicio,
            CodigoRespuestaTokenNoEncontrado,
            CodigoMetodoNoAutorizado,
            CodigoServicioNoDisponible,
            //CodigoExcepcionServicio
        ];
    }
}