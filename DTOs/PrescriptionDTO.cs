using System.ComponentModel.DataAnnotations;

namespace Kol1.DTOs
{
    public class PrescriptionDTO
    {
        public int doctorId { get; set; }
        public int patientId { get; set; }
        public string medicine { get; set; }
        [Range(0, int.MaxValue,ErrorMessage ="ilość musi być wieksza zero")]
        public int amount { get; set; }

    }
}
