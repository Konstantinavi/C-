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
