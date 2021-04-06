using System;
using System.Globalization;
using System.Text;

namespace Brt.NetStandard.Util
{
    public static class StringExtensions
    {
        private static CultureInfo _cultureProvider = CultureInfo.InvariantCulture;
        private static bool[] _lookup = CreateLookupTable();
        private static string[] _formatosData = { "ddMMyyyy", 
            "dd-MM-yyyy", 
            "dd-MM-yyyy HH:mm", 
            "dd-MM-yyyy HH:mm:ss", 
            "yyyy-MM-dd", 
            "yyyy-MM-dd HH:mm", 
            "yyyy-MM-dd HH:mm:ss", 
            "dd/MM/yyyy", 
            "dd/MM/yyyy HH:mm", 
            "dd/MM/yyyy HH:mm:ss",
            "dd.MM.yyyy",
            "dd.MM.yyyy HH:mm:ss"};

        public static string RemoveDiacritics(this string value)
        {
            var normalizedString = value.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static DateTime? ParseDate(this string value)
        {
            if (!string.IsNullOrEmpty(value))
                return DateTime.ParseExact(value, _formatosData, _cultureProvider, DateTimeStyles.None);

            return null;
        }

        public static string RemoveSpecialCharacters(this string str)
        {
            CreateLookupTable();

            char[] buffer = new char[str.Length];
            int index = 0;
            foreach (char c in str)
            {
                if (_lookup[c])
                {
                    buffer[index] = c;
                    index++;
                }
            }

            return new string(buffer, 0, index);
        }

        private static bool[] CreateLookupTable()
        {
            var auxArray = new bool[65536];
            for (char c = '0'; c <= '9'; c++) auxArray[c] = true;
            for (char c = 'A'; c <= 'Z'; c++) auxArray[c] = true;
            for (char c = 'a'; c <= 'z'; c++) auxArray[c] = true;
            return auxArray;
        }
    }
}
