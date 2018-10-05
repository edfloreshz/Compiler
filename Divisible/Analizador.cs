using System;
using System.Collections.Generic;
using System.Text;

namespace Divisible
{
    class Analizador
    {
        public bool LexicoGrafico(string input)
        {
            string inputOriginal = input;
            int state = 0;

            foreach (char number in input)
            {
                switch (state)
                {
                    case 0:
                        switch (number)
                        {
                            case '0':
                                state = 3;
                                break;
                            case '1':
                                state = 1;
                                break;
                            case '2':
                                state = 2;
                                break;
                            case '3':
                                state = 3;
                                break;
                            case '4':
                                state = 1;
                                break;
                            case '5':
                                state = 2;
                                break;
                            case '6':
                                state = 3;
                                break;
                            case '7':
                                state = 1;
                                break;
                            case '8':
                                state = 2;
                                break;
                            case '9':
                                state = 3;
                                break;
                            default:
                                break;
                        }
                        break;
                    case 1:
                        if (number.Equals('0') || number.Equals('3') || number.Equals('6') || number.Equals('9'))
                        {
                            state = 1;
                        }
                        else if(number.Equals('1') || number.Equals('4') || number.Equals('7'))
                        {
                            state = 2;
                        }
                        else if (number.Equals('2') || number.Equals('5') || number.Equals('8'))
                        {
                            state = 3;
                        }
                        break;
                    case 2:
                        if (number.Equals('0') || number.Equals('3') || number.Equals('6') || number.Equals('9'))
                        {
                            state = 2;
                        }
                        else if(number.Equals('1') || number.Equals('4') || number.Equals('7'))
                        {
                            state = 3;
                        }
                        else if (number.Equals('2') || number.Equals('5') || number.Equals('8'))
                        {
                            state = 1;
                        }
                        break;
                    case 3:
                        if (number.Equals('0') || number.Equals('3') || number.Equals('6') || number.Equals('9'))
                        {
                            state = 3;
                        }
                        else if (number.Equals('1') || number.Equals('4') || number.Equals('7'))
                        {
                            state = 1;
                        }
                        else if (number.Equals('2') || number.Equals('5') || number.Equals('8'))
                        {
                            state = 2;
                        }
                        break;
                    default:
                        break;
                }
            }

            if (state == 3)
            {
                Console.WriteLine("El numero " + inputOriginal + " es divisible entre 3");
                return true;
            }
            else
            {
                Console.WriteLine("El numero " + inputOriginal + " no es divisible entre 3");
                return false;
            }
        }
    }
}
