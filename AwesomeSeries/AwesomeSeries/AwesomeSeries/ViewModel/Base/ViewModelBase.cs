using AwesomeSeries.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AwesomeSeries.ViewModel.Base
{
    public abstract class ViewModelBase : BindableObject
    {

        protected readonly INavigationService navigateService;

        string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }

        public ViewModelBase(string title)
        {
            Title = title;
            navigateService = ViewModelLocator.Instance.Resolve<INavigationService>();
        }

        public virtual Task InitializeAsync(object navgationData)
        {
            return Task.FromResult(true);
        }
    }
}
