using Dental.Api.Models;

namespace Dental.Api.Data
{
    public interface IPatientRepository
    {
        IEnumerable<Patient> GetAll();
        Patient GetById(int id);
        Patient GetByName(string name);
        void Add(Patient product);
        void Update(Patient product);
        void Delete(int id);
    }
}
