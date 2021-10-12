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
    /// Логика взаимодействия для AddComplect.xaml
    /// </summary>
    public partial class AddComplect : Window
    {
        private const string APP_PATH = "https://localhost:5001";
        private static string manufacturer;
        private int modelId;
        private static string model;
        private static string engine;
        public AddComplect()
        {
            InitializeComponent();
            FillBoxMan();
            FillBoxAllModel();
        }

        public void FillBoxMan()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = client.GetAsync(APP_PATH + "/api/Cars/Sber").Result.Content.ReadAsStringAsync().Result;
                    List<Car> CarList = JsonConvert.DeserializeObject<List<Car>>(response);
                    foreach (Car item in CarList)
                    {
                        ComboMan.Items.Add(item.manufacturer);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.InnerException.InnerException.Message);
            }
        }

        public void FillBoxAllModel()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = client.GetAsync(APP_PATH + "/api/Cars/Sber/AllModel").Result.Content.ReadAsStringAsync().Result;
                    List<Car> CarList = JsonConvert.DeserializeObject<List<Car>>(response);
                    foreach (Car item in CarList)
                    {
                        ComboModel.Items.Add(item.model);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.InnerException.InnerException.Message);
            }
        }

        public void FillBoxModel()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(APP_PATH + "/api/Cars/Sber/" + manufacturer).Result.Content.ReadAsStringAsync().Result;
                List<Car> ModelList = JsonConvert.DeserializeObject<List<Car>>(response);
                foreach (Car item in ModelList)
                {
                    ComboModel.Items.Add(item.model);
                }
            }
        }

        private void manSelect(object sender, SelectionChangedEventArgs e)
        {
            ComboModel.Items.Clear();
            ComboBox comboBox = (ComboBox)sender;
            manufacturer = (string)comboBox.SelectedItem;
            FillBoxModel();
        }

        private void ModelSelect(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            model = (string)comboBox.SelectedItem;
        }

        private void AddComplectClick(object sender, RoutedEventArgs e)
        {
            if (textboxVolume.Text != "" & textboxPower.Text != "")
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
                    double.TryParse(textboxVolume.Text, out double d);
                    Complectation complect = new Complectation
                    {
                        id = 0,
                        model_id = modelId,
                        engine_volume = d,
                        power = Convert.ToInt32(textboxPower.Text)
                    };
                    client.BaseAddress = new Uri(APP_PATH);
                    var json = JsonConvert.SerializeObject(complect);
                    var response = client.PostAsJsonAsync("api/Cars/post/complect", json).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("OK");
                    }
                    else
                    {
                        MessageBox.Show("Error");
                    }
                    textboxVolume.Clear();
                    textboxPower.Clear();
                    ComboMan.Items.Clear();
                    ComboModel.Items.Clear();
                    FillBoxMan();
                    FillBoxAllModel();
                }
            }
        }
    }
}
