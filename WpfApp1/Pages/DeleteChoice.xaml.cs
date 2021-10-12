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
    /// Логика взаимодействия для DeleteChoice.xaml
    /// </summary>
    public partial class DeleteChoice : Window
    {
        private const string APP_PATH = "https://localhost:5001";
        private static string manufacturer;
        private static string model;
        private int modelId;

        public DeleteChoice(string man, string mod)
        {
            InitializeComponent();
            manufacturer = man;
            model = mod;
            if(mod == null)
            {
                DeleteModelButton.IsEnabled = false;
            }
            else
            {
                DeleteModelButton.IsEnabled = true;
            }
        }

        private void DeleteManufClick(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(APP_PATH + "/api/Cars/Manuf_id/" + manufacturer).Result.Content.ReadAsStringAsync().Result;
                List<Car> ModelList = JsonConvert.DeserializeObject<List<Car>>(response);
                foreach (Car item in ModelList)
                {
                    modelId = item.id;
                    using (var client1 = new HttpClient())
                    {
                        client1.BaseAddress = new Uri(APP_PATH);
                        var response1 = client1.DeleteAsync("/api/Cars/delete/model/" + modelId).Result;
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

        private void DeleteModelClick(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(APP_PATH + "/api/Cars/Model_id/" + model).Result.Content.ReadAsStringAsync().Result;
                List<Complectation> ModelList = JsonConvert.DeserializeObject<List<Complectation>>(response);
                foreach (Complectation item in ModelList)
                {
                    modelId = item.id;
                }
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(APP_PATH);
                var response = client.DeleteAsync("/api/Cars/delete/model/"+modelId).Result;
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("OK");
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
            Close();
        }

        private void DeleteChoiceCancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
