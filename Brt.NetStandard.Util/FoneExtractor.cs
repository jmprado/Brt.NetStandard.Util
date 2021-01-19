using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Brt.NetStandard.Util
{
    public static class FoneExtractor
    {
        private readonly static Regex regExFone = new Regex(@"\(?[1-9]{2}\)? ?(?:[2-8]|9? ?[1-9])[0-9]{3}\-?[0-9]{4}");

        public static string[] GetFones(string text)
        {
            var m = regExFone.Matches(text);

            var aux = new string[m.Count];
            var i = 0;

            foreach (var match in m)
            {
                aux[i] = match.ToString();
                i++;
            }

            return aux;
        }
    }
}
