using System;
using System.ComponentModel.Composition;

namespace CC.Service.DataRepositoryContracts.Extensibility.Imports
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public class DataAccessImportAttribute : ImportAttribute
    {
        public DataAccessImportAttribute()
            : base()
        {
            base.RequiredCreationPolicy = RequiredCreationPolicy = CreationPolicy.NonShared;
        }
    }
}
