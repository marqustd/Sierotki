using NUnit.Framework;
using SierotkiCore.Logic;
using System.Collections.Generic;

namespace SierotkiCore.Tests
{
    public class SierotkiLogicTests
    {
        private readonly IEnumerable<string> orphans = new[] {
            "albo", "wiêc", "lecz", "przez", "niech", "tylko" , "gdzie"
        };

        private SierotkiLogic CreateSut()
        {
            return new SierotkiLogic(new Models.Settings(), new FileLogic());
        }

        [TestCase("Jas i Malgosia", "Jas i~Malgosia")]
        [TestCase("jedz, bo ci wystygnie", "jedz, bo~ci~wystygnie")]
        public void ConcatOrphans_WhenTextProvided_ThenItShouldConcatOrpphansAndNextWordWithTylda(string text, string expected, int length)
        {
            var sut = CreateSut();
            var result = sut.ConcatOrphansInLine(text);
            Assert.AreEqual(expected, result);
        }
    }
}