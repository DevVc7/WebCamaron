using Newtonsoft.Json;
using System.Text;

namespace LabCamaronWeb.Infraestructura.Utilidades.Json
{
    public static class JsonUtil
    {
        public static StringContent Serializar<T>(T objeto)
        {
            return new StringContent(
                JsonConvert.SerializeObject(objeto),
                Encoding.UTF8,
                "application/json"
            );
        }

        public static T? Deserializar<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}