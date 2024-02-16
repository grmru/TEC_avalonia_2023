namespace TEC_avalonia_cross.Models;

public class WeatherRecord
{
    public string Time { get; set; } = "YYYY-MM-DD --:--:--";
    public float Temp { get; set; } = 0;
    public int Humidity { get; set; } = 0;
    public int Precipitation { get; set; } = 0;
}