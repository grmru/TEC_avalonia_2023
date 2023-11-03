﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive;
using Avalonia.Interactivity;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using ReactiveUI;
using TEC_avalonia_2023.Models;

namespace TEC_avalonia_2023.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";

    public ReactiveCommand<Unit, Unit> ClickCommand { get; }

    private ObservableCollection<PersonRecord> _items;
    public ObservableCollection<PersonRecord> Items
    {
        get { return _items; }
        //set { SetProperty(ref _items, value); }
        set { _items = value; }
    }

    public MainWindowViewModel()
    {
        Items = new ObservableCollection<PersonRecord>();
        ClickCommand = ReactiveCommand.Create(ClickHandler);
    }

    public void ClickHandler()
    {
        Items.Clear();

        IWorkbook workbook;
        using (FileStream fileStream = new FileStream("data.xlsx", FileMode.Open, FileAccess.Read))
        {
            workbook = new XSSFWorkbook(fileStream);
        }

        ISheet sheet = workbook.GetSheetAt(0);

        int rowIndex = 4;

        IRow row = sheet.GetRow(rowIndex);
        string cellValue = row.GetCell(1).StringCellValue;

        Items.Add(new PersonRecord() { Name = cellValue });

        while (cellValue != string.Empty)
        {
            rowIndex++;
            row = sheet.GetRow(rowIndex);
            try
            {
                cellValue = row.GetCell(1).StringCellValue;
                Items.Add(new PersonRecord() { Name = cellValue });
            }
            catch (Exception ex)
            {
                cellValue = string.Empty;
            }
        }

    }
}
