namespace Dentalo.Api.Data
{ 
    public interface IUnitOfWork : IDisposable
    {
        IPatientRepository Patients { get; }
        Task CompleteAsync();
    }
}
