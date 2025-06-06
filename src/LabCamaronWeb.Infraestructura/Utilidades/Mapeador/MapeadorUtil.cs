using System.Text.Json;

namespace LabCamaronWeb.Infraestructura.Utilidades.Mapeador
{
    public static class MapeadorUtil
    {
        public static TSalida Mapear<TSalida>(this object obj)
        {
            var json = JsonSerializer.Serialize(obj);
            return JsonSerializer.Deserialize<TSalida>(json)!;
        }
    }
}