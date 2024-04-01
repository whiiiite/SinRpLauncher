using MonoTorrent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SinRpLauncher.Extentions
{
    public static class MagnetLinkExtentions
    {
        public static bool IsMagnetLink(this string str)
        {
            string pattern = @"^magnet:\?xt=urn:[a-zA-Z0-9]+";
            return Regex.IsMatch(str, pattern);
        }


        public static string? GetHashFromMagnetLink(this string magnetLink)
        {
            if (!IsMagnetLink(magnetLink)) return null;

            string pattern = @"btih:(\w+)";
            Match match = Regex.Match(magnetLink, pattern);
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            else
            {
                // Handle case where no hash is found
                return string.Empty;
            }
        }
    }
}
