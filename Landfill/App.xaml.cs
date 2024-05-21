using Landfill.Abstractions;
using Landfill.DataAccess;
using Landfill.MVVM;
using Landfill.MVVM.ViewModels;
using Landfill.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace Landfill
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<MainWindowView>(provider => new()
            {
                DataContext = provider.GetRequiredService<MainWindowViewModel>()
            });
            services.AddSingleton<MainWindowViewModel>();
            services.AddTransient<SignInViewModel>();
            services.AddTransient<SignUpViewModel>();
            services.AddTransient<ClientViewModel>();
            services.AddTransient<SignUpSuccessViewModel>();
            services.AddTransient<SignUpSetClientInfoViewModel>();

            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<ICredentialsService, CredentialsService>();

            services.AddSingleton<Func<Type, ViewModelBase>>(provider => viewModel => (ViewModelBase)provider.GetRequiredService(viewModel));

            services.AddDbContext<IDbContext, LandfillDbContext>();

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var view = _serviceProvider.GetRequiredService<MainWindowView>();
            view.Show();

            base.OnStartup(e);
        }
    }
}
