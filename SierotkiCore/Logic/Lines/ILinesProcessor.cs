using System.Collections.Generic;

namespace SierotkiCore.Logic.Lines
{
    public interface ILinesProcessor
    {
        IAsyncEnumerable<string> ReplaceSpacesInLinesAsync(IAsyncEnumerable<string> lines);
    }
}