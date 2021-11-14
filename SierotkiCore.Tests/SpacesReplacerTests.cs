using NUnit.Framework;
using SierotkiCore.Logic.Lines;
using SierotkiCore.Models;

namespace SierotkiCore.Tests
{
    [TestFixture]
    public class SpacesReplacerTests
    {
        private ISpacesReplacer CreateSut()
        {
            return new SpacesReplacer(new Settings
            {
                Words = new[] { "albo" }
            });
        }

        [TestCase("Jas i Malgosia", "Jas i~Malgosia")]
        [TestCase("python detector.py -h", "python detector.py -h")]
        [TestCase("piatek albo wtorek", "piatek albo~wtorek")]
        public void ReplaceSpacesInLine_WhenTextProvided_ThenItShouldConcatOrpphansAndNextWordWithTylda(string text, string expected)
        {
            var sut = CreateSut();
            var result = sut.ReplaceSpacesInLine(text);
            Assert.AreEqual(expected, result);
        }
    }
}