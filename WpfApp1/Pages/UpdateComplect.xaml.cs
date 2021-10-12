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
    /// Логика взаимодействия для UpdateComplect.xaml
    /// </summary>
    public partial class UpdateComplect : Window
    {
        private const string APP_PATH = "https://localhost:5001";
        private static string manufacturer;
        private static string model;
        private static string engine;
        private int ComplectId;
        private int modelId;
        private string OldVolume;
        private string OldPower;

        public UpdateComplect()
        {
            InitializeComponent();
            FillBoxMan();
            //FillBoxAllModel();
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

        public void FillBoxEngine()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(APP_PATH + "/api/Cars/Sber/" + manufacturer + "/" + model).Result.Content.ReadAsStringAsync().Result;
                List<Complectation> EngineList = JsonConvert.DeserializeObject<List<Complectation>>(response);
                string stV = " л, ";
                string stHP = " л.с.";
                foreach (Complectation item in EngineList)
                {
                    string st = Math.Round(item.engine_volume, 1) + stV + item.power + stHP;
                    ComboPower.Items.Add(st);
                }
                //2.2 л, 256 л.с.
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
            ComboPower.Items.Clear();
            ComboBox comboBox = (ComboBox)sender;
            model = (string)comboBox.SelectedItem;
            FillBoxEngine();
        }

        private void EngineSelect(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            engine = (string)comboBox.SelectedItem;
            if(engine != null)
            {
                char[] a = engine.ToCharArray();
                int i = 0;
                string st = "";
                while (a[i] != ' ')
                {
                    st += a[i];
                    i++;
                }
                OldVolume = st;
                textboxVolume.Text = st;
                i += 4;
                st = "";
                while (a[i] != ' ')
                {
                    st += a[i];
                    i++;
                }
                OldPower = st;
                textboxPower.Text = st;
            }

        }

        private void UpdateComplectClick(object sender, RoutedEventArgs e)
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
                    var response = client.GetAsync(APP_PATH + "/api/Cars/Complect_id/" + OldVolume + "/"+ OldPower).Result.Content.ReadAsStringAsync().Result;
                    List<Complectation> ComplectList = JsonConvert.DeserializeObject<List<Complectation>>(response);
                    foreach (Complectation item in ComplectList)
                    {
                        ComplectId = item.id;
                    }
                }
                using (var client = new HttpClient())
                {
                    double.TryParse(textboxVolume.Text, out double d);
                    Complectation complect = new Complectation
                    {
                        id = ComplectId,
                        model_id = modelId,
                        engine_volume = d,
                        power = Convert.ToInt32(textboxPower.Text)
                    };
                    client.BaseAddress = new Uri(APP_PATH);
                    var json = JsonConvert.SerializeObject(complect);
                    var response = client.PutAsJsonAsync("/api/Cars/put/complect", json).Result;
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
                    ComboPower.Items.Clear();
                    FillBoxMan();
                    //FillBoxAllModel();
                }
            }
        }
    }
}
