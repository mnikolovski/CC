using System;
using System.ComponentModel.Composition;
using CC.Service.DomainProcesses.Extensibility.Markers;
using Emit.ExtensibilityProvider.Extensibility;

namespace CC.Service.DomainProcesses.Extensibility.Imports
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public class BusinessLogicImportAttribute : ConstraintImportAttribute
    {
        public BusinessLogicImportAttribute()
            : base(typeof(IBusinessController))
        {
            base.RequiredCreationPolicy = RequiredCreationPolicy = CreationPolicy.NonShared;
        }
    }
}
