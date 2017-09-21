using JwtActionFilter.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace JwtActionFilter.Controllers
{
    [JwtFilter]
    public class ValuesController : ApiController
    {
        [HttpPost]
        [NoJwtFilter]
        public Dictionary<string,string> login(member data)
        {
            if (data.account == "123" && data.password == "123")
            {
                var secret = "jhihbi;bi;bkgbkbvsdko'bcvpslpcjxz5646531osbsgl564564163@#$%vjnmpwljnmvcl;acnopqelicj";
                var paylod=new member{account=data.account,role="admin"};
                var token = Jose.JWT.Encode(paylod, Encoding.UTF8.GetBytes(secret), Jose.JwsAlgorithm.HS256);
                return new Dictionary<string,string>{{"token","Bearer "+token}};
            }
            return new Dictionary<string, string> { { "error", "錯誤" } };
        }
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [NoJwtFilter]
        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
    public class member
    {
        public string account { get; set; }
        public string password { get; set; }
        public string role { get; set; }
    }
}
