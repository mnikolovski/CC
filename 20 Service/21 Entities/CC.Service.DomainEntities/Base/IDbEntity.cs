namespace CC.Service.DomainEntities.Base
{
    /// <summary>
    /// Represent a db entity
    /// </summary>
    public interface IDbEntity
    {
        /// <summary>
        /// Entity id
        /// </summary>
        int Id { get; set; }
    }
}
