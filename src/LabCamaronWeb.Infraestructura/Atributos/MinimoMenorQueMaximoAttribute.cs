using System.ComponentModel.DataAnnotations;

namespace LabCamaronWeb.Infraestructura.Atributos
{
    public class MinimoMenorQueMaximoAttribute : ValidationAttribute
    {
        private readonly string _propertyName;

        public MinimoMenorQueMaximoAttribute(string propertyName)
        {
            _propertyName = propertyName;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            // Si el valor máximo es nulo, no hay nada que validar
            if (value == null)
                return ValidationResult.Success!;

            // Obtener la propiedad correspondiente al valor mínimo
            var propiedad = validationContext.ObjectType.GetProperty(_propertyName);
            if (propiedad == null)
                return new ValidationResult($"Propiedad {_propertyName} no encontrada.");

            // Obtener el valor mínimo
            var valorMinimo = propiedad.GetValue(validationContext.ObjectInstance, null);
            if (valorMinimo == null)
                return ValidationResult.Success!;

            // Comparar utilizando IComparable (que implementan todos los tipos numéricos)
            try
            {
                // Intentamos usar comparaciones genéricas si es posible
                if (EsNumerico(valorMinimo) && EsNumerico(value))
                {
                    // Convertir a decimal como denominador común para comparaciones numéricas
                    decimal minDecimal = Convert.ToDecimal(valorMinimo);
                    decimal maxDecimal = Convert.ToDecimal(value);

                    if (minDecimal > maxDecimal)
                        return new ValidationResult(ErrorMessage ?? "El valor mínimo debe ser menor que el valor máximo.");
                }
                else if (valorMinimo is IComparable comparableMin)
                {
                    // Intentar comparación directa si los tipos son iguales
                    if (valorMinimo.GetType() == value.GetType())
                    {
                        if (comparableMin.CompareTo(value) > 0)
                            return new ValidationResult(ErrorMessage ?? "El valor mínimo debe ser menor que el valor máximo.");
                    }
                    else if (value is IComparable comparableMax)
                    {
                        // Si los tipos son diferentes pero compatibles
                        try
                        {
                            var convertedMin = Convert.ChangeType(valorMinimo, value.GetType());
                            if (((IComparable)convertedMin).CompareTo(value) > 0)
                                return new ValidationResult(ErrorMessage ?? "El valor mínimo debe ser menor que el valor máximo.");
                        }
                        catch
                        {
                            try
                            {
                                var convertedMax = Convert.ChangeType(value, valorMinimo.GetType());
                                if (comparableMin.CompareTo(convertedMax) > 0)
                                    return new ValidationResult(ErrorMessage ?? "El valor mínimo debe ser menor que el valor máximo.");
                            }
                            catch
                            {
                                return new ValidationResult($"No se pueden comparar los tipos {valorMinimo.GetType().Name} y {value.GetType().Name}.");
                            }
                        }
                    }
                }
                else
                {
                    return new ValidationResult($"Los valores deben ser comparables.");
                }
            }
            catch (Exception ex)
            {
                return new ValidationResult($"Error al comparar valores: {ex.Message}");
            }

            return ValidationResult.Success!;
        }

        private static bool EsNumerico(object? valor)
        {
            if (valor == null) return false;

            Type tipo = valor.GetType();
            return tipo == typeof(byte) ||
                   tipo == typeof(sbyte) ||
                   tipo == typeof(short) ||
                   tipo == typeof(ushort) ||
                   tipo == typeof(int) ||
                   tipo == typeof(uint) ||
                   tipo == typeof(long) ||
                   tipo == typeof(ulong) ||
                   tipo == typeof(float) ||
                   tipo == typeof(double) ||
                   tipo == typeof(decimal);
        }
    }
}