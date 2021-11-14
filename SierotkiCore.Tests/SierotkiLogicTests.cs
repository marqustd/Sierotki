using Moq;
using NUnit.Framework;
using SierotkiCore.Extensions;
using SierotkiCore.Logic;
using SierotkiCore.Logic.Files;
using SierotkiCore.Logic.Lines;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SierotkiCore.Tests
{
    internal class SierotkiLogicTests
    {
        private Mock<IFilesLogic> filesLogic;
        private Mock<ILinesProcessor> linesProcessor;

        [SetUp]
        public void Setup()
        {
            filesLogic = new Mock<IFilesLogic>();
            linesProcessor = new Mock<ILinesProcessor>();
        }

        private ISierotkiLogic CreateSut()
        {
            return new SierotkiLogic(filesLogic.Object, linesProcessor.Object);
        }

        [Test]
        public async Task ReplaceSpacesInFolderAsync_WhenFolderPathProvided_ThenItShouldProcessEveryFile()
        {
            var files = 3;

            filesLogic.Setup(f => f.ReadDocumentAsync(It.IsNotNull<string>())).Returns(Enumerable.Empty<string>().AsAsyncEnumerable());
            filesLogic.Setup(f => f.GetPathToAllFiles(It.IsNotNull<string>(), It.IsNotNull<string>())).Returns(Enumerable.Range(1, files).Select(i => "file"));
            linesProcessor.Setup(l => l.ReplaceSpacesInLinesAsync(It.IsNotNull<IAsyncEnumerable<string>>())).Returns(Enumerable.Empty<string>().AsAsyncEnumerable());

            var sut = CreateSut();
            sut.ReplaceSpacesInFolderAsync("folder");

            filesLogic.Verify(f => f.CopyFile(It.IsNotNull<string>(), It.IsNotNull<string>()), Times.Exactly(files));
            filesLogic.Verify(f => f.WriteDocumentAsync(It.IsNotNull<string>(), It.IsNotNull<IAsyncEnumerable<string>>()), Times.Exactly(files));
        }

        [Test]
        public async Task ReplaceSpacesInTexFileAsync_WhenFileProvided_ThenItShuldCopyFileAndWrite()
        {
            filesLogic.Setup(f => f.ReadDocumentAsync(It.IsNotNull<string>())).Returns(Enumerable.Empty<string>().AsAsyncEnumerable());
            linesProcessor.Setup(l => l.ReplaceSpacesInLinesAsync(It.IsNotNull<IAsyncEnumerable<string>>())).Returns(Enumerable.Empty<string>().AsAsyncEnumerable());

            var sut = CreateSut();
            await sut.ReplaceSpacesInTexFileAsync("path").ConfigureAwait(false);

            filesLogic.Verify(f => f.CopyFile(It.IsNotNull<string>(), It.IsNotNull<string>()), Times.Once);
            filesLogic.Verify(f => f.WriteDocumentAsync(It.IsNotNull<string>(), It.IsNotNull<IAsyncEnumerable<string>>()), Times.Once);
        }
    }
}
