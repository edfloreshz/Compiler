using System;

namespace Divisible
{
    class Program
    {
        static void Main(string[] args)
        {
            Analizador analizador = new Analizador();
            Test test = new Test();

            //Console.WriteLine("Ingrese el numero que desea evaular");
            //string input = Console.ReadLine();
            
            test.TestValues();
            //analizador.LexicoGrafico(input);
        }
    }
}
