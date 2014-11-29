using System.Security.Principal;
using CC.Service.DomainEntities.Users;

namespace CC.Client.Web.Security.Entities
{
    public class SecurityUser : IIdentity, IPrincipal
    {
        public SecurityUser(User user)
        {
            CurrentUser = user;
        }

        #region Implementation of IIdentity

        /// <summary>
        /// Return the id of the current user
        /// </summary>
        public int Id
        {
            get
            {
                return CurrentUser != null ? CurrentUser.Id : 0;
            }
            set
            {
                if (CurrentUser != null)
                {
                    CurrentUser.Id = value;
                }
            }
        }

        /// <summary>
        /// User's username
        /// </summary>
        public string Username
        {
            get
            {
                return CurrentUser != null ? CurrentUser.Username : null;
            }
            set
            {
                if (CurrentUser != null)
                {
                    CurrentUser.Username = value;
                }
            }
        }

        /// <summary>
        /// User's email
        /// </summary>
        public string Email
        {
            get
            {
                return CurrentUser != null ? CurrentUser.Email : null;
            }
            set
            {
                if (CurrentUser != null)
                {
                    CurrentUser.Email = value;
                }
            }
        }

        public bool IsAnonymous
        {
            get
            {
                if (CurrentUser == null || 
                    (CurrentUser.Id == AnonymousSecurityUser.User.Id &&
                     CurrentUser.Email == AnonymousSecurityUser.User.Email) ||
                    !IsAuthenticated)
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// User's name - will be always empty
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the type of authentication used.
        /// </summary>
        /// <returns>
        /// The type of authentication used to identify the user.
        /// </returns>
        public string AuthenticationType
        {
            get { return "Forms"; }
        }

        /// <summary>
        /// Gets a value that indicates whether the user has been authenticated.
        /// </summary>
        /// <returns>
        /// true if the user was authenticated; otherwise, false.
        /// </returns>
        public bool IsAuthenticated
        {
            get
            {
                if (CurrentUser == null) return false;
                if (CurrentUser.Id == AnonymousSecurityUser.User.Id) return false;
                return CurrentUser.GetType() != typeof(AnonymousSecurityUser);
            }
        }

        #endregion

        #region IPrincipal Members

        public IIdentity Identity
        {
            get { return this; }
        }

        public bool IsInRole(string role)
        {
            return false;
        }

        #endregion

        public User CurrentUser { get; private set; }

        // Must be defined inside a class called Farenheit:
        public static implicit operator User(SecurityUser securityUser)
        {
            if (securityUser == null)
            {
                securityUser = AnonymousSecurityUser.User;
            }

            var _user = new User
            {
                Id = securityUser.Id,
                Username = securityUser.Username,
                Email = securityUser.Email
            };
            return _user;
        }
    }
}
