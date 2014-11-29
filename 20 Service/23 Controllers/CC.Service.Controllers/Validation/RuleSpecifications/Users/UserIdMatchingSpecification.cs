using System;
using System.Linq.Expressions;
using CC.Service.Controllers.Validation.RuleSpecifications.Base;
using CC.Service.DomainEntities.Users;

namespace CC.Service.Controllers.Validation.RuleSpecifications.Users
{
    /// <summary>
    /// Validate user's id
    /// </summary>
    public class UserIdMatchingSpecification : BaseSpecification<User>
    {
        #region Implementation of IValidationRule<User>

        /// <summary>
        /// Validation expression that must be fullfilled
        /// </summary>
        /// <returns></returns>
        public override Expression<Func<User, bool>> ValidationExpression
        {
            get
            {
                return x => x.Id != 0;
            }
        }

        #endregion
    }
}
