using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace PIG_TravailSession2023
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageClient : Page
    {
        string nom, adresse, numTel, email;
        int selClient;
        public PageClient()
        {
            this.InitializeComponent();
            lvClient.ItemsSource = Singleton.getInstance().getListeClientBD();
        }

        private void lvClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Singleton.getInstance().getListeClient().Count > 0 && lvClient.SelectedItem != null)
                {
                    Client lvCli = lvClient.SelectedItem as Client;
                    tblAdresse.Text = "Adresse: " + lvCli.Adresse;
                    tblEmail.Text = "Courriel: " + lvCli.Email;
                    tblId.Text = "ID: " + lvCli.Id.ToString();
                    tblNom.Text = "Nom: " + lvCli.Nom;
                    tblNumTel.Text = "Numéro de téléphone: " + lvCli.NumTel;
                }
                tbxAdresse.Visibility = Visibility.Visible;
                tbxEmail.Visibility = Visibility.Collapsed;
                tbxNom.Visibility = Visibility.Collapsed;
                tbxNumTel.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            tbxAdresse.Visibility = Visibility.Visible;
            tbxEmail.Visibility = Visibility.Visible;
            tbxNom.Visibility = Visibility.Visible;
            tbxNumTel.Visibility = Visibility.Visible;

            var cli = Singleton.getInstance().getClient(lvClient.SelectedIndex);

            tbxAdresse.Text = cli.Adresse.ToString();
            tbxEmail.Text = cli.Email.ToString();
            tbxNom.Text = cli.Nom.ToString();
            tbxNumTel.Text = cli.NumTel.ToString();

            selClient = cli.Id;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Singleton.getInstance().SupprimerClient(lvClient.SelectedIndex);
            }
            catch (Exception ex)
            {
                string s = ex.Message; Console.WriteLine(s);
            }
        }

        private void tbxNom_SelectionChanged(object sender, RoutedEventArgs e)
        {
            nom = tbxNom.Text;
        }

        private void tbxEmail_SelectionChanged(object sender, RoutedEventArgs e)
        {
            email = tbxEmail.Text;
        }

        private void tbxAdresse_SelectionChanged(object sender, RoutedEventArgs e)
        {
            adresse = tbxAdresse.Text;
        }

        private void tbxNumTel_SelectionChanged(object sender, RoutedEventArgs e)
        {
            numTel = tbxNumTel.Text;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            int id = selClient;
            Boolean erreurNom = false, erreurEmail = false, erreurAdresse = false, erreurNumTel = false;

            if (string.IsNullOrEmpty(nom))
            {
                tblErrorNom.Visibility = Visibility.Visible;
                erreurNom = true;
            }
            else
            {
                tblErrorNom.Visibility = Visibility.Collapsed;
                erreurNom = false;
            }
            if (string.IsNullOrEmpty(email))
            {
                tblErrorEmail.Visibility = Visibility.Visible;
                erreurEmail = true;
            }
            else
            {
                tblErrorEmail.Visibility = Visibility.Collapsed;
                erreurEmail = false;
            }
            if (string.IsNullOrEmpty(adresse))
            {
                tblErrorAdresse.Visibility = Visibility.Visible;
                erreurAdresse = true;
            }
            else
            {
                tblErrorAdresse.Visibility = Visibility.Collapsed;
                erreurAdresse = false;
            }
            if (string.IsNullOrEmpty(numTel))
            {
                tblErrorNumTel.Visibility = Visibility.Visible;
                erreurNumTel = true;
            }
            else
            {
                tblErrorNumTel.Visibility = Visibility.Collapsed;
                erreurNumTel = false;
            }

            if (erreurAdresse == false && erreurEmail == false && erreurNom == false && erreurNumTel == false)
            {
                Singleton.getInstance().ModifierClient(selClient, nom, adresse, numTel, email);
            }

        }
    }
}
