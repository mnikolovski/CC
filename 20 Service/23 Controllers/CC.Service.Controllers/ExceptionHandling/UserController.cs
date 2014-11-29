using CC.Common.Base.Logging;
using CC.Common.Base.Results;
using CC.Common.Logging.Extensibility;
using CC.Service.Controllers.Extensions;
using CC.Service.DomainEntities.Users;
using CC.Service.DomainProcesses;
using CC.Service.DomainProcesses.Extensibility.Exports;
using CC.Service.DomainProcesses.Extensibility.Imports;
using CC.Service.DomainProcesses.Extensibility.Markers;
using Emit.LocalizationProvider.Definition;
using Emit.LocalizationProvider.Extensibility;

namespace CC.Service.Controllers.ExceptionHandling
{
    /// <summary>
    /// General exception handling for user processes
    /// </summary>
    [ExceptionHandlingExport(typeof(IUserProcesses))]
    internal class UserExceptionHandlingController : IUserProcesses, IExceptionHandlingController
    {
        [LoggerImport]
        public ILogger Logger { get; set; }

        [LocalizationProviderImport]
        public ILocalizationProvider LocalizationProvider { get; set; }

        [ValidationImport]
        private IUserProcesses ValidationProcesses { get; set; }

        /// <summary>
        /// Get all users
        /// </summary>
        public ListResult<User> GetAll()
        {
            return this.WrapRequestResponseCall(() => ValidationProcesses.GetAll());
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="shouldNotify"></param>
        public MethodResult<int> Register(User user, bool shouldNotify = true)
        {
            return this.WrapRequestResponseCall(() => ValidationProcesses.Register(user, shouldNotify));
        }

        /// <summary>
        /// Get content by id
        /// </summary>
        /// <param name="requestingUser"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public MethodResult<User> GetById(User requestingUser, int id)
        {
            return this.WrapRequestResponseCall(() => ValidationProcesses.GetById(requestingUser, id));
        }

        /// <summary>
        /// Returns user by its username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public MethodResult<User> GetUserByUsername(string username)
        {
            return this.WrapRequestResponseCall(() => ValidationProcesses.GetUserByUsername(username));
        }

        /// <summary>
        /// Check if an user is exiting (check is done by both username and email)
        /// </summary>
        /// <param name="requestingUser"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public MethodResult<bool> IsExistingUser(User requestingUser, User user)
        {
            return this.WrapRequestResponseCall(() => ValidationProcesses.IsExistingUser(requestingUser, user));
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
            User _verifiedUser = null;
            var result = this.WrapRequestResponseCall(() => ValidationProcesses.VerifyUserCredentials(username, password, out _verifiedUser));
            verifiedUser = _verifiedUser;
            return result;
        }
    }
}
