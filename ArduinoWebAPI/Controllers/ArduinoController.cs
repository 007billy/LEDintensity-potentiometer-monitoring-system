// Controllers/ArduinoController.cs
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ArduinoController : ControllerBase
{
    private readonly ArduinoService _arduinoService;

    public ArduinoController(ArduinoService arduinoService)
    {
      _arduinoService = arduinoService ?? throw new ArgumentNullException(nameof(arduinoService)); // Provjeri da li je arduinoService null
    }

    [HttpPost("led/intensity")]
    public async Task <IActionResult> SetLedIntensityAsync([FromBody] LEDDto2 ledDto2)
    {
        var led = await _arduinoService.SetLedIntensityAsync(ledDto2.Intensity);

        var newLedDto2 = new LEDDto2
        {
            Intensity = led.Intensity
        };
        
        return Ok(led);
    }

    [HttpGet("led/last")]
    public async Task<ActionResult<ResponseLED>> GetLastLedIntensity()
    {
        var led = await _arduinoService.GetLastLedIntensityAsync(); // Pretpostavka: _ledService vraća objekt LED
        if (led == null)
        {
            return NotFound();
        }

        var ledDto = new ResponseLED
        {
            Intensity = led.Intensity,
            ChangeTime = led.ChangeTime
        };

        return Ok(ledDto);
    }


    [HttpGet("potentiometer")]
    public ActionResult<Potentiometer> GetPotentiometerValue()
    {
        var potentiometer = _arduinoService.GetPotentiometerValue();
        return Ok(potentiometer);
    }
}
