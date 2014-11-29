using System;
using System.Collections.Generic;
using System.Linq;
using CC.Service.DataRepositoryContracts;
using CC.Service.DataRepositoryContracts.Extensibility.Exports;
using CC.Service.DomainEntities.Users;

namespace CC.Service.DataRepository.Users
{
    [DataAccessExport(typeof(IUserRepository))]
    internal class UserRepository : IUserRepository
    {
        private static readonly object Lock;

        private static readonly List<User> UsersContext;

        static UserRepository()
        {
            Lock = new object();
            UsersContext = GenerateUsers();
        }

        /// <summary>
        /// Return an entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetById(int id)
        {
            return UsersContext.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Returns user by its username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User GetUserByUsername(string username)
        {
            return UsersContext.FirstOrDefault(x => string.Compare(x.Username, username, StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        /// <summary>
        /// Check if an user is exiting username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool IsExistingUser(string username)
        {
            return UsersContext.Any(x => string.Compare(x.Username, username, StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        /// <summary>
        /// return all users
        /// </summary>
        /// <returns></returns>
        public List<User> GetAll()
        {
            //throw new SqlExceptionEx("DB connection problem");
            return UsersContext;
        }

        /// <summary>
        /// Insert new user in db
        /// </summary>
        /// <param name="user"></param>
        public void Insert(User user)
        {
            lock (Lock)
            {
                user.Id = UsersContext.Count;
                UsersContext.Add(user);
            }
        }

        /// <summary>
        /// Generate some random users
        /// </summary>
        /// <returns></returns>
        private static List<User> GenerateUsers()
        {
            var users = new List<User>();
            for (int i = 1; i < 100; i++)
            {
                users.Add(new User
                {
                    Id = i,
                    Username = string.Format(@"User{0}", i),
                    Email = string.Format(@"User{0}@gmail.com", i),
                    Password = @"6aec17f2ad513de99b9147f8fe19c73042ca4ff7",
                    PasswordSalt = @"PASSWORD_SALT",
                    CreatedOn = DateTime.Now,
                });
            }
            return users;
        }
    }
}
