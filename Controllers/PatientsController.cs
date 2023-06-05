using Kol1.DTOs;
using Kol1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Net;

namespace Kol1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;
        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }
        

        [HttpGet]
        public IActionResult GetPatient(string LastName)
        {
            List<PatientDTO> patients = _patientService.GetPatient(LastName);

            if(patients.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(patients);
            }
            
        }

    }
}
