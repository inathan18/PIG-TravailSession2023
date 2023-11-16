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
using Windows.Globalization.NumberFormatting;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace PIG_TravailSession2023
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageEmploye : Page
    {
        public PageEmploye()
        {
            this.InitializeComponent();
            lvEmploye.ItemsSource = Singleton.getInstance().getListeEmployeBD();
        }

        private void lvEmploye_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Singleton.getInstance().getListeEmploye().Count > 0 && lvEmploye.SelectedItem != null) {
                    Employe lvEmp = lvEmploye.SelectedItem as Employe;
                    tblMatricule.Text = "Matricule: " + lvEmp.Matricule;
                    tblPrenom.Text = "Prénom: " + lvEmp.Prenom;
                    tblNom.Text = "Nom: " + lvEmp.Nom;
                    tblDateNaissance.Text = "Date de naissance: " + lvEmp.DateNaissance;
                    tblEmail.Text = "Courriel: " + lvEmp.Email;
                    tblAdresse.Text = "Adresse: " + lvEmp.Adresse;
                    tblDateEmbauche.Text = lvEmp.DateEmbauche;
                    tblTauxHoraire.Text = lvEmp.TauxHoraire.ToString();
                    imgURI.UriSource = lvEmp.Photo;
                    tblStatut.Text = lvEmp.Statut;
                }
                tbxAdresse.Visibility = Visibility.Collapsed;
                tbxEmail.Visibility = Visibility.Collapsed;
                tbxNom.Visibility = Visibility.Collapsed;
                tbxPhoto.Visibility = Visibility.Collapsed;
                tbxPrenom.Visibility = Visibility.Collapsed;
                tgsStatut.Visibility = Visibility.Collapsed;
                btnSave.Visibility = Visibility.Collapsed;
                tblErrorAdresse.Visibility = Visibility.Collapsed;
                tblErrorEmail.Visibility = Visibility.Collapsed;
                tblErrorNom.Visibility = Visibility.Collapsed;
                tblErrorPhoto.Visibility = Visibility.Collapsed;
                tblErrorPrenom.Visibility = Visibility.Collapsed;
                tblErrorTauxHoraire.Visibility = Visibility.Collapsed;
                
            }

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }
        private void SetNumberBoxNumberFormatter()
        {
            IncrementNumberRounder rounder = new IncrementNumberRounder();
            rounder.Increment = 0.25;
            rounder.RoundingAlgorithm = RoundingAlgorithm.RoundHalfUp;

            DecimalFormatter formatter = new DecimalFormatter();
            formatter.IntegerDigits = 1;
            formatter.FractionDigits = 2;
            formatter.NumberRounder = rounder;
            nbxTauxHoraire.NumberFormatter = formatter;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
