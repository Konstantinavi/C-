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
        static void DrawColors(List<Color> colors, string outputFile)
        {
             if (colors.Count == 0)
             {
                 Console.WriteLine("Нет цветов для отрисовки.");
                 return;
             }
             int squareSize = 40;
             int columns = (int)Math.Ceiling(Math.Sqrt(colors.Count));
             int rows = (int)Math.Ceiling((double)colors.Count / columns);
             int bitmapWidth = columns * squareSize;
             int bitmapHeight = rows * squareSize;
             try
             {
                 using Bitmap bmp = new Bitmap(bitmapWidth, bitmapHeight);
                 using Graphics g = Graphics.FromImage(bmp);
                 g.Clear(Color.White);
                 for (int i = 0; i < colors.Count; i++)
                 {
                     int col = i % columns;
                     int row = i / columns;
                     int x = col * squareSize;
                     int y = row * squareSize;
                     using var brush = new SolidBrush(colors[i]);
                     g.FillRectangle(brush, x, y, squareSize, squareSize);
                     g.DrawRectangle(Pens.LightGray, x, y, squareSize, squareSize);
                 }

                 string finalPath = outputFile + ".png";
                 bmp.Save(finalPath, ImageFormat.Png);
                 Console.WriteLine($"Изображение успешно сохранено: {Path.GetFullPath(finalPath)}");
                 }
             catch (Exception e)
             {
                 Console.WriteLine($"Произошла ошибка при сохранении: {e.Message}");
             }
         }
        static void Main(string[] args)
        {
    
