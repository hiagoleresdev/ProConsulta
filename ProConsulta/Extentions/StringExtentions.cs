using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text.RegularExpressions;

namespace ProConsulta.Extentions
{
    public static class StringExtentions
    {
        public static string SomenteCaracteres(this string input)
        {
            if(string.IsNullOrEmpty(input))
                return input;

            string pattern = @"[-\.\(\)\a]";
            string result = Regex.Replace(input, pattern, string.Empty);

            return result;  
        }
    }
}
