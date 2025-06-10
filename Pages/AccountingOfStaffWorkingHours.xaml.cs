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
    /// Логика взаимодействия для AccountingOfStaffWorkingHours.xaml
    /// </summary>
    public partial class AccountingOfStaffWorkingHours : Page
    {
        public AccountingOfStaffWorkingHours()
        {
            InitializeComponent();
        }
        private bool _isPlaceholder1 = true;
        private void GotFocus1(object sender, RoutedEventArgs e)
        {
            if (_isPlaceholder1)
            {
                TextBox1.Text = "";
                TextBox1.Foreground = Brushes.Black;
                _isPlaceholder1 = false;
            }
        }
        private void LostFocus1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBox1.Text))
            {
                TextBox1.Text = "ФИО сотрудника";
                TextBox1.Foreground = Brushes.Gray;
                _isPlaceholder1 = true;
            }
        }

        private bool _isPlaceholder2 = true;
        private void MyComboBox_GotFocus1(object sender, RoutedEventArgs e)
        {
            if (_isPlaceholder2)
            {
                ComboBox1.Text = "";
                ComboBox1.Foreground = Brushes.Black;
                _isPlaceholder2 = false;
            }
        }
        private void MyComboBox_LostFocus1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ComboBox1.Text) && ComboBox1.SelectedItem == null)
            {
                ComboBox1.Text = "Операция";
                ComboBox1.Foreground = Brushes.Gray;
                _isPlaceholder2 = true;
            }
            else if (ComboBox1.SelectedItem != null)
            {
                var selectedItem = ComboBox1.SelectedItem as ComboBoxItem;
                if (selectedItem != null)
                {
                    ComboBox1.Text = selectedItem.Content.ToString();
                    ComboBox1.Foreground = Brushes.Black;
                    _isPlaceholder2 = false;
                }
            }
        }
        private void MyComboBox_SelectionChanged1(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox1.SelectedItem != null)
            {
                var selectedItem = ComboBox1.SelectedItem as ComboBoxItem;
                if (selectedItem != null)
                {
                    ComboBox1.Text = selectedItem.Content.ToString();
                    ComboBox1.Foreground = Brushes.Black;
                    _isPlaceholder2 = false;
                }
            }
        }
        //
        private void Send_Button(object sender, RoutedEventArgs e)
        {
            string fullName = TextBox1.Text.Trim();
            string selectedOperation = (ComboBox1.SelectedItem as ComboBoxItem)?.Content.ToString();
            DateTime now = DateTime.Now;
            string todayDate = now.ToString("dd.MM.yyyy");
            TimeSpan currentTime = now.TimeOfDay; 

            if (string.IsNullOrWhiteSpace(fullName) || fullName == "ФИО сотрудника" ||
                string.IsNullOrWhiteSpace(selectedOperation) || selectedOperation == "Выберите операцию")
            {
                MessageBox.Show("Пожалуйста, заполните ФИО сотрудника и выберите тип операции.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string fileName = "staff_working_hours.json";
            string fullFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            List<StaffWorkingHoursRecord> records = new List<StaffWorkingHoursRecord>();

            try
            {
                if (File.Exists(fullFilePath))
                {
                    string jsonContent = File.ReadAllText(fullFilePath);
                    if (!string.IsNullOrWhiteSpace(jsonContent))
                    {
                        records = JsonConvert.DeserializeObject<List<StaffWorkingHoursRecord>>(jsonContent) ?? new List<StaffWorkingHoursRecord>();
                    }
                }

                StaffWorkingHoursRecord currentRecord = records.FirstOrDefault(r => r.FullName.Equals(fullName, StringComparison.OrdinalIgnoreCase) && r.Date == todayDate);

                switch (selectedOperation)
                {
                    case "Приход на работу":
                        if (currentRecord != null && currentRecord.ArrivalTime.HasValue)
                        {
                            MessageBox.Show($"Сотрудник {fullName} уже пришел на работу сегодня ({currentRecord.ArrivalTime.Value.ToString("hh\\:mm")}).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        if (currentRecord != null && currentRecord.DepartureTime.HasValue)
                        {
                            MessageBox.Show($"Сотрудник {fullName} уже ушел с работы сегодня. Невозможно записать приход.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        if (currentRecord == null)
                        {
                            currentRecord = new StaffWorkingHoursRecord
                            {
                                FullName = fullName,
                                Date = todayDate,
                                BreakDuration = TimeSpan.Zero, 
                                WorkDuration = TimeSpan.Zero 
                            };
                            records.Add(currentRecord);
                        }
                        currentRecord.ArrivalTime = currentTime;
                        currentRecord.Status = "Arrived";
                        MessageBox.Show($"Приход на работу для {fullName} записан в {currentTime.ToString("hh\\:mm")}.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;

                    case "Уход на перерыв":
                        if (currentRecord == null || !currentRecord.ArrivalTime.HasValue)
                        {
                            MessageBox.Show($"Сотрудник {fullName} не пришел на работу сегодня. Невозможно уйти на перерыв.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        if (currentRecord.Status == "OnBreak")
                        {
                            MessageBox.Show($"Сотрудник {fullName} уже находится на перерыве.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        if (currentRecord.DepartureTime.HasValue)
                        {
                            MessageBox.Show($"Сотрудник {fullName} уже ушел с работы сегодня. Невозможно уйти на перерыв.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        currentRecord.BreakStartTime = currentTime;
                        currentRecord.Status = "OnBreak";
                        MessageBox.Show($"Уход на перерыв для {fullName} записан в {currentTime.ToString("hh\\:mm")}.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;

                    case "Приход с перерыва":
                        if (currentRecord == null || currentRecord.Status != "OnBreak" || !currentRecord.BreakStartTime.HasValue)
                        {
                            MessageBox.Show($"Сотрудник {fullName} не находится на перерыве. Невозможно записать приход с перерыва.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        if (currentRecord.DepartureTime.HasValue)
                        {
                            MessageBox.Show($"Сотрудник {fullName} уже ушел с работы сегодня. Невозможно записать приход с перерыва.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        currentRecord.BreakEndTime = currentTime;
                        currentRecord.BreakDuration = (currentRecord.BreakDuration ?? TimeSpan.Zero) + (currentRecord.BreakEndTime.Value - currentRecord.BreakStartTime.Value);
                        currentRecord.Status = "Arrived"; 
                        MessageBox.Show($"Приход с перерыва для {fullName} записан в {currentTime.ToString("hh\\:mm")}. Продолжительность перерыва: {currentRecord.BreakDuration.Value.ToString("hh\\:mm")}.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;

                    case "Уход с работы":
                        if (currentRecord == null || !currentRecord.ArrivalTime.HasValue)
                        {
                            MessageBox.Show($"Сотрудник {fullName} не пришел на работу сегодня. Невозможно уйти с работы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        if (currentRecord.Status == "OnBreak")
                        {
                            MessageBox.Show($"Сотрудник {fullName} находится на перерыве. Завершите перерыв перед уходом с работы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        if (currentRecord.DepartureTime.HasValue)
                        {
                            MessageBox.Show($"Сотрудник {fullName} уже ушел с работы сегодня.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        currentRecord.DepartureTime = currentTime;
                        currentRecord.Status = "Departed"; 

                        if (currentRecord.DepartureTime.HasValue && currentRecord.ArrivalTime.HasValue)
                        {
                            TimeSpan totalTimeAtWork = currentRecord.DepartureTime.Value - currentRecord.ArrivalTime.Value;
                            currentRecord.WorkDuration = totalTimeAtWork - (currentRecord.BreakDuration ?? TimeSpan.Zero);
                        }
                        MessageBox.Show($"Уход с работы для {fullName} записан в {currentTime.ToString("hh\\:mm")}.\nВремя работы: {currentRecord.WorkDuration?.ToString("hh\\:mm")}.\nВремя перерыва: {currentRecord.BreakDuration?.ToString("hh\\:mm")}.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;

                    default:
                        MessageBox.Show("Неизвестный тип операции.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                }

                string updatedJson = JsonConvert.SerializeObject(records, Formatting.Indented);
                File.WriteAllText(fullFilePath, updatedJson);

                TextBox1.Text = "ФИО сотрудника";
                TextBox1.Foreground = Brushes.Gray;
                _isPlaceholder1 = true;
                ComboBox1.Text = "Выберите операцию";
                ComboBox1.Foreground = Brushes.Gray;
                ComboBox1.SelectedItem = null;
                _isPlaceholder2 = true;
            }
            catch (JsonSerializationException ex)
            {
                MessageBox.Show($"Ошибка при чтении или обработке JSON файла: {ex.Message}\nФайл мог быть поврежден или имел неверный формат.", "Ошибка JSON", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Ошибка ввода/вывода при работе с файлом: {ex.Message}", "Ошибка Файла", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла непредвиденная ошибка: {ex.Message}", "Общая Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Send2_Button(object sender, RoutedEventArgs e)
        {
            TextBlock1.Text = string.Empty;

            string fileName = "staff_working_hours.json";
            string fullFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            try
            {
                if (File.Exists(fullFilePath))
                {
                    string jsonContent = File.ReadAllText(fullFilePath);

                    if (string.IsNullOrWhiteSpace(jsonContent))
                    {
                        TextBlock1.Text = "Файл staff_working_hours.json пуст или не содержит данных о рабочем времени.";
                        return;
                    }

                    List<StaffWorkingHoursRecord> records = JsonConvert.DeserializeObject<List<StaffWorkingHoursRecord>>(jsonContent);

                    if (records == null || !records.Any())
                    {
                        TextBlock1.Text = "В файле staff_working_hours.json не найдено записей о рабочем времени.";
                        return;
                    }

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("--- Табель учета рабочего времени сотрудников ---");
                    sb.AppendLine();

                    var groupedRecords = records.GroupBy(r => r.FullName).OrderBy(g => g.Key);

                    foreach (var group in groupedRecords)
                    {
                        sb.AppendLine($"ФИО: {group.Key}"); 
                        sb.AppendLine("------------------------------------");

                        foreach (var record in group.OrderBy(r => DateTime.ParseExact(r.Date, "dd.MM.yyyy", null)))
                        {
                            sb.AppendLine($"  Дата: {record.Date}");
                            sb.AppendLine($"    Приход: {(record.ArrivalTime.HasValue ? record.ArrivalTime.Value.ToString("hh\\:mm") : "N/A")}");
                            sb.AppendLine($"    Уход: {(record.DepartureTime.HasValue ? record.DepartureTime.Value.ToString("hh\\:mm") : "N/A")}");
                            sb.AppendLine($"    Перерыв (начало): {(record.BreakStartTime.HasValue ? record.BreakStartTime.Value.ToString("hh\\:mm") : "N/A")}");
                            sb.AppendLine($"    Перерыв (конец): {(record.BreakEndTime.HasValue ? record.BreakEndTime.Value.ToString("hh\\:mm") : "N/A")}");
                            sb.AppendLine($"    Длительность перерыва: {(record.BreakDuration.HasValue ? record.BreakDuration.Value.ToString("hh\\:mm") : "N/A")}");
                            sb.AppendLine($"    Время работы: {(record.WorkDuration.HasValue ? record.WorkDuration.Value.ToString("hh\\:mm") : "N/A")}");
                            sb.AppendLine($"    Текущий статус: {record.Status ?? "N/A"}");
                            sb.AppendLine();
                        }
                        sb.AppendLine("====================================");
                        sb.AppendLine();
                    }
                    TextBlock1.Text = sb.ToString();
                }
                else
                {
                    TextBlock1.Text = $"Файл '{fileName}' не найден по пути: {fullFilePath}";
                    MessageBox.Show($"Файл '{fileName}' не найден. Невозможно сгенерировать отчет.", "Ошибка Файла", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (JsonSerializationException ex)
            {
                TextBlock1.Text = $"Ошибка при чтении или обработке JSON файла '{fileName}': {ex.Message}\nПроверьте формат файла.";
                MessageBox.Show($"Ошибка JSON: {ex.Message}\nПроверьте формат файла '{fileName}'.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IOException ex)
            {
                TextBlock1.Text = $"Ошибка ввода/вывода при работе с файлом '{fileName}': {ex.Message}";
                MessageBox.Show($"Ошибка Файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                TextBlock1.Text = $"Произошла непредвиденная ошибка при получении информации из файла '{fileName}': {ex.Message}";
                MessageBox.Show($"Общая Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
