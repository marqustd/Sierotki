using System.Collections.Generic;
using System.Threading.Tasks;

namespace SierotkiCore.Logic
{
    public interface IFileReader
    {
        IAsyncEnumerable<string> ReadDocumentAsync(string filepath);
        Task WriteDocumentAsync(string filepath, IAsyncEnumerable<string> lines);
    }
}