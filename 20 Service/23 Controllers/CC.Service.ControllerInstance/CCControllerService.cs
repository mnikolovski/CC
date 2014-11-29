using CC.Service.DomainProcesses;
using CC.Service.DomainProcesses.Extensibility.Markers;
using Emit.ExtensibilityProvider.Concrete;

namespace CC.Service.ControllerInstance
{
    public class CcControllerService
    {
        public static readonly CcControllerService Instance = new CcControllerService();
        private static SystemBootstrapper _bootstrapper;

        /// <summary>
        /// User processes
        /// </summary>
        public IUserProcesses User
        {
            get
            {
                return _bootstrapper.GetInstance<IUserProcesses>(new[] { typeof(IExceptionHandlingController) });
            }
        }

        /// <summary>
        /// CTOR
        /// </summary>
        private CcControllerService()
        {
            _bootstrapper = new SystemBootstrapper();
            _bootstrapper.Execute(this);
        }

        /// <summary>
        /// Calls static ctor
        /// </summary>
        public static void Init()
        {
            
        }
    }
}
