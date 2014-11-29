using System.Collections.Generic;
using Emit.RuleEngine.Rules;

namespace CC.Service.Controllers.Validation.Containers.Base
{
    public class BaseValidationContainer<T> : IValidationRuleContainer<T>
    {
        public BaseValidationContainer()
        {
            ValidationRulesForExecutution = new List<IValidationRule<T>>();
        }

        #region Implementation of IValidationRuleContainer<T>

        /// <summary>
        /// Register a rule
        /// </summary>
        /// <param name="validationRule"></param>
        public void AddValidationRule(IValidationRule<T> validationRule)
        {
            if (validationRule != null && validationRule.ValidationExpression != null)
            {
                ValidationRulesForExecutution.Add(validationRule);
            }
        }

        /// <summary>
        /// Return all validation rules registered in the container
        /// </summary>
        public List<IValidationRule<T>> GetValidationRules()
        {
            return ValidationRulesForExecutution;
        }

        #endregion

        protected List<IValidationRule<T>> ValidationRulesForExecutution { get; set; }
    }
}
