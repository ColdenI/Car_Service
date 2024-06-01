using MySql.Data.MySqlClient;
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
using System.Windows.Shapes;
using TableData;

namespace Car_Service.scr.admin
{
    /// <summary>
    /// Логика взаимодействия для AdminOrderWindow.xaml
    /// </summary>
    public partial class AdminOrderWindow : Window
    {
        public AdminOrderWindow()
        {
            InitializeComponent();
            TableDraw();
        }

        private class TableDrawData
        {
            public string order_status { get; set; }
            public string order_description { get; set; }

            public string lname { get; set; }
            public string fname { get; set; }
            public string mname { get; set; }
            public string number_phone { get; set; }

            public string car_stamp { get; set; }
            public string car_model { get; set; }
            public string car_year_release { get; set; }
            public string car_mileage { get; set; }
            public string car_license_plate_number { get; set; }
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
                            Order order = Order.GetOrderById(reader.GetInt32(0));
                            Client client = Client.GetClientById(order.client_id);
                            Car car = Car.GetCarById(order.car_id);

                            d.order_description = order.description;
                            d.order_status = order.status;
                            d.lname = client.lname;
                            d.fname = client.fname;
                            d.mname = client.mname;
                            d.number_phone = client.number_phone;
                            d.car_mileage = car.mileage.ToString() + " km";
                            d.car_license_plate_number = car.license_plate_number;
                            d.car_model = car.model;
                            d.car_stamp = car.stamp;
                            d.car_year_release = car.year_release.ToString();

                            data.Add(d);
                        }
                    }
                }
            }
            dgv.ItemsSource = data;
        }
    }
}
