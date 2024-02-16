using System;
using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using TEC_avalonia_cross.Models;
using System.Reactive;
using System.Net.Http;
using Newtonsoft.Json;

namespace TEC_avalonia_cross.ViewModels;

public class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";

    public ReactiveCommand<Unit, Unit> ClickCommand { get; }

    public ObservableCollection<WeatherRecord> Items { get; set; }

    public MainViewModel()
    {
        List<WeatherRecord> items = new List<WeatherRecord>();
        items.Add(new WeatherRecord());

        Items = new ObservableCollection<WeatherRecord>(items);
        ClickCommand = ReactiveCommand.Create(ClickHandler);
    }

    public async void ClickHandler()
    {
        Items.Clear();

        HttpClient client = new HttpClient();
        HttpResponseMessage response = 
            await client.GetAsync("https://api.open-meteo.com/v1/forecast?latitude=55.7522&longitude=37.6156&hourly=temperature_2m,relative_humidity_2m,precipitation_probability&timezone=Europe%2FMoscow");
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();

        WeatherData data = new WeatherData();
        try
        { 
            data = JsonConvert.DeserializeObject<WeatherData>(responseBody);
        }
        catch(Exception ex)
        {
            data.hourly.time.Add("ERROR");
            data.hourly.temperature_2m.Add(0);
            data.hourly.relative_humidity_2m.Add(0);
            data.hourly.precipitation_probability.Add(0);
        }

        for(int t = 0; t < data.hourly.time.Count; t++)
        {
            WeatherRecord rec = new WeatherRecord();
            rec.Time = data.hourly.time[t];
            if (data.hourly.temperature_2m.Count > t) { rec.Temp = data.hourly.temperature_2m[t]; }
            if (data.hourly.relative_humidity_2m.Count > t) { rec.Humidity = (int)data.hourly.relative_humidity_2m[t]; }
            if (data.hourly.precipitation_probability.Count > t) { rec.Precipitation = (int)data.hourly.precipitation_probability[t]; }
            Items.Add(rec);
        }
    }
}
