using ClosedXML.Excel;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PR7.Pages
{
    /// <summary>
    /// Логика взаимодействия для ExcelReportGenerator.xaml
    /// </summary>
    public partial class ExcelReportGenerator : Page
    {
        public ExcelReportGenerator()
        {
            InitializeComponent();
        }

        private void Export1_Button(object sender, RoutedEventArgs e)
        {
            string jsonFileName = "goods_data.json";
            string fullJsonFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, jsonFileName);

            List<GoodItem> goodsList = new List<GoodItem>();

            try
            {
                if (File.Exists(fullJsonFilePath))
                {
                    string jsonContent = File.ReadAllText(fullJsonFilePath);

                    if (string.IsNullOrWhiteSpace(jsonContent))
                    {
                        MessageBox.Show($"Файл '{jsonFileName}' пуст или не содержит данных.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    goodsList = JsonConvert.DeserializeObject<List<GoodItem>>(jsonContent);

                    if (goodsList == null || !goodsList.Any())
                    {
                        MessageBox.Show($"В файле '{jsonFileName}' не найдено товаров для экспорта.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show($"Файл '{jsonFileName}' не найден по пути: {fullJsonFilePath}", "Ошибка Файла", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Товары");

                    worksheet.Cell(1, 1).Value = "Артикул";
                    worksheet.Cell(1, 2).Value = "Название";
                    worksheet.Cell(1, 3).Value = "Категория";
                    worksheet.Cell(1, 4).Value = "Единица измерения";
                    worksheet.Cell(1, 5).Value = "Количество на складе";

                    worksheet.Range("A1:E1").Style.Font.Bold = true;

                    int row = 2;
                    foreach (var good in goodsList)
                    {
                        worksheet.Cell(row, 1).Value = good.Article;
                        worksheet.Cell(row, 2).Value = good.Name;
                        worksheet.Cell(row, 3).Value = good.Category;
                        worksheet.Cell(row, 4).Value = good.UnitOfMeasurement;
                        worksheet.Cell(row, 5).Value = good.Count;
                        row++;
                    }

                    worksheet.Columns().AdjustToContents();

                    SaveFileDialog saveFileDialog = new SaveFileDialog
                    {
                        Filter = "Excel Files|*.xlsx",
                        FileName = "GoodsReport.xlsx" 
                    };

                    if (saveFileDialog.ShowDialog() == true)
                    {
                        workbook.SaveAs(saveFileDialog.FileName);
                        MessageBox.Show($"Данные успешно экспортированы в файл: {saveFileDialog.FileName}", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Экспорт отменен пользователем.", "Отмена", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (JsonSerializationException ex)
            {
                MessageBox.Show($"Ошибка при чтении или обработке JSON файла: {ex.Message}\nПроверьте формат '{jsonFileName}'.", "Ошибка JSON", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Ошибка ввода/вывода при работе с файлом: {ex.Message}", "Ошибка Файла", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла непредвиденная ошибка при экспорте в Excel: {ex.Message}", "Общая Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Export2_Button(object sender, RoutedEventArgs e)
        {
            string jsonFileName = "operations_data.json";
            string fullJsonFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, jsonFileName);

            List<ItemsMovements> movementsList = new List<ItemsMovements>();

            try
            {
                if (File.Exists(fullJsonFilePath))
                {
                    string jsonContent = File.ReadAllText(fullJsonFilePath);

                    if (string.IsNullOrWhiteSpace(jsonContent))
                    {
                        MessageBox.Show($"Файл '{jsonFileName}' пуст или не содержит данных.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    movementsList = JsonConvert.DeserializeObject<List<ItemsMovements>>(jsonContent);

                    if (movementsList == null || !movementsList.Any())
                    {
                        MessageBox.Show($"В файле '{jsonFileName}' не найдено операций для экспорта.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show($"Файл '{jsonFileName}' не найден по пути: {fullJsonFilePath}", "Ошибка Файла", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Движения Товаров");

                    worksheet.Cell(1, 1).Value = "Номер операции";
                    worksheet.Cell(1, 2).Value = "Артикул";
                    worksheet.Cell(1, 3).Value = "Тип операции";
                    worksheet.Cell(1, 4).Value = "Количество";
                    worksheet.Cell(1, 5).Value = "Причина";

                    worksheet.Range("A1:E1").Style.Font.Bold = true;

                    int row = 2; 
                    foreach (var movement in movementsList)
                    {
                        worksheet.Cell(row, 1).Value = movement.OperationNumber;
                        worksheet.Cell(row, 2).Value = movement.Article;
                        worksheet.Cell(row, 3).Value = movement.OperationType;
                        worksheet.Cell(row, 4).Value = movement.Count;
                        worksheet.Cell(row, 5).Value = string.IsNullOrEmpty(movement.Reason) ? "Не указана" : movement.Reason;
                        row++;
                    }

                    worksheet.Columns().AdjustToContents();

                    SaveFileDialog saveFileDialog = new SaveFileDialog
                    {
                        Filter = "Excel Files|*.xlsx", 
                        FileName = "OperationsReport.xlsx" 
                    };

                    if (saveFileDialog.ShowDialog() == true)
                    {
                        workbook.SaveAs(saveFileDialog.FileName);
                        MessageBox.Show($"Данные успешно экспортированы в файл: {saveFileDialog.FileName}", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Экспорт отменен пользователем.", "Отмена", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (JsonSerializationException ex)
            {
                MessageBox.Show($"Ошибка при чтении или обработке JSON файла: {ex.Message}\nПроверьте формат '{jsonFileName}'.", "Ошибка JSON", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Ошибка ввода/вывода при работе с файлом: {ex.Message}", "Ошибка Файла", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла непредвиденная ошибка при экспорте в Excel: {ex.Message}", "Общая Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Export3_Button(object sender, RoutedEventArgs e)
        {
            string jsonFileName = "supplier_orders.json";
            string fullJsonFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, jsonFileName);

            List<SupplierOrder> ordersList = new List<SupplierOrder>();

            try
            {
                if (File.Exists(fullJsonFilePath))
                {
                    string jsonContent = File.ReadAllText(fullJsonFilePath);

                    if (string.IsNullOrWhiteSpace(jsonContent))
                    {
                        MessageBox.Show($"Файл '{jsonFileName}' пуст или не содержит данных.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    ordersList = JsonConvert.DeserializeObject<List<SupplierOrder>>(jsonContent);

                    if (ordersList == null || !ordersList.Any())
                    {
                        MessageBox.Show($"В файле '{jsonFileName}' не найдено заказов для экспорта.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show($"Файл '{jsonFileName}' не найден по пути: {fullJsonFilePath}", "Ошибка Файла", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Заказы Поставщикам");

                    worksheet.Cell(1, 1).Value = "Номер заказа";
                    worksheet.Cell(1, 2).Value = "Артикул";
                    worksheet.Cell(1, 3).Value = "Название товара";
                    worksheet.Cell(1, 4).Value = "Количество";
                    worksheet.Cell(1, 5).Value = "Цена";
                    worksheet.Cell(1, 6).Value = "Единица измерения";
                    worksheet.Cell(1, 7).Value = "Дата оформления";
                    worksheet.Cell(1, 8).Value = "Дата доставки";

                    worksheet.Range("A1:H1").Style.Font.Bold = true;

                    int row = 2; 
                    foreach (var order in ordersList)
                    {
                        worksheet.Cell(row, 1).Value = order.OrderNumber;
                        worksheet.Cell(row, 2).Value = order.Article;
                        worksheet.Cell(row, 3).Value = order.Name;
                        worksheet.Cell(row, 4).Value = order.Quantity;
                        worksheet.Cell(row, 5).Value = order.Price;
                        worksheet.Cell(row, 6).Value = order.UnitOfMeasurement;
                        worksheet.Cell(row, 7).Value = order.OrderDate.ToShortDateString(); 
                        worksheet.Cell(row, 8).Value = order.DeliveryDate.ToShortDateString(); 
                        row++;
                    }

                    worksheet.Columns().AdjustToContents();

                    SaveFileDialog saveFileDialog = new SaveFileDialog
                    {
                        Filter = "Excel Files|*.xlsx", 
                        FileName = "SupplierOrdersReport.xlsx" 
                    };

                    if (saveFileDialog.ShowDialog() == true)
                    {
                        workbook.SaveAs(saveFileDialog.FileName);
                        MessageBox.Show($"Данные успешно экспортированы в файл: {saveFileDialog.FileName}", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Экспорт отменен пользователем.", "Отмена", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (JsonSerializationException ex)
            {
                MessageBox.Show($"Ошибка при чтении или обработке JSON файла: {ex.Message}\nПроверьте формат '{jsonFileName}'.", "Ошибка JSON", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Ошибка ввода/вывода при работе с файлом: {ex.Message}", "Ошибка Файла", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла непредвиденная ошибка при экспорте в Excel: {ex.Message}", "Общая Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Export4_Button(object sender, RoutedEventArgs e)
        {
            string jsonFileName = "staff_working_hours.json";
            string fullJsonFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, jsonFileName);

            List<StaffWorkingHoursRecord> recordsList = new List<StaffWorkingHoursRecord>();

            try
            {
                if (File.Exists(fullJsonFilePath))
                {
                    string jsonContent = File.ReadAllText(fullJsonFilePath);

                    if (string.IsNullOrWhiteSpace(jsonContent))
                    {
                        MessageBox.Show($"Файл '{jsonFileName}' пуст или не содержит данных.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    recordsList = JsonConvert.DeserializeObject<List<StaffWorkingHoursRecord>>(jsonContent);

                    if (recordsList == null || !recordsList.Any())
                    {
                        MessageBox.Show($"В файле '{jsonFileName}' не найдено записей учета рабочего времени для экспорта.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show($"Файл '{jsonFileName}' не найден по пути: {fullJsonFilePath}", "Ошибка Файла", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Табель учета рабочего времени");

                    worksheet.Cell(1, 1).Value = "Дата";
                    worksheet.Cell(1, 2).Value = "ФИО сотрудника";
                    worksheet.Cell(1, 3).Value = "Время прихода";
                    worksheet.Cell(1, 4).Value = "Время ухода";
                    worksheet.Cell(1, 5).Value = "Начало перерыва";
                    worksheet.Cell(1, 6).Value = "Конец перерыва";
                    worksheet.Cell(1, 7).Value = "Длительность перерыва";
                    worksheet.Cell(1, 8).Value = "Время работы";
                    worksheet.Cell(1, 9).Value = "Статус";

                    worksheet.Range("A1:I1").Style.Font.Bold = true;

                    int row = 2; 
                    foreach (var record in recordsList)
                    {
                        worksheet.Cell(row, 1).Value = record.Date;
                        worksheet.Cell(row, 2).Value = record.FullName;
                        worksheet.Cell(row, 3).Value = record.ArrivalTime?.ToString("hh\\:mm") ?? string.Empty;
                        worksheet.Cell(row, 4).Value = record.DepartureTime?.ToString("hh\\:mm") ?? string.Empty;
                        worksheet.Cell(row, 5).Value = record.BreakStartTime?.ToString("hh\\:mm") ?? string.Empty;
                        worksheet.Cell(row, 6).Value = record.BreakEndTime?.ToString("hh\\:mm") ?? string.Empty;
                        worksheet.Cell(row, 7).Value = record.BreakDuration?.ToString("hh\\:mm") ?? string.Empty;
                        worksheet.Cell(row, 8).Value = record.WorkDuration?.ToString("hh\\:mm") ?? string.Empty;
                        worksheet.Cell(row, 9).Value = record.Status ?? string.Empty;
                        row++;
                    }

                    worksheet.Columns().AdjustToContents();

                    SaveFileDialog saveFileDialog = new SaveFileDialog
                    {
                        Filter = "Excel Files|*.xlsx", 
                        FileName = "StaffWorkingHoursReport.xlsx" 
                    };

                    if (saveFileDialog.ShowDialog() == true)
                    {
                        workbook.SaveAs(saveFileDialog.FileName);
                        MessageBox.Show($"Данные успешно экспортированы в файл: {saveFileDialog.FileName}", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Экспорт отменен пользователем.", "Отмена", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (JsonSerializationException ex)
            {
                MessageBox.Show($"Ошибка при чтении или обработке JSON файла: {ex.Message}\nПроверьте формат '{jsonFileName}'.", "Ошибка JSON", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Ошибка ввода/вывода при работе с файлом: {ex.Message}", "Ошибка Файла", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла непредвиденная ошибка при экспорте в Excel: {ex.Message}", "Общая Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
