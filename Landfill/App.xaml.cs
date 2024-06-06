using AutoMapper;
using Landfill.Abstractions;
using Landfill.Automapper;
using Landfill.DataAccess;
using Landfill.MVVM.ViewModels;
using Landfill.MVVM.Views;
using Landfill.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
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
            services.AddTransient<BuildProjectViewModel>();
            services.AddTransient<LoginWindowViewModel>();
            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<BuildProjectEditableViewModel>();
            services.AddTransient<EmptyViewModel>();
            services.AddTransient<BuildProjectAddNewViewModel>();
            services.AddTransient<EmployeeProfileViewModel>();
            services.AddTransient<EmployeesManagingViewModel>();
            services.AddTransient<EmployeeInfoViewModel>();
            services.AddTransient<EmployeeInfoEditableViewModel>();

            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<ICredentialsService, CredentialsService>();
            services.AddSingleton<IUserContextService, UserContextService>();
            services.AddSingleton<IItemsService, ItemsService>();
            services.AddSingleton<IEmployeeService, EmployeeService>();

            services.AddSingleton<Func<Type, ViewModelBase>>(provider => viewModel => (ViewModelBase)provider.GetRequiredService(viewModel));
            services.AddSingleton<Func<Type, Window>>(provider => view => (Window)provider.GetRequiredService(view));

            services.AddDbContext<IDbContext, LandfillDbContext>();
            services.AddAutoMapper(typeof(ModelProfile).Assembly);

            _serviceProvider = services.BuildServiceProvider();

            //ApplySeed();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            try 
            {
                _serviceProvider.GetRequiredService<IMapper>().ConfigurationProvider.AssertConfigurationIsValid();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }

            var userService = _serviceProvider.GetRequiredService<IUserContextService>();
            userService.SetUserFromStored();
            if (userService.CurrentUser != null)
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
