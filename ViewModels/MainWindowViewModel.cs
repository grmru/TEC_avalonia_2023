using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Avalonia.Interactivity;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using TEC_avalonia_2023.Models;

namespace TEC_avalonia_2023.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";

    public ObservableCollection<PersonRecord> Items { get; }

        public MainWindowViewModel()
        {
            Items = new ObservableCollection<PersonRecord>();
        }

        public void ClickHandler(object sender, RoutedEventArgs args)
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
