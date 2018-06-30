using AwesomeSeries.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeSeries.Services.API
{
    public interface ISerieService
    {
        Task<IEnumerable<SerieResponse>> getSerieAsync();
    }
}
