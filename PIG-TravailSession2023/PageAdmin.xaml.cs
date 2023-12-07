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
    public sealed partial class PageAdmin : Page
    {
        string matricule, noProjet, nomEmploye, titre, selEmpProjet;
        double nbHeures, salaire;
        public PageAdmin()
        {
            this.InitializeComponent();
            lvEmployeProjet.ItemsSource = Singleton.getInstance().getListeEmploye_projetBD();
            cbxEmploye.ItemsSource = Singleton.getInstance().getListeEmployeBD();
            cbxProjet.ItemsSource = Singleton.getInstance().getListeProjetBD();
            if (Singleton.getInstance().getListeEmployeProjet().Count > 0)
            {
                tblEmpty.Visibility = Visibility.Collapsed;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lvEmployeProjet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Singleton.getInstance().getListeEmployeProjet().Count > 0 && lvEmployeProjet.SelectedItem != null)
                {
                    Employe_projet lvEmproj = lvEmployeProjet.SelectedItem as Employe_projet;
                    tblMatricule.Text = "Matricule: " + lvEmproj.Matricule.ToString();
                    tblNomEmploye.Text = "Nom: " + lvEmproj.NomEmploye.ToString();
                    tblNoProjet.Text = "No Projet: " + lvEmproj.NoProjet.ToString();
                    tblTitre.Text = "Titre projet: " + lvEmproj.Titre.ToString();
                    tblNbHeures.Text = "Nb Heures: " + lvEmproj.NbHeures.ToString();
                    tblSalaire.Text = "Salaire: " + lvEmproj.Salaire.ToString() + " $";
                    selectedView.Visibility = Visibility.Visible;
                    btnEdit.Visibility = Visibility.Visible;
                    btnDelete.Visibility = Visibility.Visible;
                    selEmpProjet = "";
                }

                cbxEmploye.Visibility = Visibility.Collapsed;
                cbxProjet.Visibility = Visibility.Collapsed;
                nbxHeures.Visibility = Visibility.Collapsed;
                btnSave.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (Singleton.getInstance().getStatut())
            {
                cbxEmploye.Visibility = Visibility.Visible;
                cbxProjet.Visibility = Visibility.Visible;
                nbxHeures.Visibility = Visibility.Visible;

                var emProj = Singleton.getInstance().getEmploye_Projet(lvEmployeProjet.SelectedIndex);
                cbxEmploye.SelectedValue = emProj.Matricule;
                cbxProjet.SelectedValue = emProj.NoProjet;

                selEmpProjet = emProj.Id.ToString();
                
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (Singleton.getInstance().getStatut())
            {
                try
                {
                    Singleton.getInstance().SupprimerEmployeProjet(lvEmployeProjet.SelectedIndex);
                }
            }

        }

        private void cbxProjet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbxEmploye_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void nbxHeures_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
