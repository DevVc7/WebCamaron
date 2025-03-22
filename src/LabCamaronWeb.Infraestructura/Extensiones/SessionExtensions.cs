using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;

namespace LabCamaronWeb.Infraestructura.Extensiones
{
    public static class SessionExtensions
    {
        // Método para guardar un valor de tipo genérico en la sesión
        public static void Agregar<T>(this ISession session, string key, T value)
        {
            // Convertir el objeto en un arreglo de bytes
            var json = JsonConvert.SerializeObject(value);
            var bytes = Encoding.UTF8.GetBytes(json);

            session.Set(key, bytes);
        }

        // Método para eliminar un valor de tipo genérico en la sesión
        public static void Eliminar(this ISession session, string key)
        {
            session.Remove(key);
        }

        // Método para obtener un valor de tipo genérico desde la sesión
        public static T? Obtener<T>(this ISession session, string key)
        {
            // Obtener el valor como una cadena
            var isValid = session.TryGetValue(key, out var value);
            if (!isValid)
            {
                return default; // Devuelve el valor predeterminado de T si no existe el valor en la sesión
            }
            // Deserializar el valor y devolverlo como el tipo deseado
            var json = Encoding.UTF8.GetString(value);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}