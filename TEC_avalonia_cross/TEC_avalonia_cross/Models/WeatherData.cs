using System.Collections.Generic;

namespace TEC_avalonia_cross.Models;

public class WeatherData
{
    public float latitude {get;set;} = 0;
    public float longitude {get;set;} = 0;
    public double generationtime_ms {get;set;} = 0;
    public int utc_offset_seconds {get;set;} = 0;
    public string timezone {get;set;} = string.Empty;
    public string timezone_abbreviation {get;set;} = string.Empty;
    public float elevation {get;set;} = 0;
    public HourlyUnits hourly_units {get;set;} = new HourlyUnits();
    public Hourly hourly {get;set;} = new Hourly();
}