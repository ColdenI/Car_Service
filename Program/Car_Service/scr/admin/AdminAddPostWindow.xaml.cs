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

namespace Car_Service.scr.admin
{
    /// <summary>
    /// Логика взаимодействия для AdminAddPostWindow.xaml
    /// </summary>
    public partial class AdminAddPostWindow : Window
    {
        public AdminAddPostWindow()
        {
            InitializeComponent();
        }

        private void button_apply_Click(object sender, RoutedEventArgs e)
        {
            if(
                string.IsNullOrEmpty(textBox_des.Text) ||
                string.IsNullOrEmpty(textBox_title.Text) ||
                !int.TryParse(textBox_wages.Text.Trim(), out int _a) ||
                !int.TryParse(textBox_tech_name.Text.Trim(), out int _b)
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
                    query.CommandText = "INSERT INTO `post` (`title`, `description`, `wages`, `tech_name`) VALUES (@title, @description, @wages, @tech_name);";
                    query.Parameters.AddWithValue("@title", textBox_title.Text);
                    query.Parameters.AddWithValue("@description", textBox_des.Text);
                    query.Parameters.AddWithValue("@wages", textBox_wages.Text.Trim());
                    query.Parameters.AddWithValue("@tech_name", textBox_tech_name.Text.Trim());
                    query.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Успешно!");
            this.Close();
        }
    }
}
