//import logo from './logo.svg';
import './App.css';
import React, { useState } from 'react';
import axios from 'axios';

function App() {
  const [ledData, setLedData] = useState({ intensity: '', changeTime: '' });
  const [voltage, setVoltage] = useState('');
  const [intensityInput, setIntensityInput] = useState('');

  const handleGetLedData = async () => {
    try {
      const response = await fetch('http://localhost:5005/api/Arduino/led/last');
      const data = await response.json();
      setLedData({ intensity: data.intensity, changeTime: data.changeTime });
    } catch (error) {
      console.error('Error fetching LED data:', error);
    }
  };

  const handleGetVoltage = async () => {
    try {
      const response = await fetch('http://localhost:5005/api/Arduino/POTENTIOMETER');
      const data = await response.json();
      setVoltage(data.voltage);
    } catch (error) {
      console.error('Error fetching voltage:', error);
    }
  };

  const handlePostLedIntensity = async () => {
    try {
      const response = await fetch('http://localhost:5005/api/Arduino/led/intensity', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ intensity: intensityInput }),
      });

      if (response.ok) {
        const data = await response.json();
        console.log('LED intensity set:', data);
      } else {
        console.error('Error setting LED intensity');
      }
    } catch (error) {
      console.error('Error posting LED intensity:', error);
    }
  };

  return (
    <div className = "buttons">
      <h1>Arduino Control Panel</h1>
      
      <div className = "buttons" >
        <button onClick={handleGetLedData}>Get LED Data</button>
        <p>LED Intensity: {ledData.intensity}</p>
        <p>Change Time: {ledData.changeTime}</p>
      </div>
      
      <div className = "buttons">
        <button onClick={handleGetVoltage}>Get Voltage</button>
        <p>Voltage: {voltage}</p>
      </div>
      
      <div className = "buttons">
        <input
          type="number"
          value={intensityInput}
          onChange={(e) => setIntensityInput(e.target.value)}
          placeholder="Set LED Intensity"
        />
        <button onClick={handlePostLedIntensity}>Set LED Intensity</button>
      </div>
    </div>
  );
}

export default App;