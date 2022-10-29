namespace WooBoo
{
    internal class Lexer
    {
        public Lexer(string codeText, string filePath)
        {
            code = codeText;
            character = null;
            position = new Position(-1, 0, -1, filePath);
            Advance();
        }

        public void Advance()
        {

            position.Advance();

            if (position.Index < code.Length)
            {
                character = code[position.Index].ToString();
                return;
            }

            character = null;
        }

        string code;
        string? character;
        Position position;

        static string[] ignoredCharacters = { "\t" };
        static string[] keywords = { "for", "while", "if", "else", "elif", "break", "catch", "in", "return", "fun", "or", "and", "not", "as", "import", "true", "false", "true" };
        static string digitsCharacters = "0123456789";
        static string numberCharacters = digitsCharacters + ".";
        static string lettersCharacters = "QWERTYUIOPASDFGHJKLZXCVBNM";
        static string identifierCharacters = digitsCharacters + lettersCharacters + "_";
        static string[] operators = { "+", "-", "*", "/", "**" };
        static string braces = "()[]{}";
        static string DOUBLE_QOUTE = "\"";

        public List<Token> Lex()
        {
            var tokens = new List<Token>();

            while (character != null)
            {
                if (ignoredCharacters.Contains(character))
                {
                    Advance();
                }
                else if (digitsCharacters.Contains(character))
                {
                    var token = LexNumber();
                    tokens.Add(token);
                }
                else if (identifierCharacters.Contains(character))
                {
                    var token = LexIdentifier();
                    tokens.Add(token);
                }
                else if (character == DOUBLE_QOUTE)
                {
                    LexString();
                }
                else if (braces.Contains(character))
                {
                    var token = LexBrace();
                    tokens.Add(token);
                }
                foreach (var op in operators)
                {
                    if (character == op[0].ToString())
                    {
                        LexOperator();
                    }
                }

                Advance();
            }

            return tokens;
        }

        public void LexString()
        {

        }

        public Token LexNumber()
        {
            string number = "";
            int dotCount = 0;

            var start = position.Copy();


            while (character != null && numberCharacters.Contains(character))
            {
                // if (character == ".") dotCount += 1;
                // else if (dotCount > 1) throw new Exception("Number can not contain more than one dot!");
                number = number + character;
                Advance();
            }

            var end = position.Copy();

            var location = new Location(start, end);

            if (dotCount == 1) return new Token(TokenType.FLOAT, float.Parse(number), location);

            return new Token(TokenType.INTEGER, int.Parse(number), location);

        }

        public Token LexIdentifier()
        {

            var identifier = "";

            var start = position.Copy();

            while (character != null && identifierCharacters.Contains(character))
            {
                identifier = identifier + character;
                Advance();
            }

            var end = position.Copy();

            var location = new Location(start, end);

            if (keywords.Contains(identifier)) return new Token(TokenType.KEYWORD, identifier, location);

            return new Token(TokenType.IDENTIFIER, identifier, location);
        }

        public Token LexBrace()
        {

            var start = position.Copy();
            var brace = character;

            var patterns = new Dictionary<string, TokenType>(){
                { "(", TokenType.LPAREN },
                { ")", TokenType.RPAREN },
                { "[", TokenType.LBRACKET },
                { "]", TokenType.RBRACKET },
                { "{", TokenType.LBRACE },
                { "}", TokenType.RBRACE }
            };

            Advance();

            var end = position.Copy();

            var location = new Location(start, end);

            foreach (var pattern in patterns)
            {
                if (brace == pattern.Key) return new Token(pattern.Value, pattern.Key, location);
            }

            throw new Exception(string.Format("Unknown brace {0}", brace));
        }

        public void LexOperator()
        {

        }
    }
}
