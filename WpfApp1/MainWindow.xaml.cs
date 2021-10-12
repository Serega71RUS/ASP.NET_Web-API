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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using WpfApp1.Models;
using WpfApp1.Pages;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string APP_PATH = "https://localhost:5001";
        private static string manufacturer;
        private static string model;
        private static string engine;

        public MainWindow()
        {
            InitializeComponent();
            FillBoxMan();
            FillDB();
        }

        public void FillBoxMan()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = client.GetAsync(APP_PATH + "/api/Cars/Sber").Result.Content.ReadAsStringAsync().Result;
                    List<Car> CarList = JsonConvert.DeserializeObject<List<Car>>(response);
                    foreach(Car item in CarList)
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
                var response = client.GetAsync(APP_PATH + "/api/Cars/Sber/"+ manufacturer).Result.Content.ReadAsStringAsync().Result;
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
                var response = client.GetAsync(APP_PATH + "/api/Cars/Sber/" + manufacturer+ "/"+model).Result.Content.ReadAsStringAsync().Result;
                List<Complectation> EngineList = JsonConvert.DeserializeObject<List<Complectation>>(response);
                string stV = " л, ";
                string stHP = " л.с.";
                foreach (Complectation item in EngineList)
                {
                    string st = Math.Round(item.engine_volume,1) + stV + item.power + stHP;
                    ComboPower.Items.Add(st);
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
            ComboPower.Items.Clear();
            ComboBox comboBox = (ComboBox)sender;
            model = (string)comboBox.SelectedItem;
            FillBoxEngine();
        }

        private void EngineSelect(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            engine = (string)comboBox.SelectedItem;
        }

        public void FillDB()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = client.GetAsync(APP_PATH + "/api/Cars/All").Result.Content.ReadAsStringAsync().Result;
                    List<AllTable> AllCars = JsonConvert.DeserializeObject<List<AllTable>>(response);
                    datagrid1.ItemsSource = AllCars;
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.InnerException.InnerException.Message);
            }
        }

        private void AddCarButtonClick(object sender, RoutedEventArgs e)
        {
            AddCars a = new AddCars();
            a.Show();
        }

        private void AddComplectButtonClick(object sender, RoutedEventArgs e)
        {
            AddComplect a = new AddComplect();
            a.Show();
        }

        private void ActiveWindow(object sender, EventArgs e)
        {
            FillDB();
        }

        private void UpdateComplectClick(object sender, RoutedEventArgs e)
        {
            UpdateComplect a = new UpdateComplect();
            a.Show();
        }

        private void DelCarClick(object sender, RoutedEventArgs e)
        {
            DeleteCar a = new DeleteCar();
            a.Show();
        }

        private void UpdateCarClick(object sender, RoutedEventArgs e)
        {
            UpdateCar a = new UpdateCar();
            a.Show();
        }

        private void DelComplectClick(object sender, RoutedEventArgs e)
        {
            DeleteComplect a = new DeleteComplect();
            a.Show();
        }
    }
}
