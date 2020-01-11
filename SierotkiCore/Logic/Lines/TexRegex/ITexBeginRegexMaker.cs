using System.Text.RegularExpressions;

namespace SierotkiCore.Logic.Lines.TexRegex
{
    public interface ITexBeginRegexMaker
    {
        Regex PrepareSpecialBeginLineRegex();
        Regex PrepareSpecialEndLineRegex();
    }
}