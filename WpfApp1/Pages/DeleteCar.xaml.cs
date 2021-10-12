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
    /// Логика взаимодействия для DeleteCar.xaml
    /// </summary>
    public partial class DeleteCar : Window
    {
        private const string APP_PATH = "https://localhost:5001";
        public static string manufacturer;
        public static string model;
        private static string engine;
        private int ComplectId;
        private int modelId;
        private string OldVolume;
        private string OldPower;

        public DeleteCar()
        {
            InitializeComponent();
            FillBoxMan();
            FillBoxAllModel();
        }

        public void FillBoxMan()
        {
            try
            {
                ComboMan.Items.Clear();
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
                ComboModel.Items.Clear();
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

        private void manSelect(object sender, SelectionChangedEventArgs e)
        {
            ComboModel.Items.Clear();
            ComboBox comboBox = (ComboBox)sender;
            manufacturer = (string)comboBox.SelectedItem;
            FillBoxModel();

        }

        private void ModelSelect(object sender, SelectionChangedEventArgs e)
        {
            //ComboPower.Items.Clear();
            ComboBox comboBox = (ComboBox)sender;
            model = (string)comboBox.SelectedItem;
            //FillBoxEngine();
        }

        private void DeleteCarClick(object sender, RoutedEventArgs e)
        {
            DeleteChoice a = new DeleteChoice(manufacturer, model);
            a.Show();
        }

        private void DeleteCarWindowActive(object sender, EventArgs e)
        {
            FillBoxMan();
            FillBoxAllModel();
        }
    }
}
