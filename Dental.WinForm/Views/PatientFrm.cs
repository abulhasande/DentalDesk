using Dental.WinForm.Models;
using Dental.WinForm.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dental.WinForm.Views
{
    public partial class PatientFrm : Form
    {
        private readonly IPatientService _patientService;
        private List<Patient> _patients; 
        public PatientFrm(IPatientService patientService)
        {
            InitializeComponent();
            _patientService = patientService; 
        }

        private async void PatientFrm_Load(object sender, EventArgs e)
        {
            LoadPatients();
        }

        private   void btnAddPatient_Click(object sender, EventArgs e)
        {
            var newPatientFrom = new NewPatientFrm(_patientService);
            newPatientFrom.OnProductSaved =  LoadPatients;
            newPatientFrom.ShowDialog();
        }

        private  async void LoadPatients()
        {
            try
            {
                _patients = await _patientService.GetPatientsAsync();
                dgvPatient.DataSource = null;
                dgvPatient.DataSource = _patients;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchPatient();
            //var searchName = txtSearch.Text.Trim();
            //var patient = _patients.FirstOrDefault(x => string.Equals(x.Name, searchName, StringComparison.OrdinalIgnoreCase));
            //SelectProductInGrid(patient.PatientId); 


        }

        private void SelectProductInGrid(int patientId)
        {
            foreach (DataGridViewRow row in dgvPatient.Rows)
            {
                if (row.DataBoundItem is Patient p && p.PatientId == patientId)
                {
                    row.Selected = true;
                    dgvPatient.FirstDisplayedScrollingRowIndex = row.Index;
                    break;
                }
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            SearchPatient();
        }

        private void SearchPatient()
        {
            var searchName = txtSearch.Text.Trim();
            var patient = _patients.FirstOrDefault(x => string.Equals(x.Name, searchName, StringComparison.OrdinalIgnoreCase));
            if (patient != null)
            {
                dgvPatient.DataSource = null;
                var _tempPatients = new List<Patient>();
                _tempPatients.Add(patient);
                dgvPatient.DataSource = _tempPatients;
            }
            else
            {
                dgvPatient.DataSource = _patients;
            }
        }


    }
}
