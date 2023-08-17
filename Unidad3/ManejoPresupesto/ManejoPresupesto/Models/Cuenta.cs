using System.ComponentModel.DataAnnotations;

namespace ManejoPresupesto.Models
{
    public class Cuenta
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "El {0} es obligatorio")]
        [Display(Name = "Nombre de cuenta")]
        [StringLength(maximumLength: 50,
            MinimumLength = 3,
            ErrorMessage = "El {0} debe tener entre {2} y {1}")]
        public string Nombre { get; set; }

        [Display(Name = "Tipo de Cuenta")]
        [Required(ErrorMessage = "El {0} es obligatorio")]
        public int TipoCuentaId { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Debe ingresar un numero")]
        [RegularExpression("^-?\\d+(\\.\\d{1,2})?$", ErrorMessage = "Solo se permiten numeros en el balance")]
        public decimal Balance { get; set; }

        [Display(Name = "Descripción")]
        [StringLength(maximumLength: 100, 
            ErrorMessage = "El {0} debe tener un maximo de {1} letras")]
        public string Descripcion { get; set; }

        public string TipoCuenta { get; set; }
    }
}
