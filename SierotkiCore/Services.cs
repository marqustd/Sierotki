using Autofac;
using Autofac.Builder;
using SierotkiCore.Logic;
using SierotkiCore.Logic.Files;
using SierotkiCore.Logic.Lines;
using SierotkiCore.Logic.Lines.TexRegex;

namespace SierotkiCore
{
    public static class Services
    {
        public static ContainerBuilder RegisterServices(this ContainerBuilder builder)
        {
            builder.RegisterType<FilesLogic, IFilesLogic>();

            builder.RegisterType<OrphansConcater, IOrphansConcater>();
            builder.RegisterType<SierotkiLogic, ISierotkiLogic>();
            builder.RegisterType<OrphansProcessor, IOrphansProcessor>();
            builder.RegisterType<SpecialTexLineChecker, ISpecialTexLineChecker>();
            builder.RegisterType<TexBeginRegexMaker, ITexBeginRegexMaker>();
            return builder;
        }

        private static IRegistrationBuilder<T, ConcreteReflectionActivatorData, SingleRegistrationStyle> RegisterType<T, Ti>(this ContainerBuilder builder) where T : Ti
        {
            return builder.RegisterType<T>().As<Ti>();
        }
    }
}
