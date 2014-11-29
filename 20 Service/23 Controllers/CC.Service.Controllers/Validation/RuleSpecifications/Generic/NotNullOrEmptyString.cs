using System;
using System.Linq.Expressions;
using CC.Common.Helpers.Instance;
using CC.Service.Controllers.Validation.RuleSpecifications.Base;

namespace CC.Service.Controllers.Validation.RuleSpecifications.Generic
{
    /// <summary>
    /// Validate input string
    /// </summary>
    public class NotNullOrEmptyString : BaseSpecification<string>
    {
        #region Overrides of BaseSpecification<string>

        /// <summary>
        /// Validation expression that must be fullfilled
        /// </summary>
        /// <returns></returns>
        public override Expression<Func<string, bool>> ValidationExpression
        {
            get { return x => !x.IsNullOrEmpty(); }
        }

        #endregion
    }
}
