# LED Intensity and Voltage Monitoring System

## Description
This project allows users to control the intensity of an LED light and read the voltage value from a potentiometer using Arduino. The application consists of a frontend part written in React and a backend part developed in ASP.NET Web API. Data is stored in a SQLite database.

## Technologies
- **Hardware:** Arduino Uno R3, LED light, potentiometer, control board
- **Frontend:** React
- **Backend:** C#, ASP.NET Web API
- **Database:** SQLite

## Installation
1. **Clone the repository:**
   ```bash
   git clone https://github.com/username/LEDintensity-voltage-monitoring-system.git
   ```
   
2. **Frontend:**
   - Navigate to the frontend directory.
   - Install the required packages:
   ```bash
   npm install
   ```

3. **Backend:**
   - Navigate to the backend directory.
   - Run migrations to create the database:
   ```bash
   dotnet ef database update
   ```
   - Run the backend application:
   ```bash
   dotnet run
   ```

## Usage
1. Once you run the application, open a web browser at [http://localhost:5005](http://localhost:5005).
2. Users can:
   - Click the **Get LED Data** button to retrieve the current intensity of the LED light and the time of the last change.
   - Click the **Get Voltage** button to retrieve the current voltage value from the potentiometer.
   - Enter the intensity value of the LED light (0-255) in the text field and click the **Set LED Intensity** button.

## Requirements
- Arduino Uno R3
- LED light
- Potentiometer
- Control board
- SQLite
- Node.js
- .NET Core

## Project Structure
```
LEDintensity-voltage-monitoring-system/
│
├── frontend/                # React frontend application
│   ├── src/                # Source code
│   └── package.json         # Dependencies
│
├── backend/                 # ASP.NET Web API
│   ├── Controllers/         # API controllers
│   ├── Models/              # Data models
│   ├── Data/                # Database context
│   └── Program.cs           # Entry point of the application
│
└── SQL/                     # SQL scripts and migrations
    └── database.sql         # Script to create the database
```

## License
This project is licensed under [LICENSE] - see the LICENSE file for more information.
