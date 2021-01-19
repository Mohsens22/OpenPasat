﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;
using ReactiveUI;
using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;
using Splat.Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection;
using UnoTest.Shared.ViewModels;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace UnoTest
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            Initialize();
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }
        public IServiceProvider ServiceProvider { get; private set; }
        void Initialize()
        {
            var host = Host
              .CreateDefaultBuilder()
              .ConfigureAppConfiguration((hostingContext, config) =>
              {
                  config.Properties.Clear();
                  config.Sources.Clear();
                  hostingContext.Properties.Clear();

                  //foreach (var fileProvider in config.Properties.Where(p => p.Value is PhysicalFileProvider).ToList())
                  //  config.Properties.Remove(fileProvider);

                  //IHostEnvironment hostingEnvironment = hostingContext.HostingEnvironment;
                  //config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).AddJsonFile("appsettings." + hostingEnvironment.EnvironmentName + ".json", optional: true, reloadOnChange: true);


                  //if (hostingEnvironment.IsDevelopment() && !string.IsNullOrEmpty(hostingEnvironment.ApplicationName))
                  //{
                  //  Assembly assembly = Assembly.Load(new AssemblyName(hostingEnvironment.ApplicationName));
                  //  if (assembly != null)
                  //  {
                  //    config.AddUserSecrets(assembly, optional: true);
                  //  }
                  //}
                  //config.AddEnvironmentVariables();          
              })
              .ConfigureServices(ConfigureServices)
              .ConfigureLogging(loggingBuilder =>
              {
                  // remove loggers incompatible with UWP
                  {
                      var eventLoggers = loggingBuilder.Services
                      .Where(l => l.ImplementationType == typeof(EventLogLoggerProvider))
                      .ToList();

                      foreach (var el in eventLoggers)
                          loggingBuilder.Services.Remove(el);
                  }

                  //Uno.Extensions.LogExtensionPoint.AmbientLoggerFactory.WithFilter(CreateFilterLoggerSettings());
                  loggingBuilder
                  .AddSplat()
#if !__WASM__
            .AddConsole()
#else
            .ClearProviders()            
#endif

#if DEBUG
            .SetMinimumLevel(LogLevel.Debug)
#else
            .SetMinimumLevel(LogLevel.Information)
#endif
            ;

              })
              .Build();

            ServiceProvider = host.Services;
            ServiceProvider.UseMicrosoftDependencyResolver();
        }

        void ConfigureServices(IServiceCollection services)
        {
            services.UseMicrosoftDependencyResolver();
            var resolver = Splat.Locator.CurrentMutable;
            resolver.InitializeSplat();
            resolver.InitializeReactiveUI();

            var allTypes = Assembly.GetExecutingAssembly()
              .DefinedTypes
              .Where(t => !t.IsAbstract);

            // register view models
            {
                services.AddSingleton<NavigationViewModel>();
                services.AddSingleton<IScreen>(sp => sp.GetRequiredService<NavigationViewModel>());

                services.AddSingleton<StartUpViewModel>();
                services.AddSingleton<ResultsViewModel>();
                services.AddSingleton<TestViewModel>();

                var rvms = allTypes.Where(t => typeof(RoutableViewModel).IsAssignableFrom(t));
                foreach (var rvm in rvms)
                    services.AddTransient(rvm);
            }

            // register views
            {
                var vf = typeof(IViewFor<>);
                bool isGenericIViewFor(Type ii) => ii.IsGenericType && ii.GetGenericTypeDefinition() == vf;
                var views = allTypes
                  .Where(t => t.ImplementedInterfaces.Any(isGenericIViewFor));

                foreach (var v in views)
                {
                    var ii = v.ImplementedInterfaces.Single(isGenericIViewFor);

                    services.AddTransient(ii, v);
                    //Locator.CurrentMutable.Register(() => Locator.Current.GetService(v), ii, "Landscape");
                }
            }

        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
			if (System.Diagnostics.Debugger.IsAttached)
			{
				// this.DebugSettings.EnableFrameRateCounter = true;
			}
#endif

#if NET5_0 && WINDOWS
			var window = new Window();
			window.Activate();
#else
            var window = Windows.UI.Xaml.Window.Current;
#endif

            Frame rootFrame = window.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                window.Content = rootFrame;
            }

#if !(NET5_0 && WINDOWS)
            if (e.PrelaunchActivated == false)
#endif
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    var vm = ServiceProvider.GetService<NavigationViewModel>();
                    var view = ServiceProvider.GetRequiredService<IViewLocator>().ResolveView(vm);
                    rootFrame.Content = view;
                    rootFrame.DataContext = vm;
                }
                // Ensure the current window is active
                window.Activate();
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception($"Failed to load {e.SourcePageType.FullName}: {e.Exception}");
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
        
    }
}
