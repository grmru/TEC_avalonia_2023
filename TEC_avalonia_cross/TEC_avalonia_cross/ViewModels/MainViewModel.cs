using System;
using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using TEC_avalonia_cross.Models;
using System.Reactive;

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

    public void ClickHandler()
    {
        Items.Clear();

    }
}
