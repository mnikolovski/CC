using System;
using CC.Service.DomainProcesses.Extensibility.Markers;
using Emit.ExtensibilityProvider.Extensibility;

namespace CC.Service.DomainProcesses.Extensibility.Exports
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class NotificationExportAttribute : ConstraintExportAttribute
    {
        /// <param name="exportType">Types that we export</param>
        public NotificationExportAttribute(Type exportType)
            : base(exportType, typeof(INotificationController))
        {
            
        }
    }
}
