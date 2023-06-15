using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_IntroduccionCSharp.Ejemplos
{
    public class Calculadora
    {
        public Calculadora()
        {
            Console.WriteLine("Calculadora Básica");

            Console.WriteLine("Ingrese el primer número:");
            double num1 = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Ingrese el segundo número:");
            double num2 = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Seleccione una operación:");
            Console.WriteLine("1. Suma (+)");
            Console.WriteLine("2. Resta (-)");
            Console.WriteLine("3. Multiplicación (*)");
            Console.WriteLine("4. División (/)");

            Console.Write("Ingrese el número que corresponde a la operación: ");
            int opcion = Convert.ToInt32(Console.ReadLine());

            double resultado = 0;

            switch (opcion)
            {
                case 1:
                    resultado = num1 + num2;
                    Console.WriteLine($"El resultado de la suma es: {resultado}");
                    break;
                case 2:
                    resultado = num1 - num2;
                    Console.WriteLine($"El resultado de la resta es: {resultado}");
                    break;
                case 3:
                    resultado = num1 * num2;
                    Console.WriteLine($"El resultado de la multiplicación es: {resultado}");
                    break;
                case 4:
                    if (num2 != 0)
                    {
                        resultado = num1 / num2;
                        Console.WriteLine($"El resultado de la división es: {resultado}");
                    }
                    else
                    {
                        Console.WriteLine("No se puede dividir entre cero.");
                    }
                    break;
                default:
                    Console.WriteLine("Operación no valida");
                    break;
            }

        }
    }
}
