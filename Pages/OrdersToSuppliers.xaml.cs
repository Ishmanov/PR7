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
    /// Логика взаимодействия для OrdersToSuppliers.xaml
    /// </summary>
    public partial class OrdersToSuppliers : Page
    {
        public OrdersToSuppliers()
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
                TextBox2.Text = "Название";
                TextBox2.Foreground = Brushes.Gray;
                _isPlaceholder2 = true;
            }
        }
        private bool _isPlaceholder3 = true;
        private void GotFocus3(object sender, RoutedEventArgs e)
        {
            if (_isPlaceholder3)
            {
                TextBox3.Text = "";
                TextBox3.Foreground = Brushes.Black;
                _isPlaceholder3 = false;
            }
        }
        private void LostFocus3(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBox3.Text))
            {
                TextBox3.Text = "Артикул";
                TextBox3.Foreground = Brushes.Gray;
                _isPlaceholder3 = true;
            }
        }
        private bool _isPlaceholder4 = true;
        private void GotFocus4(object sender, RoutedEventArgs e)
        {
            if (_isPlaceholder4)
            {
                TextBox4.Text = "";
                TextBox4.Foreground = Brushes.Black;
                _isPlaceholder4 = false;
            }
        }
        private void LostFocus4(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBox4.Text))
            {
                TextBox4.Text = "Номер заказа";
                TextBox4.Foreground = Brushes.Gray;
                _isPlaceholder4 = true;
            }
        }
        //
        private void Send_Button(object sender, RoutedEventArgs e)
        {
            string articleInput = TextBox1.Text;
            string quantityInput = TextBox2.Text;
            string priceInput = TextBox3.Text;
            DateTime? deliveryDate = DatePicker1.SelectedDate;

            if (string.IsNullOrWhiteSpace(articleInput) || articleInput == "Артикул" ||
                string.IsNullOrWhiteSpace(quantityInput) || quantityInput == "Количество" ||
                string.IsNullOrWhiteSpace(priceInput) || priceInput == "Цена" ||
                deliveryDate == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля (Артикул, Количество, Цена, Дата доставки).", "Неполные данные", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!double.TryParse(quantityInput, out double quantity) || quantity <= 0)
            {
                MessageBox.Show("Пожалуйста, введите корректное положительное число для количества.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!double.TryParse(priceInput, out double price) || price <= 0)
            {
                MessageBox.Show("Пожалуйста, введите корректное положительное число для цены.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (deliveryDate.Value.Date <= DateTime.Today.AddDays(1).Date)
            {
                MessageBox.Show("Дата доставки должна быть не раньше завтрашнего дня.", "Неверная дата", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                        foundGood = goodsList.FirstOrDefault(g => g.Article == articleInput);
                    }
                }
                else
                {
                    MessageBox.Show($"Файл с данными о товарах '{goodsFileName}' не найден. Невозможно выполнить операцию.", "Ошибка Файла", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (foundGood == null)
                {
                    MessageBox.Show($"Товар с артикулом '{articleInput}' не найден в файле '{goodsFileName}'. Пожалуйста, убедитесь, что товар существует.", "Товар не найден", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (foundGood.Count < quantity)
                {
                    MessageBox.Show($"Недостаточно товара '{foundGood.Name}' на складе (Артикул: {foundGood.Article}) для оформления заказа. Доступно: {foundGood.Count} {foundGood.UnitOfMeasurement}", "Недостаточно товара", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string ordersFileName = "supplier_orders.json";
                string ordersFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ordersFileName);
                List<SupplierOrder> ordersList = new List<SupplierOrder>();
                int nextOrderNumber = 1;

                if (File.Exists(ordersFilePath))
                {
                    string ordersJsonContent = File.ReadAllText(ordersFilePath);
                    if (!string.IsNullOrWhiteSpace(ordersJsonContent))
                    {
                        ordersList = JsonConvert.DeserializeObject<List<SupplierOrder>>(ordersJsonContent) ?? new List<SupplierOrder>();
                        if (ordersList.Any())
                        {
                            nextOrderNumber = ordersList.Max(o => o.OrderNumber) + 1;
                        }
                    }
                }

                var newOrder = new SupplierOrder
                {
                    OrderNumber = nextOrderNumber,
                    Article = articleInput,
                    Name = foundGood.Name,
                    Quantity = quantity,
                    Price = price,
                    UnitOfMeasurement = foundGood.UnitOfMeasurement,
                    DeliveryDate = deliveryDate.Value,
                    OrderDate = DateTime.Now
                };
                ordersList.Add(newOrder);
                string updatedOrdersJson = JsonConvert.SerializeObject(ordersList, Formatting.Indented);
                File.WriteAllText(ordersFilePath, updatedOrdersJson);

                foundGood.Count -= quantity;
                string updatedGoodsJson = JsonConvert.SerializeObject(goodsList, Formatting.Indented);
                File.WriteAllText(goodsFilePath, updatedGoodsJson);

                string operationsFileName = "operations_data.json";
                string operationsFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, operationsFileName);
                List<ItemsMovements> movementsList = new List<ItemsMovements>();
                int nextOperationNumber = 1;

                if (File.Exists(operationsFilePath))
                {
                    string operationsJsonContent = File.ReadAllText(operationsFilePath);
                    if (!string.IsNullOrWhiteSpace(operationsJsonContent))
                    {
                        movementsList = JsonConvert.DeserializeObject<List<ItemsMovements>>(operationsJsonContent) ?? new List<ItemsMovements>();
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
                    OperationType = "Расход", 
                    Count = quantity,
                    Reason = "Заказ" 
                };
                movementsList.Add(newMovement);
                string updatedMovementsJson = JsonConvert.SerializeObject(movementsList, Formatting.Indented);
                File.WriteAllText(operationsFilePath, updatedMovementsJson);

                string successMessage = $"Номер заказа - {newOrder.OrderNumber}\n" +
                                        $"Заказ успешно оформлен:\n" +
                                        $"Артикул - {newOrder.Article}\n" +
                                        $"Название - {newOrder.Name}\n" +
                                        $"Количество в ед.изм. - {newOrder.Quantity} {newOrder.UnitOfMeasurement}\n" +
                                        $"Дата доставки - {newOrder.DeliveryDate.ToShortDateString()}\n" +
                                        $"Количество на складе после операции: {foundGood.Count} {foundGood.UnitOfMeasurement}";
                MessageBox.Show(successMessage, "Заказ оформлен", MessageBoxButton.OK, MessageBoxImage.Information);

                TextBox1.Text = "Артикул";
                TextBox1.Foreground = Brushes.Gray;
                _isPlaceholder1 = true;
                TextBox2.Text = "Количество";
                TextBox2.Foreground = Brushes.Gray;
                _isPlaceholder2 = true;
                TextBox3.Text = "Цена";
                TextBox3.Foreground = Brushes.Gray;
                _isPlaceholder3 = true;
                DatePicker1.SelectedDate = null;
            }
            catch (JsonSerializationException ex)
            {
                MessageBox.Show($"Ошибка при чтении или обработке JSON файла: {ex.Message}\nПроверьте формат файлов данных.", "Ошибка JSON", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Ошибка ввода/вывода при работе с файлами: {ex.Message}", "Ошибка Файла", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла непредвиденная ошибка: {ex.Message}", "Общая Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Send2_Button(object sender, RoutedEventArgs e)
        {
            string orderNumberInput = TextBox4.Text;
            DateTime? newDeliveryDate = DatePicker2.SelectedDate;

            if (string.IsNullOrWhiteSpace(orderNumberInput) || orderNumberInput == "Номер заказа" ||
                newDeliveryDate == null)
            {
                MessageBox.Show("Пожалуйста, введите номер заказа и выберите новую дату доставки.", "Неполные данные", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!int.TryParse(orderNumberInput, out int orderNumber) || orderNumber <= 0)
            {
                MessageBox.Show("Пожалуйста, введите корректный положительный номер заказа.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (newDeliveryDate.Value.Date <= DateTime.Today.AddDays(1).Date)
            {
                MessageBox.Show("Новая дата доставки должна быть не раньше завтрашнего дня.", "Неверная дата", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string ordersFileName = "supplier_orders.json";
            string ordersFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ordersFileName);
            List<SupplierOrder> ordersList = new List<SupplierOrder>();
            SupplierOrder orderToUpdate = null;

            try
            {
                if (File.Exists(ordersFilePath))
                {
                    string jsonContent = File.ReadAllText(ordersFilePath);
                    if (!string.IsNullOrWhiteSpace(jsonContent))
                    {
                        ordersList = JsonConvert.DeserializeObject<List<SupplierOrder>>(jsonContent) ?? new List<SupplierOrder>();
                        orderToUpdate = ordersList.FirstOrDefault(o => o.OrderNumber == orderNumber);
                    }
                }
                else
                {
                    MessageBox.Show($"Файл с данными о заказах '{ordersFileName}' не найден. Невозможно изменить дату.", "Ошибка Файла", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (orderToUpdate == null)
                {
                    MessageBox.Show($"Заказ с номером '{orderNumber}' не найден в файле '{ordersFileName}'.", "Заказ не найден", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                orderToUpdate.DeliveryDate = newDeliveryDate.Value;

                string updatedOrdersJson = JsonConvert.SerializeObject(ordersList, Formatting.Indented);
                File.WriteAllText(ordersFilePath, updatedOrdersJson);

                string successMessage = $"Дата доставки заказа номер {orderNumber} успешно изменена.\n" +
                                        $"Новая дата доставки - {newDeliveryDate.Value.ToShortDateString()}.";
                MessageBox.Show(successMessage, "Дата доставки изменена", MessageBoxButton.OK, MessageBoxImage.Information);

                TextBox4.Text = "Номер заказа";
                TextBox4.Foreground = Brushes.Gray;
                _isPlaceholder4 = true;
                DatePicker2.SelectedDate = null;
            }
            catch (JsonSerializationException ex)
            {
                MessageBox.Show($"Ошибка при чтении или обработке JSON файла '{ordersFileName}': {ex.Message}\nПроверьте формат файла.", "Ошибка JSON", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Ошибка ввода/вывода при работе с файлом '{ordersFileName}': {ex.Message}", "Ошибка Файла", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла непредвиденная ошибка: {ex.Message}", "Общая Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void GetInfo_Button(object sender, RoutedEventArgs e)
        {
            TextBlock1.Text = string.Empty; 
            string ordersFileName = "supplier_orders.json";
            string ordersFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ordersFileName);

            try
            {
                if (File.Exists(ordersFilePath))
                {
                    string jsonContent = File.ReadAllText(ordersFilePath);
                    if (string.IsNullOrWhiteSpace(jsonContent))
                    {
                        TextBlock1.Text = "Файл supplier_orders.json пуст или не содержит данных.";
                        return;
                    }

                    List<SupplierOrder> ordersList = JsonConvert.DeserializeObject<List<SupplierOrder>>(jsonContent);

                    if (ordersList == null || !ordersList.Any())
                    {
                        TextBlock1.Text = "В файле supplier_orders.json не найдено заказов.";
                        return;
                    }

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("--- Информация о заказах поставщикам ---");
                    foreach (var order in ordersList.OrderBy(o => o.OrderNumber)) 
                    {
                        sb.AppendLine($"Номер заказа: {order.OrderNumber}");
                        sb.AppendLine($"Артикул: {order.Article}");
                        sb.AppendLine($"Название: {order.Name}");
                        sb.AppendLine($"Количество: {order.Quantity} {order.UnitOfMeasurement}");
                        sb.AppendLine($"Цена: {order.Price:C2}"); 
                        sb.AppendLine($"Дата оформления: {order.OrderDate.ToShortDateString()}");
                        sb.AppendLine($"Дата доставки: {order.DeliveryDate.ToShortDateString()}");
                        sb.AppendLine("------------------------------------");
                    }
                    TextBlock1.Text = sb.ToString();
                }
                else
                {
                    TextBlock1.Text = $"Файл '{ordersFileName}' не найден по пути: {ordersFilePath}";
                }
            }
            catch (JsonSerializationException ex)
            {
                TextBlock1.Text = $"Ошибка при чтении или обработке JSON файла '{ordersFileName}': {ex.Message}\nПроверьте формат файла.";
                MessageBox.Show($"Ошибка JSON: {ex.Message}\nПроверьте формат файла '{ordersFileName}'.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IOException ex)
            {
                TextBlock1.Text = $"Ошибка ввода/вывода при работе с файлом '{ordersFileName}': {ex.Message}";
                MessageBox.Show($"Ошибка Файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                TextBlock1.Text = $"Произошла непредвиденная ошибка при получении информации из файла '{ordersFileName}': {ex.Message}";
                MessageBox.Show($"Общая Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
