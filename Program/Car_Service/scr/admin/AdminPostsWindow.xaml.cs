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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TableData;

namespace Car_Service.scr.admin
{
    /// <summary>
    /// Логика взаимодействия для AdminPostsWindow.xaml
    /// </summary>
    public partial class AdminPostsWindow : Window
    {
        public AdminPostsWindow()
        {
            InitializeComponent();
            TableDraw();
        }

        private class TableDrawData
        {
            public int id {  get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public string wages { get; set; }
            public string tech_name { get; set; }
        }
        private void TableDraw()
        {
            List<TableDrawData> data = new List<TableDrawData>();

            Post.LoadPosts();
            foreach (Post p in Post.Posts)
            {
                TableDrawData d = new TableDrawData();
                d.title = p.title; 
                d.description = p.description;
                d.wages = p.wages.ToString() + " руб.";
                d.tech_name = p.tech_name.ToString();

                data.Add(d);
            }
            
            dgv.ItemsSource = data;
        }

        private void button_addNewPost_Click(object sender, RoutedEventArgs e)
        {
            new AdminAddPostWindow().ShowDialog();
            TableDraw();
        }

    
    }
}
