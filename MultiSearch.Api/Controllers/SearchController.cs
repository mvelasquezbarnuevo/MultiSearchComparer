using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MultiSearch.Api.Controllers
{
    [RoutePrefix("Search")]
    public class SearchController : ApiController
    {
        [Route("")]
        public IHttpActionResult GetList()
        {
            return Ok(new { Value = "objeto2" });
        }
    }
}
