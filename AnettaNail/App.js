import React, { useState } from 'react';
import './App.css';

function App() {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [email, setEmail] = useState('');
  const [phoneNumber, setPhoneNumber] = useState('');
  const [message, setMessage] = useState('');
  const [isRegistering, setIsRegistering] = useState(false);

  const handleLogin = async () => {
    // Реализация входа...
  };

  const handleRegister = async () => {
    try {
      const response = await fetch('https://localhost:7045/api/Kasutaja/user/register', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({
          nimi: username,
          parool: password,
          email: email,
          telefoniNumber: phoneNumber,
          rollId: 3 // Присваиваем роль с id 3
        })
      });
      const data = await response.json();
      setMessage(data.message);
    } catch (error) {
      console.error('Error:', error);
      setMessage('Registreerimisviga');
    }
  };

  const toggleRegistrationForm = () => {
    setIsRegistering(!isRegistering);
  };

  return (
    <div className="App">
      <h1>Registreerimine ja sisselogimine</h1>
      <div>
        <label>Kasutajanimi:</label>
        <input type="text" value={username} onChange={(e) => setUsername(e.target.value)} />
      </div>
      <div>
        <label>Parool:</label>
        <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} />
      </div>
      {isRegistering && (
        <>
          <div>
            <label>Email:</label>
            <input type="email" value={email} onChange={(e) => setEmail(e.target.value)} />
          </div>
          <div>
            <label>Telefoni number:</label>
            <input type="tel" value={phoneNumber} onChange={(e) => setPhoneNumber(e.target.value)} />
          </div>
        </>
      )}
      {isRegistering ? (
        <button onClick={handleRegister}>Registreeru</button>
      ) : (
        <button onClick={handleLogin}>Logi sisse</button>
      )}
      <button onClick={toggleRegistrationForm}>
        {isRegistering ? 'Tagasi sisselogimisele' : 'Registreeri'}
      </button>
      <p>{message}</p>
    </div>
  );
}

export default App;
