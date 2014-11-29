using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using CC.Common.Base.Results;
using CC.Common.Helpers.Instance;

namespace CC.Client.Web.Inrastructure.Response
{
    public static class ActionResponseHelper
    {
        /// <summary>
        /// if the bl processing reuslt is faulted throws 
        /// action excution exception and return result in json format
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controller"></param>
        /// <param name="result"></param>
        public static JsonResult CreateFaultedResult<T>(this Controller controller, T result) where T : VoidResult
        {
            if (result == null || !result.IsFaulted) return null;

            controller.HttpContext.Response.Clear();
            controller.HttpContext.Response.ContentEncoding = Encoding.UTF8;
            controller.HttpContext.Response.HeaderEncoding = Encoding.UTF8;
            controller.HttpContext.Response.TrySkipIisCustomErrors = true;
            controller.HttpContext.Response.StatusCode = 400;
            controller.HttpContext.Response.ContentType = "text/plain";

            var _jsonResult = new JsonResult();
            _jsonResult.ContentEncoding = Encoding.UTF8;
            _jsonResult.ContentType = "text/plain";
            _jsonResult.Data = result.ErrorMessages.Where(x => !x.UserVisibleMessage.IsNullOrEmpty())
                                                   .Select(x => x.UserVisibleMessage.Replace(Environment.NewLine, string.Empty)).ToList();
            _jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return _jsonResult;
        }

        /// <summary>
        /// if the bl processing reuslt is faulted throws 
        /// action excution exception and return result in json format
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="statusCode"></param>
        public static JsonResult CreateFaultedResult(this Controller controller, int statusCode = 400)
        {
            controller.HttpContext.Response.Clear();
            controller.HttpContext.Response.ContentEncoding = Encoding.UTF8;
            controller.HttpContext.Response.HeaderEncoding = Encoding.UTF8;
            controller.HttpContext.Response.TrySkipIisCustomErrors = true;
            controller.HttpContext.Response.StatusCode = statusCode;
            controller.HttpContext.Response.ContentType = "text/plain";

            var _jsonResult = new JsonResult();
            _jsonResult.ContentEncoding = Encoding.UTF8;
            _jsonResult.ContentType = "text/plain";
            _jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return _jsonResult;
        }
    }
}