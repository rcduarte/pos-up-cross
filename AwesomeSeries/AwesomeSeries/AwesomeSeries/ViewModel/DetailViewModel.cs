using AwesomeSeries.ViewModel.Base;
using AwesomeSeries.Models;
using System.Threading.Tasks;

namespace AwesomeSeries.ViewModel
{
    public class DetailViewModel : ViewModelBase
    {

        string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        string _backdrop;
        public string Backdrop
        {
            get { return _backdrop; }
            set { _backdrop = value; OnPropertyChanged(); }
        }

        string _votes;
        public string Votes
        {
            get { return _votes; }
            set { _votes = value; OnPropertyChanged(); }
        }

        string _releaseDate;
        public string ReleaseDate
        {
            get { return _releaseDate; }
            set { _releaseDate = value; OnPropertyChanged(); }
        }

        public DetailViewModel() : base("")
        {

        }

        public override async Task InitializeAsync(object parameter)
        {
            var serie = (parameter as Serie);

            Title = serie.Name;
            Name = serie.Name;
            //Overview
            //Poster
            Backdrop = serie.BackdropPath;

            await base.InitializeAsync(parameter);
        }

    }
}