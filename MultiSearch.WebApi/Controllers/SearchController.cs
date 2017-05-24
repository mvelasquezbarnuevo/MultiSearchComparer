using MultiSearch.Common.Search;
using MultiSearch.SearchingCore.Engines;
using MultiSearch.WebApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MultiSearch.WebApi.Controllers
{
    [RoutePrefix("Search")]
    public class SearchController : ApiController
    {
        private readonly IEngineLoader _engineLoader;
        public SearchController(IEngineLoader engineLoader)
        {
            _engineLoader = engineLoader;
        }

        [Route("")]
        public async Task<IHttpActionResult> GetList()
        {
            List<string> args = new List<string>();
            args.Add("java");
            args.Add(".net");
            _engineLoader.Load(new DirectoryCatalog(".\\bin", "MultiSearch.*"));
            if (_engineLoader.Ready())
            {
                var result = await _engineLoader.SendRequest(new SearchRequest { Criteria = args });
                var resultFormatter = new ResultsFormatter();
                resultFormatter.Fill(result);
                return Ok(new ApiResult { WinnerWord = resultFormatter.WinnerWord, SearchEngines = resultFormatter.SearchEngines });
            }
            return Ok();
        }
    }
}
