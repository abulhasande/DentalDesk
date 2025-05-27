using Dental.WinForm.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dental.WinForm.Views
{
    public partial class PatientFrm : Form
    {
        private readonly IPatientService _patientService;
        public PatientFrm(IPatientService patientService)
        {
            InitializeComponent();
            _patientService = patientService; 
        }

        private async void PatientFrm_Load(object sender, EventArgs e)
        {
            try
            {
                var patients = await _patientService.GetPatientsAsync();
                dgvPatient.DataSource = patients;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
