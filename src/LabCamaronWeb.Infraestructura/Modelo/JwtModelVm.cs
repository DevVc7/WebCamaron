namespace LabCamaronWeb.Infraestructura.Modelo
{
    public class JwtModelVm
    {
        public string Token { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}