using System.Web;
using CC.Common.Helpers.Instance;

// ReSharper disable CheckNamespace
namespace Fokker.SecurityProvider.Extensions
// ReSharper restore CheckNamespace
{
    internal static class RequestHelpers
    {
        /// <summary>
        /// Get ip address from request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetIpAddress(this HttpRequest request)
        {
            if (request.ServerVariables.IsNull()) return null;

            var _realAddress = request.ServerVariables[@"HTTP_X_FORWARDED_FOR"];
            if (_realAddress.IsNullOrEmpty())
            {
                _realAddress = request.ServerVariables[@"HTTP_FORWARDED"];
            }
            if (_realAddress.IsNullOrEmpty())
            {
                _realAddress = request.ServerVariables[@"REMOTE_ADDR"];
            }

            return _realAddress;
        }
    }
}
