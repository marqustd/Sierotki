using NUnit.Framework;
using SierotkiCore.Logic.Lines;
using SierotkiCore.Models;

namespace SierotkiCore.Tests
{
    [TestFixture]
    public class OrphansConcaterTests
    {
        private IOrphansConcater CreateSut()
        {
            return new OrphansConcater(new Settings
            {
                Orphans = new[] { "albo" }
            });
        }

        [TestCase("Jas i Malgosia", "Jas i~Malgosia")]
        [TestCase("python detector.py -h", "python detector.py -h")]
        [TestCase("piatek albo wtorek", "piatek albo~wtorek")]
        public void ConcatOrphans_WhenTextProvided_ThenItShouldConcatOrpphansAndNextWordWithTylda(string text, string expected)
        {
            var sut = CreateSut();
            var result = sut.ConcatOrphansInLine(text);
            Assert.AreEqual(expected, result);
        }
    }
}