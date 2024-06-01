using Car_Service;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows;

namespace TableData
{
    public class User
    {
        public int id;
        public string lname;
        public string fname;
        public string mname;
        public DateTime date_birth;
        public string education;
        public DateTime start_work_date;
        public DateTime end_work_date;
        public int post_id;

        public string login;
        public string password;

        public static User GetUserById(int id)
        {
            User user = new User();
            using (var conn = new MySqlConnection(AuthWindow.SQLBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch { MessageBox.Show("MySQL server disconnect"); }

                using (var query = conn.CreateCommand())
                {
                    query.CommandTimeout = 30;
                    query.CommandText = "SELECT * FROM `user` WHERE `id` = @id";
                    query.Parameters.AddWithValue("@id", id);

                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user.id = reader.GetInt32(0);
                            user.lname = reader.GetString(1);
                            user.fname = reader.GetString(2);
                            user.mname = reader.GetString(3);
                            user.date_birth = reader.GetDateTime(4);
                            user.education = reader.GetString(5);
                            user.start_work_date = reader.GetDateTime(6);
                            user.end_work_date= reader.GetDateTime(7);
                            user.post_id = reader.GetInt32(8);
                        }
                    }
                }
            }
            using (var conn = new MySqlConnection(AuthWindow.SQLBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch { MessageBox.Show("MySQL server disconnect"); }

                using (var query = conn.CreateCommand())
                {
                    query.CommandTimeout = 30;
                    query.CommandText = "SELECT * FROM `user-auth` WHERE `user_id` = @id";
                    query.Parameters.AddWithValue("@id", id);

                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user.login = reader.GetString(0);
                            user.password = reader.GetString(1);
                        }
                    }
                }
            }

            return user;
        }
        
        public static User GetUserByLFMname(string lname, string fname, string mname)
        {
            User user = new User();
            using (var conn = new MySqlConnection(AuthWindow.SQLBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch { MessageBox.Show("MySQL server disconnect"); }

                using (var query = conn.CreateCommand())
                {
                    query.CommandTimeout = 30;
                    query.CommandText = "SELECT * FROM `user` WHERE `lname` = @lname AND `fname` = @fname AND `mname` = @mname";
                    query.Parameters.AddWithValue("@fname", fname);
                    query.Parameters.AddWithValue("@lname", lname);
                    query.Parameters.AddWithValue("@mname", mname);

                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user.id = reader.GetInt32(0);
                            user.lname = reader.GetString(1);
                            user.fname = reader.GetString(2);
                            user.mname = reader.GetString(3);
                            user.date_birth = reader.GetDateTime(4);
                            user.education = reader.GetString(5);
                            user.start_work_date = reader.GetDateTime(6);
                            user.end_work_date= reader.GetDateTime(7);
                            user.post_id = reader.GetInt32(8);
                        }
                    }
                }
            }
            using (var conn = new MySqlConnection(AuthWindow.SQLBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch { MessageBox.Show("MySQL server disconnect"); }

                using (var query = conn.CreateCommand())
                {
                    query.CommandTimeout = 30;
                    query.CommandText = "SELECT * FROM `user-auth` WHERE `user_id` = @id";
                    query.Parameters.AddWithValue("@id", user.id);

                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user.login = reader.GetString(0);
                            user.password = reader.GetString(1);
                        }
                    }
                }
            }

            return user;
        }
        
    }


    public class Post
    {
        public static Post[] Posts;

        public int id;
        public string title;
        public string description;
        public int wages;
        public int tech_name;

        public static Post GetPostById(int id)
        {
            Post obj = new Post();
            using (var conn = new MySqlConnection(AuthWindow.SQLBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch { MessageBox.Show("MySQL server disconnect"); }

                using (var query = conn.CreateCommand())
                {
                    query.CommandTimeout = 30;
                    query.CommandText = "SELECT * FROM `post` WHERE `id` = @id";
                    query.Parameters.AddWithValue("@id", id);

                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            obj.id = reader.GetInt32(0);
                            obj.title = reader.GetString(1);
                            obj.description = reader.GetString(2);
                            obj.wages = reader.GetInt32(3);
                            obj.tech_name = reader.GetInt32(4);
                        }
                    }
                }
            }
            return obj;
        }

        public static void LoadPosts()
        {
            List<Post> posts = new List<Post>();
            using (var conn = new MySqlConnection(AuthWindow.SQLBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch { MessageBox.Show("MySQL server disconnect"); }

                using (var query = conn.CreateCommand())
                {
                    query.CommandTimeout = 30;
                    query.CommandText = "SELECT * FROM `post`";

                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Post obj = new Post();
                            obj.id = reader.GetInt32(0);
                            obj.title = reader.GetString(1);
                            obj.description = reader.GetString(2);
                            obj.wages = reader.GetInt32(3);
                            obj.tech_name = reader.GetInt32(4);
                            posts.Add(obj);
                        }
                    }
                }
            }
            Posts = posts.ToArray();
        }
    }

    public class Car
    {
        public int id;
        public string stamp;
        public string model;
        public int year_release;
        public int mileage;
        public string license_plate_number;

        public static Car GetCarById(int id) 
        {
            Car obj = new Car();
            using (var conn = new MySqlConnection(AuthWindow.SQLBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch { MessageBox.Show("MySQL server disconnect"); }

                using (var query = conn.CreateCommand())
                {
                    query.CommandTimeout = 30;
                    query.CommandText = "SELECT * FROM `car` WHERE `id` = @id";
                    query.Parameters.AddWithValue("@id", id);

                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            obj.id = reader.GetInt32(0);
                            obj.stamp = reader.GetString(1);
                            obj.model = reader.GetString(2);
                            obj.year_release = reader.GetInt32(3);
                            obj.mileage = reader.GetInt32(4);
                            obj.license_plate_number = reader.GetString(5);
                        }
                    }
                }
            }
            return obj;
        }
        
        public static Car GetCarByLicense_plate_number(string license) 
        {
            Car obj = new Car();
            using (var conn = new MySqlConnection(AuthWindow.SQLBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch { MessageBox.Show("MySQL server disconnect"); }

                using (var query = conn.CreateCommand())
                {
                    query.CommandTimeout = 30;
                    query.CommandText = "SELECT * FROM `car` WHERE `license_plate_number` = @license;";
                    query.Parameters.AddWithValue("@license", license.ToLower());

                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            obj.id = reader.GetInt32(0);
                            obj.stamp = reader.GetString(1);
                            obj.model = reader.GetString(2);
                            obj.year_release = reader.GetInt32(3);
                            obj.mileage = reader.GetInt32(4);
                            obj.license_plate_number = reader.GetString(5);
                        }
                    }
                }
            }
            return obj;
        }
    }

    public class Client
    {
        public int id;
        public string lname;
        public string fname;
        public string mname;
        public string number_phone;

        public static Client GetClientById(int id)
        {
            Client user = new Client();
            using (var conn = new MySqlConnection(AuthWindow.SQLBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch { MessageBox.Show("MySQL server disconnect"); }

                using (var query = conn.CreateCommand())
                {
                    query.CommandTimeout = 30;
                    query.CommandText = "SELECT * FROM `client` WHERE `id` = @id";
                    query.Parameters.AddWithValue("@id", id);

                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user.id = reader.GetInt32(0);
                            user.lname = reader.GetString(1);
                            user.fname = reader.GetString(2);
                            user.mname = reader.GetString(3);
                            user.number_phone = reader.GetString(4);
                        }
                    }
                }
            }        
            return user;
        }
        
        public static Client GetClientByNumberPhone(string numberPhone)
        {
            Client user = new Client();
            using (var conn = new MySqlConnection(AuthWindow.SQLBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch { MessageBox.Show("MySQL server disconnect"); }

                using (var query = conn.CreateCommand())
                {
                    query.CommandTimeout = 30;
                    query.CommandText = "SELECT * FROM `client` WHERE `number_phone` = @number_phone";
                    query.Parameters.AddWithValue("@number_phone", numberPhone);

                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user.id = reader.GetInt32(0);
                            user.lname = reader.GetString(1);
                            user.fname = reader.GetString(2);
                            user.mname = reader.GetString(3);
                            user.number_phone = reader.GetString(4);
                        }
                    }
                }
            }        
            return user;
        }
    }

    public class Order
    {
        public int id;
        public string status;
        public string description;
        public int client_id;
        public int car_id;

        public static Order GetOrderById(int id)
        {
            Order obj = new Order();
            using (var conn = new MySqlConnection(AuthWindow.SQLBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch { MessageBox.Show("MySQL server disconnect"); }

                using (var query = conn.CreateCommand())
                {
                    query.CommandTimeout = 30;
                    query.CommandText = "SELECT * FROM `order` WHERE `id` = @id";
                    query.Parameters.AddWithValue("@id", id);

                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            obj.id = reader.GetInt32(0);
                            obj.status = reader.GetString(1);
                            obj.description = reader.GetString(2);
                            obj.client_id = reader.GetInt32(3);
                            obj.car_id = reader.GetInt32(4);
                        }
                    }
                }
            }
            return obj;
        }
        
        public static Order GetOrderByClientId(int id)
        {
            Order obj = new Order();
            using (var conn = new MySqlConnection(AuthWindow.SQLBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch { MessageBox.Show("MySQL server disconnect"); }

                using (var query = conn.CreateCommand())
                {
                    query.CommandTimeout = 30;
                    query.CommandText = "SELECT * FROM `order` WHERE `client_id` = @id";
                    query.Parameters.AddWithValue("@id", id);

                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            obj.id = reader.GetInt32(0);
                            obj.status = reader.GetString(1);
                            obj.description = reader.GetString(2);
                            obj.client_id = reader.GetInt32(3);
                            obj.car_id = reader.GetInt32(4);
                        }
                    }
                }
            }
            return obj;
        }

        public static Order GetOrderByCarId(int id)
        {
            Order obj = new Order();
            using (var conn = new MySqlConnection(AuthWindow.SQLBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch { MessageBox.Show("MySQL server disconnect"); }

                using (var query = conn.CreateCommand())
                {
                    query.CommandTimeout = 30;
                    query.CommandText = "SELECT * FROM `order` WHERE `car_id` = @id";
                    query.Parameters.AddWithValue("@id", id);

                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            obj.id = reader.GetInt32(0);
                            obj.status = reader.GetString(1);
                            obj.description = reader.GetString(2);
                            obj.client_id = reader.GetInt32(3);
                            obj.car_id = reader.GetInt32(4);
                        }
                    }
                }
            }
            return obj;
        }     
    }

    public class Malfunction
    {
        public int id;
        public string title;
        public string description;
        public int cost;
        public int autopart_id;
        public int user_id;

        public static Malfunction GetMalfunctionById(int id) {
            Malfunction obj = new Malfunction();
            using (var conn = new MySqlConnection(AuthWindow.SQLBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch { MessageBox.Show("MySQL server disconnect"); }

                using (var query = conn.CreateCommand())
                {
                    query.CommandTimeout = 30;
                    query.CommandText = "SELECT * FROM `malfunction` WHERE `id` = @id";
                    query.Parameters.AddWithValue("@id", id);

                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            obj.id = reader.GetInt32(0);
                            obj.title = reader.GetString(1);
                            obj.description = reader.GetString(2);
                            obj.cost = reader.GetInt32(3);
                            obj.autopart_id = reader.GetInt32(4);
                            obj.user_id = reader.GetInt32(5);
                        }
                    }
                }
            }
            return obj;
        }

        public static Malfunction[] GetMalfunctionsByOrderId(int id)
        {
            List<Malfunction> list = new List<Malfunction>();
            using (var conn = new MySqlConnection(AuthWindow.SQLBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch { MessageBox.Show("MySQL server disconnect"); }

                using (var query = conn.CreateCommand())
                {
                    query.CommandTimeout = 30;
                    query.CommandText = "SELECT `malfunction_id` FROM `malfunction_order` WHERE `order_id` = @id";
                    query.Parameters.AddWithValue("@id", id);

                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(Malfunction.GetMalfunctionById(reader.GetInt32(0)));
                        }
                    }
                }
            }
            return list.ToArray();
        }

        public static string GetStatusMalfunctionByOrderIdAndMalfunctionId(int orderId, int malfunctionId)
        {          
            using (var conn = new MySqlConnection(AuthWindow.SQLBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch { MessageBox.Show("MySQL server disconnect"); }

                using (var query = conn.CreateCommand())
                {
                    query.CommandTimeout = 30;
                    query.CommandText = "SELECT `status` FROM `malfunction_order` WHERE `order_id` = @order_id AND `malfunction_id` = @malfunction_id;";
                    query.Parameters.AddWithValue("@order_id", orderId);
                    query.Parameters.AddWithValue("@malfunction_id", malfunctionId);

                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.GetString(0);
                        }
                    }
                }
            }
            return null;
        }

        public static bool SetStatusMalfunctionByOrderIdAndMalfunctionId(int orderId, int malfunctionId, string status)
        {
            using (var conn = new MySqlConnection(AuthWindow.SQLBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch { MessageBox.Show("MySQL server disconnect"); return false; }

                using (var query = conn.CreateCommand())
                {
                    query.CommandTimeout = 30;
                    query.CommandText = "UPDATE `malfunction_order` SET `status` = @status WHERE `order_id` = @order_id AND `malfunction_id` = @malfunction_id;";
                    query.Parameters.AddWithValue("@order_id", orderId);
                    query.Parameters.AddWithValue("@malfunction_id", malfunctionId);
                    query.Parameters.AddWithValue("@status", status);
                    query.ExecuteNonQuery();
                }
            }

            return true;
        }
    }

    public class Autopart
    {
        public int id;
        public string title;
        public string description;
        public int cost;

        public static Autopart GetAutopartById(int id)
        {
            Autopart obj = new Autopart();
            using (var conn = new MySqlConnection(AuthWindow.SQLBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch { MessageBox.Show("MySQL server disconnect"); }

                using (var query = conn.CreateCommand())
                {
                    query.CommandTimeout = 30;
                    query.CommandText = "SELECT * FROM `autopart` WHERE `id` = @id";
                    query.Parameters.AddWithValue("@id", id);

                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            obj.id = reader.GetInt32(0);
                            obj.title = reader.GetString(1);
                            obj.description = reader.GetString(2);
                            obj.cost = reader.GetInt32(3);
                        }
                    }
                }
            }
            return obj;
        }
    }
}
