using System;
using System.Collections.Generic;
using System.Text;

namespace Ember.Clases
{
    class Token
    {
        public string TokenType { get; set; }
        public string Value { get; set; }

        public Token(string tokenType)
        {
            TokenType = tokenType;
            Value = string.Empty;
        }

        public Token(string tokenType, string value)
        {
            TokenType = tokenType;
            Value = value;
        }

        public void NextSymbol()
        {
        }
    }
}
