using Jose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Filters;

namespace JwtActionFilter.Filters
{
    public class JwtFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var actionfilter = actionContext.ActionDescriptor.GetCustomAttributes<NoJwtFilter>();//根據actionAtttibute選則是否執行該filter
            if (actionfilter.Count != 0)
            {
                return;
            }
            var httprequestheader = actionContext.Request.Headers;
            var secret = "jhihbi;bi;bkgbkbvsdko'bcvpslpcjxz5646531osbsgl564564163@#$%vjnmpwljnmvcl;acnopqelicj";
            var paremeter = actionContext.Request.Headers.Authorization.Parameter;//Authorization token
            var scheme = actionContext.Request.Headers.Authorization.Scheme;
            if (paremeter != null && scheme == "Bearer")
            {
                try
                {
                    var jwtObject = Jose.JWT.Decode<Dictionary<string, object>>(
                        paremeter,
                        Encoding.UTF8.GetBytes(secret),
                        JwsAlgorithm.HS256);
                }
                catch (Exception ex)
                {
                    setErrorMessage(actionContext, ex.Message);
                }
            }
            else
            {
                setErrorMessage(actionContext,"token error");
            }
        }
        public static void setErrorMessage(System.Web.Http.Controllers.HttpActionContext actionContext, string message)
        {
            actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, message);
        }
    }
}