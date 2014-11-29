using CC.Common.Base.Results;
using CC.Service.Controllers.Extensions;
using CC.Service.Controllers.Validation.Containers.Users;
using CC.Service.Controllers.Validation.RuleSpecifications.Generic;
using CC.Service.Controllers.Validation.RuleSpecifications.Users;
using CC.Service.DataRepositoryContracts;
using CC.Service.DataRepositoryContracts.Extensibility.Imports;
using CC.Service.DomainEntities.Users;
using CC.Service.DomainProcesses;
using CC.Service.DomainProcesses.Extensibility.Exports;
using CC.Service.DomainProcesses.Extensibility.Imports;
using CC.Service.DomainProcesses.Extensibility.Markers;
using Emit.RuleEngine.Engine;

namespace CC.Service.Controllers.Validation
{
    /// <summary>
    /// Validation logic for user processes
    /// </summary>
    [ValidationExport(typeof(IUserProcesses))]
    internal class UserValidationController : IUserProcesses, IValidationController
    {
        [DataAccessImport]
        private IUserRepository UserRepository { get; set; }

        [BusinessLogicImport]
        private IUserProcesses BussinesProcesses { get; set; }

        /// <summary>
        /// Get all users
        /// </summary>
        public ListResult<User> GetAll()
        {
            return BussinesProcesses.GetAll();
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="shouldNotify"></param>
        public MethodResult<int> Register(User user, bool shouldNotify = true)
        {
            var engine = RuleEvaluator<User>.New();

            engine.Register(new NewUserValidationContainer(() => UserRepository.IsExistingUser(user.Username)))
                   .Execute(user);

            if (!engine.ExecutionResult.Result)
            {
                var _result = engine.ExecutionResult.CreateFaultedResponse<MethodResult<int>>(user);
                return _result;
            }

            return BussinesProcesses.Register(user, shouldNotify);
        }

        /// <summary>
        /// Get content by id
        /// </summary>
        /// <param name="requestingUser"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public MethodResult<User> GetById(User requestingUser, int id)
        {
            var engine = RuleEvaluator<User>.New();

            engine.Register(new UserIdMatchingSpecification())
                  .Execute(new User { Id = id });

            if (!engine.ExecutionResult.Result)
            {
                var _result = engine.ExecutionResult.CreateFaultedResponse<MethodResult<User>>();
                return _result;
            }

            return BussinesProcesses.GetById(requestingUser, id);
        }

        /// <summary>
        /// Returns user by its username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public MethodResult<User> GetUserByUsername(string username)
        {
            var engine = RuleEvaluator<User>.New();

            engine.Register(new UsernameMatchingSpecification())
                   .Execute(new User { Username = username });

            if (!engine.ExecutionResult.Result)
            {
                var _result = engine.ExecutionResult.CreateFaultedResponse<MethodResult<User>>(username);
                return _result;
            }

            return BussinesProcesses.GetUserByUsername(username);
        }

        /// <summary>
        /// Check if an user is exiting (check is done by both username and email)
        /// </summary>
        /// <param name="requestingUser"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public MethodResult<bool> IsExistingUser(User requestingUser, User user)
        {
            var engine = RuleEvaluator<User>.New();
            engine.Register(new ExistingUserValidationContainer())
                  .Execute(user);

            if (!engine.ExecutionResult.Result)
            {
                var _result = engine.ExecutionResult.CreateFaultedResponse<MethodResult<bool>>();
                return _result;
            }

            return BussinesProcesses.IsExistingUser(requestingUser, user);
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

            var engine = RuleEvaluator<string>.New();

            engine.ReSetup()
                   .Register(new NotNullOrEmptyString())
                   .Execute(username);

            if (!engine.ExecutionResult.Result)
            {
                var _result = engine.ExecutionResult.CreateFaultedResponse<MethodResult<bool>>(username);
                return _result;
            }

            engine.ReSetup()
                   .Register(new NotNullOrEmptyString())
                   .Execute(password);

            if (!engine.ExecutionResult.Result)
            {
                var _result = engine.ExecutionResult.CreateFaultedResponse<MethodResult<bool>>();
                return _result;
            }

            User _verifiedUser;
            var _isVerifiedUser = BussinesProcesses.VerifyUserCredentials(username, password, out _verifiedUser);
            verifiedUser = _verifiedUser;

            return BussinesProcesses.VerifyUserCredentials(username, password, out verifiedUser);
        }
    }
}
