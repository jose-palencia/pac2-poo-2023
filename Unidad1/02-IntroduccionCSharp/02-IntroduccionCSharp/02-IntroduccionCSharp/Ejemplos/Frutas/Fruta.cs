using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_IntroduccionCSharp.Ejemplos.Frutas
{
    public class Fruta
    {
        public string Nombre { get; set; }
        public string Color { get; set; }

        public Fruta(string nombre, string color)
        {
            this.Nombre = nombre;
            this.Color = color;
        }
    }
}
