import React from 'react';
import { HealthRecord, User } from '../types';
import { Calendar, Activity, Weight, Stethoscope, Plus } from 'lucide-react';

interface HealthRecordListProps {
  records: HealthRecord[];
  user: User;
}

export const HealthRecordList: React.FC<HealthRecordListProps> = ({ records, user }) => {
  const isProfessional = user.role === 'professional';

  return (
    <div>
      {isProfessional && (
        <button className="mb-4 flex items-center gap-2 bg-green-600 text-white px-4 py-2 rounded-md hover:bg-green-700">
          <Plus className="w-4 h-4" />
          Add New Record
        </button>
      )}
      
      <div className="space-y-4">
        {records.map((record) => (
          <div key={record.id} className="bg-white rounded-lg shadow-md p-4 md:p-6">
            <div className="flex flex-col md:flex-row md:items-center justify-between mb-4 gap-2">
              <div className="flex items-center gap-2">
                <Calendar className="w-5 h-5 text-blue-600" />
                <span className="font-semibold">{record.date}</span>
              </div>
              <div className="flex flex-wrap items-center gap-2 text-sm md:text-base">
                <div className="flex items-center gap-1">
                  <Activity className="w-5 h-5 text-blue-600" />
                  <span>BP: {record.bloodPressure}</span>
                </div>
                <div className="flex items-center gap-1">
                  <Weight className="w-5 h-5 text-blue-600" />
                  <span>{record.weight} kg</span>
                </div>
              </div>
            </div>
            
            <div className="border-t pt-4">
              <div className="grid md:grid-cols-2 gap-4">
                <div>
                  <h3 className="text-sm font-semibold text-gray-600 mb-1">Diagnosis</h3>
                  <p className="text-gray-800">{record.diagnosis}</p>
                </div>
                <div>
                  <h3 className="text-sm font-semibold text-gray-600 mb-1">Treatment</h3>
                  <p className="text-gray-800">{record.treatment}</p>
                </div>
              </div>
              
              <div className="mt-4 flex flex-col md:flex-row md:items-center justify-between text-sm gap-2">
                <div className="flex items-center gap-2">
                  <Stethoscope className="w-4 h-4 text-blue-600" />
                  <span className="text-gray-600">{record.doctorName}</span>
                </div>
                <div className="text-blue-600">
                  Next Appointment: {record.nextAppointment}
                </div>
              </div>

              {isProfessional && (
                <div className="mt-4 pt-4 border-t flex gap-2 justify-end">
                  <button className="text-blue-600 hover:text-blue-800 text-sm">
                    Edit Record
                  </button>
                  <button className="text-red-600 hover:text-red-800 text-sm">
                    Delete Record
                  </button>
                </div>
              )}
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};