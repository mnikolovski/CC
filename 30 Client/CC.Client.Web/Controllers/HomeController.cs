using System.Web.Mvc;
using CC.Client.Web.Inrastructure.Response;
using CC.Client.Web.Security.AuthorizeAttributes;
using CC.Client.Web.Security.Provider;
using CC.Service.DomainEntities.Users;

namespace CC.Client.Web.Controllers
{
    public class HomeController : _BaseController
    {
        /// <summary>
        /// Landing page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (MvcFormsSecurityProvider.CurrentUser.IsAuthenticated) return RedirectToAction(@"PrivateData");
            return View();
        }

        /// <summary>
        /// List private user data
        /// </summary>
        /// <returns></returns>
        [AuthorizedUser]
        public ActionResult PrivateData()
        {
            var usersResult = DomainServices.User.GetAll();
            if (usersResult.IsFaulted) return this.CreateFaultedResult(usersResult);
            return View(usersResult.Result);
        }

        /// <summary>
        /// Login the user and redirect to private data
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public ActionResult Login(string username, string password)
        {
            var isLoggedIn = MvcFormsSecurityProvider.LoginUser(username, password, true);
            if (isLoggedIn)
            {
                return RedirectToAction(@"PrivateData");
            }
            return RedirectToAction(@"Index");
        }

        /// <summary>
        /// Logut the user from the system
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public ActionResult Logout(string username, string password)
        {
            MvcFormsSecurityProvider.Logout();
            return RedirectToAction(@"Index");
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public ActionResult Register(string username, string password)
        {
            var user = new User();
            user.Username = username;
            user.Email = string.Format(@"{0}@gmail.com", username);
            user.Password = password;

            var userRegistrationResult = DomainServices.User.Register(user);
            if (userRegistrationResult.IsFaulted) return this.CreateFaultedResult(userRegistrationResult);
            MvcFormsSecurityProvider.LoginUser(username, password, true);
            return RedirectToAction(@"PrivateData");
        }

        /// <summary>
        /// Create nullpointer exception
        /// </summary>
        /// <returns></returns>
        public ActionResult Exception()
        {
            User user = null;
            user.Username = @"marjan";
            return RedirectToAction(@"PrivateData"); 
        }
    }
}
