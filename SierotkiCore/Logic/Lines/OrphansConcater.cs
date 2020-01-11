using SierotkiCore.Models;
using System.Text;
using System.Text.RegularExpressions;

namespace SierotkiCore.Logic.Lines
{
    internal sealed class OrphansConcater : IOrphansConcater
    {
        private readonly Regex regex;

        public OrphansConcater(Settings settings)
        {
            regex = CreateRegex(settings);
        }

        public string ConcatOrphansInLine(string line)
        {
            var matches = regex.Matches(line);
            var sb = new StringBuilder(line);
            foreach (Match match in matches)
            {
                var index = match.Index;
                var length = match.Value.Length;
                index += length - 1;
                sb.Remove(index, 1);
                sb.Insert(index, "~");
            }

            return sb.ToString();
        }

        private Regex CreateRegex(Settings settings)
        {
            var sb = new StringBuilder($"\\s\\w{{1,{settings.Length}}}\\s");

            foreach (var orphan in settings.Orphans)
            {
                sb.Append($"|\\s{orphan}\\s");
            }

            var pattern = sb.ToString();
            return new Regex(pattern, RegexOptions.IgnoreCase);
        }
    }
}
