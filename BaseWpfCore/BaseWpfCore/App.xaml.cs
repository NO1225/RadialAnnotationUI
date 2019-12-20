using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using static Dna.FrameworkDI;
using static BaseWpfCore.Core.CoreDI;
using static BaseWpfCore.DI;
using Dna;

namespace BaseWpfCore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        /// <summary>
        /// Custom startup so we load our IoC immediately before anything else
        /// </summary>
        /// <param name="e"></param>
        protected override async void OnStartup(StartupEventArgs e)
        {
            // Let the base application do what it needs
            base.OnStartup(e);


            // Setup the main application 
            await ApplicationSetupAsync();

            // Log it
            Logger.LogDebugSource("Application starting...");

            //ViewModelApplication.SetDarkMode(DI.SettingsManager.IsDarkMode());

            //ViewModelApplication.SetFontSize(DI.SettingsManager.GetFontSize());

            ViewModelApplication.GoToPage(ApplicationPage.Dashboard);

            Current.MainWindow = new MainWindow();

            Current.MainWindow.Show();
        }


        /// <summary>
        /// Configures our application ready for use
        /// </summary>
        private async Task ApplicationSetupAsync()
        {
            // Setup the Dna Framework
            Framework.Construct<DefaultFrameworkConstruction>()
                .AddFileLogger()
                .AddApplicationViewModels()
                .AddClientServices()
                .Build();

            // Ensure the client data store 
            //await ClientDataStore.EnsureDataStoreAsync();

        }
    }
}
