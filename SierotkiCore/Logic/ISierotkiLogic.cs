using System.Threading.Tasks;

namespace SierotkiCore.Logic
{
    public interface ISierotkiLogic
    {
        string ConcatOrphansInLine(string line);
        Task LogicLol(string filepath);
    }
}