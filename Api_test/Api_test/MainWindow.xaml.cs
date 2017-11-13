using RestSharp;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Api_test
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Dwarf.ItemsSource = GetMidgets();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var client = new RestClient("https://student.sps-prosek.cz/~bastlma14/Api/");
            var request = new RestRequest(Method.PUT);
            request.AddParameter("id", "1");
            request.AddParameter("name", "DumMy");
            request.AddParameter("height", "69");
            request.AddParameter("weight", "96");
            request.AddParameter("beardLength", "4761"); //Nauč se psát!!!
            var res = client.Execute(request);
            Dwarf.ItemsSource = GetMidgets();

        }
        public List<Midget> GetMidgets()
        {
            var client = new RestClient("https://student.sps-prosek.cz/~bastlma14/Api/");
            var request = new RestRequest(Method.GET);
            var res = client.Execute<List<Midget>>(request);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            var queryResult = res.Data;
            if (res.ResponseStatus == ResponseStatus.Error)
            {
                throw new System.ArgumentException("Chyba na serveru, zkontroluj URL");
                //Error.Content= "Chyba na serveru, zkontroluj URL");
            }
            return queryResult;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var client = new RestClient("https://student.sps-prosek.cz/~bastlma14/Api/");
            var request = new RestRequest(Method.GET);
            request.AddParameter("name", getName.Text);
            var res = client.Execute<List<Midget>>(request);
            if (res.ResponseStatus == ResponseStatus.Error)
            {
                throw new System.ArgumentException("Chyba na serveru, zkontroluj URL");
                //Error.Content= "Chyba na serveru, zkontroluj URL");
            }
            Dwarf.ItemsSource = res.Data;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Dwarf.ItemsSource = GetMidgets();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var client = new RestClient("https://student.sps-prosek.cz/~bastlma14/Api/");
            var request = new RestRequest(Method.POST);
            int n;
            bool height = int.TryParse(Height.Text, out n);
            bool weight = int.TryParse(Weight.Text, out n);
            bool beardLength = int.TryParse(BeardLength.Text, out n);
            if (height && weight && beardLength)
                {
                request.AddParameter("name", Name.Text);
                request.AddParameter("height", Height.Text);
                request.AddParameter("weight", Weight.Text);
                request.AddParameter("beardLength", BeardLength.Text);
                var res = client.Execute(request);
                    if (res.ResponseStatus == ResponseStatus.Error)
                    {
                        throw new System.ArgumentException("Chyba na serveru, zkontroluj URL");
                        //Error.Content= "Chyba na serveru, zkontroluj URL");
                    }
                Error_label.Content = "";
                Dwarf.ItemsSource = GetMidgets();
                }
           else
           {
               Error_label.Content = "Některý z udajů obsahuje nepovolené znaky(jiné než číslo)";
           }
        }
    }
}
