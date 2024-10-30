import React from 'react';
import { createRoot } from 'react-dom/client';
import App from './App';

document.addEventListener('DOMContentLoaded', () => {
    const profileRootElement = document.getElementById('profile-root');

    if (profileRootElement) {
      
        const profileRoot = createRoot(profileRootElement);
        profileRoot.render(<App />);
    } else {
        console.error('profile-root NOT found!');
    }
});

