using System;
using System.Collections.Generic;
using System.Text;

namespace Divisible
{
    class Test
    {
        public void TestValues()
        {
            int[] testValues = new int[100000];
            Random randomNum = new Random();
            Analizador analizador = new Analizador();
            bool isDiv = false, passed = false;

            for (int i = 0; i < 100; i++)
            {
                testValues[i] = randomNum.Next(0,100000);
                if (testValues[i] % 3 == 0)
                {
                    isDiv = true;
                }
                if (isDiv == analizador.LexicoGrafico(testValues[i].ToString()))
                {
                    passed = true;
                }
            }
            if (passed == true)
            {
                Console.WriteLine("All passed");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("There were inconsistencies");
                Console.ReadKey();
            }
        }
    }
}
