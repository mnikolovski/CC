using System.Text.RegularExpressions;
using CC.Common.Helpers.Instance;

namespace CC.Common.Helpers.Regexes
{
    public static class RegexHelper
    {
        public static readonly string IsValidEmail = @"^([0-9a-zA-Z]([+-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$";
        public static readonly string IsValidPasswordFormat = @"^.{{{0},}}$";

        /// <summary>
        /// Regex matcher
        /// </summary>
        /// <param name="value"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool IsMatch(this string value, string pattern)
        {
            return value.IsMatch(pattern, RegexOptions.None);
        }

        /// <summary>
        /// Regex matcher
        /// </summary>
        /// <param name="value"></param>
        /// <param name="pattern"></param>
        /// <param name="regexOptions"></param>
        /// <returns></returns>
        public static bool IsMatch(this string value, string pattern, RegexOptions regexOptions)
        {
            if (value.IsNullOrEmpty() || pattern.IsNullOrEmpty()) return false;
            return Regex.IsMatch(value, pattern, regexOptions);
        }

        /// <summary>
        /// Return all regex group matches
        /// </summary>
        /// <param name="value"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static MatchCollection GetMatches(this string value, string pattern)
        {
            if (value.IsNullOrEmpty()) return null;
            var _regex = new Regex(pattern);
            var _regexMatches = _regex.Matches(value);
            return _regexMatches;
        }

        /// <summary>
        /// Return all regex group matches
        /// </summary>
        /// <param name="value"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static Match GetMatch(this string value, string pattern)
        {
            if (value.IsNullOrEmpty()) return Match.Empty;
            var _regex = new Regex(pattern);
            var _regexMatch = _regex.Match(value);
            return _regexMatch;
        }

        /// <summary>
        /// Replace text that match the provided pattern with the provided replacement
        /// (Ignore Case is used for compuling the regex)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="pattern"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        public static string Replace(string value, string pattern, string replacement)
        {
            if(value.IsNullOrEmpty()) return value;
            return Regex.Replace(value, pattern, replacement, RegexOptions.IgnoreCase);
        }


        /// <summary>
        /// Replace text that match the provided pattern with the provided replacement
        /// (Ignore Case is used for compuling the regex)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="pattern"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        public static string ReplaceFirst(string value, string pattern, string replacement)
        {
            if (value.IsNullOrEmpty()) return value;
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.Replace(value, replacement, 1);
        }
    }
}
