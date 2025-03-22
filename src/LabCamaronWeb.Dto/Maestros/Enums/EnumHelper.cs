namespace LabCamaronWeb.Dto.Maestros.Enums
{
    public static class EnumHelper
    {
        #region TipoColor

        private const string HEXADECIMAL = "HEX";
        private const string RGB = "RGB";

        public static string ConvertirEnumToString(this TipoColor tipoColor)
        {
            return tipoColor switch
            {
                TipoColor.HEXADECIMAL => HEXADECIMAL,
                TipoColor.RGB => RGB,
                _ => throw new NotImplementedException()
            };
        }

        public static TipoColor ConvertStringToEnum(string tipoColor)
        {
            return tipoColor switch
            {
                HEXADECIMAL => TipoColor.HEXADECIMAL,
                RGB => TipoColor.RGB,
                _ => throw new NotImplementedException()
            };
        }

        #endregion TipoColor
    }
}