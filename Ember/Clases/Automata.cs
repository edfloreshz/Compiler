using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ember.Clases
{
    class Automata
    {
        public int state = 0;
        Program path = new Program();

        public string[] SetTokens()
        {
            char[] separaciones = {' ' ,',', '\n', '\r', '(', ')', '[', ']', '{', '}' };
            string[] tokens = path.input.Split(separaciones);
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
                if (!Char.IsLetter(input.ElementAt(1)))
                {
                    return false;
                }
            }
            return true;
        }

        public List<Token> SetLexema(string[] tokens)
        {
            List<Token> AST = new List<Token>();
            while (true)
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
                                    state = 1;
                                    AST.Add(new Token("OperadorMayor", token));
                                    break;
                                case "<":
                                    state = 1;
                                    AST.Add(new Token("OperadorMenor", token));
                                    break;
                                case "=":
                                    state = 2;
                                    AST.Add(new Token("Asignacion", token));
                                    break;
                                case "!":
                                    state = 3;
                                    AST.Add(new Token("Negacion", token));
                                    break;
                                default:
                                    if (IsNumber(token))
                                    {
                                        AST.Add(new Token("Numero", token));
                                    }
                                    else if (IsLetter(token))
                                    {
                                        AST.Add(new Token("Word", token));
                                    }
                                    break;
                            }
                            break;
                        case 1:
                            switch (token)
                            {
                                case "=":
                                    AST[Convert.ToInt32(AST.LastOrDefault())].TokenType = AST[Convert.ToInt32(AST.LastOrDefault())].TokenType + "Igual";
                                    AST[Convert.ToInt32(AST.LastOrDefault())].Value = AST[Convert.ToInt32(AST.LastOrDefault())].Value + token;
                                    break;
                                default:
                                    //GetBack();
                                    break;
                            }
                            break;
                        case 2:
                            switch (token)
                            {
                                case "=":
                                    AST[Convert.ToInt32(AST.LastOrDefault())].TokenType = "Comparacion";
                                    AST[Convert.ToInt32(AST.LastOrDefault())].Value = AST[Convert.ToInt32(AST.LastOrDefault())].Value + token;
                                    break;
                                default:
                                    //GetBack();
                                    break;
                            }
                            break;
                        case 3:
                            switch (token)
                            {
                                case "=":
                                    AST[Convert.ToInt32(AST.LastOrDefault())].TokenType = "Desigual";
                                    AST[Convert.ToInt32(AST.LastOrDefault())].Value = AST[Convert.ToInt32(AST.LastOrDefault())].Value + token;
                                    break;
                                default:
                                    //GetBack();
                                    break;
                            }
                            break;
                        default:
                            AST.Add(new Token("Error", token));
                            break;
                    }
                }
            }
        }
    }
}
