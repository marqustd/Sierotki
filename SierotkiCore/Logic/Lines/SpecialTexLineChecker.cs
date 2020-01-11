using SierotkiCore.Logic.Lines.TexRegex;
using System.Text.RegularExpressions;

namespace SierotkiCore.Logic.Lines
{
    internal class SpecialTexLineChecker : ISpecialTexLineChecker
    {
        private readonly Regex startSpecialLineRegex;
        private readonly Regex stopSpecialLineRegex;
        private bool isSpecialFragment = false;

        public SpecialTexLineChecker(ITexBeginRegexMaker regexMaker)
        {
            startSpecialLineRegex = regexMaker.PrepareSpecialBeginLineRegex();
            stopSpecialLineRegex = regexMaker.PrepareSpecialEndLineRegex();
        }

        public bool CheckIfSpecialFragment(string line)
        {
            if (isSpecialFragment)
            {
                if (stopSpecialLineRegex.IsMatch(line))
                {
                    isSpecialFragment = false;
                }
            }
            else
            {
                if (startSpecialLineRegex.IsMatch(line))
                {
                    isSpecialFragment = true;
                }
            }

            return isSpecialFragment;
        }
    }
}
