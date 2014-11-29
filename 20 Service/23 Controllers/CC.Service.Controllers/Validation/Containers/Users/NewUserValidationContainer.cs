using System;
using CC.Service.Controllers.Validation.Containers.Base;
using CC.Service.Controllers.Validation.RuleSpecifications.Users;
using CC.Service.DomainEntities.Users;

namespace CC.Service.Controllers.Validation.Containers.Users
{
    /// <summary>
    /// New user container for validation
    /// </summary>
    public class NewUserValidationContainer : BaseValidationContainer<User>
    {
        public NewUserValidationContainer(Func<bool> existingUserDelegate)
        {
            ValidationRulesForExecutution.Add(new NewUserMatchingSpecification());
            ValidationRulesForExecutution.Add(new EmailMatchingSpecification());
            ValidationRulesForExecutution.Add(new IsNotExistingUserSpecification(existingUserDelegate));
        }
    }
}
