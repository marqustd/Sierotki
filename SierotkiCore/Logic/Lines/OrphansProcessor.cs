using System.Collections.Generic;

namespace SierotkiCore.Logic.Lines
{
    internal class OrphansProcessor : IOrphansProcessor
    {
        private readonly IOrphansConcater orphansConcater;
        private readonly ISpecialTexLineChecker specialLineChecker;
        private bool startLineOccured = false;
        private readonly string startLine = @"\begin{document}";

        public OrphansProcessor(IOrphansConcater orphansConcater, ISpecialTexLineChecker specialLineChecker)
        {
            this.orphansConcater = orphansConcater;
            this.specialLineChecker = specialLineChecker;
        }

        public async IAsyncEnumerable<string> ConcatOrphansInLinesAsync(IAsyncEnumerable<string> lines)
        {
            await foreach (var line in lines)
            {
                var newLine = line;

                yield return ProccesLine(newLine);
            }
        }

        private string ProccesLine(string line)
        {
            if (startLineOccured)
            {

                if (!specialLineChecker.CheckIfSpecialFragment(line))
                {
                    line = orphansConcater.ConcatOrphansInLine(line);
                }
            }
            else
            {
                if (line == startLine)
                {
                    startLineOccured = true;
                }
            }

            return line;
        }
    }
}
