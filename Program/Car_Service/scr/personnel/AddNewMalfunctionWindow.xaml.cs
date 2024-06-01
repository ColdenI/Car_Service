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

namespace Car_Service.scr.personnel
{
    /// <summary>
    /// Логика взаимодействия для AddNewMalfunctionWindow.xaml
    /// </summary>
    public partial class AddNewMalfunctionWindow : Window
    {
        List<Autopart> autoparts = new List<Autopart>();
        List<User> users = new List<User>();

        public AddNewMalfunctionWindow()
        {
            InitializeComponent();
            LoadDataInUI();
        }

        private void button_addAutopart_Click(object sender, RoutedEventArgs e)
        {
            new AddNewAutopartWindow().ShowDialog();
            LoadDataInUI();
        }

        private void LoadDataInUI()
        {
            comboBox_autopart.Items.Clear();
            comboBox_user.Items.Clear();
            autoparts.Clear();
            users.Clear();

            using (var conn = new MySqlConnection(AuthWindow.SQLBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch { MessageBox.Show("MySQL server disconnect"); }
                using (var query = conn.CreateCommand())
                {
                    query.CommandTimeout = 30;
                    query.CommandText = "SELECT `id` FROM `user` WHERE `end_work_date` = '1900-01-01';";

                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = User.GetUserById(reader.GetInt32(0));
                            if (Post.GetPostById(user.post_id).tech_name == 3 || Post.GetPostById(user.post_id).tech_name == 4)
                            {
                                users.Add(user);
                                comboBox_user.Items.Add($"{user.lname} {user.fname} {user.mname} - {Post.GetPostById(user.post_id).title}");
                            }
                        }
                    }
                }

                using (var query = conn.CreateCommand())
                {
                    query.CommandTimeout = 30;
                    query.CommandText = "SELECT `id` FROM `autopart`;";

                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Autopart autopart = Autopart.GetAutopartById(reader.GetInt32(0));
                            autoparts.Add(autopart);
                            comboBox_autopart.Items.Add(autopart.title);
                        }
                    }
                }
            }

            comboBox_autopart.SelectedIndex = -1;
            comboBox_user.SelectedIndex = -1;
        }

        private void button_apply_Click(object sender, RoutedEventArgs e)
        {
            if (
                string.IsNullOrEmpty(textBox_title.Text) ||
                string.IsNullOrEmpty(textBox_des.Text) ||
                !int.TryParse(textBox_cost.Text.Trim(), out int _a) ||
                comboBox_user.SelectedIndex == -1 ||
                comboBox_autopart.SelectedIndex == -1
                )
            {
                MessageBox.Show("Заполните все поля. Или неверный формат.");
                return;
            }

            using (var conn = new MySqlConnection(AuthWindow.SQLBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch { MessageBox.Show("MySQL server disconnect"); }
                using (var query = conn.CreateCommand())
                {
                    query.CommandTimeout = 30;
                    query.CommandText = "INSERT INTO `malfunction` (`title`, `description`, `cost`, `autopart_id`, `user_id`) VALUES (@title, @description, @cost, @autopart_id, @user_id);";
                    query.Parameters.AddWithValue("@title", textBox_title.Text);
                    query.Parameters.AddWithValue("@description", textBox_des.Text);
                    query.Parameters.AddWithValue("@cost", textBox_cost.Text.Trim());
                    query.Parameters.AddWithValue("@autopart_id", autoparts[comboBox_autopart.SelectedIndex].id);
                    query.Parameters.AddWithValue("@user_id", users[comboBox_user.SelectedIndex].id);
                    query.ExecuteNonQuery();

                }
            }
            MessageBox.Show("Успешно!");
            this.Close();
        }
    }
}
