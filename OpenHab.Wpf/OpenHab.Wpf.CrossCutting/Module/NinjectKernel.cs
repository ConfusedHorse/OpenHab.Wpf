using Ninject;

namespace OpenHab.Wpf.CrossCutting.Module
{
    public class NinjectKernel
    {
        private static IKernel _standardKernel;

        public static IKernel StandardKernel
        {
            get
            {
                if (_standardKernel != null) return _standardKernel;
                _standardKernel = new StandardKernel();
                _standardKernel.Load("OpenHab.Wpf.*.dll");
                return _standardKernel;
            }
        }
    }
}