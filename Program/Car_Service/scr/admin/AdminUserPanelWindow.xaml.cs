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
using System.Windows.Resources;
using System.Windows.Shapes;
using TableData;

namespace Car_Service.scr.admin
{
    /// <summary>
    /// Логика взаимодействия для AdminUserPanelWindow.xaml
    /// </summary>
    public partial class AdminUserPanelWindow : Window
    {
        public AdminUserPanelWindow()
        {
            InitializeComponent();
            TableDraw();
            UIUpdate();
        }

        private void button_addUser_Click(object sender, RoutedEventArgs e)
        {
            new AdminAddEditUserWindow().ShowDialog();
            TableDraw();

        }

        private void UIUpdate()
        {
            if(dgv.SelectedIndex == -1)
            {
                button_dismiss.IsEnabled = false;
                button_editUser.IsEnabled = false;
                button_reinstate.IsEnabled = false;
                return;
            }
            else
            {
                button_editUser.IsEnabled = true;
            }

            if(((TableDrawData)dgv.SelectedItem).e_work_date == "Не уволен")
            {
                button_dismiss.IsEnabled = true;
                button_reinstate.IsEnabled = false;
            }
            else
            {
                button_dismiss.IsEnabled = false;
                button_reinstate.IsEnabled = true;
            }
        }

        private class TableDrawData
        {
            public int Id { get; set; }
            public string lname { get; set; }
            public string fname { get; set; }
            public string mname { get; set; }
            public string birth_date { get; set; }
            public string education { get; set; }
            public string post { get; set; }
            public string s_work_date { get; set; }
            public string e_work_date { get; set; }
            public string login { get; set; }
            public string password { get; set; }
            public string wages { get; set; }
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
                    query.CommandText = "SELECT `id` FROM `user`";

                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TableDrawData d = new TableDrawData();
                            User user = User.GetUserById(reader.GetInt32(0));
                            Post post = Post.GetPostById(user.post_id);

                            d.Id = user.id;
                            d.lname = user.lname;
                            d.fname = user.fname;
                            d.mname = user.mname;
                            d.education = user.education;
                            d.birth_date = user.date_birth.ToString("yyyy.MM.dd");
                            d.s_work_date = user.start_work_date.ToString("yyyy.MM.dd");
                            d.e_work_date = user.end_work_date.Year == 1900 ? "Не уволен" : user.end_work_date.ToString("yyyy.MM.dd");
                            d.wages = post.wages.ToString() + " руб.";
                            d.post = post.title;
                            d.login = user.login;
                            d.password = user.password;

                            data.Add(d);
                        }
                    }
                }
            }

            dgv.ItemsSource = data;
            UIUpdate();
        }

        private void button_update_Click(object sender, RoutedEventArgs e)
        {
            TableDraw();
        }

        private void dgv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UIUpdate();
        }

        private void button_editUser_Click(object sender, RoutedEventArgs e)
        {
            new AdminAddEditUserWindow(((TableDrawData)dgv.SelectedItem).Id).ShowDialog();
            TableDraw();
        }

        private void button_dismiss_Click(object sender, RoutedEventArgs e)
        {
            using (var conn = new MySqlConnection(AuthWindow.SQLBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch { MessageBox.Show("MySQL server disconnect"); }
                using (var query = conn.CreateCommand())
                {
                    query.CommandTimeout = 30;
                    query.CommandText = "UPDATE `user` SET `end_work_date` = now() WHERE `id` = @id;";
                    query.Parameters.AddWithValue("@id", ((TableDrawData)dgv.SelectedItem).Id);
                    query.ExecuteNonQuery();

                }
            }
            TableDraw();
        }

        private void button_reinstate_Click(object sender, RoutedEventArgs e)
        {
            using (var conn = new MySqlConnection(AuthWindow.SQLBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch { MessageBox.Show("MySQL server disconnect"); }
                using (var query = conn.CreateCommand())
                {
                    query.CommandTimeout = 30;
                    query.CommandText = "UPDATE `user` SET `end_work_date` = '1900-01-01' WHERE `id` = @id;";
                    query.Parameters.AddWithValue("@id", ((TableDrawData)dgv.SelectedItem).Id);
                    query.ExecuteNonQuery();

                }
            }
            TableDraw();
        }
    }
}
