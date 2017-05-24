using MultiSearch.Common.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultiSearch.WebApi.Models
{
    public class ApiResult
    {
        public string WinnerWord { get; set; }
        public IList<ApiSearchEngine> SearchEngines { get; set; }
        

    }
}