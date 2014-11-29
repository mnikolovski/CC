using CC.Common.Base.Results;
using CC.Service.DomainEntities.Users;

namespace CC.Service.DomainProcesses
{
    /// <summary>
    /// Represents user processes
    /// </summary>
    public interface IUserProcesses
    {
        /// <summary>
        /// Get all users
        /// </summary>
        ListResult<User> GetAll();

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="shouldNotify"></param>
        MethodResult<int> Register(User user, bool shouldNotify = true);

        /// <summary>
        /// Get content by id
        /// </summary>
        /// <param name="requestingUser"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        MethodResult<User> GetById(User requestingUser, int id);

        /// <summary>
        /// Returns user by its username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        MethodResult<User> GetUserByUsername(string username);

        /// <summary>
        /// Check if an user is exiting (check is done by both username and email)
        /// </summary>
        /// <param name="requestingUser"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        MethodResult<bool> IsExistingUser(User requestingUser, User user);

        /// <summary>
        /// Verify the specified credentials in the system
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="verifiedUser"></param>
        /// <returns></returns>
        MethodResult<bool> VerifyUserCredentials(string username, string password, out User verifiedUser);
    }
}