using WooBoo;

string code = "{2.5}";
/*
    output should be:
        LBRACE {
        FLOAT 2.5
        RBRACE }
*/ 

Lexer lexer = new Lexer(code, "none");
List<Token> tokens = lexer.Lex();

foreach (Token token in tokens)
{
    Console.WriteLine(token.type.ToString() + "            " + token.value.ToString());
}
