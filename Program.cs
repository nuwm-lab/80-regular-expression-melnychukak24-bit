using System;
using System.Text.RegularExpressions;

namespace RegularExpressionExample
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string text = " приклад: https://example.com/photo.jpg і ще https://site.com/image.gif.";

            
            string pattern = @"https?:\/\/\S+\.(jpg|png|gif)";

            
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            
            MatchCollection matches = regex.Matches(text);

            if (matches.Count > 0)
            {
                Console.WriteLine(" Знайдено посилання на зображення:");
                foreach (Match match in matches)
                {
                    Console.WriteLine(match.Value);
                }
            }
            else
            {
                Console.WriteLine(" Посилань на зображення не знайдено.");
            }
        }
    }
}
