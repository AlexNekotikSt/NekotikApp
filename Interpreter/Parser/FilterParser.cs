using System.Globalization;

namespace Interpreter.Parser
{
    public class FilterParser
    {
        private readonly List<Token> _tokens;
        private int _position = 0;

        public FilterParser(List<Token> tokens)
        {
            _tokens = tokens;
        }

        public IExpression ParseExpression()
        {
            return ParseOr();
        }

        private IExpression ParseOr()
        {
            var left = ParseAnd();
            while (Match(TokenType.LogicalOperator, "OR"))
            {
                var right = ParseAnd();
                left = new OrExpression(left, right);
            }
            return left;
        }

        private IExpression ParseAnd()
        {
            var left = ParsePrimary();
            while (Match(TokenType.LogicalOperator, "AND"))
            {
                var right = ParsePrimary();
                left = new AndExpression(left, right);
            }
            return left;
        }

        private IExpression ParsePrimary()
        {
            if (Match(TokenType.OpenParen))
            {
                var expr = ParseExpression();
                Expect(TokenType.CloseParen);
                return expr;
            }

            var field = Expect(TokenType.Identifier).Value;
            var op = Expect(TokenType.Operator).Value;
            var valueToken = Next();

            return BuildCondition(field, op, valueToken);
        }

        private IExpression BuildCondition(string field, string op, Token valueToken)
        {
            //special case for widget name
            if (field.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {
                return op switch
                {
                    "=" => new WidgetNameEqualsExpression(valueToken.Value),
                    "contains" => new WidgetNameContainsExpression(valueToken.Value),
                    _ => throw new NotSupportedException($"Unsupported operator for Name: {op}")
                };
            }

            switch (valueToken.Type)
            {
                case TokenType.StringLiteral:
                    if (op == "contains")
                        return new TextContainsExpression(field, valueToken.Value);
                    break;

                case TokenType.NumberLiteral:
                    if (decimal.TryParse(valueToken.Value, out var num))
                    {
                        switch (op)
                        {
                            case ">":
                                return new NumericGreaterThanExpression(field, num);
                            case ">=":
                                return new NumericGreaterOrEqualThanExpression(field, num);
                            case "<":
                                return new NumericLessThanExpression(field, num);
                            case "<=":
                                return new NumericLessOrEqualThanExpression(field, num);
                            case "=":
                                return new NumericLessOrEqualThanExpression(field, num);
                        }
                    }
                    break;
            }

            throw new NotSupportedException($"Unsupported expression: {field} {op} {valueToken.Value}");
        }

        private Token Next() => _tokens[_position++];

        private Token Expect(TokenType type)
        {
            var token = Next();
            if (token.Type != type)
                throw new Exception($"Expected {type} but got {token.Type}");
            return token;
        }

        private bool Match(TokenType type, string? value = null)
        {
            if (_position >= _tokens.Count) 
                return false;

            var token = _tokens[_position];
            
            if (token.Type != type || (value != null && !token.Value.Equals(value, StringComparison.OrdinalIgnoreCase)))
                return false;
            
            _position++;
            
            return true;
        }
    }

}
