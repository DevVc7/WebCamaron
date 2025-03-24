using System.ComponentModel.DataAnnotations;

namespace LabCamaronWeb.Infraestructura.Atributos
{
    public class RangoEntreAttribute(string propiedadMinima, string propiedadMaximo) : ValidationAttribute
    {
        private readonly string _propertyMinima = propiedadMinima;
        private readonly string _propertyMaxima = propiedadMaximo;

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var propiedadMinima = validationContext.ObjectType.GetProperty(_propertyMinima);
            if (propiedadMinima == null)
            {
                return new ValidationResult($"Propiedad {_propertyMinima} no encontrada.");
            }

            var propiedadMaxima = validationContext.ObjectType.GetProperty(_propertyMaxima);
            if (propiedadMaxima == null)
            {
                return new ValidationResult($"Propiedad {_propertyMaxima} no encontrada.");
            }

            var valorMinimo = (float?)propiedadMinima.GetValue(validationContext.ObjectInstance, null);
            var valorMaximo = (float?)propiedadMaxima.GetValue(validationContext.ObjectInstance, null);
            var valorEntre = (float?)value;

            if (valorEntre < valorMinimo || valorEntre > valorMaximo)
            {
                return new ValidationResult(ErrorMessage ?? "El valor debe estar entre el rango indicado.");
            }

            return ValidationResult.Success!;
        }
    }
}