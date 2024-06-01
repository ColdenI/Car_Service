using MySql.Data.MySqlClient;
using System;
using System.Windows;
using TableData;

namespace Car_Service.scr.manager
{
    /// <summary>
    /// Логика взаимодействия для ManagerAddOrderWindow.xaml
    /// </summary>
    public partial class ManagerAddOrderWindow : Window
    {
        public ManagerAddOrderWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (
                !int.TryParse(textBox_mileage.Text.Trim(), out int _a) ||
                !int.TryParse(textBox_yearRelease.Text.Trim(), out int _b)
                )
            {
                MessageBox.Show("Не верный формат года или пробега.");
                return;
            }
            if (
                string.IsNullOrEmpty(textBox_lname.Text) ||
                string.IsNullOrEmpty(textBox_fname.Text) ||
                string.IsNullOrEmpty(textBox_mname.Text) ||
                string.IsNullOrEmpty(textBox_license_plate_number.Text) ||
                string.IsNullOrEmpty(textBox_numberPhone.Text) ||
                string.IsNullOrEmpty(textBox_stamp.Text) ||
                string.IsNullOrEmpty(textBox_model.Text) ||
                string.IsNullOrEmpty(textBox_yearRelease.Text) ||
                string.IsNullOrEmpty(textBox_mileage.Text)
                )
            {
                MessageBox.Show("Заполните все поля.");
                return;
            }

            using (var conn = new MySqlConnection(AuthWindow.SQLBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch { MessageBox.Show("MySQL server disconnect"); }
                using (var query = conn.CreateCommand())
                {
                    query.CommandTimeout = 30;
                    query.CommandText = "INSERT INTO `client` (`fname`, `lname`, `mname`, `number_phone`) VALUES (@fname, @lname, @mname, @number_phone);";
                    query.Parameters.AddWithValue("@lname", textBox_lname.Text);
                    query.Parameters.AddWithValue("@fname", textBox_fname.Text);
                    query.Parameters.AddWithValue("@mname", textBox_mname.Text);
                    query.Parameters.AddWithValue("@number_phone", textBox_numberPhone.Text);
                    query.ExecuteNonQuery();

                    query.CommandText = "INSERT INTO `car` (`stamp`, `model`, `year_release`, `mileage`, `license_plate_number`) VALUES (@stamp, @model, @year_release, @mileage, @license_plate_number);";
                    query.Parameters.AddWithValue("@stamp", textBox_stamp.Text);
                    query.Parameters.AddWithValue("@model", textBox_model.Text);
                    query.Parameters.AddWithValue("@year_release", textBox_yearRelease.Text.Trim());
                    query.Parameters.AddWithValue("@mileage", textBox_mileage.Text.Trim());
                    query.Parameters.AddWithValue("@license_plate_number", textBox_license_plate_number.Text);
                    query.ExecuteNonQuery();

                    query.CommandText = "INSERT INTO `order` (`status`, `description`, `client_id`, `car_id`) VALUES ('Создан', @description, @client_id, @car_id);";
                    query.Parameters.AddWithValue("@description", textBox_description.Text);
                    query.Parameters.AddWithValue("@client_id", Client.GetClientByNumberPhone(textBox_numberPhone.Text).id);
                    query.Parameters.AddWithValue("@car_id", Car.GetCarByLicense_plate_number(textBox_license_plate_number.Text).id);
                    query.ExecuteNonQuery();

                }
            }
            MessageBox.Show("Успешно!");
            this.Close();
        }
    }
}
