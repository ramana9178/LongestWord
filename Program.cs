using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LongestWord
{
    class Program
    {
        private static bool isMadeOfWords(string word, HashSet<string> dict)
        {
            if (String.IsNullOrEmpty(word)) return false;
            if (word.Length == 1)
            {
                return dict.Contains(word);
            }
            foreach (var pair in generatePairs(word).Where(pair => dict.Contains(pair.Item1)))
            {
                return dict.Contains(pair.Item2) || isMadeOfWords(pair.Item2, dict);
            }
            return false;
        }

        private static IEnumerable<Tuple<string, string>> generatePairs(string word)
        {
            for (int i = 1; i < word.Length; i++)
            {
                yield return Tuple.Create(word.Substring(0, i), word.Substring(i));
            }
        }

        static void Main(string[] args)
        {
            string[] listOfWords = File.ReadAllLines(@"wordlist.txt");

            //Arrange the words in descending order(lengthwise)
            var sortedWords = listOfWords.OrderByDescending(word => word.Length);
            var dict = new HashSet<String>(sortedWords);

            Console.WriteLine("The First longest word is: " + sortedWords.FirstOrDefault(word => isMadeOfWords(word, dict)));
            Console.WriteLine("The Second longest word is: " + sortedWords.Where(word => isMadeOfWords(word, dict)).ElementAtOrDefault(1));
            Console.WriteLine("Number of words that can be constructed: " + sortedWords.Count(word => isMadeOfWords(word, dict)));

            Console.ReadLine();
        }
    }
}
