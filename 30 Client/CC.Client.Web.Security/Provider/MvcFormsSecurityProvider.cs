using System;
using System.Web;
using System.Web.Security;
using CC.Client.Web.Security.Entities;
using CC.Common.Helpers.Instance;
using CC.Common.Helpers.Serialization;
using CC.Service.ControllerInstance;
using CC.Service.DomainEntities.Users;

namespace CC.Client.Web.Security.Provider
{
    public class MvcFormsSecurityProvider
    {
        private static readonly SecurityUser AnonymousUser;

        static MvcFormsSecurityProvider()
        {
            AnonymousUser = AnonymousSecurityUser.User;
        }

        public static SecurityUser CurrentUser
        {
            get
            {
                var _securedUser = HttpContext.Current.User as SecurityUser;
                if(_securedUser.IsNull())
                {
                    CurrentUser = AnonymousUser;
                    return AnonymousUser;
                }
                
                return _securedUser;
            }
            private set
            {
                HttpContext.Current.User = value;
                // Make sure the Principal's are in sync
                System.Threading.Thread.CurrentPrincipal = value;
            }
        }

        public static void SetAnonymousUser()
        {
            CurrentUser = AnonymousUser;
        }

        /// <summary>
        /// Login a user in the web application via auth cookie
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="rememberMe"></param>
        /// <param name="shouldRedirectToUrl"></param>
        /// <returns></returns>
        public static bool LoginUser(string username, string password, bool rememberMe, bool shouldRedirectToUrl = false)
        {
            User verifiedUser = null;

            var isValid = false;

            try
            {
                isValid = CcControllerService.Instance.User.VerifyUserCredentials(username, password, out verifiedUser).Result;
            }
            catch (Exception) {}         

            if (!isValid) return false;

            CreateFormsCookie(verifiedUser, rememberMe, shouldRedirectToUrl);

            return true;
        }

        /// <summary>
        /// Impersonate user by its username
        /// </summary>
        /// <param name="username"></param>
        public static bool ImpersonateUser(string username)
        {
            User verifiedUser = null;
            try
            {
                var userResult = CcControllerService.Instance.User.GetUserByUsername(username);
                if (userResult.IsFaulted || userResult.Result.IsNull()) return false;
                verifiedUser = userResult.Result;

            }
            catch (Exception)
            {
                return false;
            }

            CreateFormsCookie(verifiedUser, true, false);
            return true;
        }

        private static void CreateFormsCookie(User verifiedUser, bool rememberMe, bool shouldRedirectToUrl)
        {
            // set the curent principal active
            CurrentUser = new SecurityUser(verifiedUser);

            var expirationDate = DateTime.Now.AddMinutes(120);
            if (rememberMe)
            {
                expirationDate = DateTime.Now.AddMonths(1);
            }

            // build cookie data
            var _userData = verifiedUser.SerializeToJSon();
            // create auth ticket and store it in a cookie
            FormsAuthentication.Initialize();
            var ticket = new FormsAuthenticationTicket(1,
                                                        verifiedUser.Email,
                                                        DateTime.Now,
                                                        expirationDate,
                                                        true,
                                                       _userData,
                                                        FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            cookie.Expires = ticket.Expiration;
            cookie.Path = FormsAuthentication.FormsCookiePath;

            HttpContext.Current.Response.Cookies.Add(cookie);

            // if redirect is enabled redirect to destination url
            if (shouldRedirectToUrl)
            {
                //And send the user where they were heading
                var _redirectUrl = FormsAuthentication.GetRedirectUrl(verifiedUser.Email, false);
                HttpContext.Current.Response.Redirect(_redirectUrl, true);
            }            
        }

        /// <summary>
        /// Update current user data in cookie
        /// </summary>
        public static void RefreshCurrentUserData()
        {
            // if not logged then we have nothing to renew
            if (!HttpContext.Current.Request.IsAuthenticated) return;

            // get the cookie and check if we have a cookie
            var cookie = FormsAuthentication.GetAuthCookie(CurrentUser.Email, true);
            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            if(ticket.IsNull()) return;

            // build cookie data
            var userData = CurrentUser.CurrentUser.SerializeToJSon();
            // create auth ticket and store it in a cookie
            var newTicket = new FormsAuthenticationTicket(ticket.Version,
                                                        CurrentUser.Email,
                                                        ticket.IssueDate,
                                                        ticket.Expiration,
                                                        true,
                                                        userData,
                                                        FormsAuthentication.FormsCookiePath);

            cookie.Value = FormsAuthentication.Encrypt(newTicket);
            cookie.Expires = newTicket.Expiration.AddDays(1);

            HttpContext.Current.Response.Cookies.Set(cookie);
        }

        /// <summary>
        /// Set the curent princcipal with security user and renew forms ticket if expired
        /// </summary>
        public static void SetCurrentPrincipalAndPrincipalTicket()
        {
            // if not logged then we have nothing to renew
            if (!HttpContext.Current.Request.IsAuthenticated) return;
            // get the cookie and check if we have a cookie
            var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (cookie == null) return;

            // get the tiket from the cookie
            FormsAuthenticationTicket oldTicket = FormsAuthentication.Decrypt(cookie.Value);

            // if ticket can not be resolved
            if (oldTicket == null) return;

            var _verifiedUser =  SerializationHelper.DeserializeFromJSon<User>(oldTicket.UserData);
            if (!_verifiedUser.IsNull() && !oldTicket.Expired)
            {
                var newTicket = FormsAuthentication.RenewTicketIfOld(oldTicket);
                if (!newTicket.IsNull())
                {
                    cookie.Expires = newTicket.Expiration;
                }
            }

            CurrentUser = new SecurityUser(_verifiedUser);
        }

        /// <summary>
        /// Logout a user from an application
        /// </summary>
        public static void Logout(bool shouldRedirect = true)
        {
            FormsAuthentication.SignOut();
            if(shouldRedirect) FormsAuthentication.RedirectToLoginPage();
        }

        /// <summary>
        /// Return provider login url
        /// </summary>
        /// <returns></returns>
        public static string GetLoginUrl()
        {
            return FormsAuthentication.LoginUrl;
        }
    }
}
