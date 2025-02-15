import React from 'react';
import { Patient } from '../types';
import { UserCircle } from 'lucide-react';

interface PatientInfoProps {
  patient: Patient;
}

export const PatientInfo: React.FC<PatientInfoProps> = ({ patient }) => {
  return (
    <div className="bg-white rounded-lg shadow-md p-6 mb-6">
      <div className="flex items-center gap-4 mb-4">
        <UserCircle className="w-16 h-16 text-blue-600" />
        <div>
          <h2 className="text-2xl font-bold text-gray-800">{patient.name}</h2>
          <p className="text-gray-600">ID: {patient.nationalId}</p>
        </div>
      </div>
      <div className="grid grid-cols-2 gap-4">
        <div>
          <p className="text-sm text-gray-600">Date of Birth</p>
          <p className="font-medium">{patient.dateOfBirth}</p>
        </div>
        <div>
          <p className="text-sm text-gray-600">Gender</p>
          <p className="font-medium capitalize">{patient.gender}</p>
        </div>
        <div>
          <p className="text-sm text-gray-600">Contact Number</p>
          <p className="font-medium">{patient.contactNumber}</p>
        </div>
      </div>
    </div>
  );
};