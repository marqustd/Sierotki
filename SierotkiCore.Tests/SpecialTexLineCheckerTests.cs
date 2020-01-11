using FluentAssertions;
using NUnit.Framework;
using SierotkiCore.Logic.Lines;
using SierotkiCore.Logic.Lines.TexRegex;

namespace SierotkiCore.Tests
{
    [TestFixture]
    internal class SpecialTexLineCheckerTests
    {
        private SpecialTexLineChecker CreateSut()
        {
            return new SpecialTexLineChecker(new TexBeginRegexMaker());
        }


        [TestCase(@"\begin{lstlisting}[language=Python, firstline=37, lastline=45]", @"\end{lstlisting}")]
        [TestCase(@"\begin[language=Python, firstline=37, lastline=45]{lstlisting}", @"\end{lstlisting}")]
        [TestCase(@"\begin{lstlisting}", @"\end{lstlisting}")]
        public void CheckIfSpecialFragment_WhenSpecialFragmentProvided_ThenItShouldDetectIt(string startLine, string endLine)
        {
            var sut = CreateSut();
            sut.CheckIfSpecialFragment("Some line").Should().BeFalse();
            sut.CheckIfSpecialFragment(startLine).Should().BeTrue();
            sut.CheckIfSpecialFragment("Some line").Should().BeTrue();
            sut.CheckIfSpecialFragment(endLine).Should().BeFalse();
            sut.CheckIfSpecialFragment("Some line").Should().BeFalse();
        }
    }
}
