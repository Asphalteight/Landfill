using Landfill.Abstractions;
using Landfill.Automapper;
using Landfill.Common.Helpers;
using Landfill.DataAccess;
using Landfill.MVVM.ViewModels;
using Landfill.MVVM.Views;
using Landfill.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;

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
            services.AddTransient<LoginWindowView>(provider => new()
            {
                DataContext = provider.GetRequiredService<LoginWindowViewModel>()
            });
            services.AddTransient<MainWindowView>(provider => new()
            {
                DataContext = provider.GetRequiredService<MainWindowViewModel>()
            });
            services.AddTransient<SignInViewModel>();
            services.AddTransient<SignUpViewModel>();
            services.AddTransient<EmployeeViewModel>();
            services.AddTransient<SignUpSuccessViewModel>();
            services.AddTransient<SignUpSetEmployeeViewModel>();
            services.AddTransient<MenuListProjectsViewModel>();
            services.AddTransient<MenuSettingsViewModel>();
            services.AddTransient<BuildProjectViewModel>();
            services.AddTransient<LoginWindowViewModel>();
            services.AddTransient<MainWindowViewModel>();

            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<ICredentialsService, CredentialsService>();
            services.AddSingleton<IItemsService, ItemsService>();

            services.AddSingleton<Func<Type, ViewModelBase>>(provider => viewModel => (ViewModelBase)provider.GetRequiredService(viewModel));
            services.AddSingleton<Func<Type, Window>>(provider => view => (Window)provider.GetRequiredService(view));

            services.AddDbContext<IDbContext, LandfillDbContext>();
            services.AddAutoMapper(typeof(ModelToModelMapProfile).Assembly);

            _serviceProvider = services.BuildServiceProvider();

            //ApplySeed();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            var currentUserName = StorageHelper.GetStoredUser();
            if (currentUserName != null)
            {
                _serviceProvider.GetRequiredService<MainWindowView>().Show();
            }
            else
            {
                _serviceProvider.GetRequiredService<LoginWindowView>().Show();
            }


            base.OnStartup(e);
        }

        private void ApplySeed()
        {
            using var host = CreateHostBuilder().Build();
            host.StartAsync();
            host.StopAsync();
        }

        private static IHostBuilder CreateHostBuilder() =>
            new HostBuilder()
                .ConfigureServices((ctx, services) =>
                {
                    services.AddDbContext<IDbContext, LandfillDbContext>();
                    services.AddHostedService<SeedService>();
                });
    }
}
