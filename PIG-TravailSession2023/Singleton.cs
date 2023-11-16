using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Windows.ApplicationModel.Resources;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;

namespace PIG_TravailSession2023
{
    class Singleton
    {

        ObservableCollection<Client> listeClient;
        ObservableCollection<Employe> listeEmploye;
        ObservableCollection<Employe_projet> listeEmploye_projet;
        ObservableCollection<Projet> listeProjet;

        static Singleton instance = null;
        MySqlConnection con = new MySqlConnection("Server=cours.cegep3r.info;Database=a2023_420326_gr02_1343683-nathan-lafreniere-racine;Uid=1343683;Pwd1343683");

        public Singleton()
        {
            listeClient = new ObservableCollection<Client>();
            listeEmploye = new ObservableCollection<Employe>();
            listeEmploye_projet = new ObservableCollection<Employe_projet>();
            listeProjet = new ObservableCollection<Projet>();
            //reload();
        }

        public static Singleton getInstance()
        {
            if (instance == null)
            {
                instance = new Singleton();
            }
            return instance;
        }

        public ObservableCollection<Client> getListeClientBD()
        {
            listeClient.Clear();
            MySqlCommand commande = new MySqlCommand("client_select");
            commande.Connection = con;
            commande.CommandType = System.Data.CommandType.StoredProcedure;

            if (con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            MySqlDataReader r = commande.ExecuteReader();

            while (r.Read())
            {
                int id = (int)r["id"];
                string nom = (string)r["nom"];
                string adresse = (string)r["adresse"];
                string numTel = (string)r["numTel"];
                string email = (string)r["email"];
                Client c = new Client { Id = id, Nom = nom, Adresse = adresse, Email = email };

                listeClient.Add(c);
            }
            r.Close();
            con.Close();
            return listeClient;
        }

        public ObservableCollection<Employe> getListeEmployeBD()
        {
            listeEmploye.Clear();
            MySqlCommand commande = new MySqlCommand("employe_select");
            commande.Connection = con;
            commande.CommandType = System.Data.CommandType.StoredProcedure;

            if (con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }

            MySqlDataReader r = commande.ExecuteReader();

            while (r.Read())
            {
                string matricule = (string)r["matricule"];
                string nom = (string)r["nom"];
                string prenom = (string)r["prenom"];
                string email = (string)r["email"];
                string adresse = (string)r["adresse"];
                string statut = (string)r["statut"];
                string dateNaissance = (string)r["dateNaissance"];
                string dateEmbauche = (string)r["dateEmbauche"];
                double tauxHoraire = (double)r["tauxHoraire"];
                Uri photo = new Uri((string)r["photo"], UriKind.Absolute);

                Employe e = new Employe
                {
                    Matricule = matricule,
                    Nom = nom,
                    Prenom = prenom,
                    Email = email,
                    Adresse = adresse,
                    Statut = statut,
                    DateNaissance = dateNaissance,
                    DateEmbauche = dateEmbauche,
                    TauxHoraire = tauxHoraire,
                    Photo = photo
                };

                listeEmploye.Add(e);
            }

            r.Close();
            con.Close();
            return listeEmploye;
        }

        public ObservableCollection<Employe_projet> getListeEmploye_projetBD()
        {
            listeEmploye_projet.Clear();
            MySqlCommand commande = new MySqlCommand("employeprojet_select");
            commande.Connection = con;
            commande.CommandType = System.Data.CommandType.StoredProcedure;

            if (con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }

            MySqlDataReader r = commande.ExecuteReader();

            while (r.Read())
            {

                int id = (int)r["id"];
                string matricule = (string)r["matricule"];
                string noProjet = (string)r["noProjet"];
                double nbHeures = (double)r["nbHeures"];
                double salaire = (double)r["salaire"];

                Employe_projet ep = new Employe_projet
                {
                    Id = id,
                    Matricule = matricule,
                    NoProjet = noProjet,
                    NbHeures = nbHeures,
                    Salaire = salaire
                };

                listeEmploye_projet.Add(ep);
            }
            r.Close();
            con.Close();
            return listeEmploye_projet;
        }

        public ObservableCollection<Projet> getListeProjetBD()
        {
            listeProjet.Clear();
            MySqlCommand commande = new MySqlCommand("projet_select");
            commande.Connection = con;
            commande.CommandType = System.Data.CommandType.StoredProcedure;

            if (con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }

            MySqlDataReader r = commande.ExecuteReader();

            while (r.Read())
            {
                string noProjet = (string)r["noProjet"];
                string titre = (string)r["titre"];
                string dateDebut = (string)r["dateDebut"];
                string description = (string)r["description"];
                string statut = (string)r["statut"];
                int nbEmployes = (int)r["nbEmployes"];
                int client = (int)r["client"];
                double budget = (double)r["budget"];
                double salaires = (double)r["salaires"];

                Projet p = new Projet
                {
                    NoProjet = noProjet,
                    Titre = titre,
                    DateDebut = dateDebut,
                    Description = description,
                    Statut = statut,
                    NbEmployes = nbEmployes,
                    Client = client,
                    Budget = budget,
                    Salaires = salaires,
                };
                listeProjet.Add(p);
            }
            r.Close();
            con.Close();
            return listeProjet;
        }
        public List<Projet> getListeProjetBDCSV()
        {
            
            MySqlCommand commande = new MySqlCommand("projet_select");
            commande.Connection = con;
            commande.CommandType = System.Data.CommandType.StoredProcedure;

            if (con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }

            MySqlDataReader r = commande.ExecuteReader();
            List<Projet> l = new List<Projet>();

            while (r.Read())
            {
                string noProjet = (string)r["noProjet"];
                string titre = (string)r["titre"];
                string dateDebut = (string)r["dateDebut"];
                string description = (string)r["description"];
                string statut = (string)r["statut"];
                int nbEmployes = (int)r["nbEmployes"];
                int client = (int)r["client"];
                double budget = (double)r["budget"];
                double salaires = (double)r["salaires"];

                Projet p = new Projet
                {
                    NoProjet = noProjet,
                    Titre = titre,
                    DateDebut = dateDebut,
                    Description = description,
                    Statut = statut,
                    NbEmployes = nbEmployes,
                    Client = client,
                    Budget = budget,
                    Salaires = salaires,
                };
                l.Add(p);
            }
            r.Close();
            con.Close();
            return l;
        }

        public ObservableCollection<Client> getListeClient()
        {
            return listeClient;
        }

        public ObservableCollection<Projet> getListeProjet()
        {
            return listeProjet;
        }

        public ObservableCollection<Employe_projet> getListeEmployeProjet()
        {
            return listeEmploye_projet;
        }

        public ObservableCollection<Employe> getListeEmploye()
        {
            return listeEmploye;
        }

        public void AjouterEmploye(Employe employe)
        {
            string matricule = employe.Matricule;
            string nom = employe.Nom;
            string prenom = employe.Prenom;
            string email = employe.Email;
            string adresse = employe.Adresse;
            string statut = employe.Statut;
            string dateNaissance = employe.DateNaissance;
            string dateEmbauche = employe.DateEmbauche;
            double tauxHoraire = employe.TauxHoraire;
            Uri photo = employe.Photo;

            try
            {
                MySqlCommand commande = new MySqlCommand("employe_ajout");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("@e_matricule", matricule);
                commande.Parameters.AddWithValue("@e_nom", nom);
                commande.Parameters.AddWithValue("@e_prenom", prenom);
                commande.Parameters.AddWithValue("@e_email", email);
                commande.Parameters.AddWithValue("@e_adresse", adresse);
                commande.Parameters.AddWithValue("@e_statut", statut);
                commande.Parameters.AddWithValue("@e_dateNaissance", DateOnly.Parse(dateNaissance));
                commande.Parameters.AddWithValue("@e_dateEmbauche", DateOnly.Parse(dateEmbauche));
                commande.Parameters.AddWithValue("@e_tauxHoraire", tauxHoraire);
                commande.Parameters.AddWithValue("@e_photo", photo.ToString());

                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                commande.Prepare();
                commande.ExecuteNonQuery();
                con.Close();
                //reload();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void AjouterClient(Client client)
        {
            int id = client.Id;
            string nom = client.Nom;
            string adresse = client.Adresse;
            string numTel = client.NumTel;
            string email = client.Email;

            try
            {
                MySqlCommand commande = new MySqlCommand("client_ajout");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("@c_id", id);
                commande.Parameters.AddWithValue("@c_nom", nom);
                commande.Parameters.AddWithValue("@c_adresse", adresse);
                commande.Parameters.AddWithValue("@c_numTel", numTel);
                commande.Parameters.AddWithValue("@c_email", email);

                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }

                commande.Prepare();
                commande.ExecuteNonQuery();

                con.Close();
                //reload();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AjouterProjet(Projet projet)
        {
            string noProjet = projet.NoProjet;
            string titre = projet.Titre;
            string dateDebut = projet.DateDebut;
            string description = projet.Description;
            string statut = projet.Statut;
            int nbEmployes = projet.NbEmployes;
            int client = projet.Client;
            double budget = projet.Budget;
            double salaires = projet.Salaires;

            try
            {
                MySqlCommand commande = new MySqlCommand("projet_ajout");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("@p_noProjet", noProjet);
                commande.Parameters.AddWithValue("@p_titre", titre);
                commande.Parameters.AddWithValue("@p_dateDebut", DateOnly.Parse(dateDebut));
                commande.Parameters.AddWithValue("@p_description", description);
                commande.Parameters.AddWithValue("@p_statut", statut);
                commande.Parameters.AddWithValue("@p_nbEmployes", nbEmployes);
                commande.Parameters.AddWithValue("@p_client", client);
                commande.Parameters.AddWithValue("@p_budget", budget);
                commande.Parameters.AddWithValue("@p_salaires", salaires);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                commande.Prepare();
                commande.ExecuteNonQuery();

                con.Close();
                //reload();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AjouterEmployeProjet(Employe_projet employe_Projet)
        {
            int id = employe_Projet.Id;
            string matricule = employe_Projet.Matricule;
            string noProjet = employe_Projet.NoProjet;
            double nbHeures = employe_Projet.NbHeures;
            double salaire = employe_Projet.Salaire;

            try
            {
                MySqlCommand commande = new MySqlCommand("employeprojet_ajout");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("@ep_id", id);
                commande.Parameters.AddWithValue("@ep_matricule", matricule);
                commande.Parameters.AddWithValue("@ep_noProjet", noProjet);
                commande.Parameters.AddWithValue("@ep_nbHeures", nbHeures);
                commande.Parameters.AddWithValue("@ep_salaire", salaire);

                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                commande.Prepare();
                commande.ExecuteNonQuery();

                con.Close();
                //reload();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void SupprimerClient(int position)
        {
            try
            {
                int id = listeClient[position].Id;
                MySqlCommand commande = new MySqlCommand("client_delete");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("@c_id", id);

                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                commande.Prepare();
                commande.ExecuteNonQuery();
                con.Close();
                //reload();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void SupprimerEmploye(int position)
        {
            try
            {
                string matricule = listeEmploye[position].Matricule;
                MySqlCommand commande = new MySqlCommand("employe_delete");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("@e_matricule", matricule);

                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                commande.Prepare();
                commande.ExecuteNonQuery();
                con.Close();
                //reload();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void SupprimerProjet(int position)
        {
            try
            {
                string noProjet = listeProjet[position].NoProjet;
                MySqlCommand commande = new MySqlCommand("projet_delete");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("@p_noProjet", noProjet);

                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                commande.Prepare();
                commande.ExecuteNonQuery();
                con.Close();
                //reload();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void SupprimerEmployeProjet(int position)
        {
            try
            {
                int id = listeEmploye_projet[position].Id;
                MySqlCommand commande = new MySqlCommand("employeprojet_delete");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("@ep_id", id);

                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                commande.Prepare();
                commande.ExecuteNonQuery();
                con.Close();
                //reload();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public void ModifierEmploye(string matricule, string nom, string prenom, string email, string adresse, string statut, double tauxHoraire, Uri photo)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("employe_update");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("@e_matricule", matricule);
                commande.Parameters.AddWithValue("@e_nom", nom);
                commande.Parameters.AddWithValue("@e_prenom", prenom);
                commande.Parameters.AddWithValue("@e_email", email);
                commande.Parameters.AddWithValue("@e_adresse", adresse);
                commande.Parameters.AddWithValue("@e_statut", statut);
                commande.Parameters.AddWithValue("@e_tauxHoraire", tauxHoraire);
                commande.Parameters.AddWithValue("@e_photo", photo.ToString());

                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                commande.Prepare();
                commande.ExecuteNonQuery();
                con.Close();
                //reload();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ModifierClient(int id, string nom, string adresse, string numTel, string email)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("client_update");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("@c_id", id);
                commande.Parameters.AddWithValue("@c_nom", nom);
                commande.Parameters.AddWithValue("@c_adresse", adresse);
                commande.Parameters.AddWithValue("@c_numTel", numTel);
                commande.Parameters.AddWithValue("@c_email", email);

                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }

                commande.Prepare();
                commande.ExecuteNonQuery();

                con.Close();
                //reload();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void ModifierProjet (string noProjet, string titre, string dateDebut, string description, string statut, int nbEmployes, int client, double budget, double salaires)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("projet_update");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("@p_noProjet", noProjet);
                commande.Parameters.AddWithValue("@p_titre", titre);
                commande.Parameters.AddWithValue("@p_dateDebut", DateOnly.Parse(dateDebut));
                commande.Parameters.AddWithValue("@p_description", description);
                commande.Parameters.AddWithValue("@p_statut", statut);
                commande.Parameters.AddWithValue("@p_nbEmployes", nbEmployes);
                commande.Parameters.AddWithValue("@p_client", client);
                commande.Parameters.AddWithValue("@p_budget", budget);
                commande.Parameters.AddWithValue("@p_salaires", salaires);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                commande.Prepare();
                commande.ExecuteNonQuery();

                con.Close();
                //reload();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void ModifierEmployeProjet(int id, string matricule, string noProjet, double nbHeures, double salaire)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("employeprojet_update");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("@ep_id", id);
                commande.Parameters.AddWithValue("@ep_matricule", matricule);
                commande.Parameters.AddWithValue("@ep_noProjet", noProjet);
                commande.Parameters.AddWithValue("@ep_nbHeures", nbHeures);
                commande.Parameters.AddWithValue("@ep_salaire", salaire);

                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                commande.Prepare();
                commande.ExecuteNonQuery();

                con.Close();
                //reload();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void reload()
        {
            listeClient.Clear();
            listeEmploye.Clear();
            listeProjet.Clear();
            listeEmploye_projet.Clear();
        }

        private void reloadEmploye()
        {
            listeEmploye.Clear();
            MySqlCommand commande = new MySqlCommand("employe_select");
            commande.Connection = con;
            commande.CommandType = System.Data.CommandType.StoredProcedure;

            if (con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }

            MySqlDataReader r = commande.ExecuteReader();

            while (r.Read())
            {
                string matricule = (string)r["matricule"];
                string nom = (string)r["nom"];
                string prenom = (string)r["prenom"];
                string email = (string)r["email"];
                string adresse = (string)r["adresse"];
                string statut = (string)r["statut"];
                string dateNaissance = (string)r["dateNaissance"];
                string dateEmbauche = (string)r["dateEmbauche"];
                double tauxHoraire = (double)r["tauxHoraire"];
                Uri photo = new Uri((string)r["photo"], UriKind.Absolute);

                Employe e = new Employe
                {
                    Matricule = matricule,
                    Nom = nom,
                    Prenom = prenom,
                    Email = email,
                    Adresse = adresse,
                    Statut = statut,
                    DateNaissance = dateNaissance,
                    DateEmbauche = dateEmbauche,
                    TauxHoraire = tauxHoraire,
                    Photo = photo
                };

                listeEmploye.Add(e);
            }

            r.Close();
            con.Close();
            


        }


    }
}
