import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import FirstComponent from './components/FirstComponent';

const App = () => (
  <Router>
    <Routes>
      <Route path="/" element={<FirstComponent />} />
      <Route path="*" element={<FirstComponent />} /> {/* Catch-all route */}
    </Routes>
  </Router>
);

export default App;
