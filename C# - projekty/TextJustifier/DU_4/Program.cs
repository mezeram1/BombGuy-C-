using System;
using System.Collections.Generic;
using System.IO;

namespace DU_3
{
    /* Programování v C#
    * DÚ 4
    * Multiple text justification
    */
    internal class Program
    {
        // class that catches error and reacts to it
        static class ExeptionHandler
        {
            // function for catching argument errors
            static public bool ArgErr(string[] args)
            {
                if (args.Length < 3 || (args[0] == "--highlight-spaces" && args.Length < 4))
                {
                    Console.WriteLine("Argument Error");
                    return true;
                }
                // check if the third argument is positive integer or zero
                try
                {
                    if (Convert.ToInt32(args[args.Length - 1]) < 0)
                    {
                        Console.WriteLine("Argument Error");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Argument Error");
                    return true;
                }
                return false;
            }
            static public LineProcessor OutputErr(string[] args)
            {
                try
                {
                    LineProcessor p = new LineProcessor(new System.IO.StreamWriter(args[args.Length - 2]), Convert.ToInt32(args[args.Length - 1]));
                    return p;
                }
                catch (Exception)
                {
                    Console.WriteLine("File Error");
                    return null;
                }
            }
        }
        // Class that prepares line for output and than prints it to outputfile
        class LineProcessor
        {
            public System.IO.StreamWriter writer;
            private int chars_to_line; // line size in chars
            private string line = "";
            private char wordSeparator = ' ';
            private string lineEnd = "\n";
            private bool emptyOut = true;
            public bool new_paragraph = false; // true if the sentence which is going to be print should be in a next paragraph

            public LineProcessor(StreamWriter writer, int chars_to_line)
            {
                this.writer = writer;
                this.chars_to_line = chars_to_line;
            }

            public void HighlightSpaces()
            {
                wordSeparator = '.';
                lineEnd = "<-\n";
            }
            // function that takes word from reader and prepares one line for output
            public void AddWord(string word)
            {
                emptyOut = false;
                if (chars_to_line >= line.Length + word.Length + 1) // test if the word could be appended to actual line
                {
                    if (line.Length != 0)
                        line += ' ';
                    line += word;
                }
                else
                {
                    if (line.Length != 0)
                        ProcessLine();
                    line = word;
                }
            }
            // function that fills up line with spaces and prints it to output file
            private void ProcessLine()
            {
                if (new_paragraph)
                {
                    // indent new paragraph
                    writer.Write(lineEnd);
                }
                int to_fill = chars_to_line - line.Length;
                string[] words = line.Split(' ');
                if (words.Length > 1) // test if line is not a silgle word
                {
                    int spaces = to_fill / (words.Length - 1) + 1;
                    for (int i = 0; i < words.Length - 1; i++)
                    {
                        writer.Write(words[i]);
                        if (to_fill % (words.Length - 1) > i)
                            writer.Write(new String(wordSeparator, spaces + 1));
                        else
                            writer.Write(new String(wordSeparator, spaces));
                    }
                }
                writer.Write(words[words.Length - 1] + lineEnd);
                new_paragraph = false;
            }
            // function that formate and prints to output file last line of paragraph
            public void EndOfParagraph()
            {
                if (line.Length != 0 || (!new_paragraph && emptyOut))
                    WriteLine();
                line = "";
            }
            private void WriteLine()
            {
                string[] words = line.Split(' ');
                for (int i = 0; i < words.Length - 1; i++)
                {
                    writer.Write(words[i] + wordSeparator);
                }
                writer.Write(words[words.Length - 1] + lineEnd);
            }
        }
        // class that reads whole input file and passes words to LineProcessor
        class WordReader
        {
            private char[] white_space_chars = { '\n', ' ', '\t' };
            public StreamReader reader;
            private string current_word = "";
            private bool empty_line = true; // true if first chars of input line are white space chars

            public WordReader(StreamReader sr)
            {
                this.reader = sr;
            }
            public bool IsWhiteSpace(char ch)
            {
                return Array.Exists(white_space_chars, x => x == ch);
            }
            // function that reads input file char by char and passes words to LineProcessor
            public void ReadFile(LineProcessor p)
            {
                char ch;
                while (((ch = (char)reader.Read()) + 65536) % 65536 != 65535)
                {
                    if (!IsWhiteSpace(ch))
                    {
                        current_word += ch; // append char to actual word
                        empty_line = false;
                    }
                    else
                    {
                        if (ch == '\n')
                        {
                            if (empty_line && !p.new_paragraph) //test if the line is full of white space chars and is the first one of this lines
                            {
                                p.new_paragraph = true;
                                p.EndOfParagraph();
                                current_word = "";
                            }
                            empty_line = true;
                        }
                        // pass word to the LineProcessor and clears it
                        if (current_word.Length != 0)
                            p.AddWord(current_word);
                        current_word = "";
                    }
                }
                p.new_paragraph = false;
            }
        }
        private static void Main(string[] args)
        {
            int firstInputIndex = 0;
            LineProcessor p;
            if (!ExeptionHandler.ArgErr(args) && (p = ExeptionHandler.OutputErr(args)) != null)
            { 
                if (args[0] == "--highlight-spaces")
                {
                    firstInputIndex = 1;
                    p.HighlightSpaces();
                }
                WordReader wr;
                using (p.writer)
                {
                    for (int i = firstInputIndex; i < args.Length - 2; i++)
                    {
                        try
                        {
                            wr = new WordReader(new StreamReader(args[i]));
                            wr.reader = new StreamReader(args[i]);
                            wr.ReadFile(p);
                        }
                        catch (IOException)
                        {
                            continue;
                        }
                    }
                    p.EndOfParagraph();
                }
                
            }
        }
    }
}