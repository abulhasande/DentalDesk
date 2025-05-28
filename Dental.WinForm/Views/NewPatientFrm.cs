using Dental.WinForm.Models;
using Dental.WinForm.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dental.WinForm.Views
{
    public partial class NewPatientFrm : Form
    {
        private string _gender; 
        private readonly IPatientService _patientService;

        public Action OnProductSaved { get; set; }
        public NewPatientFrm(IPatientService patientService)
        {
            InitializeComponent();
            _patientService = patientService; 
        }

        private void rbtnMale_CheckedChanged(object sender, EventArgs e)
        {
            _gender = "Male"; 
        }

        private void rbtnFemale_CheckedChanged(object sender, EventArgs e)
        {
            _gender = "Female";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateProductForm(out string error))
            {
                MessageBox.Show(error, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var patient = new Patient()
            {
                Name = txtName.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                DOB = dtpDOB.Text.Trim().ToString(),
                Allergies = txtAllergies.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                CreatedAt = DateTime.Today,
                Gender = _gender,
                Phone   = txtPhone.Text.Trim()
            };

            if(await _patientService.CreateProductAsync(patient))
            {
                MessageBox.Show("Product created.");
                OnProductSaved?.Invoke(); // call the main form method                
                this.Close();
            }
        }

        private bool ValidateProductForm(out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                errorMessage = "Patient name is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                errorMessage = "Patient Phone is required.";
                return false;
            }



            return true;
        }
    }
}
