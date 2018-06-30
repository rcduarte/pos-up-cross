using AwesomeSeries.Models;
using Refit;
using System.Threading.Tasks;

namespace AwesomeSeries.Infra.API
{
    [Headers("Content-Type: application/json")]
    public interface ITmdbApi
    {
        [Get("/tv/popular?api_key={apiKey}")]
        Task<SerieResponse> GetSerieResponseAsync(string apiKey);

    }
}
