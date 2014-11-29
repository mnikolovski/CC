using System.Web.Mvc;
using CC.Client.Web.Security.Entities;
using CC.Client.Web.Security.Provider;
using CC.Common.Helpers.Instance;
using CC.Service.ControllerInstance;

namespace CC.Client.Web.Controllers
{
    public class _BaseController : Controller
    {
        /// <summary>
        /// Represents the domain services container
        /// </summary>
        protected CcControllerService DomainServices
        {
            get { return CcControllerService.Instance; }
        }

        /// <summary>
        /// Return the logged in user in the system
        /// </summary>
        public SecurityUser CurrentUser
        {
            get { return MvcFormsSecurityProvider.CurrentUser; }
        }

        /// <summary>
        /// Return the logged in user in the system
        /// </summary>
        public SecurityUser CurrentUserAsClone
        {
            get
            {
                if (CurrentUser.IsNull()) return null;
                var user = new SecurityUser(CurrentUser);
                return user;
            }
        }
    }
}
