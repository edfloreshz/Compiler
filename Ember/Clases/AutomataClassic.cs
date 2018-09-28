using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;


namespace Ember.Clases
{
    class AutomataClassic
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

        public char[] SetTokens()
        {
            string input = IsUnix
                ? File.ReadAllText(Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).Parent.FullName, "GitHub/Ember/Ember/Input/Input.txt"))
                : File.ReadAllText(Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).Parent.FullName, "Input\\Input.txt"));

            input = input + "$";
            char[] tokens = new char[1000];
            int i = 0;
            foreach (char caracter in input)
            {
                tokens[i] = caracter;
                i++;
            }
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
        
        public List<Token> SetLexema(char[] tokens)
        {
            foreach (var token in tokens)
            {
                switch (state)
                {
                    case 0:
                        switch (token)
                        {
                            case '\0':
                                state = 4;
                                break;
                            case '\n':
                                state = 0;
                                break;
                            case ' ':
                                break;
                            case '(':
                                AST.Add(new Token("ParentesisAbierto", token.ToString()));
                                break;
                            case ')':
                                AST.Add(new Token("ParentesisCerrado", token.ToString()));
                                break;
                            case '{':
                                AST.Add(new Token("LlaveAbierta", token.ToString()));
                                break;
                            case '}':
                                AST.Add(new Token("LlaveCerrada", token.ToString()));
                                break;
                            case '"':
                                AST.Add(new Token("Comillas", token.ToString()));
                                break;
                            case ';':
                                AST.Add(new Token("FinDeLinea", token.ToString()));
                                break;
                            case ':':
                                AST.Add(new Token("DosPuntos", token.ToString()));
                                break;
                            case '+':
                                AST.Add(new Token("Adición", token.ToString()));
                                break;
                            case '-':
                                AST.Add(new Token("Sustracción", token.ToString()));
                                break;
                            case '*':
                                AST.Add(new Token("Multiplicación", token.ToString()));
                                break;
                            case '/':
                                AST.Add(new Token("División", token.ToString()));
                                break;
                            case '>':
                                state = 1;
                                AST.Add(new Token("OperadorMayor", token.ToString()));
                                break;
                            case '<':
                                state = 1;
                                AST.Add(new Token("OperadorMenor", token.ToString()));
                                break;
                            case '=':
                                state = 2;
                                AST.Add(new Token("Asignacion", token.ToString()));
                                break;
                            case '!':
                                state = 3;
                                AST.Add(new Token("Negacion", token.ToString()));
                                break;
                            default:
                                if (IsNumber(token.ToString()))
                                {
                                    AST.Add(new Token("Numero", token.ToString()));
                                    state = 1;
                                }
                                else if (IsLetter(token.ToString()))
                                {
                                    state = 1;
                                    if (token != ' ')
                                    {
                                        AST.Add(new Token("Identificador", token.ToString()));
                                    }
                                    else
                                    {
                                        state = 0;
                                    }
                                }
                                else if (AST.LastOrDefault().TokenType == "Numero" && IsLetter(token.ToString()) == true)
                                {
                                    state = 1;
                                    AST.Add(new Token("Error", token.ToString()));
                                }
                                break;
                        }
                        break;
                    case 1:
                        if (token == ' ')
                        {
                            state = 0;
                            break;
                        }
                        else if (IsNumber(AST.LastOrDefault().Value) && IsLetter(token.ToString()))
                        {
                            AST.LastOrDefault().TokenType = "Error";
                            AST.LastOrDefault().Value = AST.LastOrDefault().Value + token;
                            state = 2;
                            
                            break;
                        }
                        else if (IsNumber(AST.LastOrDefault().Value) && IsNumber(token.ToString()))
                        {
                            if (token == ' ')
                            {
                                state = 0;
                            }
                            else
                            {
                                AST.LastOrDefault().Value = AST.LastOrDefault().Value + token;
                            }
                            break;
                        }
                        else if (IsLetter(AST.LastOrDefault().Value) && IsLetter(token.ToString()) || IsNumber(token.ToString()))
                        {
                            AST.LastOrDefault().Value = AST.LastOrDefault().Value + token;
                            break;
                        }
                        else if (AST.LastOrDefault().TokenType == "Error" && IsLetter(token.ToString()) == true)
                        {
                            if (token == ' ')
                            {
                                state = 0;
                            }
                            else
                            {
                                AST.LastOrDefault().Value = AST.LastOrDefault().Value + token;
                            }
                            break;
                        }
                        else
                        {
                            switch (token)
                            {
                                case '=':
                                    AST.LastOrDefault().TokenType = AST.LastOrDefault().TokenType + "Igual";
                                    AST.LastOrDefault().Value = AST.LastOrDefault().Value + token;
                                    break;
                                case '\0':
                                    state = 4;
                                    break;
                                case ')':
                                    AST.Add(new Token("ParentesisCerrado", token.ToString()));
                                    state = 0;
                                    break;
                                case '"':
                                    AST.Add(new Token("Comillas", token.ToString()));
                                    state = 0;
                                    break;
                                case ';':
                                    AST.Add(new Token("FinDeLinea", token.ToString()));
                                    state = 0;
                                    break;
                                case ':':
                                    AST.Add(new Token("DosPuntos", token.ToString()));
                                    state = 0;
                                    break;
                                case '}':
                                    AST.Add(new Token("LlaveCerrada", token.ToString()));
                                    state = 0;
                                    break;
                                case '+':
                                    AST.Add(new Token("Adición", token.ToString()));
                                    state = 0;
                                    break;
                                case '-':
                                    AST.Add(new Token("Sustracción", token.ToString()));
                                    state = 0;
                                    break;
                                case '*':
                                    AST.Add(new Token("Multiplicación", token.ToString()));
                                    state = 0;
                                    break;
                                case '/':
                                    AST.Add(new Token("División", token.ToString()));
                                    state = 0;
                                    break;
                                default:
                                    //GetBack();
                                    break;
                            }
                            break;
                        }
                    case 2:
                        if (AST.LastOrDefault().TokenType == "Numero" && IsNumber(token.ToString()))
                        {
                            AST.LastOrDefault().Value = AST.LastOrDefault().Value + token;
                        }
                        else if (!IsNumber(token.ToString()))
                        {
                            if (token == ' ')
                            {
                                state = 0;
                            }
                            else
                            {
                                state = 2;
                                AST.LastOrDefault().Value = AST.LastOrDefault().Value + token;
                            }
                        }
                        switch (AST.LastOrDefault().Value)
                        {
                            case "==":
                                AST.LastOrDefault().TokenType = "Comparacion";
                                break;
                            default:
                                //GetBack();
                                break;
                        }
                        break;
                    case 3:
                        switch (token)
                        {
                            case '=':
                                AST.LastOrDefault().TokenType = "Desigual";
                                AST.LastOrDefault().Value = AST.LastOrDefault().Value + token;
                                break;
                            default:
                                //GetBack();
                                break;
                        }
                        break;
                }
            }
            return AST;
        }
    }
}
