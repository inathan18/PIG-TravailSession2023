using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIG_TravailSession2023
{
    class Employe
    {
        string matricule, nom, prenom, email, adresse, statut, dateNaissance, dateEmbauche;
        double tauxHoraire;
        Uri photo;

        public Employe()
        {

        }

        public Employe(string matricule, string nom, string prenom, string email, string adresse, string statut, string dateNaissance, string dateEmbauche, double tauxHoraire, Uri photo)
        {
            this.matricule = matricule;
            this.nom = nom;
            this.prenom = prenom;
            this.email = email;
            this.adresse = adresse;
            this.statut = statut;
            this.dateNaissance = dateNaissance;
            this.dateEmbauche = dateEmbauche;
            this.tauxHoraire = tauxHoraire;
            this.photo = photo;
        }
        public Employe( string nom, string prenom, string email, string adresse, string statut, string dateNaissance, string dateEmbauche, double tauxHoraire, Uri photo)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.email = email;
            this.adresse = adresse;
            this.statut = statut;
            this.dateNaissance = dateNaissance;
            this.dateEmbauche = dateEmbauche;
            this.tauxHoraire = tauxHoraire;
            this.photo = photo;
        }
        public Employe(string nom, string prenom, string email, string adresse, string statut,  double tauxHoraire, Uri photo)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.email = email;
            this.adresse = adresse;
            this.statut = statut;
            this.tauxHoraire = tauxHoraire;
            this.photo = photo;
        }

        public string Matricule
        {
            get => matricule;
            set=> matricule = value;
        }

        public string Nom
        {
            get => nom;
            set => nom = value;
        }

        public string Prenom
        {
            get=> nom; set => nom = value;
        }

        public string Email
        {
            get => email; set => email = value;
        }

        public string Adresse
        {
            get => adresse; set => adresse = value;
        }

        public string Statut
        {
            get => statut; set => statut = value;
        }

        public string DateNaissance
        {
            get => dateNaissance; set => dateNaissance = value;
        }

        public string DateEmbauche
        {
            get => dateEmbauche; set => dateEmbauche = value;
        }

        public double TauxHoraire
        {
            get => tauxHoraire; set => tauxHoraire = value;
        }

        public Uri Photo
        {
            get => photo; set => photo = value;
        }

        public string toCSV()
        {
            return matricule + ';' + nom + ';' + prenom + ";" + email + ";" + adresse + ";" + statut + ";" + dateNaissance + ";" + dateEmbauche + tauxHoraire.ToString() + ";" + photo.ToString();
        }
    }
}
