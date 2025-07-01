using System.Globalization;

namespace Engine.Rendering.Utils;

public static class ExpressionEvaluator
{
    public static float Eval(string expr, Dictionary<string, float> variables)
    {
        var tokenizer = new Tokenizer(expr);
        var parser = new Parser(tokenizer, variables);
        return parser.ParseExpression();
    }

    private enum TokenType { Number, Identifier, Operator, Percent, LeftParen, RightParen, End }

    private class Token
    {
        public TokenType Type;
        public string Text = "";
    }

    private class Tokenizer
    {
        private readonly string _input;
        private int _pos;

        public Tokenizer(string input)
        {
            _input = input;
        }

        public Token NextToken()
        {
            while (_pos < _input.Length && char.IsWhiteSpace(_input[_pos])) _pos++;

            if (_pos >= _input.Length)
                return new Token { Type = TokenType.End };

            char ch = _pos < _input.Length ? _input[_pos] : '\0';

            if (char.IsDigit(ch) || ch == '.')
            {
                int start = _pos;
                while (_pos < _input.Length && (char.IsDigit(_input[_pos]) || _input[_pos] == '.')) _pos++;
                if (_pos < _input.Length && _input[_pos] == '%')
                    return new Token { Type = TokenType.Percent, Text = _input[start..++_pos] };
                return new Token { Type = TokenType.Number, Text = _input[start.._pos] };
            }

            if (char.IsLetter(ch) || ch == '.')
            {
                int start = _pos;
                while (_pos < _input.Length && (char.IsLetterOrDigit(_input[_pos]) || _input[_pos] == '.')) _pos++;
                return new Token { Type = TokenType.Identifier, Text = _input[start.._pos] };
            }

            _pos++;
            return ch switch
            {
                '+' or '-' or '*' or '/' => new Token { Type = TokenType.Operator, Text = ch.ToString() },
                '(' => new Token { Type = TokenType.LeftParen },
                ')' => new Token { Type = TokenType.RightParen },
                _ => throw new Exception($"Unexpected character: {ch}")
            };
        }
    }

    private class Parser
    {
        private readonly Tokenizer _tokenizer;
        private readonly Dictionary<string, float> _vars;
        private Token _current;

        public Parser(Tokenizer tokenizer, Dictionary<string, float> variables)
        {
            _tokenizer = tokenizer;
            _vars = variables;
            _current = _tokenizer.NextToken();
        }

        private void Eat(TokenType expected)
        {
            if (_current.Type != expected)
                throw new Exception($"Expected {expected} but got {_current.Type}");
            _current = _tokenizer.NextToken();
        }

        public float ParseExpression() => ParseAddSub();

        private float ParseAddSub()
        {
            float result = ParseMulDiv();
            while (_current.Type == TokenType.Operator && (_current.Text == "+" || _current.Text == "-"))
            {
                var op = _current.Text;
                Eat(TokenType.Operator);
                float right = ParseMulDiv();
                result = op == "+" ? result + right : result - right;
            }
            return result;
        }

        private float ParseMulDiv()
        {
            float result = ParseFactor();
            while (_current.Type == TokenType.Operator && (_current.Text == "*" || _current.Text == "/"))
            {
                var op = _current.Text;
                Eat(TokenType.Operator);
                float right = ParseFactor();
                result = op == "*" ? result * right : result / right;
            }
            return result;
        }

        private float ParseFactor()
        {
            return _current.Type switch
            {
                TokenType.Number => ParseNumber(),
                TokenType.Percent => ParsePercent(),
                TokenType.Identifier => ParseVariable(),
                TokenType.LeftParen => ParseParen(),
                _ => throw new Exception($"Unexpected token: {_current.Type}")
            };
        }

        private float ParseNumber()
        {
            float value = float.Parse(_current.Text, CultureInfo.InvariantCulture);
            Eat(TokenType.Number);
            return value;
        }

        private float ParsePercent()
        {
            string number = _current.Text.TrimEnd('%');
            float percent = float.Parse(number, CultureInfo.InvariantCulture);
            Eat(TokenType.Percent);
            if (_vars.TryGetValue("y", out float baseValue))
                return baseValue * (percent / 100f);
            throw new Exception("Percent expressions require 'y' variable in context.");
        }

        private float ParseVariable()
        {
            string name = _current.Text;
            Eat(TokenType.Identifier);
            if (_vars.TryGetValue(name, out float val))
                return val;
            throw new Exception($"Unknown variable '{name}'");
        }

        private float ParseParen()
        {
            Eat(TokenType.LeftParen);
            float result = ParseExpression();
            Eat(TokenType.RightParen);
            return result;
        }
    }
}