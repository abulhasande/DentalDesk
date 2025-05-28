using Dentalo.Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Numerics;
using System.Reflection;
using System.Transactions;

namespace Dentalo.Api.Data
{
    public class PatientRepository : IPatientRepository
    {
        private readonly SqlConnection _connection;
        private readonly ILogger<PatientRepository> _logger;
        private readonly SqlTransaction _transaction;
        public PatientRepository(SqlConnection connection, ILogger<PatientRepository> logger, SqlTransaction transaction)
        {
            _connection = connection;
            _logger = logger;
            _transaction = transaction;
        }

        public async Task AddAsync(Patient patient)
        {
            var cmd = new SqlCommand(@"INSERT INTO Patients (Name, Phone, Email, Address, DOB, Gender, Allergies, CreatedAt)
            VALUES (@Name, @Phone, @Email, @Address, @DOB, @Gender, @Allergies, @CreatedAt)", _connection)
            {
                Transaction = _transaction
            };

            cmd.Parameters.AddWithValue("@Name", patient.Name);
            cmd.Parameters.AddWithValue("@Phone", patient.Phone);
            cmd.Parameters.AddWithValue("@Email", patient.Email);
            cmd.Parameters.AddWithValue("@Address", patient.Address);
            cmd.Parameters.AddWithValue("@DOB", patient.DOB);
            cmd.Parameters.AddWithValue("@Gender", patient.Gender);
            cmd.Parameters.AddWithValue("@Allergies", (object?)patient.Allergies ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedAt", patient.CreatedAt ?? DateTime.UtcNow);

            try
            {
                await cmd.ExecuteNonQueryAsync();
                _logger.LogInformation("Patient added: {Name}", patient.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding patient.");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {

            var cmd = new SqlCommand("DELETE FROM Patients WHERE PatientId = @id", _connection)
            {
                Transaction = _transaction
            };
            cmd.Parameters.AddWithValue("@id", id);
            try
            {
                await cmd.ExecuteNonQueryAsync();
                _logger.LogInformation("Patient deleted: ID {Id}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting patient ID {Id}.", id);
                throw;
            }
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            var patients = new List<Patient>();
            var cmd = new SqlCommand("SELECT * from Patients", _connection)
            {
                Transaction = _transaction
            };

            try
            {
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        patients.Add(new Patient
                        {
                            PatientId = (int)reader["PatientId"],
                            Name = reader["Name"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Email = reader["Email"].ToString(),
                            Address = reader["Address"].ToString(),
                            DOB = reader["DOB"].ToString(),
                            Gender = reader["Gender"].ToString(),
                            Allergies = reader["Allergies"] as string,
                            CreatedAt = reader["CreatedAt"] as DateTime?
                        });
                    }
                }
                _logger.LogInformation("Fetched all patients successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all patients.");
                throw;
            }

            return patients;
        }

        public async Task<Patient> GetByIdAsync(int id)
        {
            var cmd = new SqlCommand("SELECT * FROM Patients WHERE PatientId = @id", _connection)
            {
                Transaction = _transaction
            };
            cmd.Parameters.AddWithValue("@id", id);

            try
            {
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        _logger.LogInformation("Patient with ID {Id} retrieved.", id);
                        return new Patient
                        {
                            PatientId = (int)reader["PatientId"],
                            Name = reader["Name"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Email = reader["Email"].ToString(),
                            Address = reader["Address"].ToString(),
                            DOB = reader["DOB"].ToString(),
                            Gender = reader["Gender"].ToString(),
                            Allergies = reader["Allergies"] as string,
                            CreatedAt = reader["CreatedAt"] as DateTime?
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching patient with ID {Id}.", id);
                throw;
            }

            return null;
        }
        //public async Task<Patient> GetByIdAsync(int id)
        //{
        //    var patient = new Patient();
        //    var cmd = new SqlCommand("SELECT * FROM Patients WHERE PatientId = @id", _connection)
        //    {
        //        Transaction = _transaction
        //    };
        //    cmd.Parameters.AddWithValue("@id", id);

        //    try
        //    {
        //        using (var reader = await cmd.ExecuteReaderAsync())
        //        {
        //            if(await reader.ReadAsync())
        //            {
        //                _logger.LogInformation("Patient with ID {Id} retrieved.", id);

        //                patient.PatientId = (int)reader["PatientId"];
        //                patient.Name = reader["Name"].ToString();
        //                patient.Phone = reader["Phone"].ToString();
        //                patient.Email = reader["Email"].ToString();
        //                patient.Address = reader["Address"].ToString(); 
        //                patient.DOB = reader["DOB"].ToString();
        //                patient.Gender = reader["Gender"].ToString();
        //                patient.Allergies = reader["Allergies"] as string;
        //                patient.CreatedAt = reader["CreatedAt"] as DateTime?;

        //            }
        //            await reader.CloseAsync();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error fetching patient with ID {Id}.", id);
        //        throw;
        //    }


        //    return patient;
        //}

        public async  Task UpdateAsync(Patient patient)
        {
            var cmd = new SqlCommand(@"
            UPDATE Patients
            SET Name = @Name, Phone = @Phone, Email = @Email, Address = @Address,
                DOB = @DOB, Gender = @Gender, Allergies = @Allergies
            WHERE PatientId = @PatientId", _connection)
            {
                Transaction = _transaction
            };

            cmd.Parameters.AddWithValue("@PatientId", patient.PatientId);
            cmd.Parameters.AddWithValue("@Name", patient.Name);
            cmd.Parameters.AddWithValue("@Phone", patient.Phone);
            cmd.Parameters.AddWithValue("@Email", patient.Email);
            cmd.Parameters.AddWithValue("@Address", patient.Address);
            cmd.Parameters.AddWithValue("@DOB", patient.DOB);
            cmd.Parameters.AddWithValue("@Gender", patient.Gender);
            cmd.Parameters.AddWithValue("@Allergies", (object?)patient.Allergies ?? DBNull.Value);

            try
            {
                await cmd.ExecuteNonQueryAsync();
                _logger.LogInformation("Patient updated: ID {Id}", patient.PatientId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating patient ID {Id}.", patient.PatientId);
                throw;
            }
        }
    }
}
