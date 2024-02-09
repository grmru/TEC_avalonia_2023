using System;
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

    public ObservableCollection<PersonRecord> Items { get; set; }

    public MainWindowViewModel()
    {
        List<PersonRecord> items = new List<PersonRecord>();
        items.Add(new PersonRecord() 
            { 
                Name = "John Doe", 
                Id = 1, 
                TabNumber = "1234", 
                Email = "johndoe@me.com", 
                PhoneNumber = "123-456-7890" 
            });
        items.Add(new PersonRecord()
        {
            Name = "Jane Doe", 
            Id = 2, 
            TabNumber = "1234", 
            Email = "johndoe@me.com", 
            PhoneNumber = "123-456-7890"
        });

        Items = new ObservableCollection<PersonRecord>(items);
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
