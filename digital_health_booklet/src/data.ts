import { Patient, HealthRecord, User } from './types';

export const patientData: Patient = {
  id: "P001",
  name: "Chimwemwe Banda",
  dateOfBirth: "1990-05-15",
  gender: "female",
  nationalId: "MW199005150001",
  contactNumber: "+265888123456"
};

export const healthRecords: HealthRecord[] = [
  {
    id: "HR001",
    date: "2024-02-15",
    weight: 65.5,
    bloodPressure: "120/80",
    diagnosis: "Malaria",
    treatment: "Artemether/Lumefantrine 80/480mg - 1 tablet twice daily for 3 days",
    nextAppointment: "2024-02-22",
    doctorName: "Dr. Nyasa"
  },
  {
    id: "HR002",
    date: "2024-01-20",
    weight: 66.0,
    bloodPressure: "118/78",
    diagnosis: "Upper Respiratory Tract Infection",
    treatment: "Amoxicillin 500mg - 1 capsule three times daily for 5 days",
    nextAppointment: "2024-01-27",
    doctorName: "Dr. Mwanza"
  }
];

export const demoUsers: User[] = [
  {
    id: "U001",
    email: "patient@example.com",
    name: "Chimwemwe Banda",
    role: "patient"
  },
  {
    id: "U002",
    email: "doctor@example.com",
    name: "Dr. Nyasa",
    role: "professional",
    professionalId: "DOC001"
  }
];