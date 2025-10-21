using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RegularExpressionExample
{
    class Program
    {
        // === 1️⃣ Підготовка шаблонів ===
        // Усі патерни зібрані в словник: ключ — назва типу, значення — Regex
        private static readonly Dictionary<string, Regex> Patterns = new Dictionary<string, Regex>
        {
            // Зображення (jpg, png, gif, svg, webp)
            ["Images"] = new Regex(
                @"https?:\/\/[^\s""'<>]+?\.(jpg|png|gif|svg|webp)\b(?:[?#][^\s""'<>]*)?",
                RegexOptions.IgnoreCase | RegexOptions.Compiled),

            // Звичайні URL (будь-які сайти, навіть без зображень)
            ["URLs"] = new Regex(
                @"https?:\/\/[^\s""'<>]+",
                RegexOptions.IgnoreCase | RegexOptions.Compiled),

            // Дати у форматі dd.mm.yyyy або dd/mm/yyyy
            ["Dates"] = new Regex(
                @"\b\d{1,2}[./]\d{1,2}[./]\d{2,4}\b",
                RegexOptions.Compiled),

            // IPv4-адреси
            ["IP Addresses"] = new Regex(
                @"\b(?:(?:25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(?:25[0-5]|2[0-4]\d|[01]?\d\d?)\b",
                RegexOptions.Compiled)
        };

        static void Main(string[] args)
        {
            string text = @"
                Ось приклади даних:
                - Зображення: https://example.com/photo.jpg
                - Ще одне: http://images.site.org/pic.webp?ver=2
                - Звичайний сайт: https://mysite.ua/page?id=10
                - Дата: 21.10.2025
                - IP: 192.168.1.1
                - Ще IP: 8.8.8.8
            ";

            // === 2️⃣ Пошук і підрахунок ===
            var results = FindAllPatterns(text);

            // === 3️⃣ Вивід результатів ===
            Console.WriteLine("🔍 Результати пошуку:\n");

            foreach (var kvp in results)
            {
                string patternName = kvp.Key;
                List<string> foundItems = kvp.Value;

                Console.WriteLine($"🟩 {patternName} ({foundItems.Count} знайдено):");

                if (foundItems.Count > 0)
                {
                    foreach (var item in foundItems)
                        Console.WriteLine("   • " + item);
                }
                else
                {
                    Console.WriteLine("   — нічого не знайдено.");
                }

                Console.WriteLine();
            }

            // === 4️⃣ Загальна статистика ===
            Console.WriteLine("📊 Підсумок:");
            foreach (var kvp in results)
            {
                Console.WriteLine($"   {kvp.Key}: {kvp.Value.Count}");
            }
        }

        /// <summary>
        /// Здійснює пошук усіх збігів для всіх шаблонів одночасно.
        /// </summary>
        private static Dictionary<string, List<string>> FindAllPatterns(string input)
        {
            var output = new Dictionary<string, List<string>>();

            foreach (var kvp in Patterns)
            {
                string name = kvp.Key;
                Regex regex = kvp.Value;

                var matches = regex.Matches(input);
                var list = new List<string>();

                foreach (Match match in matches)
                {
                    string value = match.Value.Trim();

                    // Додаємо лише унікальні результати
                    if (!list.Contains(value))
                        list.Add(value);
                }

                output[name] = list;
            }

            return output;
        }
    }
}
