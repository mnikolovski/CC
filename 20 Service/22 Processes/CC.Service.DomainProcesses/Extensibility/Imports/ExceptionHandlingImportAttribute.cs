using System;
using System.ComponentModel.Composition;
using CC.Service.DomainProcesses.Extensibility.Markers;
using Emit.ExtensibilityProvider.Extensibility;

namespace CC.Service.DomainProcesses.Extensibility.Imports
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public class ExceptionHandlingImportAttribute : ConstraintImportAttribute
    {
        public ExceptionHandlingImportAttribute()
            : base(typeof(IExceptionHandlingController))
        {
            base.RequiredCreationPolicy = RequiredCreationPolicy = CreationPolicy.NonShared;
        }
    }
}
