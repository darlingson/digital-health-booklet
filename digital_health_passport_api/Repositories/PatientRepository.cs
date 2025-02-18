using Microsoft.EntityFrameworkCore;
using digital_health_passport_api.Data;
using digital_health_passport_api.Models;

namespace digital_health_passport_api.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly ApplicationDbContext _context;

    public PatientRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Data.Entities.Patient> GetAll()
    {
        return _context.Patients.ToList();
    }

    public Data.Entities.Patient? Get(int id)
    {
        return _context.Patients.FirstOrDefault(p => p.Id == id);
    }

    public Data.Entities.Patient Add(Data.Entities.Patient item)
    {
        _context.Patients.Add(item);
        _context.SaveChanges();
        return item;
    }

    public void Remove(int id)
    {
        var patient = _context.Patients.Find(id);
        if (patient != null)
        {
            _context.Patients.Remove(patient);
            _context.SaveChanges();
        }
    }

    public bool Update(Data.Entities.Patient item)
    {
        var existingPatient = _context.Patients.Find(item.Id);
        if (existingPatient == null) return false;

        _context.Entry(existingPatient).CurrentValues.SetValues(item);
        _context.SaveChanges();
        return true;
    }
}
