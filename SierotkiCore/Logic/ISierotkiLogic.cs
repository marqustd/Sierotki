using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("SierotkiCore.Tests")]
namespace SierotkiCore.Logic
{
    public interface ISierotkiLogic
    {
        Task ConcatOrphansInTexFileAsync(string filepath);
        Task ConcatOrphansInFolderAsync(string folderpath);
    }
}