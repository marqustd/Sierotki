using SierotkiCore.Logic.Files;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SierotkiCore.Logic
{
    internal sealed class SierotkiLogic : ISierotkiLogic
    {
        private readonly FileLogic fileLogic;
        private readonly OrphansConcater orphansConcater;

        public SierotkiLogic(FileLogic fileLogic, OrphansConcater orphansConcater)
        {
            this.fileLogic = fileLogic;
            this.orphansConcater = orphansConcater;
        }

        public async Task ConcatOrphansInTexFileAsync(string filepath)
        {
            var newFilePath = filepath + "(copy)";
            fileLogic.CopyFile(filepath, newFilePath);
            var oryginalLines = fileLogic.ReadDocumentAsync(newFilePath);
            var newLines = ConcatOrphansInLinesAsync(oryginalLines);
            await fileLogic.WriteDocumentAsync(filepath, newLines);
        }

        private async IAsyncEnumerable<string> ConcatOrphansInLinesAsync(IAsyncEnumerable<string> lines)
        {
            var startedProcessing = false;
            var startLine = "begin{document}";

            await foreach (var line in lines)
            {
                var newLine = line;
                if (startedProcessing)
                {
                    newLine = orphansConcater.ConcatOrphansInLine(newLine);
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
    }
}
