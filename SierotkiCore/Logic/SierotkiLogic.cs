using SierotkiCore.Logic.Files;
using SierotkiCore.Logic.Lines;
using System.Threading.Tasks;

namespace SierotkiCore.Logic
{
    internal sealed class SierotkiLogic : ISierotkiLogic
    {
        private readonly IFilesLogic filesLogic;
        private readonly IOrphansProcessor orphansProcessor;

        public SierotkiLogic(IFilesLogic fileLogic, IOrphansProcessor orphansProcessor)
        {
            filesLogic = fileLogic;
            this.orphansProcessor = orphansProcessor;
        }

        public async Task ConcatOrphansInFolderAsync(string folderpath)
        {
            var files = filesLogic.GetPathToAllFiles(folderpath, "*.tex");
            foreach (var file in files)
            {
                await ConcatOrphansInTexFileAsync(file);
            }
        }

        public async Task ConcatOrphansInTexFileAsync(string filepath)
        {
            var newFilePath = filepath + "(copy)";
            filesLogic.CopyFile(filepath, newFilePath);
            var oryginalLines = filesLogic.ReadDocumentAsync(newFilePath);
            var newLines = orphansProcessor.ConcatOrphansInLinesAsync(oryginalLines);
            await filesLogic.WriteDocumentAsync(filepath, newLines);
        }
    }
}
