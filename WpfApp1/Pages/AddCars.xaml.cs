using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using WpfApp1.Models;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddCars.xaml
    /// </summary>
    public partial class AddCars : Window
    {
        private const string APP_PATH = "https://localhost:5001";
        public AddCars()
        {
            InitializeComponent();
        }

        private void AddCarButtonClick(object sender, RoutedEventArgs e)
        {
            if(textboxMan.Text != "" & textboxModel.Text != "")
            {
                using (var client = new HttpClient())
                {
                    Car car = new Car { id = 0, manufacturer = textboxMan.Text, model = textboxModel.Text };
                    client.BaseAddress = new Uri(APP_PATH);
                    var json = JsonConvert.SerializeObject(car);
                    var response = client.PostAsJsonAsync("api/Cars/post/car", json).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("OK");
                    }
                    else
                    {
                        MessageBox.Show("Error");
                    }
                    textboxMan.Clear();
                    textboxModel.Clear();
                }
            }
        }

        private void CloseWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }
    }
}
