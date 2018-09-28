using Ember.Clases;
using System;


namespace Ember
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            Automata Automata = new Automata();
            AutomataClassic AutomataClassic = new AutomataClassic();
            Program path = new Program();

            AutomataClassic.SetLexema(AutomataClassic.SetTokens());
            //Automata.SetLexema(Automata.SetTokens());
            foreach (var token in AutomataClassic.AST)
            {
                Console.WriteLine("Tipo: " + token.TokenType);
                Console.WriteLine("Valor: " + token.Value);
                Console.WriteLine(" ");

            }
            Console.ReadKey();
        }
    }
}
