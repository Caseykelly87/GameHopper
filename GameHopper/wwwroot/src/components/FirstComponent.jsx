import React, { useState } from 'react';
import ReactDOM from 'react-dom';

const FirstComponent = () => {
  // Define state to toggle visibility
  const [showText, setShowText] = useState(false);

  // Toggle the state when button is clicked
  const toggleText = () => {
    setShowText(!showText);
  };

  return (
    <div>
      <h2>WELCOME to</h2>
      {showText && <h1>GameHopper!</h1>}
      <button onClick={toggleText}>
        {showText ? 'Hide' : 'Show'}
      </button>
    </div>
  );
};

export default FirstComponent;