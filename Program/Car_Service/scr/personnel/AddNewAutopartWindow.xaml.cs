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
    /// Логика взаимодействия для AddNewAutopartWindow.xaml
    /// </summary>
    public partial class AddNewAutopartWindow : Window
    {
        public AddNewAutopartWindow()
        {
            InitializeComponent();
            textBox_title.MaxLength = 45;
            textBox_des.MaxLength = 300;
        }

        private void button_apply_Click(object sender, RoutedEventArgs e)
        {
           if(
                string.IsNullOrEmpty(textBox_des.Text) ||                 
                string.IsNullOrEmpty(textBox_title.Text) ||                 
                !int.TryParse(textBox_cost.Text.Trim(), out int _a)
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
                    query.CommandText = "INSERT INTO `autopart` (`title`, `description`, `cost`) VALUES (@title, @description, @cost);";
                    query.Parameters.AddWithValue("@title", textBox_title.Text);
                    query.Parameters.AddWithValue("@description", textBox_des.Text);
                    query.Parameters.AddWithValue("@cost", textBox_cost.Text.Trim());
                    query.ExecuteNonQuery();
                 
                }
            }
            MessageBox.Show("Успешно!");
            this.Close();
        }
    }
}
