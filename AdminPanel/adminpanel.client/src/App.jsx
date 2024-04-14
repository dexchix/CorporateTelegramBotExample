import { useEffect, useState } from 'react';
import './App.css';

function App() {
    const [forecasts, setForecasts] = useState();

    useEffect(() => {
        // populateWeatherData();
        getRequests();
    }, []);

    const contents = forecasts === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>Date</th>
                    <th>Temp. (C)</th>
                    <th>Temp. (F)</th>
                    <th>Summary</th>
                </tr>
            </thead>
            <tbody>
                {forecasts.map(forecast => 
      <tr key={forecast.number}>
      <td>{forecast.number}</td>
      <td>{forecast.number}</td>
      <td>{forecast.number}</td>
      <td>{forecast.number}</td>
  </tr>
                )}
            </tbody>
        </table>;

    return (
        <div>
            <h1 id="tabelLabel">Weather forecast</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
        </div>
    );
    
    async function populateWeatherData() {
        const response = await fetch('weatherforecast/GetWeatherForecast');
        debugger;
        const data = await response.json();
        setForecasts(data);
    }

    async function getRequests() {
        const response = await fetch('RequestsForDays/GetActiveRequests');
        debugger;
        const data = await response.json();
        setForecasts(data);
    }
}

export default App;



//"https://localhost:5173/RequestsForDays/GetActiveRequests"
