using System;
using System.Runtime.Serialization;
using CC.Service.DomainEntities.Base;

namespace CC.Service.DomainEntities.Users
{
    /// <summary>
    /// Represents a system user
    /// </summary>
    [Serializable, DataContract]
    public class User : BaseDomainEntity
    {
        /// <summary>
        /// User's firstname
        /// </summary>
        [DataMember]
        public string Firstname { get; set; }

        /// <summary>
        /// User's lastname
        /// </summary>
        [DataMember]
        public string Lastname { get; set; }

        /// <summary>
        /// User's unique username
        /// </summary>
        [DataMember]
        public string Username { get; set; }

        /// <summary>
        /// User's hashed pass
        /// </summary>
        [DataMember]
        public string Password { get; set; }

        /// <summary>
        /// Salt used for the encrypting the password
        /// </summary>
        [DataMember]
        public string PasswordSalt { get; set; }

        /// <summary>
        /// Email of the user. Used for auth. the user in the system.
        /// </summary>
        [DataMember]
        public string Email { get; set; }
    }
}
