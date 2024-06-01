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

namespace Car_Service.scr.manager
{
    /// <summary>
    /// Логика взаимодействия для AutopartInfoWindow.xaml
    /// </summary>
    public partial class AutopartInfoWindow : Window
    {
        public AutopartInfoWindow(int autopartID)
        {
            InitializeComponent();
            Autopart autopart = Autopart.GetAutopartById(autopartID);
            textBox_cost.Content = autopart.cost;
            textBox_title.Content = autopart.title;
            textBox_des.Text = autopart.description;

            textBox_des.IsReadOnly = true;
        }
    }
}
