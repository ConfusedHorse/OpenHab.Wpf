using Ninject.Modules;

namespace OpenHab.Wpf.ViewModel.Module
{
    public class ViewModule : NinjectModule
    {
        public override void Load()
        {
            //Bind<TODOViewModel>().ToSelf().InSingletonScope();
        }
    }
}