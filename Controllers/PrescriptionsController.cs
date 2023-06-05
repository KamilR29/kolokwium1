using Kol1.DTOs;
using Kol1.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Kol1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {

        private readonly IPrescriptionsService _prescriptionsService;

        public PrescriptionsController(IPrescriptionsService prescriptionsService)
        {
            _prescriptionsService = prescriptionsService;
        }
        [HttpPost]
        public IActionResult PostPrescription(PrescriptionDTO prescription)
        {
            try
            {
                int id = _prescriptionsService.SendPrescription(prescription);
                if (prescription == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(id);
                }
            }
            catch (RowNotInTableException ex)
            {   
                return NotFound(ex);
            }catch (ArgumentException ex)
            {
                return BadRequest(ex);
            }
            


        }


    }
}
