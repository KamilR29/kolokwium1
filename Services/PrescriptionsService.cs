using Kol1.DTOs;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Kol1.Services
{
    public class PrescriptionsService : IPrescriptionsService
    {
        private string conectionString;

        public PrescriptionsService(IConfiguration configuration)
        {
            conectionString = configuration.GetConnectionString("Database");
        }

        public int SendPrescription(PrescriptionDTO prescription)
        {

            using(SqlConnection con = new SqlConnection(conectionString))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                try
                {
                    using(SqlCommand command = new SqlCommand())
                    {
                        command.Connection = con;
                        command.Transaction = tran;

                        int doctorIdTmp = GetDoctor(command, prescription.doctorId);
                        if (doctorIdTmp == 0)
                        {
                            throw new RowNotInTableException("nie istnieje");
                        }
                        if (prescription.amount < 0)
                        {
                            throw new ArgumentException("miejsze zero");
                        }
                        int medicineIdTmp = GetMedicine(new SqlCommand("",con,tran), prescription.medicine);
                        if (medicineIdTmp == 0)
                        {
                            PostMedicine(command,prescription.medicine);

                        }
                      

                        command.CommandText =
                        $"INSERT INTO Prescription (Doctor_Id, Patient_Id, Medicine_Id, Amount, CreatedAt) VALUES (@DoctorId, @PatientId, @MedicineId, @Amount, @CreatedAt)";
                        command.Parameters.AddWithValue("@DoctorId", prescription.doctorId);
                        command.Parameters.AddWithValue("@PatientId", prescription.patientId);
                        command.Parameters.AddWithValue("@MedicineId", prescription.medicine);
                        command.Parameters.AddWithValue("@Amount", prescription.amount);
                        command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                        command.ExecuteNonQuery();


                        command.CommandType = CommandType.Text;
                        int result = GetLastId(command);

                        tran.Commit();
                        return result;
                    }
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
     
        }
        private int GetLastId(SqlCommand command)
        {
            command.CommandText = "SELECT Id FROM Prescription ORDER BY Id DESC ";

            var result = (int)command.ExecuteScalar();

            return result;
        }
        private int GetDoctor(SqlCommand command, int id)
        {
            command.CommandText = "SELECT Id FROM Doctor Where id = "+id;

            var result = (int)command.ExecuteScalar();

            return result;
        }
        private int GetMedicine(SqlCommand command, string name)
        {
            command.CommandText = $"SELECT Id FROM Medicine Where name = @name";
            command.Parameters.AddWithValue("@name", name);


            var result = (int)command.ExecuteScalar();

            return result;
        }
        private void PostMedicine(SqlCommand command, string name)
        {

            command.CommandText =
            $"INSERT INTO Medicine (Name) VALUES (@Id)";
            command.Parameters.AddWithValue("@Id", name);
            command.ExecuteNonQuery();
        }

    }
}
