using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AwesomeSeries.Infra;
using AwesomeSeries.Infra.API;
using AwesomeSeries.Models;

namespace AwesomeSeries.Services.API
{
    public class SerieService : ISerieService
    {
        readonly ITmdbApi _api;

        public SerieService(ITmdbApi api)
        {
            _api = api;
        }

        public async Task<SerieResponse> getSerieAsync()
        {

            return await _api.GetSerieResponseAsync(AppSettings.ApiKey);

        }
    }
}
