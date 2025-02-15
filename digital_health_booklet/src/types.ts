export interface Patient {
  id: string;
  name: string;
  dateOfBirth: string;
  gender: 'male' | 'female';
  nationalId: string;
  contactNumber: string;
}

export interface HealthRecord {
  id: string;
  date: string;
  weight: number;
  bloodPressure: string;
  diagnosis: string;
  treatment: string;
  nextAppointment: string;
  doctorName: string;
}

export interface VitalSigns {
  weight: number;
  bloodPressure: string;
  temperature: number;
  pulseRate: number;
}

export interface User {
  id: string;
  email: string;
  name: string;
  role: 'patient' | 'professional';
  professionalId?: string;
}

export interface AuthState {
  user: User | null;
  isAuthenticated: boolean;
}