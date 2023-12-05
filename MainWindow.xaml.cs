//TODO: pulir modificar, detalles y eliminar
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

namespace CRUDMenu
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        SecondWindow secondWindow = null;
        //List<Country> countries = new List<Country>();


        public MainWindow()
        {
            InitializeComponent();

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.CheckEnabledButtons();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            secondWindow = new SecondWindow(this);
            secondWindow.ChangeToAddCountryView();
            secondWindow.ShowDialog(); // Helpful to avoid user's interaction with the main window while the second one is opened
        
        }

        internal void AddCountryToList(Country country) //Internal to make it accessible from SecondWindow
        {
            //countries.Add(country);
            lsBCountries.Items.Add(country);
        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            secondWindow = new SecondWindow(this);
            CheckForSelectedCountry();
            secondWindow.ChangeToModifyView(); //It's important to call this method before the ShowDialog() one, otherwise won't work!
            secondWindow.ShowDialog();
        }

        //Checks if the main menu buttons should be enabled or not
        internal void CheckEnabledButtons()
        {
            if (lsBCountries.SelectedItems.Count > 0)
            {
                btnModify.IsEnabled = true;
                btnDelete.IsEnabled = true;
                btnDetails.IsEnabled = true;
            }
            else
            {
                btnModify.IsEnabled = false;
                btnDelete.IsEnabled = false;
                btnDetails.IsEnabled = false;
            }
        }

        //Implemented to prevent passing 'Country' as an argument to multiple methods
        private void CheckForSelectedCountry()
        {
            Country country = (Country)lsBCountries.SelectedItem;
            secondWindow.currentCountry = country;
        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            secondWindow = new SecondWindow(this);
            CheckForSelectedCountry();
            secondWindow.ChangeToDetailsView();
            secondWindow.ShowDialog();

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            secondWindow = new SecondWindow(this);
            secondWindow.DeleteCountry();
        }
    }
}
