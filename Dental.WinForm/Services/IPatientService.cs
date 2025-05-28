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
        Task<Patient> GetProductByIdAsync(int id);
        Task<Patient> GetProductByNameAsync(int id);
        Task<bool> CreateProductAsync(Patient patient);
        Task<bool> UpdateProductAsync(Patient patient);
        Task<bool> DeleteProductAsync(int id);
    }
}
