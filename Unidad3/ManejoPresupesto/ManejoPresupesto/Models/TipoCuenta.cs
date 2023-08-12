using ManejoPresupesto.Validaciones;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ManejoPresupesto.Models
{
    public class TipoCuenta: IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "La cantidad de letras debe ser entre 3 y 50")]
        [Display(Name = "Nombre de Tipo de Cuenta")]
        [Remote(action: "VerificarExisteTipoCuenta", controller: "TiposCuenta")]
        //[PrimeraLetraMayuscula]
        public string Nombre { get; set; }
        public int UsuarioId { get; set; }
        public int Orden { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            if (Nombre != null && Nombre.Length > 0)
            {
                var primeraLetra = Nombre.ToString()[0].ToString();

                if (primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe ser mayuscula",
                        new[] { nameof(Nombre)});
                }
            }
        }



        //[Display(Name = "Correo Eletrónico")]
        //[Required(ErrorMessage = "El campo {0} es obligatorio")]
        //[EmailAddress(ErrorMessage = "El campo {0} debe ser un correo valido")]
        //public string Email { get; set; }

        //[Required(ErrorMessage = "El campo {0} es obligatorio")]
        //[Range(minimum:18, maximum: 120, ErrorMessage = "El valor de {0} debe estar entre {1} y {2}")]
        //public int Edad { get; set; }

        //[Url(ErrorMessage = "El campo debe tener una URL valida")]
        //public string Url { get; set; }

        //[CreditCard(ErrorMessage = "La tarjeta no tiene un formato valido")]
        //public string TarjetaCredito { get; set; }
    }
}
