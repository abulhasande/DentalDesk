
using Microsoft.Data.SqlClient;
using System.Transactions;

namespace Dentalo.Api.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SqlConnection _connection;
        private SqlTransaction _transaction;
        public IPatientRepository Patients { get; }

        public UnitOfWork(IConfiguration configuration, ILogger<PatientRepository> logger)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("Database"));
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            Patients = new PatientRepository(_connection, logger, _transaction) { };
        }



        public async Task CompleteAsync()
        {
            await _transaction.CommitAsync();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }
    }
}
