using System.Collections.Generic;

namespace SierotkiCore.Logic.Lines
{
    public interface IOrphansProcessor
    {
        IAsyncEnumerable<string> ConcatOrphansInLinesAsync(IAsyncEnumerable<string> lines);
    }
}