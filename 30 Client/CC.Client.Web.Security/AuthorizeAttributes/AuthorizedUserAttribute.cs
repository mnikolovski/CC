using System;
using System.Web;
using System.Web.Mvc;
using CC.Client.Web.Security.Provider;

namespace CC.Client.Web.Security.AuthorizeAttributes
{
    /// <summary>
    /// Controller action calls for registered user
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AuthorizedUserAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (MvcFormsSecurityProvider.CurrentUser == null) return false;
            return MvcFormsSecurityProvider.CurrentUser.IsAuthenticated &&
                  !MvcFormsSecurityProvider.CurrentUser.IsAnonymous;
        }
    }
}
