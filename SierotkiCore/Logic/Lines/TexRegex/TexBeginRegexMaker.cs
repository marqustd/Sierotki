using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SierotkiCore.Logic.Lines.TexRegex
{
    internal class TexBeginRegexMaker : ITexBeginRegexMaker
    {
        private readonly IList<string> pauseProccesingKeyWords = new[] { "lstlisting" };

        public Regex PrepareSpecialEndLineRegex()
        {
            var sb = new StringBuilder();
            sb.Append(CreateNewEndSpecialLinePattern(pauseProccesingKeyWords.First()));
            for (var i = 1; i < pauseProccesingKeyWords.Count; i++)
            {
                var keyWord = pauseProccesingKeyWords[i];
                sb.Append("|" + CreateNewEndSpecialLinePattern(keyWord));
            }
            var pattern = sb.ToString();
            return new Regex(pattern);
        }

        public Regex PrepareSpecialBeginLineRegex()
        {
            var sb = new StringBuilder();
            sb.Append(CreateNewBeginSpecialLinePattern(pauseProccesingKeyWords.First()));
            for (var i = 1; i < pauseProccesingKeyWords.Count; i++)
            {
                var keyWord = pauseProccesingKeyWords[i];
                sb.Append("|" + CreateNewBeginSpecialLinePattern(keyWord));
            }
            var pattern = sb.ToString();
            return new Regex(pattern);
        }

        private string CreateNewBeginSpecialLinePattern(string keyWord)
        {
            var option1 = $"\\\\begin{{{keyWord}}}\\[.*\\]";
            var option2 = $"\\\\begin\\[.*\\]{{{keyWord}}}";
            var option3 = $"\\\\begin{{{keyWord}}}";
            var pattern = $"{option1}|{option2}|{option3}";
            return pattern; ;
        }

        private string CreateNewEndSpecialLinePattern(string keyWord)
        {
            var pattern = $"\\\\end{{{keyWord}}}";
            return pattern;
        }
    }
}
