using SierotkiCore.Logic.Files;
using SierotkiCore.Logic.Lines;
using System.Threading.Tasks;

namespace SierotkiCore.Logic
{
    internal sealed class SierotkiLogic : ISierotkiLogic
    {
        private readonly IFilesLogic filesLogic;
        private readonly ILinesProcessor linesProcessor;

        public SierotkiLogic(IFilesLogic fileLogic, ILinesProcessor linesProcessor)
        {
            filesLogic = fileLogic;
            this.linesProcessor = linesProcessor;
        }

        public async Task ReplaceSpacesInFolderAsync(string folderpath)
        {
            var files = filesLogic.GetPathToAllFiles(folderpath, "*.tex");
            foreach (var file in files)
            {
                await ReplaceSpacesInTexFileAsync(file);
            }
        }

        public async Task ReplaceSpacesInTexFileAsync(string filepath)
        {
            var newFilePath = filepath + "(copy)";
            filesLogic.CopyFile(filepath, newFilePath);
            var oryginalLines = filesLogic.ReadDocumentAsync(newFilePath);
            var newLines = linesProcessor.ReplaceSpacesInLinesAsync(oryginalLines);
            await filesLogic.WriteDocumentAsync(filepath, newLines);
        }
    }
}
