using CC.Common.Base.Results;
using CC.Service.DomainEntities.Users;
using CC.Service.DomainProcesses;
using CC.Service.DomainProcesses.Extensibility.Exports;
using CC.Service.DomainProcesses.Extensibility.Markers;

namespace CC.Service.Controllers.Notifications
{
    [NotificationExport(typeof(IUserProcesses))]
    internal sealed class UserNotificationController : IUserProcesses, INotificationController
    {
        /// <summary>
        /// Get all users
        /// </summary>
        public ListResult<User> GetAll()
        {
            return new ListResult<User>();
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="shouldNotify"></param>
        public MethodResult<int> Register(User user, bool shouldNotify = true)
        {
            // Send verification email to user via relay services
            return new MethodResult<int>();
        }

        /// <summary>
        /// Get content by id
        /// </summary>
        /// <param name="requestingUser"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public MethodResult<User> GetById(User requestingUser, int id)
        {
            return new MethodResult<User>();
        }

        /// <summary>
        /// Returns user by its username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public MethodResult<User> GetUserByUsername(string username)
        {
            return new MethodResult<User>();
        }

        /// <summary>
        /// Check if an user is exiting (check is done by both username and email)
        /// </summary>
        /// <param name="requestingUser"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public MethodResult<bool> IsExistingUser(User requestingUser, User user)
        {
            return new MethodResult<bool>();
        }

        /// <summary>
        /// Verify the specified credentials in the system
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="verifiedUser"></param>
        /// <returns></returns>
        public MethodResult<bool> VerifyUserCredentials(string username, string password, out User verifiedUser)
        {
            verifiedUser = null;
            return new MethodResult<bool>();
        }
    }
}
