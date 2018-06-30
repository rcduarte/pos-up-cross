using AwesomeSeries.Models;
using AwesomeSeries.Services;
using AwesomeSeries.Services.API;
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

        public ObservableCollection<Serie> Itens { get; }

        public MainViewModel(ISerieService serieService) : base("Awesome Series")
        {
            _serieService = serieService;

            Itens = new ObservableCollection<Serie>();

            ItemClickCommand = new Command<Serie>(async (item) => await ItemCommandExecute(item)); 
        }

        private async Task ItemCommandExecute(Serie item)
        {
            if (item != null)
            {
                await navigateService.NavigateToAsync<DetailViewModel>(item);
            }
        }

        public override async Task InitializeAsync (object navgationData)
        {
            await base.InitializeAsync(navgationData);

            await LoadDataAsuncAsync();
        }

        private async Task LoadDataAsuncAsync()
        {
            var result = await _serieService.getSerieAsync();

            AddItens(result);
        }

        private void AddItens(SerieResponse result)
        {
            Itens.Clear();

            result?.Series.ToList()?.ForEach(i => Itens.Add(i));

        }
    }
}
