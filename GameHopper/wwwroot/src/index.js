import React from 'react';
import { createRoot } from 'react-dom/client';
import App from './App';
import Profile from './components/Profile';

const rootElement = document.getElementById('root');
const root = createRoot(rootElement);
root.render(<App />);

const profileRootElement = document.getElementById('profile-root');
if (profileRootElement) {
  const profileRoot = createRoot(profileRootElement);
  profileRoot.render(<Profile />);
}