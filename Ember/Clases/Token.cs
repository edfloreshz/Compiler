using System;
using System.Collections.Generic;
using System.Text;

namespace Ember.Clases
{
    class Token
    {
        public int TokenType { get; set; }
        public string Value { get; set; }

        public Token()
        {
            TokenType = 0;
            Value = string.Empty;
        }

        public Token(int tokenType)
        {
            TokenType = tokenType;
            Value = string.Empty;
        }

        public Token(int tokenType, string value)
        {
            TokenType = tokenType;
            Value = value;
        }

    }
}
