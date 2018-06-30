using AwesomeSeries.Models;
using AwesomeSeries.Services;
using AwesomeSeries.Services.API;
using AwesomeSeries.Services.Navigation;
using AwesomeSeries.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AwesomeSeries.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        readonly ISerieService _serieService;

        public ICommand ItemClickCommand { get; }
        public ObservableCollection<Serie> Items { get; }

        public MainViewModel(ISerieService serieService) : base("Awesome Series")
        {
            _serieService = serieService;
            Items = new ObservableCollection<Serie>();
            ItemClickCommand = new Command<Serie>(async (item)
                => await ItemClickCommandExecute(item));
        }

        async Task ItemClickCommandExecute(Serie serie)
        {
            if (serie != null)
                await navigateService.NavigateToAsync<DetailViewModel>(serie);
        }

        public override async Task InitializeAsync(object navgationData)
        {
            await base.InitializeAsync(navgationData);

            await LoadDataAsync();
        }

        async Task LoadDataAsync()
        {
            var result = await _serieService.getSerieAsync();

            AddItens(result);
        }

        private void AddItens(SerieResponse result)
        {
            Items.Clear();
            result?.Series.ToList()?.ForEach(i => Items.Add(i));
        }
    }
}