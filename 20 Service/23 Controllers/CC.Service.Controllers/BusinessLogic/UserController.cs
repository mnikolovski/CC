using System;
using CC.Common.Base.Results;
using CC.Common.Helpers.Cryptography;
using CC.Common.Helpers.Instance;
using CC.Service.Controllers.Configuration;
using CC.Service.DataRepositoryContracts;
using CC.Service.DataRepositoryContracts.Extensibility.Imports;
using CC.Service.DomainEntities.Users;
using CC.Service.DomainProcesses;
using CC.Service.DomainProcesses.Extensibility.Exports;
using CC.Service.DomainProcesses.Extensibility.Imports;
using CC.Service.DomainProcesses.Extensibility.Markers;

namespace CC.Service.Controllers.BusinessLogic
{
    /// <summary>
    /// Business logic for user processes
    /// </summary>
    [BusinessLogicExport(typeof(IUserProcesses))]
    internal class UserBusinessController : IUserProcesses, IBusinessController
    {
        [NotificationImport]
        private IUserProcesses UserProcesses { get; set; }

        [DataAccessImport]
        private IUserRepository UserRepository { get; set; }

        /// <summary>
        /// Get all users
        /// </summary>
        public ListResult<User> GetAll()
        {
            var users = UserRepository.GetAll();
            return new ListResult<User>(users);
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="shouldNotify"></param>
        public MethodResult<int> Register(User user, bool shouldNotify = true)
        {
            // create the user
            user.PasswordSalt = HashHelper.GenerateSalt(MainConfig.MinSaltLenght);
            //user.UnmaskedPassword = string.Copy(user.Password);
            if (user.Password.IsNullOrEmpty()) user.Password = HashHelper.GenerateSalt(5);
            user.Password = (user.Password.Trim() + user.PasswordSalt).ToSha1();
            user.CreatedOn = DateTime.UtcNow;
            user.Email = user.Email.ToLower().Trim();
            user.Username = user.Username.ToLower();
            UserRepository.Insert(user);

            if (shouldNotify)
            {
                // notify the daemons
                UserProcesses.Register(user);
            }

            return new MethodResult<int>(user.Id);
        }

        /// <summary>
        /// Get content by id
        /// </summary>
        /// <param name="requestingUser"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public MethodResult<User> GetById(User requestingUser, int id)
        {
            var user = UserRepository.GetById(id);
            return new MethodResult<User>(user);
        }

        /// <summary>
        /// Returns user by its username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public MethodResult<User> GetUserByUsername(string username)
        {
            var user = UserRepository.GetUserByUsername(username);
            return new MethodResult<User>(user);
        }

        /// <summary>
        /// Check if an user is exiting (check is done by both username and email)
        /// </summary>
        /// <param name="requestingUser"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public MethodResult<bool> IsExistingUser(User requestingUser, User user)
        {
            var isExistingUser = UserRepository.IsExistingUser(user.Username);
            return new MethodResult<bool>(isExistingUser);
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
            username = username.ToLower();
            var user = UserRepository.GetUserByUsername(username);

            // verify user for login
            var isVerified = !user.IsNull() && user.Password.VerifySha1(password + user.PasswordSalt);

            // if user is existing remove pwd and pwd salt
            if (!user.IsNull())
            {
                verifiedUser = new User();
                verifiedUser.Id = user.Id;
                verifiedUser.Username = user.Username;
                verifiedUser.Email = user.Email;
                verifiedUser.Firstname = user.Firstname;
                verifiedUser.Lastname = user.Lastname;
            }
            return new MethodResult<bool>(isVerified);
        }
    }
}
