using Ninject.Modules;
using OpenHab.Wpf.CrossCutting.Context;

namespace OpenHab.Wpf.CrossCutting.Module
{
    public class ContextModule : NinjectModule
    {
        public override void Load()
        {
            Bind<GlobalContext>().ToSelf().InSingletonScope();
            Bind<RestContext>().ToSelf().InSingletonScope();
        }
    }
}