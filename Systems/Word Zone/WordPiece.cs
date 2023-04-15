using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using System.Linq;
using System.Collections;


namespace WordZone
{
    public class WordPiece
    {
        public struct Token
        {

            public enum TokenType
            {
                Word, Command, Author , EmptyBracket
            }
            public TokenType tokenType { get; private set; }
            public string take { get; private set; }
            public List<int> paraments { get; private set; }

            public Token(TokenType tokenType, string take, List<int> parament)
            {
                this.tokenType = tokenType;
                this.take = take;
                this.paraments = parament;
            }
        }

        public List<Token> tokens = new();

        public object Current => throw new NotImplementedException();


        private WordPiece()
        {

        }
        public static WordPiece Parser(string origin)
        {
            Debug.Assert(origin!=null);
            WordPiece piece = new WordPiece();


            Regex regex = new Regex(@"([^\[\]]+)|\[(.*?)\]");
            const int OUTSIDE = 1;
            const int INSIDE = 2;
            foreach (Match match in regex.Matches(origin))
            {
                if (match.Groups[OUTSIDE].Success)
                {
                    string take = match.Groups[OUTSIDE].Value;

                    piece.tokens.Add(new Token(Token.TokenType.Word, take, null));
                }
                else if (match.Groups[INSIDE].Success)
                {
                    string[] commands = match.Groups[INSIDE].Value.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (commands.Length > 0)
                        foreach (string command in commands)
                        {
                            if (int.TryParse(command, out int num))
                            {
                                AddParament(num);
                            }
                            else if (command.Equals("∞"))
                            {
                                AddParament(int.MaxValue);
                            }
                            else if (command[0].Equals('@'))
                            {

                            }
                            else
                            {
                                piece.tokens.Add(new Token(Token.TokenType.Command, command, new List<int>()));
                            }

                            void AddParament(int parament)
                            {
                                if (piece.tokens.Count < 0)
                                {
                                    throw new ApplicationException("错误的DSL语法：参数必须放置于指令效果后面");
                                }
                                if (piece.tokens.Last().tokenType == Token.TokenType.Command)
                                {
                                    piece.tokens.Last().paraments.Add(parament);
                                }
                            }
                        }
                    else
                    {
                        piece.tokens.Add(new Token(Token.TokenType.EmptyBracket,null,null));
                    }

                }
            }
            //piece.log();
            return piece;
        }

        private void log()
        {
            foreach (var token in tokens)
            {
                Debug.Log("-------------------");
                Debug.Log(token.tokenType);
                Debug.Log(token.take);
                if (token.paraments == null)
                    Debug.Log("无参数");
                else
                    Debug.Log(string.Join(' ', token.paraments));
            }
        }


    }

}
