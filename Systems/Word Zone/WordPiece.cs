using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using System.Linq;
using System.Collections;


//自定义WordZone库，实现了一个DSL解析器，用于将文本解析成一个Token列表
namespace WordZone
{
    public class WordPiece
    {
        public struct Token
        {
            //定义了标记类型（单词、命令、作者和空括号）
            public enum TokenType
            {
                Word, Command, Author, EmptyBracket
            }
            public TokenType tokenType { get; private set; }
            public string take { get; private set; }            //表示当前标记的具体内容
            public List<int> paraments { get; private set; }    //存储命令的参数列表

            public Token(TokenType tokenType, string take, List<int> parament)
            {
                this.tokenType = tokenType;
                this.take = take;
                this.paraments = parament;
            }
        }

        public List<Token> tokens = new();

        public object Current => throw new NotImplementedException();

        public Action OnFinish;
        private WordPiece()
        {
            //
        }

        //将原始文本解析为一系列Token，将字符串转换成单词片段
        public static WordPiece Parser(string origin)
        {
            Debug.Assert(origin != null);
            WordPiece piece = new WordPiece();

            //支持在方括号中使用参数
            Regex regex = new Regex(@"([^\[\]]+)|\[(.*?)\]");
            const int OUTSIDE = 1;
            const int INSIDE = 2;
            foreach (Match match in regex.Matches(origin))
            {
                //检测字符串是否存在
                if (match.Groups[OUTSIDE].Success)
                {
                    string take = match.Groups[OUTSIDE].Value;

                    piece.tokens.Add(new Token(Token.TokenType.Word, take, null));
                }
                else if (match.Groups[INSIDE].Success)
                {
                    //按空格拆分成多个单词
                    string[] commands = match.Groups[INSIDE].Value.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (commands.Length > 0)
                        //解析DSL表达式
                        foreach (string command in commands)
                        {
                            //尝试转换成整数类型
                            if (int.TryParse(command, out int num))
                            {
                                //转换成功，该整数添加到参数列表中
                                AddParament(num);
                            }
                            else if (command.Equals("∞"))
                            {
                                //转换失败，该单词以"∞"开头，将最大整数值添加到参数列表中
                                AddParament(int.MaxValue);
                            }
                            else if (command[0].Equals('@'))
                            {
                                //转换失败，该单词以"@"开头，则忽略该单词
                                //
                            }
                            else
                            {
                                piece.tokens.Add(new Token(Token.TokenType.Command, command, new List<int>()));
                            }

                            //添加语句
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
                        piece.tokens.Add(new Token(Token.TokenType.EmptyBracket, null, null));
                    }
                }
            }
            //piece.log();
            return piece;
        }

        //在控制台上打印Token列表进行调试
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
