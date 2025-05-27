using Dental.Api.Data;
using Dental.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;


namespace Dental.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _repository;
          public PatientController(IPatientRepository repos )
        {
            _repository = repos;
         
        }

        [HttpGet]
        public ActionResult<IEnumerable<Patient>> GetAll()
        {
            return Ok(_repository.GetAll());
        }

        [HttpPost]
        public IActionResult Create([FromBody] Patient patient)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _repository.Add(patient);
            //return CreatedAtAction(nameof(Get), new { id = patient.PatientId }, patient);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] Patient patient)
        {
            if (patient == null)
                return BadRequest("Patient can not be  null");

            var existingProduct = _repository.GetById(patient.PatientId);
            if (existingProduct == null)
                return NotFound();

            _repository.Update(patient);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _repository.GetById(id);
            if (product == null)
                return NotFound();

            _repository.Delete(id);
            return NoContent();
        }


        [HttpGet("{id}")]
        public ActionResult<Patient> Get(int id)
        {
            var product = _repository.GetById(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

    }
}
