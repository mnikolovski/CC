using CC.Service.Controllers.Validation.Containers.Base;
using CC.Service.Controllers.Validation.RuleSpecifications.Base;
using CC.Service.DomainEntities.Users;

namespace CC.Service.Controllers.Validation.Containers.Generic
{
    public class RequestingUserValidationContainer : BaseValidationContainer<User>
    {
        public RequestingUserValidationContainer()
        {
            ValidationRulesForExecutution.Add(new BaseDomainEntitySpecification<User>());
        }
    }
}
