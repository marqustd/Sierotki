using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SierotkiCore.Logic.Files
{
    internal sealed class FilesLogic : IFilesLogic
    {
        public async IAsyncEnumerable<string> ReadDocumentAsync(string filepath)
        {
            using var file = new StreamReader(filepath);
            var line = string.Empty;
            while ((line = await file.ReadLineAsync()) != null)
            {
                yield return line;
            }
            file.Close();
        }

        public async Task WriteDocumentAsync(string filepath, IAsyncEnumerable<string> lines)
        {
            using var file = new StreamWriter(filepath);
            await foreach (var line in lines)
            {
                await file.WriteLineAsync(line);
            }
            file.Close();
        }

        public void CopyFile(string filepath, string newFilepath)
        {
            File.Copy(filepath, newFilepath);
        }

        public IEnumerable<string> GetPathToAllFiles(string path, string extension)
        {
            return Directory.GetFiles(path, extension, SearchOption.AllDirectories);
        }
    }
}
