using System;
using System.Web.Mvc;
using System.Web.Routing;
using CC.Common.Base.Logging;
using CC.Common.Logging.Extensibility;
using CC.Service.ControllerInstance;
using Emit.ExtensibilityProvider.Concrete;

namespace CC.Client.Web.App_Start
{
    public class CcWebService
    {
        public static readonly CcWebService Instance;
        private static SystemBootstrapper _bootstrapper;

        [LoggerImport]
        public ILogger Logger { get; set; }

        static CcWebService()
        {
            try
            {
                Instance = new CcWebService();
            }
            catch (Exception)
            {
                
               
            }
        }

        private CcWebService()
        {
            _bootstrapper = new SystemBootstrapper();
            _bootstrapper.Execute(this);
        }

        /// <summary>
        /// Start initializing the container (by invoking a method static constructor will be called)
        /// </summary>
        public static void Init()
        {
            // init the service container
            CcControllerService.Init();
            // register all areas
            AreaRegistration.RegisterAllAreas();
        }

        /// <summary>
        /// Register system routes
        /// </summary>
        /// <param name="routes"></param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        /// <summary>
        /// Register global action filters
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }

        /// <summary>
        /// Register automatic mappings
        /// </summary>
        public static void RegisterAutoMapperMaps()
        {
            
        }

        /// <summary>
        /// Register custom model binders
        /// </summary>
        public static void RegisterModelBinders()
        {
         
        }

        /// <summary>
        /// Strip MVC response header
        /// </summary>
        public static void StripHeaders()
        {
            MvcHandler.DisableMvcResponseHeader = true;
        }
    }
}
