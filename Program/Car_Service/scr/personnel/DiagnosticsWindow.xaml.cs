using Car_Service.scr.manager;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TableData;

namespace Car_Service.scr.personnel
{
    /// <summary>
    /// Логика взаимодействия для DiagnosticsWindow.xaml
    /// </summary>
    public partial class DiagnosticsWindow : Window
    {
        List<Malfunction> AllMalfunctions = new List<Malfunction>();
        List<Malfunction> Malfunctions = new List<Malfunction>();

        private int orderID;

        public DiagnosticsWindow(int orderId)
        {
            InitializeComponent();
            orderID = orderId;
            LoadMalfunction();
            listBox_AllMalfunction.MouseDoubleClick += ListBox_AllMalfunction_MouseDoubleClick;
            listBox_malfunction.MouseDoubleClick += ListBox_malfunction_MouseDoubleClick;
        }

        private void ListBox_malfunction_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (listBox_malfunction.SelectedIndex == -1) return;
            Malfunctions.RemoveAt(listBox_malfunction.SelectedIndex);
            listBox_malfunction.Items.RemoveAt(listBox_malfunction.SelectedIndex);
        }

        private void ListBox_AllMalfunction_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (listBox_AllMalfunction.SelectedIndex == -1) return;
            Malfunction mal = Malfunction.GetMalfunctionById(int.Parse(listBox_AllMalfunction.SelectedItem.ToString().Split(' ')[0]));
            if(listBox_malfunction.Items.Contains($"{mal.id} {mal.title}")) return;
            Malfunctions.Add(mal);
            listBox_malfunction.Items.Add($"{mal.id} {mal.title}");
        }    

        private void LoadMalfunction(bool isLoadInCar = true)
        {
            AllMalfunctions.Clear();
            listBox_AllMalfunction.Items.Clear();
            using (var conn = new MySqlConnection(AuthWindow.SQLBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch { MessageBox.Show("MySQL server disconnect"); }

                using (var query = conn.CreateCommand())
                {
                    query.CommandTimeout = 30;
                    query.CommandText = "SELECT `id` FROM `malfunction`;";

                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Malfunction malfunction = Malfunction.GetMalfunctionById(reader.GetInt32(0));
                            if (User.GetUserById(malfunction.user_id).end_work_date.Year == 1900)
                                AllMalfunctions.Add(malfunction);
                        }
                    }
                }
            }
            UpdateListMalfunction();

            if (!isLoadInCar) return;
            Malfunctions.Clear();
            listBox_malfunction.Items.Clear();
            foreach (Malfunction i in Malfunction.GetMalfunctionsByOrderId(orderID))
            {
                Malfunctions.Add(i);
                listBox_malfunction.Items.Add($"{i.id} {i.title}");
            }
        }

        private void UpdateListMalfunction()
        {
            listBox_AllMalfunction.Items.Clear();

            foreach (Malfunction m in AllMalfunctions)
            {
                if (
                    m.title.ToLower().Contains(textBox_filter.Text.ToLower()) ||
                    m.description.ToLower().Contains(textBox_filter.Text.ToLower()) ||
                    string.IsNullOrEmpty(textBox_filter.Text)
                    ) listBox_AllMalfunction.Items.Add($"{m.id} {m.title}");
            }
        }

        private void textBox_filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateListMalfunction();
        }

        private void button_addNewMalfunction1_Click(object sender, RoutedEventArgs e)
        {
            new AddNewMalfunctionWindow().ShowDialog();
            LoadMalfunction(false);
        }

        private void listBox_AllMalfunction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox_AllMalfunction.SelectedIndex == -1)
            {
                grBoxAbout.IsEnabled = false;
                return;
            }
            grBoxAbout.IsEnabled = true;
            Malfunction mal = Malfunction.GetMalfunctionById(int.Parse(listBox_AllMalfunction.SelectedItem.ToString().Split(' ')[0]));
            label_cost.Content = mal.cost;
            label_des.Text = mal.description;
            label_title.Content = mal.title;
            User u = User.GetUserById(mal.user_id);
            lable_user.Content = $"{u.lname} {u.fname} {u.mname} ";
        }

        private void button_autopartInfo_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_AllMalfunction.SelectedIndex == -1) return;
            Malfunction mal = Malfunction.GetMalfunctionById(int.Parse(listBox_AllMalfunction.SelectedItem.ToString().Split(' ')[0]));
            new AutopartInfoWindow(mal.autopart_id).ShowDialog();
        }

        private void button_apply_Click(object sender, RoutedEventArgs e)
        {
            using (var conn = new MySqlConnection(AuthWindow.SQLBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch { MessageBox.Show("MySQL server disconnect"); }

                using (var query = conn.CreateCommand())
                {
                    query.CommandTimeout = 30;
                    query.Parameters.AddWithValue("@order_id", orderID);

                    query.CommandText = "DELETE FROM `malfunction_order` WHERE `order_id` = @order_id;";
                    query.ExecuteNonQuery();

                    foreach (Malfunction i in Malfunctions) {
                        query.CommandText = $"INSERT INTO `malfunction_order` (`malfunction_id`, `order_id`, `status`) VALUES ('{i.id}', @order_id, 'В процессе');";
                        query.ExecuteNonQuery();
                    }
                    query.CommandText = "UPDATE `order` SET `status` = 'Диагностика завершена' WHERE `id` = @id;";
                    query.Parameters.AddWithValue("@id", orderID);
                    query.ExecuteNonQuery();

                }
            }
            MessageBox.Show("Успешно!");
            this.Close();
        }
    }
}
