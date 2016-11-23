using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace task61
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, List<string>> enrus = new Dictionary<string, List<string>>();
            Dictionary<string, List<string>> rusen = new Dictionary<string, List<string>>();
          
            ConsoleKeyInfo inkey;

            //starter content
            AddWord(ref rusen, ref enrus, "Замок", "Castle");
            AddWord(ref rusen, ref enrus, "замок", "lock");
            AddWord(ref enrus, ref rusen, "key", "ключ");
            AddWord(ref enrus, ref rusen, "spring", "ключ");
            AddWord(ref enrus, ref rusen, "spring", "ключ");
            AddWord(ref enrus, ref rusen, "spring", "ключ");
            AddWord(ref enrus, ref rusen, "spring", "весна");

            Console.WriteLine("---");

            //main loop
            do
            {
                Console.WriteLine(
                    " 1 - add word Ru-En\n " +
                    "2 - add word En-Ru\n " +
                    "3 - print Ru-En\n " +
                    "4 - print En-Ru\n " +
                    "5 translate Ru-En\n " +
                    "6 translate En-Ru\n " +
                    "q - quit\n ");
                Console.Write("input action:");
                inkey = Console.ReadKey();
                Console.WriteLine();
                switch (inkey.Key)
                {
                    case ConsoleKey.D1:
                    {
                        AddWord(ref rusen, ref enrus, ReadWord(), ReadWord("input translation:"));
                        break;
                    }
                    case ConsoleKey.D2:
                    {
                        AddWord(ref enrus, ref enrus, ReadWord(), ReadWord("input translation:"));
                        break;
                    }
                    case ConsoleKey.D3:
                    {
                        PrintDic(rusen);
                        Console.WriteLine("---");
                        break;
                    }
                    case ConsoleKey.D4:
                    {
                        PrintDic(enrus);
                        Console.WriteLine("---");
                        break;
                    }
                    case ConsoleKey.D5:
                    {
                        PrintTranslation(rusen, ReadWord());
                        Console.WriteLine("---");
                        break;
                    }
                    case ConsoleKey.D6:
                    {
                        PrintTranslation(enrus, ReadWord());
                        Console.WriteLine("---");
                        break;
                    }

                }
            } while (inkey.Key != ConsoleKey.Q);
            Console.WriteLine("program quit. press any key.");
            Console.ReadKey();
        }

        static void AddWord(ref Dictionary<string, List<string>> dict, ref Dictionary<string, List<string>> dict2, string word, string translation)
        {
            List<string> wordList;
            word = word.ToLower();
            translation = translation.ToLower();

            if (!dict.TryGetValue(word, out wordList))
            {
                wordList = new List<string>();
                dict.Add(word, wordList);
            }
            else if (wordList.Contains(translation)) { return; }
            
            wordList.Add(translation);
            //здесь можно бы и сократить проверку.
            if (!dict2.TryGetValue(translation, out wordList))
            {
                wordList = new List<string>();
                dict2.Add(translation, wordList);
            }
            wordList.Add(word);
        }

        static string ReadWord(string text = "input word:")
        { 
            Console.Write(text);
            string aword = Console.ReadLine();
            string lower = aword?.ToLower();
            return lower?.Trim(' ');
        }

        static void PrintDic(Dictionary<string, List<string>> dict)
        {
            foreach (var vWord in dict)
            {
                Console.Write($" {vWord.Key} - ");
                foreach (var aTranslate in vWord.Value)
                {
                    Console.Write($" {aTranslate}, ");
                }
                Console.WriteLine();
            }
        }

        static void PrintTranslation(Dictionary<string, List<string>> dict, string word)
        {
            List<string> translationsList;
            if (dict.TryGetValue(word, out translationsList))
            {
                Console.Write($" {word} - ");
                foreach (var translation in translationsList)
                {
                    Console.Write($"{translation}, ");
                }
            }
            else
            {
                Console.WriteLine("not in dictonary");
            }
            Console.WriteLine();
        }
    }
}
