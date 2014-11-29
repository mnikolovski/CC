using System;
using System.Web;
using System.Web.Mvc;
using CC.Client.Web.Security.Provider;

namespace CC.Client.Web.Security.AuthorizeAttributes
{
    /// <summary>
    /// Controller action calls for registered and verified user
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class AuthorizedAndVerifiedUserAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (MvcFormsSecurityProvider.CurrentUser == null) return false;
            return MvcFormsSecurityProvider.CurrentUser.IsAuthenticated;
        }
    }
}
