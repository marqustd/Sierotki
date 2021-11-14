using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SierotkiCore.Logic
{
    public interface ISierotkiLogic
    {
        Task ReplaceSpacesInTexFileAsync(string filepath);
        Task ReplaceSpacesInFolderAsync(string folderpath);
    }
}