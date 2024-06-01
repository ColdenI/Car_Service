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
    /// Логика взаимодействия для AdminAddEditUserWindow.xaml
    /// </summary>
    public partial class AdminAddEditUserWindow : Window
    {
        private bool isEdit = false;
        private int userId = -1;

        public AdminAddEditUserWindow()
        {
            InitializeComponent();
            LoadPosts();
            datePicker_birthDate.DisplayDate = DateTime.Now;
            this.Title = "Добавить";
        }

        public AdminAddEditUserWindow(int user_id)
        {
            InitializeComponent();
            LoadPosts();
            datePicker_birthDate.DisplayDate = DateTime.Now;
            this.Title = "Изменить";
            isEdit = true;
            userId = user_id;
            User user = User.GetUserById(user_id);

            textBox_fname.Text = user.fname;
            textBox_lname.Text = user.lname;
            textBox_mname.Text = user.mname;
            textBox_login.Text = user.login;
            textBox_password.Text = user.password;
            textBox_educ.Text = user.education;
            comboBox_post.SelectedIndex = user.post_id - 1;
            datePicker_birthDate.SelectedDate = user.date_birth;
        }

        private void LoadPosts()
        {
            comboBox_post.Items.Clear();
            Post.LoadPosts();
            foreach(Post i in Post.Posts)
                comboBox_post.Items.Add(i.title);
        }

        private void button_apply_Click(object sender, RoutedEventArgs e)
        {
            #region Check Rules
            if (
                string.IsNullOrEmpty(textBox_fname.Text) ||
                string.IsNullOrEmpty(textBox_lname.Text) ||
                string.IsNullOrEmpty(textBox_login.Text) ||
                string.IsNullOrEmpty(textBox_password.Text) ||
                string.IsNullOrEmpty(textBox_educ.Text) ||
                comboBox_post.SelectedIndex == -1 ||
                textBox_login.Text.Length < 8 ||
                textBox_password.Text.Length < 8 
                )
            {
                MessageBox.Show("Заполните все поля!\nЛогин и пароль не меньше 8 символов.");
                return;
            }

            bool isCorrLogin = false;
            using (var conn = new MySqlConnection(AuthWindow.SQLBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch { MessageBox.Show("MySQL server disconnect"); }
                using (var query = conn.CreateCommand())
                {
                    query.CommandTimeout = 30;
                    query.CommandText = "SELECT COUNT(*) FROM `user-auth` WHERE `login` = @login;";
                    query.Parameters.AddWithValue("@login", textBox_login.Text);

                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read()) isCorrLogin = (reader.GetInt32(0) == 0);
                    }
                }
            }
            if (!isCorrLogin && !isEdit)
            {
                MessageBox.Show("Этот логин уже занят!");
                return;
            }

            #endregion

            if (isEdit)
            {
                using (var conn = new MySqlConnection(AuthWindow.SQLBuilder.ConnectionString))
                {
                    try { conn.Open(); }
                    catch { MessageBox.Show("MySQL server disconnect"); }
                    using (var query = conn.CreateCommand())
                    {
                        query.CommandTimeout = 30;
                        query.CommandText = "UPDATE `user` SET lname = @lname, fname = @fname, mname = @mname, date_birth = @date_birth, education = @education, post_id = @post_id WHERE `id` = @id;";                     
                        query.Parameters.AddWithValue("@id", userId);
                        query.Parameters.AddWithValue("@lname", textBox_lname.Text);
                        query.Parameters.AddWithValue("@fname", textBox_fname.Text);
                        query.Parameters.AddWithValue("@mname", textBox_mname.Text);
                        query.Parameters.AddWithValue("@education", textBox_educ.Text);
                        query.Parameters.AddWithValue("@date_birth", datePicker_birthDate.DisplayDate.ToString("yyyy-MM-dd"));
                        query.Parameters.AddWithValue("@post_id", comboBox_post.SelectedIndex + 1);
                        query.ExecuteNonQuery();

                        query.CommandText = "UPDATE `user-auth` SET `login` = @login, `password` = @password WHERE `user_id` = @id;";
                        query.Parameters.AddWithValue("@login", textBox_login.Text);
                        query.Parameters.AddWithValue("@password", textBox_password.Text);
                        query.ExecuteNonQuery();

                    }
                }
                MessageBox.Show("Успешно!");
                this.Close();
            }
            else
            {

                using (var conn = new MySqlConnection(AuthWindow.SQLBuilder.ConnectionString))
                {
                    try { conn.Open(); }
                    catch { MessageBox.Show("MySQL server disconnect"); }
                    using (var query = conn.CreateCommand())
                    {
                        query.CommandTimeout = 30;
                        query.CommandText = "INSERT INTO `user` (`lname`, `fname`, `mname`, `date_birth`, `education`, `start_work_date`, `end_work_date`, `post_id`) VALUES (@lname, @fname, @mname, @date_birth, @education, now(), '1900-01-01', @post_id);";
                        query.Parameters.AddWithValue("@lname", textBox_lname.Text);
                        query.Parameters.AddWithValue("@fname", textBox_fname.Text);
                        query.Parameters.AddWithValue("@mname", textBox_mname.Text);
                        query.Parameters.AddWithValue("@education", textBox_educ.Text);
                        query.Parameters.AddWithValue("@date_birth", datePicker_birthDate.DisplayDate.ToString("yyyy-MM-dd"));
                        query.Parameters.AddWithValue("@post_id", comboBox_post.SelectedIndex + 1);
                        query.ExecuteNonQuery();

                        query.CommandText = "INSERT INTO `user-auth` (`login`, `password`, `user_id`) VALUE (@login, @password, @id);";
                        query.Parameters.AddWithValue("@id", User.GetUserByLFMname(textBox_lname.Text, textBox_fname.Text, textBox_mname.Text).id);
                        query.Parameters.AddWithValue("@login", textBox_login.Text);
                        query.Parameters.AddWithValue("@password", textBox_password.Text);
                        query.ExecuteNonQuery();

                    }
                }
                MessageBox.Show("Успешно!");
                this.Close();
            }
        }
    }
}
