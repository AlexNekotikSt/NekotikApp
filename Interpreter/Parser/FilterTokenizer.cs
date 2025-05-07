using System.Text.RegularExpressions;

namespace Interpreter.Parser
{
    public static class FilterTokenizer
    {
        public static List<Token> Tokenize(string input)
        {
            var tokens = new List<Token>();
            var parts = Regex.Matches(input, @"\(|\)|'[^']*'|\S+");

            foreach (Match match in parts)
            {
                var value = match.Value;

                switch (value)
                {
                    case "(":
                        tokens.Add(new Token { Type = TokenType.OpenParen, Value = value });
                        break;
                    case ")":
                        tokens.Add(new Token { Type = TokenType.CloseParen, Value = value });
                        break;
                    default:
                        if (value.Equals("AND", StringComparison.OrdinalIgnoreCase) || value.Equals("OR", StringComparison.OrdinalIgnoreCase))
                        {
                            tokens.Add(new Token { Type = TokenType.LogicalOperator, Value = value.ToUpper() });
                        }
                        else if (Regex.IsMatch(value, @"^(=|>|<|>=|<=|contains)$", RegexOptions.IgnoreCase))
                        {
                            tokens.Add(new Token { Type = TokenType.Operator, Value = value.ToLower() });
                        }
                        else if (Regex.IsMatch(value, @"^'\w.*'$"))
                        {
                            tokens.Add(new Token { Type = TokenType.StringLiteral, Value = value.Trim('\'') });
                        }
                        else if (decimal.TryParse(value, out _))
                        {
                            tokens.Add(new Token { Type = TokenType.NumberLiteral, Value = value });
                        }
                        else
                        {
                            tokens.Add(new Token { Type = TokenType.Identifier, Value = value });
                        }

                        break;
                }
            }

            return tokens;
        }
    }
}
