using SierotkiCore.Logic.Files;
using SierotkiCore.Logic.Lines;
using System.Threading.Tasks;

namespace SierotkiCore.Logic
{
    internal sealed class SierotkiLogic : ISierotkiLogic
    {
        private readonly IFileLogic fileLogic;
        private readonly IOrphansProcessor orphansProcessor;

        public SierotkiLogic(IFileLogic fileLogic, IOrphansProcessor orphansProcessor)
        {
            this.fileLogic = fileLogic;
            this.orphansProcessor = orphansProcessor;
        }

        public async Task ConcatOrphansInTexFileAsync(string filepath)
        {
            var newFilePath = filepath + "(copy)";
            fileLogic.CopyFile(filepath, newFilePath);
            var oryginalLines = fileLogic.ReadDocumentAsync(newFilePath);
            var newLines = orphansProcessor.ConcatOrphansInLinesAsync(oryginalLines);
            await fileLogic.WriteDocumentAsync(filepath, newLines);
        }
    }
}
