using Dental.WinForm.Services;
using Dental.WinForm.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dental.WinForm
{
    public partial class MainWindow : Form
    {
        private readonly IServiceProvider _serviceProvider; 
        public MainWindow(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider; 
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        private void btnPatient_Click(object sender, EventArgs e)
        {
            var patientFrm = new PatientFrm(_serviceProvider.GetRequiredService<IPatientService>());
            patientFrm.ShowDialog();
        }
    }
}
