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

namespace Car_Service.scr.manager
{
    /// <summary>
    /// Логика взаимодействия для MalfunctionInfoWindow.xaml
    /// </summary>
    public partial class MalfunctionInfoWindow : Window
    {
        private int orderID = -1;

        public MalfunctionInfoWindow(int orderID)
        {
            InitializeComponent();
            this.orderID = orderID;
            TableDraw();
        }

        private class TableDrawData
        {
            public int Id { get; set; }
            public int Id_autopart { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public string autopart { get; set; } // ссылка на форму где можно посмотреть инфу о запчасти
            public string cost_work { get; set; }
            public string user_lfmname { get; set; }
            public string status { get; set; }

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
                    query.CommandText = "SELECT * FROM `malfunction_order` WHERE `order_id` = @order_id";
                    query.Parameters.AddWithValue("@order_id", orderID);


                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TableDrawData d = new TableDrawData();
                            Order order = Order.GetOrderById(reader.GetInt32(2));
                            Malfunction malfunction = Malfunction.GetMalfunctionById(reader.GetInt32(1));
                            User user = User.GetUserById(malfunction.user_id);
                            Autopart autopart = Autopart.GetAutopartById(malfunction.autopart_id);

                            d.Id = malfunction.id;
                            d.Id_autopart = autopart.id;
                            d.title = malfunction.title;
                            d.description = malfunction.description;
                            d.autopart = autopart.title;
                            d.cost_work = malfunction.cost.ToString();
                            d.user_lfmname = $"{user.lname} {user.fname} {user.mname}";
                            d.status = Malfunction.GetStatusMalfunctionByOrderIdAndMalfunctionId(orderID, malfunction.id);

                            data.Add(d);
                        }
                    }
                }
            }
            dgv.ItemsSource = data;
            UIUpdate();
        }

        private void UIUpdate()
        {
            if (dgv.SelectedIndex == -1)
            {
                button_autopartInfo.IsEnabled = false;
            }
            else
            {
                button_autopartInfo.IsEnabled = true;
            }
        }

        private void dgv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UIUpdate();
        }

        private void button_autopartInfo_Click(object sender, RoutedEventArgs e)
        {
            new AutopartInfoWindow(((TableDrawData)dgv.SelectedItem).Id_autopart).ShowDialog();
        }
    }
}
