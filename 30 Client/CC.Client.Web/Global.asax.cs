using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using CC.Client.Web.App_Start;
using CC.Client.Web.Security.Provider;

namespace CC.Client.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // init the web service container (web app as service bootstrapped)
            CcWebService.Init();
            // register system routes
            CcWebService.RegisterRoutes(RouteTable.Routes);
            // register global action filters
            CcWebService.RegisterGlobalFilters(GlobalFilters.Filters);
            // register all automap class mappings
            CcWebService.RegisterAutoMapperMaps();
            // register all custom model binders
            CcWebService.RegisterModelBinders();
            // strip mvc headers from response
            CcWebService.StripHeaders();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            if (exception != null)
            {
                CcWebService.Instance.Logger.Log(exception, MvcFormsSecurityProvider.CurrentUser);
            }
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            // check for ticket renewals and set the current principal
            MvcFormsSecurityProvider.SetCurrentPrincipalAndPrincipalTicket();
        }

        protected void Application_EndRequest()
        {
            var context = new HttpContextWrapper(this.Context);

            // If we're an ajax request and forms authentication caused a 302, 
            // then we actually need to do a 401
            if (FormsAuthentication.IsEnabled && context.Response.StatusCode == (int)HttpStatusCode.Found
                && context.Request.IsAjaxRequest())
            {
                context.Response.Clear();
                context.Response.StatusCode = 401;
            }
        }
    }
}