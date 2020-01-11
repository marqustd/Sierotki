using SierotkiCore.Models;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SierotkiCore.Logic
{
    public class SierotkiLogic : ISierotkiLogic
    {
        private readonly Regex regex;
        private readonly Settings settings;
        private readonly FileLogic fileLogic;

        public SierotkiLogic(Settings settings, FileLogic fileReader)
        {
            this.settings = settings;
            fileLogic = fileReader;
            regex = CreateRegex();
        }

        private Regex CreateRegex()
        {
            var sb = new StringBuilder($"\\b\\w{{1,{settings.Length}}}\\b");

            foreach (var orphan in settings.Orphans)
            {
                sb.Append($"|\\b{orphan}\\b");
            }

            var pattern = sb.ToString();
            return new Regex(pattern);
        }


        public async Task LogicLol(string filepath)
        {
            await fileLogic.WriteDocumentAsync(filepath, ProcessDocumentAsync());
        }

        public async IAsyncEnumerable<string> ProcessDocumentAsync()
        {
            var startedProcessing = false;
            var startLine = "begin{document}";

            await foreach (var line in fileLogic.ReadDocumentAsync(settings.FilePath))
            {
                var newLine = line;
                if (startedProcessing)
                {
                    newLine = ConcatOrphansInLine(newLine);
                }
                else
                {
                    if (newLine.Contains(startLine))
                    {
                        startedProcessing = true;
                    }
                }
                yield return newLine;
            }
        }

        public string ConcatOrphansInLine(string line)
        {
            var matches = regex.Matches(line);
            var sb = new StringBuilder(line);
            foreach (Match match in matches)
            {
                var index = match.Index;
                var length = match.Value.Length;
                index += length;
                sb.Remove(index, 1);
                sb.Insert(index, "~");
            }

            return sb.ToString();
        }
    }
}
