namespace digital_health_passport_api;

public interface IPatientRepository
{
    List<Data.Entities.Patient> GetAll();
    Data.Entities.Patient? Get(int id);
    Data.Entities.Patient Add(Data.Entities.Patient item);
    void Remove(int id);
    bool Update(Data.Entities.Patient item);
}