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
        
        public List<Token> SetLexema(char[] tokens)
        {
            foreach (var token in tokens)
            {
                switch (state)
                {
                    case 0: // Caso 0 - Todos los tipos posibles de entradas
                        switch (token)
                        {
                            case '\0':
                                state = 5;
                                break;
                            case '\n':
                                state = 0;
                                break;
                            case '\t':
                                state = 0;
                                break;
                            case ' ':
                                state = 0;
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
                                state = 1;
                                AST.Add(new Token("Asignacion", token.ToString()));
                                break;
                            case '!':
                                state = 1;
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
                    case 1: // Caso 1 - Detecta simbolos que trascienden al estado 0.
                        switch (AST.LastOrDefault().Value == "==")
                        {
                            case true:
                                switch (IsNumber(token.ToString()))
                                {
                                    case true:
                                        if (token.ToString() != AST.LastOrDefault().Value)
                                        {
                                            AST.Add(new Token("Numero", token.ToString()));
                                        }
                                        state = 0;
                                        break;
                                    default:
                                        AST.LastOrDefault().TokenType = "Error";
                                        AST.LastOrDefault().Value = AST.LastOrDefault().Value + token;
                                        break;
                                }
                                break;
                            default:
                                switch (IsNumber(AST.LastOrDefault().Value))
                                {
                                    case true:
                                        switch (IsNumber(token.ToString()))
                                        {
                                            case true:
                                                AST.LastOrDefault().Value = AST.LastOrDefault().Value + token;
                                                break;
                                            default:
                                                switch (token)
                                                {
                                                    case ' ':
                                                        state = 0;
                                                        break;
                                                    case '\0':
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
                                                    case '>':
                                                        state = 1;
                                                        AST.Add(new Token("OperadorMayor", token.ToString()));
                                                        break;
                                                    case '<':
                                                        state = 1;
                                                        AST.Add(new Token("OperadorMenor", token.ToString()));
                                                        break;
                                                    case '=':
                                                        AST.Add(new Token("Asignacion", token.ToString()));
                                                        state = 2;
                                                        break;
                                                    default:
                                                        AST.LastOrDefault().TokenType = "Error";
                                                        AST.LastOrDefault().Value = AST.LastOrDefault().Value + token;
                                                        break;
                                                }
                                                break;
                                        }
                                        break;
                                    default:
                                        break;
                                }
                                break;
                        }
                        switch (IsLetter(AST.LastOrDefault().Value))
                        {
                            case true:
                                switch (IsLetter(token.ToString()))
                                {
                                    case true:
                                        AST.LastOrDefault().Value = AST.LastOrDefault().Value + token;
                                        break;
                                    default:
                                        switch (token)
                                        {
                                            case ' ':
                                                state = 0;
                                                break;
                                            case '\0':
                                                state = 0;
                                                break;
                                            default:
                                                switch (IsNumber(token.ToString()))
                                                {
                                                    case true:
                                                        AST.LastOrDefault().Value = AST.LastOrDefault().Value + token;
                                                        break;
                                                    default:
                                                        AST.LastOrDefault().TokenType = "Error";
                                                        AST.LastOrDefault().Value = AST.LastOrDefault().Value + token;
                                                        break;
                                                }
                                                break;
                                        }
                                        break;
                                }
                                break;
                            default:
                                break;
                        }
                        switch (!IsNumber(AST.LastOrDefault().Value) && !IsLetter(AST.LastOrDefault().Value) || AST.LastOrDefault().Value != "=")
                        {
                            case true:
                                switch (token)
                                {
                                    case ' ':
                                        state = 0;
                                        break;
                                    
                                    default:
                                        switch (AST.LastOrDefault().Value)
                                        {
                                            case "<":
                                                AST.LastOrDefault().TokenType = AST.LastOrDefault().TokenType + "Igual";
                                                AST.LastOrDefault().Value = AST.LastOrDefault().Value + token.ToString();
                                                state = 3;
                                                break;
                                            case ">":
                                                AST.LastOrDefault().TokenType = AST.LastOrDefault().TokenType + "Igual";
                                                AST.LastOrDefault().Value = AST.LastOrDefault().Value + token.ToString();
                                                state = 3;
                                                break;
                                            case "=":
                                                state = 2;
                                                switch (token)
                                                {
                                                    case '=':
                                                        AST.LastOrDefault().TokenType = "Comparacion";
                                                        AST.LastOrDefault().Value = AST.LastOrDefault().Value + token.ToString();
                                                        break;
                                                    default:
                                                        break;
                                                }
                                                break;
                                            case "!":
                                                AST.LastOrDefault().TokenType = "Desigual";
                                                AST.LastOrDefault().Value = AST.LastOrDefault().Value + token;
                                                state = 3;
                                                break;
                                            default:
                                                break;
                                        }
                                        break;
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    case 2: // Caso 2 - Detecta simbolo de comparacion
                        switch (IsNumber(token.ToString()))
                        {
                            case true:
                                if (token.ToString() != AST.LastOrDefault().Value)
                                {
                                    AST.Add(new Token("Numero", token.ToString()));
                                }
                                state = 0;
                                break;
                            default:
                                switch (AST.LastOrDefault().TokenType)
                                {
                                    case "MayorIgual":
                                        switch (token)
                                        {
                                            case ' ':
                                                state = 0;
                                                break;
                                            case '\0':
                                                state = 0;
                                                break;
                                            default:
                                                AST.LastOrDefault().TokenType = "Error";
                                                AST.LastOrDefault().Value = AST.LastOrDefault().Value + token;
                                                state = 2;
                                                break;
                                        }
                                        break;
                                    case "MenorIgual":
                                        switch (token)
                                        {
                                            case ' ':
                                                state = 0;
                                                break;
                                            case '\0':
                                                state = 0;
                                                break;
                                            default:
                                                AST.LastOrDefault().TokenType = "Error";
                                                AST.LastOrDefault().Value = AST.LastOrDefault().Value + token;
                                                state = 2;
                                                break;
                                        }
                                        break;
                                    case "Asignacion":
                                        switch (token)
                                        {
                                            case ' ':
                                                state = 0;
                                                break;
                                            case '\0':
                                                state = 0;
                                                break;
                                            case '=':
                                                AST.LastOrDefault().TokenType = "Comparacion";
                                                AST.LastOrDefault().Value = AST.LastOrDefault().Value + token;
                                                state = 1;
                                                break;
                                            default:
                                                AST.LastOrDefault().TokenType = "Error";
                                                AST.LastOrDefault().Value = AST.LastOrDefault().Value + token;
                                                state = 2;
                                                break;
                                        }
                                        break;
                                    case "Desigual":
                                        switch (token)
                                        {
                                            case ' ':
                                                state = 0;
                                                break;
                                            case '\0':
                                                state = 0;
                                                break;
                                            default:
                                                AST.LastOrDefault().TokenType = "Error";
                                                AST.LastOrDefault().Value = AST.LastOrDefault().Value + token;
                                                state = 2;
                                                break;
                                        }
                                        break;
                                    case "Error":
                                        switch (token)
                                        {
                                            case ' ':
                                                state = 0;
                                                break;
                                            case '\0':
                                                state = 0;
                                                break;
                                            default:
                                                AST.LastOrDefault().Value = AST.LastOrDefault().Value + token;
                                                break;
                                        }
                                        break;
                                    default:
                                        break;
                                }
                                break;
                        }
                        break;
                    case 3: // Caso 3 - Detecta errores en simbholos que trascienden el estado 1
                        switch (AST.LastOrDefault().TokenType)
                        {
                            case "MayorIgual":
                                switch (token)
                                {
                                    case ' ':
                                        state = 0;
                                        break;
                                    case '\0':
                                        state = 0;
                                        break;
                                    default:
                                        AST.LastOrDefault().TokenType = "Error";
                                        AST.LastOrDefault().Value = AST.LastOrDefault().Value + token;
                                        state = 2;
                                        break;
                                }
                                break;
                            case "MenorIgual":
                                switch (token)
                                {
                                    case ' ':
                                        state = 0;
                                        break;
                                    case '\0':
                                        state = 0;
                                        break;
                                    default:
                                        AST.LastOrDefault().TokenType = "Error";
                                        AST.LastOrDefault().Value = AST.LastOrDefault().Value + token;
                                        state = 2;
                                        break;
                                }
                                break;
                            case "Desigual":
                                switch (token)
                                {
                                    case ' ':
                                        state = 0;
                                        break;
                                    case '\0':
                                        state = 0;
                                        break;
                                    default:
                                        AST.LastOrDefault().TokenType = "Error";
                                        AST.LastOrDefault().Value = AST.LastOrDefault().Value + token;
                                        state = 2;
                                        break;
                                }
                                break;
                            case "Error":
                                switch (token)
                                {
                                    case ' ':
                                        state = 0;
                                        break;
                                    case '\0':
                                        state = 0;
                                        break;
                                    default:
                                        AST.LastOrDefault().Value = AST.LastOrDefault().Value + token;
                                        break;
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    case 5: // Caso 5 - Termina el programa
                        break;
                }
            }
            return AST;
        }


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
                if (input.ElementAt(i) == '_')
                {
                    return true;
                }
                if (!Char.IsLetter(input.ElementAt(i)))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
