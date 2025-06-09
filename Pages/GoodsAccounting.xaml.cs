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
    /// Логика взаимодействия для GoodsAccounting.xaml
    /// </summary>
    public partial class GoodsAccounting : Page
    {
        

        public GoodsAccounting()
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
                TextBox1.Text = "Артикул";
                TextBox1.Foreground = Brushes.Gray;
                _isPlaceholder1 = true;
            }
        }
        private bool _isPlaceholder2 = true;
        private void GotFocus2(object sender, RoutedEventArgs e)
        {
            if (_isPlaceholder2)
            {
                TextBox2.Text = "";
                TextBox2.Foreground = Brushes.Black;
                _isPlaceholder2 = false;
            }
        }
        private void LostFocus2(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBox2.Text))
            {
                TextBox2.Text = "Кол-во";
                TextBox2.Foreground = Brushes.Gray;
                _isPlaceholder2 = true;
            }
        }



        private bool _isPlaceholder3 = true; 
        private void MyComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (_isPlaceholder3)
            {
                ComboBox1.Text = "";
                ComboBox1.Foreground = Brushes.Black;
                _isPlaceholder3 = false;
            }
        }
        private void MyComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ComboBox1.Text) && ComboBox1.SelectedItem == null)
            {
                ComboBox1.Text = "Цена";
                ComboBox1.Foreground = Brushes.Gray;
                _isPlaceholder3 = true;
            }
            else if (ComboBox1.SelectedItem != null)
            {
                var selectedItem = ComboBox1.SelectedItem as ComboBoxItem;
                if (selectedItem != null)
                {
                    ComboBox1.Text = selectedItem.Content.ToString();
                    ComboBox1.Foreground = Brushes.Black;
                    _isPlaceholder3 = false;
                }
            }
        }
        private void MyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox1.SelectedItem != null)
            {
                var selectedItem = ComboBox1.SelectedItem as ComboBoxItem;
                if (selectedItem != null)
                {
                    ComboBox1.Text = selectedItem.Content.ToString();
                    ComboBox1.Foreground = Brushes.Black;
                    _isPlaceholder3 = false;
                }
            }
        }
        private bool _isPlaceholder4 = true;
        private void MyComboBox_GotFocus2(object sender, RoutedEventArgs e)
        {
            if (_isPlaceholder4)
            {
                ComboBox2.Text = "";
                ComboBox2.Foreground = Brushes.Black;
                _isPlaceholder4 = false;
            }
        }
        private void MyComboBox_LostFocus2(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ComboBox2.Text) && ComboBox2.SelectedItem == null)
            {
                ComboBox2.Text = "ед/изм";
                ComboBox2.Foreground = Brushes.Gray;
                _isPlaceholder4 = true;
            }
            else if (ComboBox2.SelectedItem != null)
            {
                var selectedItem = ComboBox2.SelectedItem as ComboBoxItem;
                if (selectedItem != null)
                {
                    ComboBox2.Text = selectedItem.Content.ToString();
                    ComboBox2.Foreground = Brushes.Black;
                    _isPlaceholder4 = false;
                }
            }
        }
        private void MyComboBox_SelectionChanged2(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox2.SelectedItem != null)
            {
                var selectedItem = ComboBox2.SelectedItem as ComboBoxItem;
                if (selectedItem != null)
                {
                    ComboBox2.Text = selectedItem.Content.ToString();
                    ComboBox2.Foreground = Brushes.Black;
                    _isPlaceholder4 = false;
                }
            }
        }
        //

        private bool _isPlaceholder11 = true;
        private void GotFocus11(object sender, RoutedEventArgs e)
        {
            if (_isPlaceholder11)
            {
                TextBox11.Text = "";
                TextBox11.Foreground = Brushes.Black;
                _isPlaceholder11 = false;
            }
        }
        private void LostFocus11(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBox11.Text))
            {
                TextBox11.Text = "Артикул";
                TextBox11.Foreground = Brushes.Gray;
                _isPlaceholder11 = true;
            }
        }

        private bool _isPlaceholder12 = true;
        private void MyComboBox_GotFocus12(object sender, RoutedEventArgs e)
        {
            if (_isPlaceholder12)
            {
                ComboBox12.Text = "";
                ComboBox12.Foreground = Brushes.Black;
                _isPlaceholder12 = false;
            }
        }
        private void MyComboBox_LostFocus12(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ComboBox12.Text) && ComboBox12.SelectedItem == null)
            {
                ComboBox12.Text = "Операция";
                ComboBox12.Foreground = Brushes.Gray;
                _isPlaceholder12 = true;
            }
            else if (ComboBox12.SelectedItem != null)
            {
                var selectedItem = ComboBox12.SelectedItem as ComboBoxItem;
                if (selectedItem != null)
                {
                    ComboBox12.Text = selectedItem.Content.ToString();
                    ComboBox12.Foreground = Brushes.Black;
                    _isPlaceholder12 = false;
                }
            }
        }
        private void MyComboBox_SelectionChanged12(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox12.SelectedItem != null)
            {
                var selectedItem = ComboBox12.SelectedItem as ComboBoxItem;
                if (selectedItem != null)
                {
                    ComboBox12.Text = selectedItem.Content.ToString();
                    ComboBox12.Foreground = Brushes.Black;
                    _isPlaceholder12 = false;
                }
            }
        }

        private bool _isPlaceholder13 = true;
        private void GotFocus12(object sender, RoutedEventArgs e)
        {
            if (_isPlaceholder13)
            {
                TextBox12.Text = "";
                TextBox12.Foreground = Brushes.Black;
                _isPlaceholder13 = false;
            }
        }
        private void LostFocus12(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBox12.Text))
            {
                TextBox12.Text = "Кол-во";
                TextBox12.Foreground = Brushes.Gray;
                _isPlaceholder13 = true;
            }
        }

        private bool _isPlaceholder14 = true;
        private void GotFocus13(object sender, RoutedEventArgs e)
        {
            if (_isPlaceholder14)
            {
                TextBox13.Text = "";
                TextBox13.Foreground = Brushes.Black;
                _isPlaceholder14 = false;
            }
        }
        private void LostFocus13(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBox13.Text))
            {
                TextBox13.Text = "Кол-во";
                TextBox13.Foreground = Brushes.Gray;
                _isPlaceholder14 = true;
            }
        }

        //
        private void Send_Button(object sender, RoutedEventArgs e)
        {
            string art = TextBox1.Text;
            string name = TextBox2.Text;
            string category = ComboBox1.Text;
            string measurement = ComboBox2.Text;

            if (art != "Артикул" && name != "Название" && category != "Категория" && measurement != "ед/изм" && art.All(char.IsDigit))
            {
                var newGood = new GoodItem
                {
                    Article = art,
                    Name = name,
                    Category = category,
                    UnitOfMeasurement = measurement,
                    Count = 0.0,
                };

                string fileName = "goods_data.json";
                string fullFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
                List<GoodItem> goodsList = new List<GoodItem>();

                try
                {
                    if (File.Exists(fullFilePath))
                    {
                        string jsonContent = File.ReadAllText(fullFilePath);
                        if (!string.IsNullOrWhiteSpace(jsonContent))
                        {
                            goodsList = JsonConvert.DeserializeObject<List<GoodItem>>(jsonContent) ?? new List<GoodItem>();
                        }
                    }
                    if (goodsList == null)
                    {
                        goodsList = new List<GoodItem>();
                    }

                    if (goodsList.Any(g => g.Article == newGood.Article))
                    {
                        MessageBox.Show($"Товар с артикулом '{newGood.Article}' уже существует в списке.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    goodsList.Add(newGood);
                    string updatedJson = JsonConvert.SerializeObject(goodsList, Formatting.Indented);
                    File.WriteAllText(fullFilePath, updatedJson);

                    MessageBox.Show("Данные успешно сохранены в файл: " + fullFilePath, "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    TextBox1.Text = "Артикул";
                    TextBox1.Foreground = Brushes.Gray;
                    _isPlaceholder1 = true;
                    TextBox2.Text = "Название";
                    TextBox2.Foreground = Brushes.Gray;
                    _isPlaceholder2 = true;
                    ComboBox1.Text = "Категория";
                    ComboBox1.Foreground = Brushes.Gray;
                    ComboBox1.SelectedItem = null;
                    _isPlaceholder3 = true;
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
            else
            {
                MessageBox.Show("Пожалуйста, заполните все поля корректными данными.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        //
        private void Send2_Button(object sender, RoutedEventArgs e)
        {
            string articleInput = TextBox11.Text;
            string operationType = ComboBox12.Text;
            string countInput = TextBox12.Text;
            string reason = TextBox13.Text;

            if (reason == "Причина") reason = null;
            if (articleInput == "Артикул" || string.IsNullOrWhiteSpace(articleInput) ||
                operationType == "Операция" || string.IsNullOrWhiteSpace(operationType) ||
                countInput == "Кол-во" || string.IsNullOrWhiteSpace(countInput))
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля для учета движения товара (Артикул, Операция, Кол-во).", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!double.TryParse(countInput, out double parsedCount) || parsedCount <= 0)
            {
                MessageBox.Show("Пожалуйста, введите корректное положительное число для количества.", "Ошибка Ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string goodsFileName = "goods_data.json";
            string goodsFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, goodsFileName);
            List<GoodItem> goodsList = new List<GoodItem>();
            GoodItem foundGood = null;

            try
            {
                if (File.Exists(goodsFilePath))
                {
                    string jsonContent = File.ReadAllText(goodsFilePath);
                    if (!string.IsNullOrWhiteSpace(jsonContent))
                    {
                        goodsList = JsonConvert.DeserializeObject<List<GoodItem>>(jsonContent) ?? new List<GoodItem>();
                    }
                }
                else
                {
                    MessageBox.Show($"Файл с данными о товарах '{goodsFileName}' не найден. Невозможно выполнить операцию.", "Ошибка Файла", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                foundGood = goodsList.FirstOrDefault(g => g.Article == articleInput);
                if (foundGood == null)
                {
                    MessageBox.Show($"Товар с артикулом '{articleInput}' не найден в списке товаров.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (operationType == "Приход")
                {
                    foundGood.Count += parsedCount;
                }
                else if (operationType == "Расход")
                {
                    if (foundGood.Count - parsedCount < 0)
                    {
                        MessageBox.Show($"Недостаточно товара '{foundGood.Name}' на складе для операции 'Расход'. Доступно: {foundGood.Count}", "Ошибка Количества", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    foundGood.Count -= parsedCount;
                }
                else
                {
                    MessageBox.Show("Неизвестный тип операции. Пожалуйста, выберите 'Приход' или 'Расход'.", "Ошибка Операции", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                string updatedGoodsJson = JsonConvert.SerializeObject(goodsList, Formatting.Indented);
                File.WriteAllText(goodsFilePath, updatedGoodsJson);
            }
            catch (JsonSerializationException ex)
            {
                MessageBox.Show($"Ошибка при чтении или обработке файла товаров: {ex.Message}\nПроверьте формат '{goodsFileName}'.", "Ошибка JSON", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Ошибка ввода/вывода при работе с файлом товаров: {ex.Message}", "Ошибка Файла", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла непредвиденная ошибка при обновлении данных о товарах: {ex.Message}", "Общая Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //

            string operationsFileName = "operations_data.json";
            string operationsFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, operationsFileName);
            List<ItemsMovements> movementsList = new List<ItemsMovements>();
            int nextOperationNumber = 1;
            try
            {
                if (File.Exists(operationsFilePath))
                {
                    string jsonContent = File.ReadAllText(operationsFilePath);
                    if (!string.IsNullOrWhiteSpace(jsonContent))
                    {
                        movementsList = JsonConvert.DeserializeObject<List<ItemsMovements>>(jsonContent) ?? new List<ItemsMovements>();
                        if (movementsList.Any())
                        {
                            nextOperationNumber = movementsList.Max(m => m.OperationNumber) + 1;
                        }
                    }
                }
                var newMovement = new ItemsMovements
                {
                    OperationNumber = nextOperationNumber,
                    Article = articleInput,
                    OperationType = operationType,
                    Count = parsedCount,
                    Reason = reason
                };

                movementsList.Add(newMovement);
                string updatedMovementsJson = JsonConvert.SerializeObject(movementsList, Formatting.Indented);
                File.WriteAllText(operationsFilePath, updatedMovementsJson);

                string message = $"Операция #{nextOperationNumber} успешно записана.\nКоличество товара '{foundGood.Name}' (Артикул: {foundGood.Article}) на складе: {foundGood.Count} {foundGood.UnitOfMeasurement}";
                MessageBox.Show(message, "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                TextBox11.Text = "Артикул";
                TextBox11.Foreground = Brushes.Gray;
                _isPlaceholder11 = true;
                ComboBox12.Text = "Операция";
                ComboBox12.Foreground = Brushes.Gray;
                ComboBox12.SelectedItem = null;
                _isPlaceholder12 = true;
                TextBox12.Text = "Кол-во";
                TextBox12.Foreground = Brushes.Gray;
                _isPlaceholder13 = true;
                TextBox13.Text = "Причина";
                TextBox13.Foreground = Brushes.Gray;
                _isPlaceholder14 = true;
            }
            catch (JsonSerializationException ex)
            {
                MessageBox.Show($"Ошибка при чтении или обработке файла операций: {ex.Message}\nПроверьте формат '{operationsFileName}'.", "Ошибка JSON", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Ошибка ввода/вывода при работе с файлом операций: {ex.Message}", "Ошибка Файла", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла непредвиденная ошибка при записи операции: {ex.Message}", "Общая Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        //
        private void GetInfo1_Button(object sender, RoutedEventArgs e)
        {
            TextBlock1.Text = string.Empty; 
            string fileName = "goods_data.json";
            string fullFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            try
            {
                if (File.Exists(fullFilePath))
                {
                    string jsonContent = File.ReadAllText(fullFilePath);
                    if (string.IsNullOrWhiteSpace(jsonContent))
                    {
                        TextBlock1.Text = "Файл goods_data.json пуст или не содержит данных.";
                        return;
                    }

                    List<GoodItem> goodsList = JsonConvert.DeserializeObject<List<GoodItem>>(jsonContent);

                    if (goodsList == null || !goodsList.Any())
                    {
                        TextBlock1.Text = "В файле goods_data.json не найдено товаров.";
                        return;
                    }

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("--- Информация о товарах ---");
                    foreach (var good in goodsList)
                    {
                        sb.AppendLine($"Артикул: {good.Article}");
                        sb.AppendLine($"Название: {good.Name}");
                        sb.AppendLine($"Категория: {good.Category}");
                        sb.AppendLine($"Ед.изм.: {good.UnitOfMeasurement}");
                        sb.AppendLine($"Кол-во на складе: {good.Count}");
                        sb.AppendLine("---------------------------");
                    }
                    TextBlock1.Text = sb.ToString();
                }
                else
                {
                    TextBlock1.Text = $"Файл '{fileName}' не найден по пути: {fullFilePath}";
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
        private void GetInfo2_Button(object sender, RoutedEventArgs e)
        {
            TextBlock1.Text = string.Empty; 
            string fileName = "operations_data.json";
            string fullFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            try
            {
                if (File.Exists(fullFilePath))
                {
                    string jsonContent = File.ReadAllText(fullFilePath);
                    if (string.IsNullOrWhiteSpace(jsonContent))
                    {
                        TextBlock1.Text = "Файл operations_data.json пуст или не содержит данных.";
                        return;
                    }

                    List<ItemsMovements> movementsList = JsonConvert.DeserializeObject<List<ItemsMovements>>(jsonContent);

                    if (movementsList == null || !movementsList.Any())
                    {
                        TextBlock1.Text = "В файле operations_data.json не найдено операций.";
                        return;
                    }

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("--- Информация о движениях товаров ---");
                    foreach (var movement in movementsList)
                    {
                        sb.AppendLine($"Номер операции: {movement.OperationNumber}");
                        sb.AppendLine($"Артикул: {movement.Article}");
                        sb.AppendLine($"Тип операции: {movement.OperationType}");
                        sb.AppendLine($"Кол-во: {movement.Count}");
                        sb.AppendLine($"Причина: {(string.IsNullOrEmpty(movement.Reason) ? "Не указана" : movement.Reason)}");
                        sb.AppendLine("------------------------------------");
                    }
                    TextBlock1.Text = sb.ToString();
                }
                else
                {
                    TextBlock1.Text = $"Файл '{fileName}' не найден по пути: {fullFilePath}";
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
