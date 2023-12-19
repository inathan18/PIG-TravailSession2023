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
        int selEmp, selProj;
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
            if (Singleton.getInstance().getStatut())
            {
                cbxEmploye.Visibility = Visibility.Visible;
                cbxProjet.Visibility = Visibility.Visible;
                nbxHeures.Visibility = Visibility.Visible;
                btnSave.Visibility = Visibility.Visible;
            }
        }

        private void lvEmployeProjet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Singleton.getInstance().getListeEmployeProjet().Count > 0 && lvEmployeProjet.SelectedItem != null)
                {
                    Employe_projet lvEmproj = lvEmployeProjet.SelectedItem as Employe_projet;
                    tblMatricule.Text = "Matricule: " + lvEmproj.Matricule.ToString();
                    tblNomEmploye.Text = "Nom Employe: " + lvEmproj.NomEmploye.ToString();
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
                salaire = emProj.Salaire;

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
                catch (Exception ex)
                {
                    string s = ex.Message;
                    Console.WriteLine(s);
                }
            }

        }

        

        private void cbxProjet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selProj = -1;
            selProj = cbxProjet.SelectedIndex;
            Console.WriteLine(selProj);
            

        }

        private void cbxEmploye_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selEmp = -1;
            selEmp = cbxEmploye.SelectedIndex;
            Console.WriteLine(selEmp);
            

        }

        private void nbxHeures_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
        {
            nbHeures = nbxHeures.Value;

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Boolean erreurHeure, erreurEmploye, erreurProjet, erreurMultipleProjet;
            noProjet = Singleton.getInstance().getProjet(selProj).NoProjet;
            matricule = Singleton.getInstance().getEmploye(selEmp).Matricule;
            erreurMultipleProjet = Singleton.getInstance().validateEmployeProject(selEmp, selProj);
            

            if (erreurMultipleProjet)
            {
                
                tblErreurMultipleProjet.Visibility = Visibility.Visible;
            }
            else
            {
                tblErreurMultipleProjet.Visibility = Visibility.Collapsed;
            }

            if (string.IsNullOrEmpty(nbxHeures.ToString()) || nbxHeures.ToString() == "NaN")
            {
                tblErrorHeures.Visibility = Visibility.Visible;
                erreurHeure = true;
            }
            else
            {
                tblErrorHeures.Visibility = Visibility.Collapsed;
                erreurHeure = false;

            }
            if (string.IsNullOrEmpty(selEmp.ToString()) || selEmp.ToString() == "NaN")
            {
                tblErrorEmploye.Visibility = Visibility.Visible;
                erreurEmploye = true;

            }
            else
            {
                tblErrorEmploye.Visibility = Visibility.Collapsed;
                erreurEmploye = false;
            }

            if (string.IsNullOrEmpty(selProj.ToString()) || selProj.ToString() == "NaN")
            {
                tblErrorProjet.Visibility = Visibility.Visible;
                erreurProjet = true;
            }
            else
            {
                tblErrorProjet.Visibility = Visibility.Collapsed;
                erreurProjet = false;
            }

            if (erreurProjet == false && erreurEmploye == false && erreurHeure == false && erreurMultipleProjet == false)
            {
                if (string.IsNullOrEmpty(selEmpProjet) || selEmpProjet == "NaN")
                {
                    Employe_projet employe_Projet = new Employe_projet(matricule, noProjet, nbHeures, 0);
                    Singleton.getInstance().AjouterEmployeProjet(employe_Projet);
                }
                else
                {
                    var idProj = Int32.Parse(selEmpProjet);
                    Singleton.getInstance().ModifierEmployeProjet(idProj, matricule, noProjet, nbHeures, salaire);
                }
            }



        }
    }
}
