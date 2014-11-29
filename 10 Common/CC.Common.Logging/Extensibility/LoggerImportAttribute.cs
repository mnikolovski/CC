using System;
using System.ComponentModel.Composition;

namespace CC.Common.Logging.Extensibility
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public class LoggerImportAttribute : ImportAttribute
    {
        public LoggerImportAttribute()
            : base()
        {
            base.RequiredCreationPolicy = CreationPolicy.NonShared;
        }
    }
}
