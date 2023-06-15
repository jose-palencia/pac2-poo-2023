using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_IntroduccionCSharp.Ejemplos
{
    public class GeneradorPassword
    {
        public GeneradorPassword()
        {
            Console.WriteLine("Generador de Contraseñas");
            Console.WriteLine("Ingrese la longitud que espera de la contraseña:");

            int longitud = Convert.ToInt32(Console.ReadLine());
            string password = GenerarClave(longitud);

            Console.WriteLine("Contraseña generada: " + password);
            Console.ReadLine();
        }

        private string GenerarClave(int longitud) 
        {
            const string caracteresMayusculas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string caractreresMinusculas = "abcdefghijklmnopqrstuvwxyz";
            const string numeros = "0123456789";
            const string caracteresEspeciales = "!@#$%&*()_-=+<>?";

            StringBuilder password = new StringBuilder();
            Random random = new Random();

            password.Append(caracteresMayusculas[random.Next(caracteresMayusculas.Length)]);
            password.Append(caractreresMinusculas[random.Next(caractreresMinusculas.Length)]);
            password.Append(numeros[random.Next(numeros.Length)]);
            password.Append(caracteresEspeciales[random.Next(caracteresEspeciales.Length)]);

            for (int i = 4; i < longitud; i++) 
            {
                string caracteresDisponibles = caracteresMayusculas 
                    + caractreresMinusculas + numeros + caracteresEspeciales;
                password.Append(caracteresDisponibles[random.Next(caracteresDisponibles.Length)]);
            }

            // Mezclar la contraseña
            for (int i = 0; i < password.Length; i++)
            {
                int posicionAleatoria = random.Next(password.Length);
                char temp = password[i];
                password[i] = password[posicionAleatoria];
                password[posicionAleatoria] = temp;
            }

            return password.ToString();
        }
    }
}
