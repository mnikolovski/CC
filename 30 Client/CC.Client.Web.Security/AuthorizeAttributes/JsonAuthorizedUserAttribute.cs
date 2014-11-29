using System.Net;
using System.Web.Mvc;
using CC.Client.Web.Security.Provider;

namespace CC.Client.Web.Security.AuthorizeAttributes
{
    public class JsonAuthorizedUserAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Check the token for the post action
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var _isValidRequest = IsValidHoneyPotRequest(filterContext);
            if (!_isValidRequest)
            {
               EndRequest(filterContext);
            }
        }

        /// <summary>
        /// Validate a token based request
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        public bool IsValidHoneyPotRequest(ActionExecutingContext filterContext)
        {
            if (MvcFormsSecurityProvider.CurrentUser == null) return false;
            return MvcFormsSecurityProvider.CurrentUser.IsAuthenticated;
        }

        /// <summary>
        /// Ends the request
        /// </summary>
        /// <param name="filterContext"></param>
        private static void EndRequest(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            filterContext.Result = new HttpStatusCodeResult((int)HttpStatusCode.Forbidden);
        }
    }
}
