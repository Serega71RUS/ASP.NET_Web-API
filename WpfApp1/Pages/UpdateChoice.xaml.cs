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
    /// Логика взаимодействия для UpdateChoice.xaml
    /// </summary>
    public partial class UpdateChoice : Window
    {
        private const string APP_PATH = "https://localhost:5001";
        private static string oldmanufacturer;
        private static string oldmodel;
        private static string manufacturer;
        private static string model;
        private int modelId;

        public UpdateChoice(string oldman, string oldmod, string man, string mod)
        {
            InitializeComponent();
            oldmanufacturer = oldman;
            oldmodel = oldmod;
            manufacturer = man;
            model = mod;
            if (oldmod == null)
            {
                UpdateModelButton.IsEnabled = false;
            }
            else
            {
                UpdateModelButton.IsEnabled = true;
            }
        }

        private void UpdateManufClick(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(APP_PATH + "/api/Cars/Manuf_id/" + oldmanufacturer).Result.Content.ReadAsStringAsync().Result;
                List<Car> ModelList = JsonConvert.DeserializeObject<List<Car>>(response);
                foreach (Car item in ModelList)
                {
                    //modelId = item.id;
                    //manufacturer = item.manufacturer;
                    //model = item.model;
                    using (var client1 = new HttpClient())
                    {
                        //double.TryParse(textboxVolume.Text, out double d);
                        Car complect = new Car
                        {
                            id = item.id,
                            manufacturer = manufacturer,
                            model = item.model
                        };
                        client1.BaseAddress = new Uri(APP_PATH);
                        var json = JsonConvert.SerializeObject(complect);
                        var response1 = client1.PutAsJsonAsync("/api/Cars/put/car", json).Result;
                        if (response1.IsSuccessStatusCode)
                        {
                            MessageBox.Show("OK");
                        }
                        else
                        {
                            MessageBox.Show("Error");
                        }
                    }

                }
            }

            Close();
        }

        private void UpdateModelClick(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(APP_PATH + "/api/Cars/Model_id/" + oldmodel).Result.Content.ReadAsStringAsync().Result;
                List<Car> ModelList = JsonConvert.DeserializeObject<List<Car>>(response);
                foreach (Car item in ModelList)
                {
                    modelId = item.id;
                }
            }
            using (var client = new HttpClient())
            {
                //double.TryParse(textboxVolume.Text, out double d);
                Car complect = new Car
                {
                    id = modelId,
                    manufacturer = manufacturer,
                    model = model
                };
                client.BaseAddress = new Uri(APP_PATH);
                var json = JsonConvert.SerializeObject(complect);
                var response = client.PutAsJsonAsync("/api/Cars/put/car", json).Result;
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("OK");
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri(APP_PATH);
            //    var response = client.DeleteAsync("/api/Cars/delete/complect/" + modelId).Result;
            //    if (response.IsSuccessStatusCode)
            //    {
            //        MessageBox.Show("OK");
            //    }
            //    else
            //    {
            //        MessageBox.Show("Error");
            //    }
            //}
            Close();
        }

        private void UpdateChoiceCancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
