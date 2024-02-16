using System.Collections.Generic;

namespace TEC_avalonia_cross.Models;

public class Hourly
{
    public List<string> time {get;set;} = new List<string>();
    public List<float> temperature_2m {get;set;} = new List<float>();
    public List<float> relative_humidity_2m {get;set;} = new List<float>();
    public List<float> precipitation_probability {get;set;} = new List<float>();
}