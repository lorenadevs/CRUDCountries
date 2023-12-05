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
using System.Windows.Shapes;

namespace CRUDMenu
{
    /// <summary>
    /// </summary>
    public partial class SecondWindow : Window
    {

        MainWindow mainWindow;
        internal Country currentCountry; //needed Object to reduce calling almost every function with Country as a param. It's the country we are working with while modifying, getting details or deleting.

        public SecondWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            //currentCountry = new Country();
        }

        //Calls diferent methods depending on the selected CRUD action
        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {

            bool invalid = string.IsNullOrEmpty(txBCountryName.Text) || string.IsNullOrEmpty(txBCountryCode.Text);

            if (!invalid)
            {

                if (lblActionTitle.Content.ToString() == "adding")
                {
                    AddCountry();
                }
                else if (lblActionTitle.Content.ToString() == "modifying")
                {
                    ModifyCountry();

                }
                else if (lblActionTitle.Content.ToString() == "details")
                {
                    GetDetails();
                }
                else
                {
                    DeleteCountry();
                }
            }
            else
            {
                MessageBox.Show("Make sure you fill both; the country code and the country name!", "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
            }

        }

        //Prevents string inputs
        private void txBCountryCode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0)) // text, position
            {
                lblNumericInfo.Visibility = Visibility.Visible;

                e.Handled = true; //ignore user's interaction

            }
            else
            {
                lblNumericInfo.Visibility = Visibility.Hidden;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        internal void ChangeToAddCountryView()
        {
            lblActionTitle.Content = "adding";
            lblActionSubtitle.Content = "a new country";

        }

        private void AddCountry()
        {
            Country country = new Country(int.Parse(txBCountryCode.Text), txBCountryName.Text);
            mainWindow.AddCountryToList(country);
            this.Close();
        }


        internal void ChangeToModifyView()
        {
            lblActionTitle.Content = "modifying";
            lblActionSubtitle.Content = "a country";
            txBCountryCode.Text = currentCountry.Code.ToString();
            txBCountryName.Text = currentCountry.Name;
        }


        private void ModifyCountry()
        {
            currentCountry = new Country(int.Parse(txBCountryCode.Text), txBCountryName.Text);
            currentCountry.Name = txBCountryName.Text;
            currentCountry.Code = int.Parse(txBCountryCode.Text);

            mainWindow.lsBCountries.Items[mainWindow.lsBCountries.SelectedIndex] = currentCountry;

            //mainWindow.lsBCountries.Items[mainWindow.lsBCountries.SelectedIndex] = new Country(int.Parse(txBCountryCode.Text), txBCountryName.Text);

            this.Close();

        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }

        internal void ChangeToDetailsView()
        {
            lblActionTitle.Content = "details";
            lblActionSubtitle.Content = "of the selected country";
            txBCountryCode.Text = currentCountry.Code.ToString();
            txBCountryCode.IsEnabled = false;
            txBCountryName.Text = currentCountry.Name;
            txBCountryName.IsEnabled = false;
            btnCancel.Visibility = Visibility.Collapsed;
            btnAccept.HorizontalAlignment = HorizontalAlignment.Center;
            btnAccept.VerticalAlignment = VerticalAlignment.Center;
            btnAccept.Margin = new Thickness(0, 0, 0, 0); //To keep it aligned even with the non-visible "cancel" button

        }

        private void GetDetails()
        {
            lblCode.IsEnabled = true;
            lblName.IsEnabled = true;
            this.Close();
        }

        private void txBCountryCode_TextChanged(object sender, TextChangedEventArgs e)
        {


        }


        //Removes the country by getting the selected index at the countries listBox
        internal void DeleteCountry()
        {
            mainWindow.lsBCountries.Items.RemoveAt(mainWindow.lsBCountries.SelectedIndex);
        }
    }
}
