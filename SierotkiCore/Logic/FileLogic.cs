using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SierotkiCore.Logic
{
    public sealed class FileLogic : IFileReader
    {
        public async IAsyncEnumerable<string> ReadDocumentAsync(string filepath)
        {
            using var file = new StreamReader(filepath);
            var line = string.Empty;
            while ((line = await file.ReadLineAsync()) != null)
            {
                yield return line;
            }
        }

        public async Task WriteDocumentAsync(string filepath, IAsyncEnumerable<string> lines)
        {
            using var file = new StreamWriter(filepath);
            await foreach (var line in lines)
            {
                await file.WriteLineAsync(line);
            }
        }
    }
}
