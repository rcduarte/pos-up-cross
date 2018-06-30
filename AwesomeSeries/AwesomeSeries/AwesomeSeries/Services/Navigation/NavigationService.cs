using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwesomeSeries.ViewModel;
using AwesomeSeries.ViewModel.Base;
using AwesomeSeries.Views;
using Xamarin.Forms;


namespace AwesomeSeries.Services
{
    public class NavigationService : INavigationService
    {
        protected readonly Dictionary<Type, Type> _mappings;

        protected Application CurrentApplication
        {
            get { return Application.Current; }
        }

        public NavigationService()
        {
            _mappings = new Dictionary<Type, Type>();
            CreateViewModelMappings();
        }

        private void CreateViewModelMappings()
        {
            _mappings.Add(typeof(MainViewModel), typeof(MainView));
            _mappings.Add(typeof(DetailViewModel), typeof(DetailView));
        }

        public async Task Initialize()
        {
            await NavigateToAsync<MainViewModel>();
        }

        public async Task NavigateAndClearBackStackAsync<TViewModel>(object parameter = null) where TViewModel : ViewModelBase
        {
            Page page = CreateAndBingPage(typeof(TViewModel), parameter);

            var navigaionPage = CurrentApplication.MainPage as NavigationPage;

            await navigaionPage.PushAsync(page);

            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);

            if (navigaionPage != null && navigaionPage.Navigation.NavigationStack.Count > 0)
            {
                var existingPages = navigaionPage.Navigation.NavigationStack.ToList();

                foreach (var existingPage in existingPages)
                {
                    if (existingPage != page)
                    {
                        navigaionPage.Navigation.RemovePage(existingPage);
                    }
                } 
            }
            

        }

        public async Task NavigateBackAsync()
        {
            if (CurrentApplication.MainPage == null)
            {
                await CurrentApplication.MainPage.Navigation.PopAsync();
            }
        }

        public async Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
            => InternalNavigateToAsync(typeof(TViewModel), null);

        public async Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
            => InternalNavigateToAsync(typeof(TViewModel), parameter);
        
        public async Task NavigateToAsync(Type viewModelType)
            => InternalNavigateToAsync(viewModelType, null);
        
        public async Task NavigateToAsync(Type viewModelType, object parameter)
        => InternalNavigateToAsync(viewModelType, parameter);

        async Task InternalNavigateToAsync (Type viewModelType, object parameter)
        {
            Page page = CreateAndBingPage(viewModelType, parameter);

            var navigationPage = CurrentApplication.MainPage as NavigationPage;

            if (navigationPage != null)
            {
                await navigationPage.PushAsync(page);
            }
            else
            {
                CurrentApplication.MainPage = new NavigationPage(page);
            }

            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
        }

        Page CreateAndBingPage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new Exception($"Mapeamento nao encontrado para a {viewModelType}");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            ViewModelBase viewModel = ViewModelLocator.Instance.Resolve(viewModelType) as ViewModelBase;

            page.BindingContext = viewModel;

            return page;
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!_mappings.ContainsKey(viewModelType))
            {
                throw new Exception($"View não encontrada para essa ViewModel: {viewModelType}");
            }

            return _mappings[viewModelType];
        }

        #region Not Implemented

        public Task RemoveLastFromBackStack()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
