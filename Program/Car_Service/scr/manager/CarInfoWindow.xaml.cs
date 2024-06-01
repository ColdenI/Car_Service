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
    /// Логика взаимодействия для CarInfoWindow.xaml
    /// </summary>
    public partial class CarInfoWindow : Window
    {
        public CarInfoWindow(int car_id)
        {
            InitializeComponent();
            Car car = Car.GetCarById(car_id);

            textBox_license_plate_number.Content = car.license_plate_number;
            textBox_mileage.Content = car.mileage + " km";
            textBox_model.Content = car.model;
            textBox_stamp.Content = car.stamp;
            textBox_year_release.Content = car.year_release;
        }
    }
}
