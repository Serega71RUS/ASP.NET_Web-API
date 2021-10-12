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
    /// Логика взаимодействия для DeleteComplect.xaml
    /// </summary>
    public partial class DeleteComplect : Window
    {
        private const string APP_PATH = "https://localhost:5001";
        private static string manufacturer;
        private static string model;
        private static string engine;
        private int ComplectId;
        private int modelId;
        private string OldVolume;
        private string OldPower;

        public DeleteComplect()
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
            if (engine != null)
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
                //textboxVolume.Text = st;
                i += 4;
                st = "";
                while (a[i] != ' ')
                {
                    st += a[i];
                    i++;
                }
                OldPower = st;
                //textboxPower.Text = st;
            }

        }

        private void DeleteComplectClick(object sender, RoutedEventArgs e)
        {
            string messageBoxText = "Вы действительно хотите удалить информацию о комплектации?";
            string caption = "Внимание!";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result;
            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    // User pressed Yes
                    using (var client = new HttpClient())
                    {
                        var response = client.GetAsync(APP_PATH + "/api/Cars/Complect_id/" + OldVolume + "/" + OldPower).Result.Content.ReadAsStringAsync().Result;
                        List<Complectation> ModelList = JsonConvert.DeserializeObject<List<Complectation>>(response);
                        foreach (Complectation item in ModelList)
                        {
                            modelId = item.id;
                        }
                    }
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(APP_PATH);
                        var response = client.DeleteAsync("/api/Cars/delete/complect/" + modelId).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("OK");
                        }
                        else
                        {
                            MessageBox.Show("Error");
                        }
                    }
                    break;
                case MessageBoxResult.No:
                    // User pressed No
                    break;
            }
        }
    }
}
