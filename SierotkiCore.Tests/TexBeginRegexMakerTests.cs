using FluentAssertions;
using NUnit.Framework;
using SierotkiCore.Logic.Lines.TexRegex;

namespace SierotkiCore.Tests
{
    [TestFixture]
    internal class RegexMakerTests
    {
        private TexBeginRegexMaker CreateSut()
        {
            return new TexBeginRegexMaker();
        }

        [TestCase(@"\begin{lstlisting}[language=Python, firstline=37, lastline=45]")]
        [TestCase(@"\begin[language=Python, firstline=37, lastline=45]{lstlisting}")]
        [TestCase(@"\begin{lstlisting}")]
        public void PrepareSpecialBeginLineRegex_WhenLineProvide_ThenItShouldFindMatch(string line)
        {
            var sut = CreateSut();
            var regex = sut.PrepareSpecialBeginLineRegex();
            regex.IsMatch(line).Should().BeTrue();
        }

        [TestCase(@"some line")]
        public void PrepareSpecialBeginLineRegex_WhenLineProvide_ThenItShouldNotFindMatch(string line)
        {
            var sut = CreateSut();
            var regex = sut.PrepareSpecialBeginLineRegex();
            regex.IsMatch(line).Should().BeFalse();
        }

        [TestCase(@"\end{lstlisting}")]
        public void PrepareSpecialEndLineRegex_WhenLineProvide_ThenItShouldFindMatch(string line)
        {
            var sut = CreateSut();
            var regex = sut.PrepareSpecialEndLineRegex();
            regex.IsMatch(line).Should().BeTrue();
        }

        [TestCase(@"some line")]
        public void PrepareSpecialEndLineRegex_WhenLineProvide_ThenItShouldNotFindMatch(string line)
        {
            var sut = CreateSut();
            var regex = sut.PrepareSpecialEndLineRegex();
            regex.IsMatch(line).Should().BeFalse();
        }
    }
}
