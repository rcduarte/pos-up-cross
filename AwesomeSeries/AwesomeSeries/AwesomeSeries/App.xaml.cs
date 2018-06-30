using AwesomeSeries.Services;
using AwesomeSeries.ViewModel.Base;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace AwesomeSeries
{
	public partial class App : Application
	{

        public App ()
		{
			InitializeComponent();

            BuildDependencies();

            InitNavigation();
		}

        private void InitNavigation()
        {
            var navigationServise = ViewModelLocator.Instance.Resolve<INavigationService>();

            navigationServise.Initialize();
            
        }

        private void BuildDependencies()
        {
            ViewModelLocator.Instance.Build();
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
