using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RegularExpressionExample
{
    /// <summary>
    /// Клас для пошуку шаблонів у тексті за допомогою регулярних виразів.
    /// </summary>
    public static class TextPatternFinder
    {
        // Константа для шаблону посилань на зображення
        private const string ImageUrlPattern =
            @"https?:\/\/[^\s\)\]\>\""',;]+?\.(jpg|png|gif)(?=$|\s|[\)\]\>\""',;:?!])";

        /// <summary>
        /// Виконує пошук збігів у тексті за заданим шаблоном.
        /// </summary>
        /// <param name="text">Вхідний текст.</param>
        /// <param name="pattern">Регулярний вираз.</param>
        /// <param name="options">Параметри Regex (за замовчуванням IgnoreCase + Compiled).</param>
        /// <returns>Колекція знайдених збігів.</returns>
        public static MatchCollection SearchWithRegex(string text, string pattern, 
            RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Текст не може бути порожнім.", nameof(text));

            try
            {
                Regex regex = new Regex(pattern, options);
                return regex.Matches(text);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"❌ Помилка в шаблоні регулярного виразу: {ex.Message}");
                return MatchCollection.Empty;
            }
        }

        /// <summary>
        /// Знаходить усі посилання на зображення у форматі .jpg, .png або .gif.
        /// </summary>
        /// <param name="text">Вхідний текст.</param>
        /// <returns>Список знайдених посилань.</returns>
        public static List<string> FindImageLinks(string text)
        {
            List<string> results = new();

            foreach (Match match in SearchWithRegex(text, ImageUrlPattern))
                results.Add(match.Value);

            return results;
        }
    }

    class Program
    {
        static void Main()
        {
            string text = "Приклади: https://example.com/img/photo.jpg, http://site.net/pic.png?ver=2, " +
                          "і навіть https://host.com/gif/image.gif.";

            try
            {
                var links = TextPatternFinder.FindImageLinks(text);

                if (links.Count > 0)
                {
                    Console.WriteLine("🔍 Знайдені посилання на зображення:");
                    foreach (var link in links)
                        Console.WriteLine(link);
                }
                else
                {
                    Console.WriteLine("❌ Посилань на зображення не знайдено.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Виникла помилка: {ex.Message}");
            }
        }
    }
}
