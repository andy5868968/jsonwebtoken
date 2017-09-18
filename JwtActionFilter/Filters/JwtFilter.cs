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
            var httprequestheader = actionContext.Request.Headers;
            var tokens = httprequestheader.GetValues("token");
            var secret ="jhihbi;bi;bkgbkbvsdko'bcvpslpcjxzolvjnmpwljnmvcl;acnopqelicj";
            string token = "";
            var authorization = actionContext.Request.Headers.Authorization.Parameter;//Authorization token
            var authorization2 = actionContext.Request.Headers.Authorization.Scheme;
            var payload = new Dictionary<string, object>()//測試payload 物件
            {
                { "sub", "mr.x@contoso.com" },
                { "exp", 1300819380 }
            };
            var jwtencoding = Jose.JWT.Encode(payload, Encoding.UTF8.GetBytes(secret), JwsAlgorithm.HS256);

            bool compair = secret.Equals(jwtencoding.ToString());

            try
            {
                var jwtObject = Jose.JWT.Decode<Dictionary<string,object>>(
                    authorization,
                    Encoding.UTF8.GetBytes(secret),
                    JwsAlgorithm.HS256);
            }
            catch (Exception ex)
            {
                setErrorMessage(actionContext, ex.Message);
            }


            foreach (var item in tokens)
            {
                token = item.ToString().Trim();
            }
            bool result = (token == "1236") ? true : false;
            if (!result)
            {
                setErrorMessage(actionContext, "errror");
            }
        }
        public static void setErrorMessage(System.Web.Http.Controllers.HttpActionContext actionContext, string message)
        {
            actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, message);
        }
        public class test
        {
            public string id { get; set; }
            public string obj { get; set; }
        }
    }
}