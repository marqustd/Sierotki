using System.Threading.Tasks;

namespace SierotkiCore.Logic
{
    public interface ISierotkiLogic
    {
        Task ConcatOrphansInTexFileAsync(string filepath);
    }
}