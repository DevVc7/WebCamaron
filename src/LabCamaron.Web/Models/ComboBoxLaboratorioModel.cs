namespace LabCamaron.Web.Models
{
    public class ComboBoxLaboratorioModel
    {
        public int Orden { get; set; }
        public long Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public long IdEmpresa { get; set; }
    }
}