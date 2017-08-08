using Ninject.Modules;

namespace OpenHab.Wpf.Business.Module
{

    public class BusinessModule : NinjectModule
    {
        public override void Load()
        {
            //Bind<>().ToSelf().InSingletonScope();
        }
    }
}