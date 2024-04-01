using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Launcher.Classes
{
    /// <summary>
    /// Helper for working with string and char pointers(char arrays)
    /// </summary>
    public class StringHelper
    {
        public static int CountChar(string str, char ch)
        {
            int c = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (ch == str[i])
                    c++;
            }
            return c;
        }


        /// <summary>
        /// Replace all found chars in string
        /// </summary>
        /// <param name="str"></param>
        /// <param name="chFind"></param>
        /// <param name="chReplace"></param>
        /// <returns></returns>
        public static string ReplaceAllChars(string str, char chFind, char chReplace)
        {
            string tmp = string.Empty;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == chFind)
                    tmp += chReplace;
                else
                    tmp += str[i];
            }

            return tmp;
        } 


        /// <summary>
        /// Join char array to string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string JoinCharsArray(char[] str)
        {
            string strJoined = string.Empty;
            for (int i = 0; i < str.Length; i++)
            {
                strJoined += str[i];
            }

            return strJoined;
        }


        /// <summary>
        /// Replace first char in string
        /// </summary>
        /// <param name="str"></param>
        /// <param name="rep"></param>
        /// <returns></returns>
        public static string ReplaceFirst(string str, char rep)
        {
            char[] chs = str.ToCharArray();
            chs[0] = rep;
            str = StringHelper.JoinCharsArray(chs);
            return str;
        }


        /// <summary>
        /// Cut chars from end of string for make it right size
        /// </summary>
        /// <param name="str"></param>
        /// <param name="needSize"></param>
        /// <returns></returns>
        public static string CutStringSize(string str, int needSize)
        {
            int lnth = str.Length;
            if (lnth == needSize || lnth < needSize)
                return str;

            while(str.Length != needSize)
            {
                str = str.Remove(str.Length - 1);
            }

            return str;
        }


        /// <summary>
        /// Push char to string
        /// </summary>
        /// <param name="str"></param>
        /// <param name="pushChar"></param>
        /// <returns></returns>
        public static string PushFront(string str, char pushChar)
        {
            return pushChar + str;
        }


        /// <summary>
        /// Push char to back of char array
        /// </summary>
        /// <param name="strPtr"></param>
        /// <param name="appendChar"></param>
        /// <returns></returns>
        public static char[] PushBackCharArray(char[] strPtr, char appendChar)
        {
            int i = 0;
            bool isPushed = false;
            while (!isPushed)
            {
                if (strPtr[i] == '\0')
                {
                    strPtr[i] = appendChar;
                    isPushed = true;
                }
                else if (strPtr[i] != '\0' && i+1 == strPtr.Length) // if next iteration is limit of array
                {
                    strPtr[i] = appendChar;
                    isPushed = true;
                }
                i++;
            }

            return strPtr;
        }


        public static string PushBack(string str, char pushChar)
        {
            return str + pushChar;
        }


        public static bool HasNonAsciiChars(string str)
        {
            // 1 ascii char is 1 byte. If Bytes in string more than string lenght it means
            // that string have non ascii char
            // if bytes in string and string lenght isnt equal, return true. Else false 
            return (Encoding.UTF8.GetByteCount(str) != str.Length);
        }


        public static bool HasVowelsLetters(string str)
        {
            string vowelsLetters = "EUOIAeuoia";
            foreach(char ch in str)
            {
                if (vowelsLetters.Contains(ch)) 
                    return true;
            }

            return false;
        }


        public static string StripHtml(string str)
        {
            return Regex.Replace(str, "<.*?>", String.Empty);
        }
    }
}
