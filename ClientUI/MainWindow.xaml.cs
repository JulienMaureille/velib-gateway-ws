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

using iws;

namespace ClientUI
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Service1 service = new Service1();

        public MainWindow()
        {
            InitializeComponent();
            listeVilles.Items.Add("Toulouse");
        }

        private void listeVilles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listeStations.Items.Clear();
            foreach (string station in service.GetStations(e.AddedItems[0].ToString().Split(',')[0]))
            {
                listeStations.Items.Add(station);
            }
        }

        private void listeStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count <= 0)
            {
                return;
            }

            nomStation.Content = "Détails : station "+e.AddedItems[0].ToString().Split('\n')[0];
            detailsStation.Items.Clear();
            foreach (string info in service.GetInfo(e.AddedItems[0].ToString().Split('\n')[0], listeVilles.SelectedItem.ToString().Split(',')[0]))
            {
                detailsStation.Items.Add(info);
            }
        }

    }
}
