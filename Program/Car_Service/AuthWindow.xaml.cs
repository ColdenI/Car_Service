using Car_Service.scr.admin;
using Car_Service.scr.manager;
using Car_Service.scr.personnel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Car_Service
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {

        public static MySqlConnectionStringBuilder SQLBuilder;
        public static User ThisUser;

        public AuthWindow()
        {
            InitializeComponent();
            SQLBuilder = new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                Database = "car_service",
                UserID = "root",
                Password = "password",
                SslMode = MySqlSslMode.Required,
            };

        }

        private void apply_button_Click(object sender, RoutedEventArgs e)
        {

            using (var conn = new MySqlConnection(AuthWindow.SQLBuilder.ConnectionString))
            {
                try
                {
                    conn.Open();
                }
                catch
                {
                    MessageBox.Show("MySQL server disconnect");
                }
                using (var query = conn.CreateCommand())
                {
                    query.CommandTimeout = 30;
                    query.CommandText = "SELECT COUNT(*) FROM `user-auth` WHERE (password=@password AND login=@login);";
                    query.Parameters.AddWithValue("@password", textBox_password.Password);
                    query.Parameters.AddWithValue("@login", textBox_login.Text);

                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.GetInt32(0) == 0)
                            {
                                MessageBox.Show("Логин или пароль неверны!");
                                return;
                            }
                        }
                    }
                    query.CommandText = "SELECT `id` FROM `user` WHERE `id`=(SELECT `user_id` FROM `user-auth` WHERE (`password` = @password AND `login`=@login));";

                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = User.GetUserById(reader.GetInt32(0));
                            ThisUser = user;

                            if (user.end_work_date.Year != 1900)
                            {
                                MessageBox.Show("Вы уволены...");
                                return;
                            }

                            authWindow.Visibility = Visibility.Hidden;
                            textBox_login.Text = string.Empty;
                            textBox_password.Password = string.Empty;

                            switch (Post.GetPostById(user.post_id).tech_name)
                            {
                                case 1: new AdminPanelWindow().ShowDialog(); break;
                                case 2: new ManagerPanelWindow().ShowDialog(); break;
                                case 3: new MainPersonnelWindow().ShowDialog(); break;
                                case 4: new MainPersonnelWindow().ShowDialog(); break;
                            }
                            this.Close();
                            return;
                        }
                    }
                }
            }
        }
    }
}
