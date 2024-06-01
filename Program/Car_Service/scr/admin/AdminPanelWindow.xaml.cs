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
    /// Логика взаимодействия для AdminPanelWindow.xaml
    /// </summary>
    public partial class AdminPanelWindow : Window
    {
        public AdminPanelWindow()
        {
            InitializeComponent();
        }

        private void button_user_Click(object sender, RoutedEventArgs e)
        {
            window.Visibility = Visibility.Hidden;
            new AdminUserPanelWindow().ShowDialog();
            window.Visibility = Visibility.Visible;
        }

        private void button_order_Click(object sender, RoutedEventArgs e)
        {
            window.Visibility = Visibility.Hidden;
            new AdminOrderWindow().ShowDialog();
            window.Visibility = Visibility.Visible;
        }

        private void button_client_Click(object sender, RoutedEventArgs e)
        {
            window.Visibility = Visibility.Hidden;
            new AdminClientsWindow().ShowDialog();
            window.Visibility = Visibility.Visible;
        }

        private void button_posts_Click(object sender, RoutedEventArgs e)
        {
            window.Visibility = Visibility.Hidden;
            new AdminPostsWindow().ShowDialog();
            window.Visibility = Visibility.Visible;
        }
    }
}
