using System;
using System.ComponentModel.Composition;

namespace CC.Service.DataRepositoryContracts.Extensibility.Exports
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class DataAccessExportAttribute : ExportAttribute
    {
        /// <param name="exportType">Types that we export</param>
        public DataAccessExportAttribute(Type exportType)
            : base(exportType)
        {
            
        }
    }
}
