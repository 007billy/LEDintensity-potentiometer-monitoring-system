// Services/ArduinoService.cs
using System;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


public class ArduinoService : IDisposable
{
    private readonly SerialPort _serialPort;
    private readonly AppDbContext _dbContext;
    private bool _disposed = false;
    


    public ArduinoService(AppDbContext dbContext)
    {
        _serialPort = new SerialPort("COM3", 9600); // Promijenite na stvarni COM port
        _serialPort.Open();
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); // Provjeri da li je dbContext null
    }
    
    

    public async Task<LED> SetLedIntensityAsync(int intensity)
    {
        if (_serialPort == null || !_serialPort.IsOpen)
        {
            throw new InvalidOperationException("Serial port is not initialized or not open.");
        }

        if (_dbContext == null)
        {
            throw new InvalidOperationException("Database context is not initialized.");
        }

        // Pretpostavka: Arduino očekuje vrijednost u formatu "LXXX" gdje je XXX intenzitet.
        string command = $"L{intensity:000}";
        _serialPort.WriteLine(command); 

        var led = new LED
        {
            Intensity = intensity,
            ChangeTime = DateTime.Now // automatski postavljamo trenutno vrijeme
        };
        

        // Dodajte kod za spremanje u bazu podataka ovdje
        _dbContext.LED.Add(led);
        await _dbContext.SaveChangesAsync(); // Spremi promjene asinkrono
    
        return led;
    }

    public async Task<LED> GetLastLedIntensityAsync()
    {
        // Dohvati LED s najnovijim ChangeTime iz baze podataka
        //return _leds.OrderByDescending(led => led.ChangeTime).FirstOrDefault();
        return await _dbContext.LED
            .OrderByDescending(led => led.ChangeTime)
            .FirstOrDefaultAsync();
    }

    public Potentiometer GetPotentiometerValue()
    {
        // Pretpostavka: Arduino šalje vrijednost u formatu "PXXX"
        _serialPort.WriteLine("GETPOT");
        string response = _serialPort.ReadLine();
        double numdata = double.Parse(response);
        double voltage = numdata * 5 /1024;
        return new Potentiometer { Voltage = voltage, LastChanged = DateTime.Now };
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // Oslobađanje managed resursa
                if (_serialPort != null && _serialPort.IsOpen)
                {
                    _serialPort.Close();
                    _serialPort.Dispose();
                }
            }

            // Oslobađanje unmanaged resursa

            _disposed = true;
        }
    }
}
