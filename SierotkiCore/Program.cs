﻿using Autofac;
using NDesk.Options;
using SierotkiCore.Logic;
using SierotkiCore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SierotkiCore
{
    internal class Program
    {
        private static readonly OptionSet argsParser = new OptionSet();

        private static Settings ParseArgs(IEnumerable<string> args)
        {
            var settings = new Settings();
            var showHelp = false;

            argsParser.Add("l|length=", "the maximum length of orphan (defaul: 1)", (int l) => settings.Length = l);
            argsParser.Add("i|input=", "the path to .tex file", i => settings.FilePath = i);
            argsParser.Add("h|help", "show this message and exit", v => showHelp = v != null);

            settings.Orphans = argsParser.Parse(args);

            if (settings.Length < 1)
            {
                throw new OptionException("Length must be positive integer!", "LENGTH");
            }
            if (string.IsNullOrWhiteSpace(settings.FilePath))
            {
                throw new OptionException("A filepath to .tex file must be provided", "INPUT");
            }
            if (showHelp)
            {
                return null;
            }

            return settings;
        }

        public static async Task Main(string[] args)
        {
            try
            {
                var settings = ParseArgs(args);
                if (settings is null)
                {
                    ShowHelp();
                }

                var container = new ContainerBuilder().RegisterServices();
                container.RegisterInstance(settings);

                using (var services = container.Build().BeginLifetimeScope())
                {
                    var logic = services.Resolve<ISierotkiLogic>();
                    await logic.ConcatOrphansInTexFileAsync(settings.FilePath);

                }
            }
            catch (OptionException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Try `greet --help' for more information.");//todo
            }
        }

        private static void ShowHelp()
        {
            Console.WriteLine("Usage: SierotkiCore [OPTIONS]+ orphans");
            //todo opis
            Console.WriteLine();
            Console.WriteLine("Options:");
            argsParser.WriteOptionDescriptions(Console.Out);
        }
    }
}
