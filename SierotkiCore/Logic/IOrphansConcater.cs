using System.Text.RegularExpressions;

namespace SierotkiCore.Logic
{
    public interface IOrphansConcater
    {
        string ConcatOrphansInLine(string line);
    }
}