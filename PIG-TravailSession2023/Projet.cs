using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIG_TravailSession2023
{
    class Projet
    {

        string noProjet, titre, dateDebut, description, statut;
        int nbEmployes, client;
        double budget, salaires;

        public Projet()
        {

        }

        public Projet(string noProjet, string titre, string dateDebut, string description, string statut, int nbEmployes, int client, double budget, double salaires)
        {
            this.noProjet = noProjet;
            this.titre = titre;
            this.dateDebut = dateDebut;
            this.description = description;
            this.statut = statut;
            this.nbEmployes = nbEmployes;
            this.client = client;
            this.budget = budget;
            this.salaires = salaires;
        }

        public string NoProjet
        {
            get => noProjet;
            set => noProjet = value;
        }

        public string Titre
        {
            get => titre;
            set => titre = value;
        }

        public string DateDebut
        {
            get => dateDebut; set => dateDebut = value;
        }

        public string Description
        {
            get => description; set => description = value;
        }

        public string Statut
        {
            get => statut; set => statut = value;
        }

        public int NbEmployes { get => nbEmployes; set => nbEmployes = value; }

        public int Client
        {
            get => client; set => client = value;
        }

        public double Budget { get => budget; set => budget = value; }

        public double Salaires
        {
            get => salaires;
            set => salaires = value;
        }

        public string toCSV()
        {
            return noProjet + ';' + titre + ';' + dateDebut + ';' + description + ";" + statut + ";" + nbEmployes.ToString() + ";" + client.ToString() + ';' + budget.ToString() + ';' + statut.ToString();
        }
    }
}
