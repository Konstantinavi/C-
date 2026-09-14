using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace laba1
{
    class Program
    {
        static Dictionary<string, Color> ColorMap = new(StringComparer.OrdinalIgnoreCase)
        {
            { "красн", Color.Red },
            { "ал", Color.Crimson },
            { "багр", Color.DarkRed },
            { "зелен", Color.Green },
            { "изумруд", Color.MediumSeaGreen },
            { "малахит", Color.MediumSeaGreen },
            { "син", Color.Blue },
            { "голуб", Color.LightBlue },
            { "лазур", Color.LightSkyBlue },
            { "ультрамарин", Color.Blue },
            { "желт", Color.Yellow },
            { "золот", Color.Gold },
            { "лимон", Color.LemonChiffon },
            { "бел", Color.White },
            { "черн", Color.Black },
            { "сер", Color.Gray },
            { "фиолетов", Color.Purple },
            { "лилов", Color.Purple },
            { "оранжев", Color.Orange },
            { "коричнев", Color.Brown },
            { "розов", Color.Pink },
            { "бирюз", Color.Turquoise },
        };
        static (string Name, string FullPath)[] FindAllTxt()
        {
            string buildFolder = AppDomain.CurrentDomain.BaseDirectory;
            return Directory.GetFiles(buildFolder, "*.txt", SearchOption.AllDirectories)
                    .Select(path => (Path.GetFileNameWithoutExtension(path), path))
                    .ToArray();
        }
        static (List<string> coloredWords, List<Color> colors) FindColors(string text)
        {
            var colorsList = new List<Color>();
            var coloredWords = new List<string>();
            string roots = string.Join("|", ColorMap.Keys.Select(Regex.Escape));
            string suffixes = @"(?:ов|н|ян)?(?:еньк|оньк)?";
            string endings = "ий|ый|ой|ая|ое|ее|ую|ого|его|их|ым|им|ыми|ими|ому|ему|ою|ею|ом|ем|ые|ие|ых|их|а|я|о|ы|и|ей";
            var pattern = $"^({roots}){suffixes}({endings})?$";
            var colorRegex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            var wordsMatches = Regex.Matches(text, @"\b[\p{IsCyrillic}a-zA-Z]+\b");
            foreach (Match wordMatch in wordsMatches)
            {
                 var match = colorRegex.Match(wordMatch.Value);
                if (match.Success)
                {
                    string key = match.Groups[1].Value.ToLower();
                    string wordLower = wordMatch.Value.ToLower();
                    if (wordLower.Length < 2 && !ColorMap.ContainsKey(wordLower))
                        continue;
                    coloredWords.Add(wordLower);
                    colorsList.Add(ColorMap[key]);
                    Console.WriteLine($"Найдено слово: {wordLower}");
                 }
            }
            return (coloredWords, colorsList);
        }
