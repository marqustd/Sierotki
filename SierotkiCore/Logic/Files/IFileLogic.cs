using System.Collections.Generic;
using System.Threading.Tasks;

namespace SierotkiCore.Logic.Files
{
    public interface IFileLogic
    {
        void CopyFile(string filepath, string newFilepath);
        IAsyncEnumerable<string> ReadDocumentAsync(string filepath);
        Task WriteDocumentAsync(string filepath, IAsyncEnumerable<string> lines);
    }
}