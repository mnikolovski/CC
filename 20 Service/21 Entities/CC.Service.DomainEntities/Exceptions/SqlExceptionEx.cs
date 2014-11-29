using System;

namespace CC.Service.DomainEntities.Exceptions
{
    public class SqlExceptionEx : Exception
    {
        public SqlExceptionEx()
        {
        }

        public SqlExceptionEx(string message)
            : base(message)
        {
        }
    }
}
