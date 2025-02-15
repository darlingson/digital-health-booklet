import React from 'react';
import { BookOpen, LogOut } from 'lucide-react';
import { User } from '../types';

interface HeaderProps {
  user: User;
  onLogout: () => void;
}

export const Header: React.FC<HeaderProps> = ({ user, onLogout }) => {
  return (
    <header className="bg-blue-600 text-white py-4">
      <div className="container mx-auto px-4">
        <div className="flex items-center justify-between">
          <div className="flex items-center gap-2">
            <BookOpen className="w-8 h-8" />
            <h1 className="text-2xl font-bold">Digital Health Booklet</h1>
          </div>
          
          <div className="flex items-center gap-4">
            <span className="text-sm md:text-base">
              {user.role === 'professional' ? '👨‍⚕️ ' : '👤 '}
              {user.name}
            </span>
            <button
              onClick={onLogout}
              className="flex items-center gap-1 bg-blue-700 hover:bg-blue-800 px-3 py-1 rounded-md text-sm"
            >
              <LogOut className="w-4 h-4" />
              <span className="hidden md:inline">Logout</span>
            </button>
          </div>
        </div>
      </div>
    </header>
  );
};