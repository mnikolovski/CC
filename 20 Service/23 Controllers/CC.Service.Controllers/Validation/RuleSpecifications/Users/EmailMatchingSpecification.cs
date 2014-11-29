using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using CC.Common.Helpers.Regexes;
using CC.Service.Controllers.Validation.RuleSpecifications.Base;
using CC.Service.DomainEntities.Users;

namespace CC.Service.Controllers.Validation.RuleSpecifications.Users
{
    /// <summary>
    /// Validate user's email
    /// </summary>
    public class EmailMatchingSpecification : BaseSpecification<User>
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
                return x => x.Email.IsMatch(RegexHelper.IsValidEmail, RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.CultureInvariant);
            }
        }

        #endregion
    }
}
