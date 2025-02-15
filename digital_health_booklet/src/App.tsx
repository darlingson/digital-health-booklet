import React, { useState } from 'react';
import { patientData, healthRecords } from './data';
import { PatientInfo } from './components/PatientInfo';
import { HealthRecordList } from './components/HealthRecordList';
import { Login } from './components/Login';
import { Header } from './components/Header';
import { PatientLookup } from './components/PatientLookup';
import { User, Patient } from './types';

function App() {
  const [user, setUser] = useState<User | null>(null);
  const [selectedPatient, setSelectedPatient] = useState<Patient | null>(null);

  const handleLogin = (loggedInUser: User) => {
    setUser(loggedInUser);
    if (loggedInUser.role === 'patient') {
      setSelectedPatient(patientData);
    }
  };

  const handleLogout = () => {
    setUser(null);
    setSelectedPatient(null);
  };

  if (!user) {
    return <Login onLogin={handleLogin} />;
  }

  return (
    <div className="min-h-screen bg-gray-100">
      <Header user={user} onLogout={handleLogout} />

      <main className="container mx-auto px-4 py-8">
        {user.role === 'professional' && !selectedPatient && (
          <PatientLookup
            onPatientSelect={setSelectedPatient}
            patients={[patientData]} // In a real app, this would be a larger database of patients
          />
        )}

        {selectedPatient && (
          <>
            <PatientInfo patient={selectedPatient} />
            <div className="mb-6">
              <h2 className="text-xl font-semibold text-gray-800 mb-4">Health Records</h2>
              <HealthRecordList records={healthRecords} user={user} />
            </div>
          </>
        )}
      </main>
    </div>
  );
}

export default App;