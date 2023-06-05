using Kol1.DTOs;

namespace Kol1.Services
{
    public interface IPatientService
    {
        public List<PatientDTO> GetPatient(string LastName);
    }
}
