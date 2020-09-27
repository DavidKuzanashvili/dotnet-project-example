using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;

namespace App.Infrastructure.Utils.Common
{
    public static class Slugifier
    {
        public static string Slugify(string str, int charCount = 90)
        {
            if (string.IsNullOrEmpty(str)) return null;

            str = StrimHTML(str);

            var geoAlpha = new List<string> { "ა", "ბ", "გ", "დ", "ე", "ვ", "ზ", "თ", "ი", "კ", "ლ", "მ", "ნ", "ო", "პ", "ჟ", "რ", "ს", "ტ", "უ", "ფ", "ქ", "ღ", "ყ", "შ", "ჩ", "ც", "ძ", "წ", "ჭ", "ხ", "ჯ", "ჰ" };
            var geoEngAlpha = new List<string> { "a", "b", "g", "d", "e", "v", "z", "t", "i", "k", "l", "m", "n", "o", "p", "zh", "r", "s", "t", "u", "p", "k", "gh", "k", "sh", "ch", "ts", "dz", "ts", "ch", "kh", "j", "h" };

            var rusAlpha = new List<string> { "а", "б", "в", "г", "д", "е", "ё", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п", "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ъ", "ы", "ь", "э", "ю", "я" };
            var rusUpAlpha = new List<string> { "А", "Б", "В", "Г", "Д", "Е", "Ё", "Ж", "З", "И", "Й", "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ц", "Ч", "Ш", "Щ", "Ъ", "Ы", "Ь", "Э", "Ю", "Я" };
            var rusEngAlpha = new List<string> { "a", "b", "v", "g", "d", "ye", "yo", "zh", "z", "ee", "y", "k", "l", "m", "n", "o", "p", "r", "s", "t", "u", "f", "kh", "ts", "ch", "sh", "sh", "i", "i", "", "", "", "" };

            for (var i = 0; i < geoAlpha.Count; i++)
            {
                str = str.Replace(geoAlpha[i], geoEngAlpha[i]);
            }

            for (var i = 0; i < rusAlpha.Count; i++)
            {
                str = str.Replace(rusAlpha[i], rusEngAlpha[i]);
                str = str.Replace(rusUpAlpha[i], rusEngAlpha[i]);
            }

            str = Regex.Replace(str, @"[^a-zA-Z0-9\s-]", "-"); // invalid chars      
            str = Regex.Replace(str, @"\s+", " ").Trim(); // convert multiple spaces into one space   

            str = str.Substring(0, str.Length <= charCount ? str.Length : charCount).Trim(); // cut and trim it   

            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            str = Regex.Replace(str, @"\-+", "-").Trim();
            str = Regex.Replace(str, @"\-$", "").Trim();

            return str;

        }

        public static string StrimHTML(string html)
        {
            if (string.IsNullOrEmpty(html)) return "";
            return HttpUtility.HtmlDecode(Regex.Replace(html, "<.*?>", string.Empty));
        }
    }
}
