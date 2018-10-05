using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Ember.Clases
{
    class Automata
    {
        public int state = 0;
        public List<Token> AST = new List<Token>();

        public static bool IsUnix
        {
            get
            {
                int p = (int)Environment.OSVersion.Platform;
                return (p == 4) || (p == 6) || (p == 128);
            }
        }

        public string[] SetTokens()
        {
            string input = IsUnix
                ? File.ReadAllText(Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).Parent.FullName, "GitHub/Ember/Ember/Input/Input.txt"))
                : File.ReadAllText(Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).Parent.FullName, "Input\\Input.txt"));

            char[] separaciones = {' ' ,',', '\n', '\r', '(', ')', '[', ']', '{', '}' };
            string[] tokens = input.Split(separaciones);
            return tokens;
        }

        bool IsNumber(string s)
        {
            foreach (char c in s)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return s.Any();
        }
        bool IsLetter(String input)
        {
            for (int i = 0; i != input.Length; ++i)
            {
                if (!Char.IsLetter(input.ElementAt(i)))
                {
                    return false;
                }
            }
            return true;
        }

        public List<Token> SetLexema(string[] tokens)
        {
                foreach (var token in tokens)
                {
                    switch (state)
                    {
                        case 0:
                            switch (token)
                            {
                                case "(":
                                    AST.Add(new Token("ParentesisAbierto", token));
                                    break;
                                case ")":
                                    AST.Add(new Token("ParentesisCerrado", token));
                                    break;
                                case "{":
                                    AST.Add(new Token("LlaveAbierta", token));
                                    break;
                                case "}":
                                    AST.Add(new Token("LlaveCerrada", token));
                                    break;
                                case "[":
                                    AST.Add(new Token("CorcheteAbierto", token));
                                    break;
                                case "]":
                                    AST.Add(new Token("CorcheteCerrado", token));
                                    break;
                                case "+":
                                    AST.Add(new Token("Adicion", token));
                                    break;
                                case "-":
                                    AST.Add(new Token("Substraccion", token));
                                    break;
                                case "*":
                                    AST.Add(new Token("Multiplicacion", token));
                                    break;
                                case "/":
                                    AST.Add(new Token("Division", token));
                                    break;
                                case ">":
                                    AST.Add(new Token("OperadorMayor", token));
                                    break;
                                case "<":
                                    AST.Add(new Token("OperadorMenor", token));
                                    break;
                                case "=":
                                    AST.Add(new Token("Asignacion", token));
                                    break;
                                case "==":
                                    AST.Add(new Token("Comparacion", token));
                                    break;
                                case "!=":
                                    AST.Add(new Token("Desigual", token));
                                    break;
                                case "!":
                                    AST.Add(new Token("Negacion", token));
                                    break;
                                case ";":
                                    AST.Add(new Token("FinDeLinea", token));
                                    break;
                                default:
                                    if (IsNumber(token))
                                    {
                                        AST.Add(new Token("Numero", token));
                                    }
                                    else if (IsLetter(token))
                                    {
                                        AST.Add(new Token("Identificador", token));
                                    }
                                    break;
                            }
                            break;
                        default:
                            AST.Add(new Token("Error", token));
                            break;
                }
            }
            return AST;
        }
    }
}
