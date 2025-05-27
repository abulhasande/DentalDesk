using Dental.WinForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dental.WinForm.Services
{
    public interface IPatientService
    {
        Task<List<Patient>> GetPatientsAsync();
    }
}
