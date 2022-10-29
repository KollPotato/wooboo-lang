namespace WooBoo
{
    internal class Token
    {
        public Token(TokenType type, object value, Location location)
        {
            this.type = type;
            this.value = value;
            this.location = location;
        }

        public TokenType type;
        public object value;
        public Location location;
    }

    public enum TokenType
    {
        FLOAT = 1,
        INTEGER = 2,
        STRING = 3,
        IDENTIFIER = 4,
        LBRACE = 5,
        RBRACE = 6,
        LPAREN = 7,
        RPAREN = 8,
        LBRACKET = 9,
        RBRACKET = 10,
        PLUS = 11,
        SUB = 12,
        MULT = 13,
        DIV = 14,
        EQUAL = 15,
        ASSIGN = 16,
        COLON = 17,
        SEMICOLON = 18,
        POWER = 19,
        LAMBDA = 20,
        GREATER = 21,
        LESS = 22,
        NOT_EQUAL = 23,
        GREATER_EQUAL = 24,
        LESS_EQUAL = 25,
        COMMA = 26,
        DOT = 27,
        KEYWORD = 28,
        PLUS_ASSIGN = 29,
        SUB_ASSIGN = 30,
        NULT_ASSIGN = 31,
        DIV_ASSIGN = 32,
        MODULO = 34,
        NOT = 35,
        OR = 36
    }
}
