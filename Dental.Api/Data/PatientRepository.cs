using Dental.Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;

using ILogger = Serilog.ILogger;
namespace Dental.Api.Data
{
    public class PatientRepository : IPatientRepository
    {
        private readonly string _connectionString;
        private readonly ILogger _logger;
        public PatientRepository(string connectionString, ILogger logger)
        {
            _connectionString = connectionString;
            _logger = logger; 
        }

        public void Add(Patient patient)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                string query = @"
                                INSERT INTO Patients (Name, Phone, Email, Address, DOB, Gender, Allergies, CreatedAt)
                                VALUES (@Name, @Phone, @Email, @Address, @DOB, @Gender, @Allergies, @CreatedAt)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", patient.Name);
                cmd.Parameters.AddWithValue("@Phone", patient.Phone);
                cmd.Parameters.AddWithValue("@Email", patient.Email);
                cmd.Parameters.AddWithValue("@Address", patient.Address);
                cmd.Parameters.AddWithValue("@DOB", patient.DOB);
                cmd.Parameters.AddWithValue("@Gender", patient.Gender);
                cmd.Parameters.AddWithValue("@Allergies", (object?)patient.Allergies ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CreatedAt", (object?)patient.CreatedAt ?? DBNull.Value);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception  ex)
            {
                _logger.Error(ex.Message);
            }
                        
        }


        public void Delete(int id)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                string query = "DELETE FROM Patients WHERE PatientId = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }

        }

        public IEnumerable<Patient> GetAll()
        {
            try
            {
                var patients = new List<Patient>();
                using SqlConnection conn = new SqlConnection(_connectionString);
                string query = "SELECT * FROM Patients";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    patients.Add(new Patient
                    {
                        PatientId = Convert.ToInt32(reader["PatientId"]),
                        Name = reader["Name"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Email = reader["Email"].ToString(),
                        Address = reader["Address"].ToString(),
                        DOB = reader["DOB"].ToString(),
                        Gender = reader["Gender"].ToString(),
                        Allergies = reader["Allergies"] != DBNull.Value ? reader["Allergies"].ToString() : null,
                        CreatedAt = reader["CreatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedAt"]) : (DateTime?)null
                    });
                }
                return patients;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
            
        }

        public Patient GetById(int id)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                string query = "SELECT * FROM Patients WHERE PatientId = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Patient
                    {
                        PatientId = Convert.ToInt32(reader["PatientId"]),
                        Name = reader["Name"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Email = reader["Email"].ToString(),
                        Address = reader["Address"].ToString(),
                        DOB = reader["DOB"].ToString(),
                        Gender = reader["Gender"].ToString(),
                        Allergies = reader["Allergies"] != DBNull.Value ? reader["Allergies"].ToString() : null,
                        CreatedAt = reader["CreatedAt"] != DBNull.Value
                                                                        ? Convert.ToDateTime(reader["CreatedAt"])
                                                                        : (DateTime?)null
                    };
                }
            }
            catch ( Exception ex)
            {
                _logger.Error(ex.Message); 
            }

            return null;
        }

        public void Update(Patient patient)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                string query = @"
                                UPDATE Patients SET 
                                    Name = @Name, 
                                    Phone = @Phone, 
                                    Email = @Email, 
                                    Address = @Address, 
                                    DOB = @DOB, 
                                    Gender = @Gender, 
                                    Allergies = @Allergies, 
                                    CreatedAt = @CreatedAt
                                WHERE PatientId = @PatientId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", patient.Name);
                cmd.Parameters.AddWithValue("@Phone", patient.Phone);
                cmd.Parameters.AddWithValue("@Email", patient.Email);
                cmd.Parameters.AddWithValue("@Address", patient.Address);
                cmd.Parameters.AddWithValue("@DOB", patient.DOB);
                cmd.Parameters.AddWithValue("@Gender", patient.Gender);
                cmd.Parameters.AddWithValue("@Allergies", (object?)patient.Allergies ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CreatedAt", (object?)patient.CreatedAt ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@PatientId", patient.PatientId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message); ;
            }
           
        }
    }
}
