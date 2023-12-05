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
        string nom, prenom, email, adresse, urlPhoto, statut, selEmploye, dateNaissance, dateEmbauche;
        double tauxHoraire;
        public PageEmploye()
        {
            this.InitializeComponent();

            lvEmploye.ItemsSource = Singleton.getInstance().getListeEmployeBD();
            if (Singleton.getInstance().getListeEmploye().Count > 0 )
            {
                tblEmpty.Visibility = Visibility.Collapsed;
            }
        }

        private void lvEmploye_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Singleton.getInstance().getListeEmploye().Count > 0 && lvEmploye.SelectedItem != null)
                {
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
                    selectedView.Visibility = Visibility.Visible;
                    btnDelete.Visibility = Visibility.Visible;
                    btnEdit.Visibility = Visibility.Visible;
                    selEmploye = "";
                }


                tbxAdresse.Visibility = Visibility.Collapsed;
                tbxEmail.Visibility = Visibility.Collapsed;
                tbxNom.Visibility = Visibility.Collapsed;
                tbxPhoto.Visibility = Visibility.Collapsed;
                tbxPrenom.Visibility = Visibility.Collapsed;
                tgsStatut.Visibility = Visibility.Collapsed;
                nbxTauxHoraire.Visibility = Visibility.Collapsed;
                btnSave.Visibility = Visibility.Collapsed;
                tblErrorAdresse.Visibility = Visibility.Collapsed;
                tblErrorEmail.Visibility = Visibility.Collapsed;
                tblErrorNom.Visibility = Visibility.Collapsed;
                tblErrorPhoto.Visibility = Visibility.Collapsed;
                tblErrorPrenom.Visibility = Visibility.Collapsed;
                tblErrorTauxHoraire.Visibility = Visibility.Collapsed;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

            if (Singleton.getInstance().getStatut())
            {
                tbxPrenom.Visibility = Visibility.Visible;
                tbxNom.Visibility = Visibility.Visible;
                tbxEmail.Visibility = Visibility.Visible;
                tbxAdresse.Visibility = Visibility.Visible;
                nbxTauxHoraire.Visibility = Visibility.Visible;
                tbxPhoto.Visibility = Visibility.Visible;
                tgsStatut.Visibility = Visibility.Visible;
                btnSave.Visibility = Visibility.Visible;

                var emp = Singleton.getInstance().getEmploye(lvEmploye.SelectedIndex);
                tbxAdresse.Text = emp.Adresse.ToString();
                tbxEmail.Text = emp.Email.ToString();
                tbxNom.Text = emp.Nom.ToString();
                tbxPhoto.Text = emp.Photo.ToString();
                tbxPrenom.Text = emp.Prenom.ToString();
                nbxTauxHoraire.Value = emp.TauxHoraire;

                if (emp.Statut == "Permanent")
                {
                    tgsStatut.IsOn = true;
                }
                else
                {
                    tgsStatut.IsOn = false;
                }

                selEmploye = emp.Matricule;

            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (Singleton.getInstance().getStatut())
            {
                try
                {
                    Singleton.getInstance().SupprimerEmploye(lvEmploye.SelectedIndex);
                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                }

            }



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
            string matricule = selEmploye;
            Boolean erreurNom = false, erreurPrenom = false, erreurEmail = false, erreurAdresse = false, erreurTauxHoraire = false, erreurPhoto = false;

            if (string.IsNullOrEmpty(prenom))
            {
                tblErrorPrenom.Visibility = Visibility.Visible;
                erreurPrenom = true;
            }
            else
            {
                tblErrorPrenom.Visibility = Visibility.Collapsed;
                erreurPrenom = false;
            }
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
            if (string.IsNullOrEmpty(tauxHoraire.ToString()) || tauxHoraire.ToString() == "NaN")
            {
                tblErrorTauxHoraire.Visibility = Visibility.Visible;
                erreurTauxHoraire = true;
            }
            else
            {
                tblErrorTauxHoraire.Visibility = Visibility.Collapsed;
                erreurTauxHoraire = false;
            }
            if (string.IsNullOrEmpty(urlPhoto))
            {
                tblErrorPhoto.Visibility = Visibility.Visible;
                erreurPhoto = true;
            }
            else
            {
                tblErrorPhoto.Visibility = Visibility.Collapsed;
                erreurPhoto = false;
            }

            if (erreurAdresse == false && erreurEmail == false && erreurNom == false && erreurPhoto == false && erreurPrenom == false && erreurTauxHoraire == false)
            {
                if (string.IsNullOrEmpty(selEmploye.ToString()))
                {
                    Uri photo = new Uri(urlPhoto);
                    Employe employe = new Employe(nom, prenom, email, adresse, statut, dateNaissance, dateEmbauche, tauxHoraire, photo);
                    Singleton.getInstance().AjouterEmploye(employe);
                }
                else
                {
                    Singleton.getInstance().ModifierEmploye(matricule, nom, prenom, email, adresse, statut, tauxHoraire, new Uri(urlPhoto));
                }
            }
        }

        private void tbxPrenom_SelectionChanged(object sender, RoutedEventArgs e)
        {
            prenom = tbxPrenom.Text;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (Singleton.getInstance().getStatut())
            {
                selEmploye = "";
                tbxPrenom.Visibility = Visibility.Visible;
                tbxNom.Visibility = Visibility.Visible;
                tbxEmail.Visibility = Visibility.Visible;
                tbxAdresse.Visibility = Visibility.Visible;
                nbxTauxHoraire.Visibility = Visibility.Visible;
                tbxPhoto.Visibility = Visibility.Visible;
                tgsStatut.Visibility = Visibility.Visible;
                btnSave.Visibility = Visibility.Visible;
                dpDateEmbauche.Visibility = Visibility.Visible;
                dpDateNaissance.Visibility = Visibility.Visible;
            }
            else
            {

            }


        }

        private void dpDateEmbauche_SelectedDateChanged(DatePicker sender, DatePickerSelectedValueChangedEventArgs args)
        {
            dateEmbauche = dpDateEmbauche.SelectedDate.Value.ToString("yyyy-MM-dd");

        }

        private void dpDateNaissance_SelectedDateChanged(DatePicker sender, DatePickerSelectedValueChangedEventArgs args)
        {
            dateNaissance = dpDateNaissance.SelectedDate.Value.ToString("yyyy-MM-dd");

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

        private void nbxTauxHoraire_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
        {
            tauxHoraire = nbxTauxHoraire.Value;
        }

        private void tbxPhoto_SelectionChanged(object sender, RoutedEventArgs e)
        {
            urlPhoto = tbxPhoto.Text;
        }

        private void tgsStatut_Toggled(object sender, RoutedEventArgs e)
        {
            if (tgsStatut.IsOn == true)
            {
                statut = "Permanent";
            }
            else
            {
                statut = "Journalier";
            }
        }
    }
}
