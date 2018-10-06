using Ember.Clases;
using System;


namespace Ember
{
    class Program
    {
        static void Main(string[] args)
        {
            Token token;
            Automata automata = new Automata();

            while (true)
            {
                token = automata.SetLexema();
                switch (token.TokenType)
                {
                    case 1:
                        Console.WriteLine("Parentesis abierto  " + token.Value + '\n');
                        break;
                    case 2:
                        Console.WriteLine("Parentesis cerrado  " + token.Value + '\n');
                        break;
                    case 3:
                        Console.WriteLine("Operador Aritmetico  " + token.Value + '\n');
                        break;
                    case 4:
                        Console.WriteLine("Operador Relacional  " + token.Value + '\n');
                        break;
                    case 5:
                        Console.WriteLine("Asignacion  " + token.Value + '\n');
                        break;
                    case 6:
                        Console.WriteLine("Identificador " + token.Value + '\n');
                        break;
                    case 7:
                        Console.WriteLine("Numero Natural  " + token.Value + '\n');
                        break;
                    case 8:
                        Console.WriteLine("Error " + token.Value + '\n');
                        break;
                    case 9:
                        Console.WriteLine("Llave Abierta " + token.Value + '\n');
                        break;
                    case 10:
                        Console.WriteLine("Llave Cerrada " + token.Value + '\n');
                        break;
                    case 11:
                        Console.WriteLine("Fin De Declaracion " + token.Value + '\n');
                        break;
                    case 12:
                        Console.WriteLine("Palabra Reservada " + token.Value + '\n');
                        break;
                    case 13:
                        Console.WriteLine("Comentario " + token.Value + '\n');
                        break;
                    case 14:
                        Console.WriteLine("ComentarioMultilinea " + token.Value + '\n');
                        break;
                    case 15:
                        Console.WriteLine("Fin Del Fichero " + token.Value + '\n');
                        Console.ReadKey();
                        break;
                    case 16:
                        Console.WriteLine("Tipo De Dato " + token.Value + '\n');
                        break;
                    default:
                        return;
                }
            }
        }
    }
}
