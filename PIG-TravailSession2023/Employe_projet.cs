using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIG_TravailSession2023
{
    class Employe_projet
    {
        int id;
        string matricule, noProjet, nomEmploye, titre;
        double nbHeures, salaire;

        public Employe_projet()
        {

        }

        public Employe_projet(int id, string matricule, string noProjet, double nbHeures, double salaire)
        {
            this.id = id;
            this.matricule = matricule;
            this.noProjet = noProjet;
            this.nbHeures = nbHeures;
            this.salaire = salaire;
        }

        public Employe_projet(string matricule, string noProjet, double nbHeures, double salaire)
        {
            this.matricule = matricule;
            this.noProjet = noProjet;
            this.nbHeures = nbHeures;
            this.salaire = salaire;
        }
        public Employe_projet(string matricule, string noProjet, double nbHeures, double salaire, string nomEmploye, string titre)
        {
            this.matricule = matricule;
            this.noProjet = noProjet;
            this.nbHeures = nbHeures;
            this.salaire = salaire;
            this.nomEmploye = nomEmploye;
            this.titre = titre;

        }

        public int Id
        {
            get => id; set => id = value;
        }

        public string Matricule
        {
            get => matricule;
            set => matricule = value;
        }

        public string NoProjet
        {
            get => noProjet; set => noProjet = value;
        }
        public string NomEmploye
        {
            get => nomEmploye; set => nomEmploye = value;
        }
        public string Titre
        {
            get => titre; set => titre = value;
        }

        public double NbHeures
        {
            get => nbHeures; set => nbHeures = value;
        }

        public double Salaire
        {
            get => salaire; set => salaire = value;
        }

        public string toCSV()
        {
            return id.ToString() + ';' + matricule + ';' + noProjet + ";" + nbHeures.ToString() + ';' + salaire.ToString() + ';' + nomEmploye.ToString() + ';' + titre.ToString();
        }
    }
}
