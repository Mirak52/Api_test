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
    }
}
