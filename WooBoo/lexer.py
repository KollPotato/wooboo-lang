import string

from .position import Position
from .token import Token

DIGITS = string.digits
LETTERS = string.ascii_letters
DOUBLE_QOUTE = "\""
OPERATORS_STARTS = ["*", "/", "+", "-", "%", "=", "!", ">", "<"]
BRACES = ["(", ")", "{", "}", "[", "]"]
KEYWORDS = ["for", "while", "var", "const", "break", "continue", "if", "else", "elif"]
IDENTIFIER_CHARACTERS = LETTERS + DIGITS + "_"

class Lexer:
    def __init__(self, code: str):
        self.code = code
        self.position = Position(-1, 0, -1)
        self.character = None
        self.advance()

    def advance(self) -> tuple:
        """Returns tuple with first element as character and position as a character position before advancing"""
        character = self.character
        position = self.position.copy()
        self.position.advance(self.character)
        self.character = self.code[self.position.index] if self.position.index < len(self.code) else None
        return character, position

    def lex(self) -> list[Token]:
        tokens = []
        
        while self.character != None:
            if self.character in [" ", "\t"]:
                self.advance()
            elif self.character in DIGITS:
                tokens.append(self.lex_number())
            elif self.character in LETTERS:
                tokens.append(self.lex_identifier())
            elif self.character == DOUBLE_QOUTE:
                tokens.append(self.lex_string())
            elif self.character in OPERATORS_STARTS:
                tokens.append(self.lex_operator())
            elif self.character in BRACES:
                tokens.append(self.lex_brace()) 
            else:
                tokens.append(self.lex_other())

        return tokens

    def lex_identifier(self):
        identifier = ""
        start_position = self.position.copy()
        while self.character != None and self.character in IDENTIFIER_CHARACTERS:
            identifier = identifier + self.character
            self.advance()

        if identifier[0] in DIGITS:
            print("LexerError: Identifier can not start with a number")
            exit(1)

        end_position = self.position.copy()
        return Token(Token.IDENTIFIER, str(identifier), (start_position, end_position))
        
    def lex_number(self):
        string_number = ""
        start_position = self.position.copy()
        while self.character != None and self.character in DIGITS:
            string_number = string_number + self.character
            self.advance()
        return Token(Token.INTEGER, int(string_number), (start_position, self.position))

    def lex_string(self):
        pass

    def lex_operator(self):
        if self.character == "+":
            _, position = self.advance()
            return Token(Token.PLUS, "+", (position, position))

        elif self.character == "=":
            _, start_position = self.advance()
            end_position = self.position.copy()
            if self.character == "=":
                self.advance()
                return Token(Token.EQUAL, "==", (start_position, end_position))
            elif self.character == ">":
                self.advance()
                return Token(Token.LAMBDA, "=>", (start_position, end_position))
            return Token(Token.ASSIGN, "=", (start_position, start_position))

        elif self.character == ">":
            _, start_position = self.advance()
            end_position = self.position.copy()
            if self.character == "=":
                self.advance()
                return Token(Token.GREATER_EQUAL, ">=", (start_position, end_position))
            return Token(Token.GREATER, ">", (start_position, start_position))  

        elif self.character == "<":
            _, start_position = self.advance()
            end_position = self.position.copy()
            if self.character == "=":
                self.advance()
                return Token(Token.LESS_EQUAL, "<=", (start_position, end_position))
            return Token(Token.LESS, "<", (start_position, start_position))

        elif self.character == "!":
            _, start_position = self.advance()
            end_position = self.position.copy()
            if self.character == "=":
                self.advance()
                return Token(Token.NOT_EQUAL, "!=", (start_position, end_position))

        elif self.character == "-":
            _, position = self.advance()
            return Token(Token.SUB, "-", (position, position))

        elif self.character == "/":
            _, position = self.advance()
            return Token(Token.DIV, "/", (position, position))

        elif self.character == "*":
            _, start_position = self.advance()
            end_position = self.position.copy()
            if self.character == "*":
                self.advance()
                return Token(Token.POWER, "**", (start_position, end_position))
            return Token(Token.MULT, "*", (start_position, start_position))

    def lex_brace(self):

        if self.character == "(":
            _, position = self.advance()
            return Token(Token.LPAREN, "(", (position, position))

        elif self.character == ")":
            _, position = self.advance()
            return Token(Token.RPAREN, ")", (position, position))

        elif self.character == "}":
            _, position = self.advance()
            return Token(Token.RBRACE, "}", (position, position))

        elif self.character == "{":
            _, position = self.advance()
            return Token(Token.LBRACE, "{", (position, position))
        
        elif self.character == "[":
            _, position = self.advance()
            return Token(Token.LBRACE, "[", (position, position))
        
        elif self.character == "]":
            _, position = self.advance()
            return Token(Token.LBRACE, "]", (position, position))

    def lex_other(self):
        if self.character == ",":
            _, position = self.advance()
            return Token(Token.COMMA, ",", (position, position))
        print("Undefined token")
        exit(1)
