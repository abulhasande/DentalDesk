using Dental.WinForm.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dental.WinForm
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var services = new ServiceCollection();
            // Define the base URL here
            var apiBaseUrl = "https://localhost:7030";

            services.AddHttpClient<IPatientService, PatientService>(client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl);
            });

            var provider = services.BuildServiceProvider();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow(provider));
        }
    }
}
