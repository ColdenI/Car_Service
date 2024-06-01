using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
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
using TableData;

namespace Car_Service.scr.manager
{
    /// <summary>
    /// Логика взаимодействия для ManagerPanelWindow.xaml
    /// </summary>
    public partial class ManagerPanelWindow : Window
    {
        public ManagerPanelWindow()
        {
            InitializeComponent();
            TableDraw();
        }

        private void button_addOrder_Click(object sender, RoutedEventArgs e)
        {
            new ManagerAddOrderWindow().ShowDialog();
        }

        private class TableDrawData
        {
            public int Id { get; set; }
            public int Id_car { get; set; }
            public int Id_client { get; set; }
            public string lname { get; set; }
            public string fname { get; set; }
            public string mname { get; set; }
            public string status { get; set; }
            public string number_phone { get; set; }
            public string description { get; set; }
            public string car { get; set; } // ссылка на отдельную форму с описанием машины
            public string cost { get; set; }
            public string malfunction { get; set; }// ссылка на отдельную форму с таблицей поломок

        }
        private void TableDraw()
        {
            List<TableDrawData> data = new List<TableDrawData>();
            
            using (var conn = new MySqlConnection(AuthWindow.SQLBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch { MessageBox.Show("MySQL server disconnect"); }

                using (var query = conn.CreateCommand())
                {
                    query.CommandTimeout = 30;
                    query.CommandText = "SELECT `id` FROM `order`";

                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TableDrawData d = new TableDrawData();
                            TableData.Order order = TableData.Order.GetOrderById(reader.GetInt32(0));
                            Client client = Client.GetClientById(order.client_id);
                            Car car = Car.GetCarById(order.car_id);

                            d.Id = order.id;
                            d.Id_client = client.id;
                            d.Id_car = car.id;
                            d.lname = client.lname;
                            d.fname = client.fname;
                            d.mname = client.mname;
                            d.status = order.status;
                            d.number_phone = client.number_phone;
                            d.description = order.description;
                            d.car = $"{car.stamp} {car.model} {car.year_release}";

                            d.cost = calculate_the_amount(order.id).ToString();

                            data.Add(d);
                        }
                    }
                }
            }
            dgv.ItemsSource = data;
            UIUpdate();
        }

        private int calculate_the_amount(int orderId)
        {
            int sum = 0;

            using (var conn = new MySqlConnection(AuthWindow.SQLBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch { MessageBox.Show("MySQL server disconnect"); }

                using (var query = conn.CreateCommand())
                {
                    query.CommandTimeout = 30;
                    query.CommandText = "SELECT * FROM `malfunction_order` WHERE `order_id` = @order_id";
                    query.Parameters.AddWithValue("@order_id", orderId);

                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Malfunction malfunction = Malfunction.GetMalfunctionById(reader.GetInt32(1));
                            Autopart autopart = Autopart.GetAutopartById(malfunction.autopart_id);

                            sum += autopart.cost;
                            sum += malfunction.cost;
                        }
                    }
                }
            }
            return sum;
        }

        private void button_update_Click(object sender, RoutedEventArgs e)
        {
            TableDraw();
        }

        private void button_carInfo_Click(object sender, RoutedEventArgs e)
        {
            if (dgv.SelectedIndex == -1) return;
            new CarInfoWindow(((TableDrawData)dgv.SelectedItem).Id_car).ShowDialog();
        }

        private void UIUpdate()
        {
            if (dgv.SelectedIndex == -1)
            {
                button_carInfo.IsEnabled = false;
                button_malfunctionInfo.IsEnabled = false;
                button_setStatus_End.IsEnabled = false;
            }
            else
            {
                button_carInfo.IsEnabled = true;
                button_malfunctionInfo.IsEnabled = true;
                button_setStatus_End.IsEnabled = true;
            }
        }

        private void button_malfunctionInfo_Click(object sender, RoutedEventArgs e)
        {
            if (dgv.SelectedIndex == -1) return;
            new MalfunctionInfoWindow(((TableDrawData)dgv.SelectedItem).Id).ShowDialog();
        }

        private void dgv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UIUpdate();
        }

        private void button_setStatus_End_Click(object sender, RoutedEventArgs e)
        {
            using (var conn = new MySqlConnection(AuthWindow.SQLBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch { MessageBox.Show("MySQL server disconnect"); }
                using (var query = conn.CreateCommand())
                {
                    query.CommandTimeout = 30;
                    query.CommandText = "UPDATE `order` SET `status` = 'Завершён' WHERE `id` = @id;";
                    query.Parameters.AddWithValue("@id", ((TableDrawData)dgv.SelectedItem).Id);
                    query.ExecuteNonQuery();
                }
            }
            TableDraw();
        }

        private void dgv_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            UIUpdate();
        }

        private void button_setStatus_cancel_Click(object sender, RoutedEventArgs e)
        {
            using (var conn = new MySqlConnection(AuthWindow.SQLBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch { MessageBox.Show("MySQL server disconnect"); }
                using (var query = conn.CreateCommand())
                {
                    query.CommandTimeout = 30;
                    query.CommandText = "UPDATE `order` SET `status` = 'Отменён' WHERE `id` = @id;";
                    query.Parameters.AddWithValue("@id", ((TableDrawData)dgv.SelectedItem).Id);
                    query.ExecuteNonQuery();
                }
            }
            TableDraw();
        }
    }
}
