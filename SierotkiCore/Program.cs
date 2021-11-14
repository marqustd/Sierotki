using Autofac;
using SierotkiCore.Logic;
using SierotkiCore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SierotkiCore
{
    internal class Program
    {
        /// <summary>
        /// The program replaces spaces with non-breaking space characters `~` in .tex files before one-character words. One-character word at the end of a line is considered a typographical mistake in Poland. This mistake is called “sierotka” (pl. orphan).
        /// </summary>
        /// <param name="filePath">Path to a single .tex file.</param>
        /// <param name="folderPath">Path to a folder with several .tex files. Folders can be nested.</param>
        /// <param name="length">Maximum length of a word which should be fixed.</param>
        /// <param name="words">List of words which should be considered a short word.</param>
        public static async Task Main(string filePath = null, string folderPath = null, int length = 1, IEnumerable<string> words = null)
        {
            try
            {
                var settings = new Settings
                {
                    FilePath = filePath,
                    FolderPath = folderPath,
                    Length = length,
                };

                if (words != null)
                {
                    settings.Words = words;
                }

                var container = new ContainerBuilder().RegisterServices();
                container.RegisterInstance(settings);

                using (var services = container.Build().BeginLifetimeScope())
                {
                    var logic = services.Resolve<ISierotkiLogic>();

                    if (!string.IsNullOrWhiteSpace(settings.FilePath))
                    {
                        await logic.ReplaceSpacesInTexFileAsync(settings.FilePath);
                    }
                    else if (!string.IsNullOrWhiteSpace(settings.FolderPath))
                    {
                        await logic.ReplaceSpacesInFolderAsync(settings.FolderPath);
                    }
                    else
                    {
                        throw new ArgumentException("No input provided.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Try `--help' for more information.");
            }
        }
    }
}
