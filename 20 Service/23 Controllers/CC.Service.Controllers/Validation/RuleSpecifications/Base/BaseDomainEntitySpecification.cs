using System;
using System.Linq.Expressions;
using CC.Service.DomainEntities.Base;

namespace CC.Service.Controllers.Validation.RuleSpecifications.Base
{
    /// <summary>
    /// Validate base entity agains null check and emtpy ObjectId as id check
    /// </summary>
    public class BaseDomainEntitySpecification<T> : BaseSpecification<T> where T : BaseDomainEntity
    {
        #region Implementation of IValidationRule<User>

        /// <summary>
        /// Validation expression that must be fullfilled
        /// </summary>
        /// <returns></returns>
        public override Expression<Func<T, bool>> ValidationExpression
        {
            get { return x => x != null && x.Id != 0; }
        }

        #endregion
    }
}
