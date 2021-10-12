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
    /// Логика взаимодействия для UpdateCar.xaml
    /// </summary>
    public partial class UpdateCar : Window
    {
        private const string APP_PATH = "https://localhost:5001";
        private static string manufacturer;
        private static string model;
        private static string engine;
        private int ComplectId;
        private int modelId;
        private string OldManuf;
        private string OldModel;

        public UpdateCar()
        {
            InitializeComponent();
            FillBoxMan();
            //FillBoxAllModel();
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
            //if(ComboModel.SelectedItem == null)
            //{
                ComboModel.Items.Clear();
                ComboBox comboBox = (ComboBox)sender;
                manufacturer = (string)comboBox.SelectedItem;
                textboxManuf.Text = manufacturer;
                FillBoxModel();
            //}


        }

        private void ModelSelect(object sender, SelectionChangedEventArgs e)
        {
            //ComboPower.Items.Clear();
            ComboBox comboBox = (ComboBox)sender;
            model = (string)comboBox.SelectedItem;
            textboxModel.Text = model;
            ComboModel.SelectedItem = model;
            //if (manufacturer == null)
            //{
            //    using (var client = new HttpClient())
            //    {
            //        var response = client.GetAsync(APP_PATH + "/api/Cars/Manuf/" + model).Result.Content.ReadAsStringAsync().Result;
            //        List<Car> ModelList = JsonConvert.DeserializeObject<List<Car>>(response);
            //        foreach (Car item in ModelList)
            //        {
            //            manufacturer = item.manufacturer;
            //        }
            //    }
            //    ComboMan.SelectedItem = manufacturer;
            //    textboxManuf.Text = manufacturer;
            //}
            //textboxModel.Text = model;
            //FillBoxEngine();
        }

        private void UpdateCarClick(object sender, RoutedEventArgs e)
        {
            OldManuf = manufacturer;
            OldModel = model;
            manufacturer = textboxManuf.Text;
            model = textboxModel.Text;
            UpdateChoice a = new UpdateChoice(OldManuf, OldModel, manufacturer, model);
            a.Show();
        }
        private void UpdateCarWindowActive(object sender, EventArgs e)
        {
            FillBoxMan();
            //FillBoxAllModel();
        }
    }
}
