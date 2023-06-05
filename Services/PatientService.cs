using Kol1.DTOs;
using System.Data.SqlClient;

namespace Kol1.Services
{
    public class PatientService : IPatientService
    {
        private string conectionString;

        public PatientService(IConfiguration configuration)
        {
            conectionString = configuration.GetConnectionString("Database");
        }
        public List<PatientDTO> GetPatient(string LastName)
        {
            using (SqlConnection connection = new SqlConnection(conectionString))
            {
                connection.Open();

                using SqlCommand command = new SqlCommand($"SELECT * FROM Patient WHERE lastName = @LastName", connection);
                
                command.Parameters.AddWithValue("@LastName", LastName);


                SqlDataReader reader = command.ExecuteReader();

                var PatientList = new List<PatientDTO>();

                while (reader.Read())
                {
                    PatientList.Add(new PatientDTO
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        DateOfBirth = (DateTime)reader["DateOfBirth"]

                    });
                }

                reader.Close();
                connection.Close();
                return PatientList;
            }
            
        }
    }
}
