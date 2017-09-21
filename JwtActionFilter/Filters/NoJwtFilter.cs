using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace JwtActionFilter.Filters
{
    public class NoJwtFilter : ActionFilterAttribute
    {
    }
}