using System;
using System.Linq.Expressions;
using Emit.RuleEngine.Rules;

namespace CC.Service.Controllers.Validation.RuleSpecifications.Base
{
    /// <summary>
    /// Base class in order to remove the need of implementing
    /// the same implementation for expression execution
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseSpecification<T> : IValidationRule<T>
    {
        #region Implementation of IValidationRule<T>

        /// <summary>
        /// Validation expression that must be fullfilled
        /// </summary>
        /// <returns></returns>
        public abstract Expression<Func<T, bool>> ValidationExpression { get; }

        /// <summary>
        /// Execute the defined expression
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Execute(T entity)
        {
            Result = ValidationExpression.Compile()(entity);
            return Result;
        }

        /// <summary>
        /// Execution result
        /// </summary>
        public bool Result { get; private set; }

        #endregion
    }
}
