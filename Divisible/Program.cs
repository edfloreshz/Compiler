using System;

namespace Divisible
{
    class Program
    {
        static void Main(string[] args)
        {
            bool again = true;
            string respuesta = "";
            while (again == true)
            {
                Analizador analizador = new Analizador();
                Test test = new Test();

                Console.WriteLine("Ingrese el numero que desea evaular");
                string input = Console.ReadLine();

                //test.TestValues();
                analizador.LexicoGrafico(input);
                Console.WriteLine("Desea evaluar otro numero? y/n ");
                respuesta = Console.ReadLine();
                if (respuesta != "y")
                {
                    again = false;
                }
            }
        }
    }
}
