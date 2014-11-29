using System;
using System.Runtime.Serialization;

namespace CC.Service.DomainEntities.Base
{
    /// <summary>
    /// Represents base domain entity
    /// </summary>
    [Serializable]
    [DataContract]
    public class BaseDomainEntity : IDbEntity
    {
        /// <summary>
        /// Entity Id
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Indicates when the records have been created in the system
        /// </summary>
        [DataMember]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// CTOR
        /// </summary>
        public BaseDomainEntity()
        {
            CreatedOn = DateTime.UtcNow;
        }
    }
}
