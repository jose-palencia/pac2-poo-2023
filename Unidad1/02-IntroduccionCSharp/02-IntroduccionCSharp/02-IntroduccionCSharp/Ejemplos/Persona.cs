using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_IntroduccionCSharp.Ejemplos
{
    public class Persona
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Edad { get; set; }

        public Persona()
        {
            
        }

        public Persona(string nombre, string apellidos, int edad)
        {
            this.Nombres = nombre;
            this.Edad = edad;
            this.Apellidos = apellidos;
        }

        public void Saludar() 
        {
            Console.WriteLine($"¡Hola! Mi nombre es {this.Nombres} {this.Apellidos} y tengo {Edad} años.");
        }
    }
}
