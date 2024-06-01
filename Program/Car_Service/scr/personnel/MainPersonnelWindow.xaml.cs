using Car_Service.scr.manager;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TableData;

namespace Car_Service.scr.personnel
{
    /// <summary>
    /// Логика взаимодействия для MainPersonnelWindow.xaml
    /// </summary>
    public partial class MainPersonnelWindow : Window
    {
        public MainPersonnelWindow()
        {
            InitializeComponent();
            User us = AuthWindow.ThisUser;
            this.Title += $" - {Post.GetPostById(AuthWindow.ThisUser.post_id).title}: {us.lname} {us.fname} {us.mname}";

            if (Post.GetPostById(AuthWindow.ThisUser.post_id).tech_name != 4)
                button_diagnostics.Visibility = Visibility.Hidden;
            TableDraw();
            UIUpdate();
        }

        private class TableDrawData_
        {
            public int Id { get; set; }
            public int Id_order { get; set; }
            public int Id_autopart { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public string autopart { get; set; } // ссылка на отдельную инфу
            public string status { get; set; }
        }
        private class TableDrawData
        {
            public int Id { get; set; }
            public int Id_car { get; set; }
            public string status { get; set; }
            public string description { get; set; }
            public string car { get; set; } // ссылка на отдельную форму с описанием машины
        }
        private void TableDraw()
        {
            dgv_.ItemsSource = new List<TableDrawData_>();
            List<TableDrawData> data = new List<TableDrawData>();

            using (var conn = new MySqlConnection(AuthWindow.SQLBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch { MessageBox.Show("MySQL server disconnect"); }

                using (var query = conn.CreateCommand())
                {
                    query.CommandTimeout = 30;
                    query.CommandText = "SELECT `id` FROM `order` WHERE `status` <> 'Завершён' AND `status` <> 'Готово' AND `status` <> 'Отменён';";

                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TableDrawData d = new TableDrawData();
                            TableData.Order order = TableData.Order.GetOrderById(reader.GetInt32(0));
                            Car car = Car.GetCarById(order.car_id);

                            d.Id = order.id;
                            d.Id_car = order.car_id;
                            d.status = order.status;
                            d.description = order.description;
                            d.car = $"{car.stamp} {car.model} {car.year_release}";

                            bool _fl = false;
                            if (Post.GetPostById(AuthWindow.ThisUser.post_id).tech_name == 4) _fl = true;
                            foreach (Malfunction i in Malfunction.GetMalfunctionsByOrderId(order.id))
                            {
                                if (i.user_id == AuthWindow.ThisUser.id && order.status == "Диагностика завершена") _fl = true;
                            }
                            if (_fl)
                                data.Add(d);
                        }
                    }
                }
            }
            dgv.ItemsSource = data;
            //UIUpdate();
        }

        private void TableDraw_(int orderId)
        {
            List<TableDrawData_> data = new List<TableDrawData_>();

            foreach (Malfunction i in Malfunction.GetMalfunctionsByOrderId(orderId))
            {
                if (i.user_id == AuthWindow.ThisUser.id || Post.GetPostById(AuthWindow.ThisUser.id).tech_name == 4)
                {
                    TableDrawData_ d = new TableDrawData_();
                    d.Id = i.id;
                    d.Id_order = orderId;
                    d.Id_autopart = i.autopart_id;
                    d.autopart = Autopart.GetAutopartById(i.autopart_id).title;
                    d.description = i.description;
                    d.title = i.title;
                    d.status = Malfunction.GetStatusMalfunctionByOrderIdAndMalfunctionId(orderId, i.id);

                    data.Add(d);
                }
            }

            dgv_.ItemsSource = data;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (dgv.SelectedIndex == -1) return;
            /*
            if (Order.GetOrderByCarId(((TableDrawData)dgv.SelectedItem).Id).status == "Диагностика завершена") 
            {
                MessageBox.Show("Диагностика уже выполнена.");
                return; 
            }*/
            new DiagnosticsWindow(((TableDrawData)dgv.SelectedItem).Id).ShowDialog();
            TableDraw();
        }

        private void button_update_Click(object sender, RoutedEventArgs e)
        {
            TableDraw();
        }

        private void UIUpdate()
        {
            bool en1 = !(dgv.SelectedIndex == -1);
            bool en2 = !(dgv_.SelectedIndex == -1);
            buttonSetOrderStatus.IsEnabled = en1;
            button_carInfo.IsEnabled = en1;
            button_diagnostics.IsEnabled = en1;

            button_autopartInfo.IsEnabled = en2;
            button_setStatusMalfunction.IsEnabled = en2;
        }

        private void dgv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgv.SelectedIndex == -1) return;
            TableDraw_(((TableDrawData)dgv.SelectedItem).Id);
            UIUpdate();
        }

        private void dgv__SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UIUpdate();
        }

        private void button_carInfo_Click(object sender, RoutedEventArgs e)
        {
            if (dgv.SelectedIndex == -1) return;
            new CarInfoWindow(((TableDrawData)dgv.SelectedItem).Id_car).ShowDialog();
        }

        private void button_autopartInfo_Click(object sender, RoutedEventArgs e)
        {
            if (dgv_.SelectedIndex == -1) return;
            new AutopartInfoWindow(((TableDrawData_)dgv_.SelectedItem).Id_autopart).ShowDialog();
        }

        private void button_setStatusMalfunction_Click(object sender, RoutedEventArgs e)
        {
            if (dgv_.SelectedIndex == -1) return;
            TableDrawData_ tdd = (TableDrawData_)dgv_.SelectedItem;
            if (tdd.status == "Готово") return;
            Malfunction.SetStatusMalfunctionByOrderIdAndMalfunctionId(tdd.Id_order, tdd.Id, "Готово");

            using (var conn = new MySqlConnection(AuthWindow.SQLBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch { MessageBox.Show("MySQL server disconnect"); }

                using (var query = conn.CreateCommand())
                {
                    query.CommandTimeout = 30;
                    query.CommandText = "UPDATE `order` SET `status` = @status WHERE `id` = @order_id;";
                    query.Parameters.AddWithValue("@order_id", tdd.Id_order);
                    query.Parameters.AddWithValue("@status", "В процессе");
                    query.ExecuteNonQuery();
                }
            }

            TableDraw();
            TableDraw_(tdd.Id_order);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (dgv.SelectedIndex == -1) return;

            using (var conn = new MySqlConnection(AuthWindow.SQLBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch { MessageBox.Show("MySQL server disconnect"); }

                using (var query = conn.CreateCommand())
                {
                    query.CommandTimeout = 30;
                    query.CommandText = "SELECT COUNT(`id`) FROM `malfunction_order` WHERE `order_id` = @order_id AND `status` <> 'Готово';";
                    query.Parameters.AddWithValue("@order_id", ((TableDrawData)dgv.SelectedItem).Id);

                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.GetInt32(0) != 0)
                            {
                                MessageBox.Show("Вы не можете изменить статус на «Готово», поскольку не все неисправности устранены.");
                                return;
                            }
                        }
                    }
                }

                using (var query = conn.CreateCommand())
                {
                    query.CommandTimeout = 30;
                    query.CommandText = "UPDATE `order` SET `status` = @status WHERE `id` = @order_id;";
                    query.Parameters.AddWithValue("@order_id", ((TableDrawData)dgv.SelectedItem).Id);
                    query.Parameters.AddWithValue("@status", "Готово");
                    query.ExecuteNonQuery();
                }
            }

            TableDraw();
        }
    }
}
