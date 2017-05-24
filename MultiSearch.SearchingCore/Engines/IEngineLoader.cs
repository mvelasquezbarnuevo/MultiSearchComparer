using MultiSearch.Common.Formatters;
using MultiSearch.Common.Search;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Threading.Tasks;

namespace MultiSearch.SearchingCore.Engines
{
    public interface IEngineLoader
    {
        void Load(DirectoryCatalog catalog);
        bool Ready();
        List<PluginDescriptor> GetAvailablePlugins();
        Task<ContestResponse> SendRequest(ISearchRequest searchrequest);
    }
}