using System.Collections.Generic;

namespace SierotkiCore.Logic.Lines
{
    internal class LinesProcessor : ILinesProcessor
    {
        private readonly ISpacesReplacer spacesReplacer;
        private readonly ISpecialTexLineChecker specialLineChecker;
        private bool startLineOccured = false;
        private readonly string startLine = @"\begin{document}";

        public LinesProcessor(ISpacesReplacer spacesReplacer, ISpecialTexLineChecker specialLineChecker)
        {
            this.spacesReplacer = spacesReplacer;
            this.specialLineChecker = specialLineChecker;
        }

        public async IAsyncEnumerable<string> ReplaceSpacesInLinesAsync(IAsyncEnumerable<string> lines)
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
                    line = spacesReplacer.ReplaceSpacesInLine(line);
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
