using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System.Net;
using WhiteBlackList.WebUI.Middlewares;

namespace WhiteBlackList.WebUI.Filters
{
    public class CheckWhiteList:ActionFilterAttribute
    {
        private readonly IPList _ipList;
        public CheckWhiteList(IOptions<IPList> ipList)
        {
            _ipList = ipList.Value;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var reqIPAddress = context.HttpContext.Connection.RemoteIpAddress;
            var isWhiteList = this._ipList.WhiteList.Where(x => IPAddress.Parse(x).Equals(reqIPAddress)).Any();

            if (!isWhiteList) 
            {
                context.Result = new StatusCodeResult((int)HttpStatusCode.Forbidden);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
