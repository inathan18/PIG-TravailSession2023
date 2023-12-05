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
using Windows.ApplicationModel.UserDataTasks.DataProvider;
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
        string description, titre, dateDebut, statut, selProjet, noProjet;
        double budget;
        int nbEmployes, client;
        public PageProjet()
        {
            this.InitializeComponent();
            lvProjet.ItemsSource = Singleton.getInstance().getListeProjetBD();
            cbxClient.ItemsSource = Singleton.getInstance().getListeClientBD();
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
                    selectedView.Visibility = Visibility.Visible;
                    btnEdit.Visibility = Visibility.Visible;
                    btnDelete.Visibility = Visibility.Visible;
                    selProjet = "";

                }

                tbxDescription.Visibility = Visibility.Collapsed;
                tbxTitre.Visibility = Visibility.Collapsed;
                nbxBudget.Visibility = Visibility.Collapsed;
                nbxNbEmployes.Visibility = Visibility.Collapsed;
                tgsStatut.Visibility = Visibility.Collapsed;
                dpDateDebut.Visibility = Visibility.Collapsed;
                cbxClient.Visibility = Visibility.Collapsed;
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
                tbxDescription.Visibility = Visibility.Visible;
                tbxTitre.Visibility = Visibility.Visible;
                nbxBudget.Visibility = Visibility.Visible;
                nbxNbEmployes.Visibility = Visibility.Visible;
                tgsStatut.Visibility = Visibility.Visible;
                dpDateDebut.Visibility = Visibility.Visible;
                cbxClient.Visibility = Visibility.Visible;

                var proj = Singleton.getInstance().getProjet(lvProjet.SelectedIndex);
                tbxDescription.Text = proj.Description.ToString();
                tbxTitre.Text = proj.Titre.ToString();
                nbxBudget.Value = proj.Budget;
                nbxNbEmployes.Value = proj.NbEmployes;
                dpDateDebut.SelectedDate = DateTime.Parse(proj.DateDebut);
                cbxClient.SelectedValue = proj.Client;

                if (proj.Statut == "Terminé")
                {
                    tgsStatut.IsEnabled = true;
                }
                else
                {
                    tgsStatut.IsEnabled = false;
                }

                selProjet = proj.NoProjet;
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

            if (Singleton.getInstance().getStatut())
            {
                try
                {
                    Singleton.getInstance().SupprimerProjet(lvProjet.SelectedIndex);
                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                    Console.WriteLine(s);
                }


            }

        }

        private void tbxTitre_SelectionChanged(object sender, RoutedEventArgs e)
        {
            titre = tbxTitre.Text;

        }

        private void dpDateDebut_SelectedDateChanged(DatePicker sender, DatePickerSelectedValueChangedEventArgs args)
        {
            dateDebut = dpDateDebut.ToString();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (Singleton.getInstance().getStatut())
            {
                selProjet = "";
                tbxDescription.Visibility = Visibility.Visible;
                tbxTitre.Visibility = Visibility.Visible;
                nbxBudget.Visibility = Visibility.Visible;
                nbxNbEmployes.Visibility = Visibility.Visible;
                tgsStatut.Visibility = Visibility.Visible;
                dpDateDebut.Visibility = Visibility.Visible;
                cbxClient.Visibility = Visibility.Visible;

            }


        }

        private void cbxClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Client lvClie = cbxClient.SelectedItem as Client;
            client = lvClie.Id;
        }

        private void tbxDescription_SelectionChanged(object sender, RoutedEventArgs e)
        {
            description = tbxDescription.Text;
        }

        private void nbxBudget_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
        {
            budget = nbxBudget.Value;
        }

        private void nbxNbEmployes_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
        {
            nbEmployes = (int)nbxNbEmployes.Value;
        }

        private void tgsStatut_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (tgsStatut.IsEnabled)
            {
                statut = "Terminé";
            }
            else
            {
                statut = "En cours";
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string noProj = selProjet;
            Boolean erreurTitre = false, erreurDescription = false, erreurDateDebut = false, erreurBudget = false, erreurNbEmployes = false, erreurClient = false;

            if (string.IsNullOrEmpty(titre))
            {
                tblErrorTitre.Visibility = Visibility.Visible;
                erreurTitre = true;
            }
            else
            {
                tblErrorTitre.Visibility = Visibility.Collapsed;
                erreurTitre = false;
            }
            if (string.IsNullOrEmpty(description))
            {
                tblErrorDescription.Visibility = Visibility.Visible;
                erreurDescription = true;
            }
            else
            {
                tblErrorDescription.Visibility = Visibility.Collapsed;
                erreurDescription = false;
            }
            if (string.IsNullOrEmpty(budget.ToString()) || budget.ToString() == "NaN")
            {
                tblErrorBudget.Visibility = Visibility.Visible;
                erreurBudget = true;
            }
            else
            {
                tblErrorBudget.Visibility = Visibility.Collapsed;
                erreurBudget = false;
            }
            if (string.IsNullOrEmpty(dateDebut))
            {
                tblErrorDateDebut.Visibility = Visibility.Visible;
                erreurDateDebut = true;
            }
            else
            {
                tblErrorDateDebut.Visibility = Visibility.Collapsed;
                erreurDateDebut = false;
            }
            if (string.IsNullOrEmpty(nbEmployes.ToString()) || nbEmployes.ToString() == "NaN")
            {
                tblErrorNbEmployes.Visibility = Visibility.Visible;
                erreurNbEmployes = true;
            }
            else
            {
                tblErrorNbEmployes.Visibility = Visibility.Collapsed;
                erreurNbEmployes = false;
            }
            if (string.IsNullOrEmpty(client.ToString()) || client.ToString() == "NaN")
            {
                tblErrorClient.Visibility = Visibility.Visible;
                erreurClient = true;
            }
            else
            {
                tblErrorClient.Visibility = Visibility.Collapsed;
                erreurClient = false;
            }

            if (erreurBudget == false && erreurDateDebut == false && erreurDescription == false && erreurNbEmployes == false && erreurTitre == false && erreurClient == false)
            {
                if (string.IsNullOrEmpty(selProjet))
                {
                    Projet projet = new Projet(titre, dateDebut, description, statut, nbEmployes, client, budget, 0);
                    Singleton.getInstance().AjouterProjet(projet);
                }
                else
                {
                    Singleton.getInstance().ModifierProjet(noProjet, titre, dateDebut, description, statut, nbEmployes, client, budget);
                }
            }
        }
    }
}
