using Kol1.DTOs;

namespace Kol1.Services
{
    public interface IPrescriptionsService
    {

        public int SendPrescription(PrescriptionDTO prescription);
    }
}
