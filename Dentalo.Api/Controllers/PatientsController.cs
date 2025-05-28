using Dentalo.Api.Data;
using Dentalo.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dentalo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var patients =await _unitOfWork.Patients.GetAllAsync();

            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var patient = _unitOfWork.Patients.GetByIdAsync(id);

            return patient != null ? Ok(patient) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Patient patient)
        {
            await _unitOfWork.Patients.AddAsync(patient);

            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(Get), new { id = patient.PatientId }, patient);
        }

        [HttpPut]
        public async Task<IActionResult> Update( Patient patient)
        {
            if (patient == null) return BadRequest();

            await _unitOfWork.Patients.UpdateAsync(patient);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _unitOfWork.Patients.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}
