using System.Collections.Generic;
using CC.Service.DomainEntities.Users;

namespace CC.Service.DataRepositoryContracts
{
    public interface IUserRepository
    {
        /// <summary>
        /// Insert new user in db
        /// </summary>
        /// <param name="user"></param>
        void Insert(User user);

        /// <summary>
        /// return all users
        /// </summary>
        /// <returns></returns>
        List<User> GetAll();

        /// <summary>
        /// Check if an user is exiting username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        bool IsExistingUser(string username);

        /// <summary>
        /// Returns user by its username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        User GetUserByUsername(string username);

        /// <summary>
        /// Return an entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        User GetById(int id);
    }
}
