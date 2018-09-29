using System;
using System.Collections.Generic;
using System.Text;

namespace Divisible
{
    class Analizador
    {
        public bool LexicoGrafico(string input)
        {
            int state = 0;
            bool isDivisible = false;
            foreach (char number in input)
            {
                switch (state)
                {
                    case 0:
                        switch (number)
                        {
                            case '0':
                                isDivisible = true;
                                state = 3;
                                break;
                            case '1':
                                state = 1;
                                break;
                            case '2':
                                state = 2;
                                break;
                            case '3':
                                isDivisible = true;
                                state = 3;
                                break;
                            case '4':
                                state = 1;
                                break;
                            case '5':
                                state = 2;
                                break;
                            case '6':
                                isDivisible = true;
                                state = 3;
                                break;
                            case '7':
                                state = 1;
                                break;
                            case '8':
                                state = 2;
                                break;
                            case '9':
                                isDivisible = true;
                                state = 3;
                                break;
                            default:
                                break;
                        }
                        break;
                    case 1:
                        isDivisible = false;
                        state = 4;
                        break;
                    case 2:
                        isDivisible = false;
                        state = 4;
                        break;
                    case 3:
                        if (number.Equals('0') || number.Equals('3') || number.Equals('6') || number.Equals('9'))
                        {
                            isDivisible = true;
                            state = 3;
                        }
                        else
                        {
                            isDivisible = false;
                            state = 4;
                        }

                        break;
                    default:
                        break;
                }
            }
            if (isDivisible == true)
            {
                //Console.WriteLine("El numero " + input + " es divisible entre 3");
                //Console.ReadKey();
                return true;
            }
            else
            {
                //Console.WriteLine("El numero " + input + " no es divisible entre 3");
                //Console.ReadKey();
                return false;
            }
        }
    }
}
