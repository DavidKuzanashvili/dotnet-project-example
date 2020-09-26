using App.Domain.Utils.Settings;
using System.IO;

namespace App.Domain.Utils.Extensions
{
    public static class StringExtension
    {
        public static string GetDisplayUrl(this string fileName, string additionalPath = "")
        {
            var result = string.Empty;
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                result = Path.Combine(AppSettings.DisplayUrl, additionalPath, fileName);
            }
            return result;
        }
    }
}
