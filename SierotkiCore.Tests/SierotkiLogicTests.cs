using NUnit.Framework;
using SierotkiCore.Logic.Files;
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
        [TestCase("python detector.py -h", "python detector.py -h")]
        public void ConcatOrphans_WhenTextProvided_ThenItShouldConcatOrpphansAndNextWordWithTylda(string text, string expected)
        {
            var sut = CreateSut();
            var result = sut.ConcatOrphansInLine(text);
            Assert.AreEqual(expected, result);
        }
    }
}