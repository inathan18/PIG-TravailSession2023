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
    public sealed partial class PageProjet : Page
    {
        public PageProjet()
        {
            this.InitializeComponent();
        }

        private void lvProjet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Singleton.getInstance().getListeProjet().Count > 0 && lvProjet.SelectedItem != null)
                {
                    Projet lvProj = lvProjet.SelectedItem as Projet;
                    tblBudget.Text = "Budget: " + lvProj.Budget.ToString() + " $";
                    tblClient.Text = "Client: " + lvProj.Client.ToString();
                    tblDateDebut.Text = "Date de début: " + lvProj.DateDebut.ToString();
                    tblDescription.Text = "Description: " + lvProj.Description;
                    tblNbEmployes.Text = "Nombre d'employés: " + lvProj.NbEmployes.ToString();
                    tblNoProjet.Text = "Numéro de projet: " + lvProj.NoProjet;
                    tblStatut.Text = "Statut: " + lvProj.Statut;
                    tblTotalSalaire.Text = "Salaires: " + lvProj.Salaires.ToString();
                    tblTitre.Text = "Titre: " + lvProj.Titre;
                }

                tbxDescription.Visibility = Visibility.Collapsed;
                tbxTitre.Visibility = Visibility.Collapsed;
                nbxBudget.Visibility = Visibility.Collapsed;
                nbxNbEmployes.Visibility = Visibility.Collapsed;
                tgsStatut.Visibility = Visibility.Collapsed;
                dpDateDebut.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tbxTitre_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void dpDateDebut_SelectedDateChanged(DatePicker sender, DatePickerSelectedValueChangedEventArgs args)
        {

        }

        private void tbxDescription_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void nbxBudget_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
        {

        }

        private void nbxNbEmployes_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
        {

        }

        private void tgsStatut_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
