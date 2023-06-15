using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_IntroduccionCSharp.Ejemplos
{
    public class Vocales
    {
        public Vocales()
        {
            Console.WriteLine("Ingrese una cadena de texto: ");
            string texto = Console.ReadLine();

            int contadorVocales = 0;

            foreach (var caracter in texto)
            {
                if (EsVocal(caracter))
                {
                    contadorVocales++;
                }
            }

            Console.WriteLine($"El número de vocales en el texto es: {contadorVocales}");
            Console.ReadLine();

        }

        private bool EsVocal(char caracter) 
        {
            return "aeiouáéíóúAEIOUÁÉÍÓÚ".Contains(caracter);
        }
    }
}
