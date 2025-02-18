using System.Text.Json;
using digital_health_passport_api.Data.Entities;
using digital_health_passport_api.Models;
using Microsoft.AspNetCore.Mvc;
namespace digital_health_passport_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController:ControllerBase
{
    private readonly ILogger<PatientsController> _logger;
    private readonly IPatientRepository _repository;
    public PatientsController(ILogger<PatientsController> logger,IPatientRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }
    [HttpGet]
    public ActionResult<IEnumerable<Patient>> GetAll()
    {
        try
        {
            var patients = _repository.GetAll();
            return Ok(patients);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving patients");
            return StatusCode(500, "An error occurred while retrieving patients.");
        }
    }

    [HttpGet("{id}")]
    public ActionResult<Patient> Get(int id)
    {
        try
        {
            var patient = _repository.Get(id);
            if (patient == null) return NotFound();
            return Ok(patient);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving patient"+id.ToString());
            return StatusCode(500, "An error occurred while retrieving patient with id."+id.ToString());
        }
    }

    [HttpPost]
    public ActionResult<Patient> Add([FromBody] PatientModel patient)
    {
        try
        {
            
            var newPatient = _repository.Add(new Patient
            {
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                DateOfBirth = patient.DateOfBirth,
                NationalId = patient.NationalId,
                PhoneNumber = patient.PhoneNumber
            });
            return CreatedAtAction(nameof(Get), new { id = newPatient.Id }, newPatient);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding patient"+ JsonSerializer.Serialize(patient));
            return StatusCode(500, "An error occurred while adding patient.");
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] PatientModel patient)
    {
        try
        {
            if (id != patient.Id) return BadRequest();
            var updatedPatient = new Patient
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                DateOfBirth = patient.DateOfBirth,
                NationalId = patient.NationalId,
                PhoneNumber = patient.PhoneNumber
            };
            if (!_repository.Update(updatedPatient)) return NotFound();
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating patient");
            return StatusCode(500, "An error occurred while updating patient: " + JsonSerializer.Serialize(patient));
        }   
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            _repository.Remove(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting patient");
            return StatusCode(500, "An error occurred while deleting patient with id :"+ id.ToString() );
        }
    }
}