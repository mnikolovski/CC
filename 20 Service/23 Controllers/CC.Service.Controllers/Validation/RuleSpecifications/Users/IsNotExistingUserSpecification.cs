using System;
using System.Linq.Expressions;
using CC.Service.Controllers.Validation.RuleSpecifications.Base;
using CC.Service.DomainEntities.Users;

namespace CC.Service.Controllers.Validation.RuleSpecifications.Users
{
    /// <summary>
    /// Check if a user is existing in database
    /// </summary>
    public class IsNotExistingUserSpecification : BaseSpecification<User>
    {
        public IsNotExistingUserSpecification(Func<bool> existingUserDelegate)
        {
            ExistingUserDelegate = existingUserDelegate;
        }

        private Func<bool> ExistingUserDelegate { get; set; }

        #region Overrides of BaseSpecification<User>

        /// <summary>
        /// Validation expression that must be fullfilled
        /// </summary>
        /// <returns></returns>
        public override Expression<Func<User, bool>> ValidationExpression
        {
            get
            {
                return x => ExistingUserDelegate() == false;
            }
        }

        #endregion
    }
}
