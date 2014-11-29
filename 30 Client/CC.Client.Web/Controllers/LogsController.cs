using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CC.Client.Web.App_Start;
using CC.Client.Web.Inrastructure.Request;
using CC.Client.Web.Models.Logs;
using CC.Common.Helpers.Instance;

namespace CC.Client.Web.Controllers
{
    public class LogsController : _BaseController
    {
        [HttpPost]
        public void Log([Required]LogModel logModel)
        {
            if(logModel.IsNull()) return;
            logModel.RequestData = new RequestExtractor().GetTrackingStatistic(true);
            CcWebService.Instance.Logger.Log(logModel, true);
        }
    }
}
