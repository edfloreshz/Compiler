using Ember.Clases;
using System;
using System.IO;
using System.Reflection;

namespace Ember
{
    class Program
    {
        public string input = File.ReadAllText(Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).Parent.FullName, "Input\\Input.txt"));

        static void Main(string[] args)
        {
            Automata Automata = new Automata();
            AutomataClassic AutomataClassic = new AutomataClassic();
            Program path = new Program();

            AutomataClassic.SetLexema(AutomataClassic.SetTokens());
            Automata.SetLexema(Automata.SetTokens());
            Console.ReadKey();
        }
    }
}
