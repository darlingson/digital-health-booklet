import React, { useState } from 'react';
import { Search, QrCode, UserPlus } from 'lucide-react';
import { QRScanner } from './QRScanner';
import { NewPatientForm } from './NewPatientForm';
import { Patient } from '../types';

interface PatientLookupProps {
  onPatientSelect: (patient: Patient) => void;
  patients: Patient[];
}

export const PatientLookup: React.FC<PatientLookupProps> = ({ onPatientSelect, patients }) => {
  const [showScanner, setShowScanner] = useState(false);
  const [showNewPatientForm, setShowNewPatientForm] = useState(false);
  const [searchId, setSearchId] = useState('');
  const [error, setError] = useState('');

  const handleSearch = () => {
    const patient = patients.find(p => p.nationalId === searchId || p.id === searchId);
    if (patient) {
      onPatientSelect(patient);
      setError('');
    } else {
      setError('Patient not found');
    }
  };

  const handleQRCodeResult = (result: string) => {
    setSearchId(result);
    setShowScanner(false);
    const patient = patients.find(p => p.nationalId === result || p.id === result);
    if (patient) {
      onPatientSelect(patient);
      setError('');
    } else {
      setError('Patient not found');
    }
  };

  if (showNewPatientForm) {
    return <NewPatientForm onClose={() => setShowNewPatientForm(false)} onPatientCreated={onPatientSelect} />;
  }

  return (
    <div className="bg-white rounded-lg shadow-md p-6">
      <div className="flex flex-col md:flex-row md:items-center gap-4 mb-6">
        <div className="flex-1">
          <h2 className="text-xl font-semibold text-gray-800 mb-4">Find Patient</h2>
          <div className="flex gap-2">
            <div className="flex-1 relative">
              <input
                type="text"
                value={searchId}
                onChange={(e) => setSearchId(e.target.value)}
                placeholder="Enter Patient ID"
                className="w-full px-4 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
              />
              {error && <p className="text-red-500 text-sm mt-1">{error}</p>
              }
            </div>
            <button
              onClick={handleSearch}
              className="flex items-center gap-2 bg-blue-600 text-white px-4 py-2 rounded-md hover:bg-blue-700"
            >
              <Search className="w-4 h-4" />
              <span>Search</span>
            </button>
          </div>
        </div>

        <div className="flex gap-2">
          <button
            onClick={() => setShowScanner(true)}
            className="flex items-center gap-2 bg-gray-600 text-white px-4 py-2 rounded-md hover:bg-gray-700"
          >
            <QrCode className="w-4 h-4" />
            <span>Scan QR</span>
          </button>
          <button
            onClick={() => setShowNewPatientForm(true)}
            className="flex items-center gap-2 bg-green-600 text-white px-4 py-2 rounded-md hover:bg-green-700"
          >
            <UserPlus className="w-4 h-4" />
            <span>New Patient</span>
          </button>
        </div>
      </div>

      {showScanner && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center p-4">
          <div className="bg-white rounded-lg p-6 max-w-lg w-full">
            <h3 className="text-lg font-semibold mb-4">Scan Patient QR Code</h3>
            <QRScanner
              onResult={handleQRCodeResult}
              onClose={() => setShowScanner(false)}
            />
          </div>
        </div>
      )}
    </div>
  );
};