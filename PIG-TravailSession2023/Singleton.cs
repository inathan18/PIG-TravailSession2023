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
using System.Security.Cryptography;
using System.IO;


namespace PIG_TravailSession2023
{
    class Singleton
    {

        ObservableCollection<Client> listeClient;
        ObservableCollection<Employe> listeEmploye;
        ObservableCollection<Employe_projet> listeEmploye_projet;
        ObservableCollection<Projet> listeProjet;
        ObservableCollection<User> listeUser;
        bool loginStatus = false;

        static Singleton instance = null;
        MySqlConnection con = new MySqlConnection("Server=cours.cegep3r.info;Database=a2023_420325ri_fabeq26;Uid=1343683;Pwd=1343683");

        public Singleton()
        {
            listeClient = new ObservableCollection<Client>();
            listeEmploye = new ObservableCollection<Employe>();
            listeEmploye_projet = new ObservableCollection<Employe_projet>();
            listeProjet = new ObservableCollection<Projet>();
            listeUser = new ObservableCollection<User>();
            reload();
        }

        public static Singleton getInstance()
        {
            if (instance == null)
            {
                instance = new Singleton();
            }
            return instance;
        }

        public bool getStatut()
        {
            return loginStatus;
        }

        public string genererSHA256(string texte)
        {
            var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(texte));

            StringBuilder sb = new StringBuilder();
            foreach (Byte b in bytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }


        public bool AdminLogin(string user, string pass)
        {
            try
            {
                
                MySqlCommand commande = new MySqlCommand("login");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("@usager", user);

                con.Open();
                commande.Prepare();

                MySqlDataReader r = commande.ExecuteReader();
                if (r.Read())
                {
                    string username = (string)r["user"];
                    string password = (string)r["password"];

                    if(password == pass)
                    {
                        loginStatus = true;
                    }
                    else
                    {
                        loginStatus = false;
                    }
                }
                r.Close();
                con.Close();
                return loginStatus;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        public ObservableCollection<Client> getListeClientBD()
        {
            listeClient.Clear();
            MySqlCommand commande = new MySqlCommand("get_allclients");
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
                Client c = new Client { Id = id, Nom = nom, Adresse = adresse, NumTel = numTel, Email = email };

                listeClient.Add(c);
            }
            r.Close();
            con.Close();
            return listeClient;
        }

        public ObservableCollection<Employe> getListeEmployeBD()
        {
            listeEmploye.Clear();
            MySqlCommand commande = new MySqlCommand("get_allemployees");
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
                string dateNaissance = Convert.ToString(r["dateNaissance"]);
                string dateEmbauche = Convert.ToString(r["dateEmbauche"]);
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
            MySqlCommand commande = new MySqlCommand("get_allemployeeprojects");
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
                string nomEmploye = (string)r["Nom employé"];
                string titre = (string)r["titre"];

                Employe_projet ep = new Employe_projet
                {
                    Id = id,
                    Matricule = matricule,
                    NoProjet = noProjet,
                    NbHeures = nbHeures,
                    Salaire = salaire,
                    NomEmploye = nomEmploye,
                    Titre = titre

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
            MySqlCommand commande = new MySqlCommand("get_allprojects");
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

            MySqlCommand commande = new MySqlCommand("get_allprojects");
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
                MySqlCommand commande = new MySqlCommand("add_employe");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("@anom", nom);
                commande.Parameters.AddWithValue("@aprenom", prenom);
                commande.Parameters.AddWithValue("@aemail", email);
                commande.Parameters.AddWithValue("@aadresse", adresse);
                commande.Parameters.AddWithValue("@astatut", statut);
                commande.Parameters.AddWithValue("@adateNaissance", DateOnly.Parse(dateNaissance));
                commande.Parameters.AddWithValue("@adateEmbauche", DateOnly.Parse(dateEmbauche));
                commande.Parameters.AddWithValue("@atauxHoraire", tauxHoraire);
                commande.Parameters.AddWithValue("@aphoto", photo.ToString());

                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                commande.Prepare();
                commande.ExecuteNonQuery();
                con.Close();
                reload();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void AjouterClient(Client client)
        {
            string nom = client.Nom;
            string adresse = client.Adresse;
            string numTel = client.NumTel;
            string email = client.Email;

            try
            {
                MySqlCommand commande = new MySqlCommand("add_client");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("@anom", nom);
                commande.Parameters.AddWithValue("@aadresse", adresse);
                commande.Parameters.AddWithValue("@anumTel", numTel);
                commande.Parameters.AddWithValue("@aemail", email);

                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }

                commande.Prepare();
                commande.ExecuteNonQuery();

                con.Close();
                reload();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



        public void AjouterProjet(Projet projet)
        {
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
                MySqlCommand commande = new MySqlCommand("add_project");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("@atitre", titre);
                commande.Parameters.AddWithValue("@adateDebut", DateOnly.Parse(dateDebut));
                commande.Parameters.AddWithValue("@adescription", description);
                commande.Parameters.AddWithValue("@astatut", statut);
                commande.Parameters.AddWithValue("@anbEmployes", nbEmployes);
                commande.Parameters.AddWithValue("@aclient", client);
                commande.Parameters.AddWithValue("@abudget", budget);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                commande.Prepare();
                commande.ExecuteNonQuery();

                con.Close();
                reload();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AjouterEmployeProjet(Employe_projet employe_Projet)
        {
            string matricule = employe_Projet.Matricule;
            string noProjet = employe_Projet.NoProjet;
            double nbHeures = employe_Projet.NbHeures;
            double salaire = employe_Projet.Salaire;

            try
            {
                MySqlCommand commande = new MySqlCommand("add_employeeproject");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("@amatricule", matricule);
                commande.Parameters.AddWithValue("@anoProjet", noProjet);
                commande.Parameters.AddWithValue("@anbHeures", nbHeures);

                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                commande.Prepare();
                commande.ExecuteNonQuery();

                con.Close();
                reload();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public Employe getEmploye(int position)
        {
            return listeEmploye[position];
        }
        public Client getClient(int position)
        {
            return listeClient[position];
        }
        public Projet getProjet(int position)
        {
            return listeProjet[position];
        }

        public void SupprimerClient(int position)
        {
            try
            {
                int id = listeClient[position].Id;
                MySqlCommand commande = new MySqlCommand("delete_client");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("@aid", id);

                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                commande.Prepare();
                commande.ExecuteNonQuery();
                con.Close();
                reload();
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
                MySqlCommand commande = new MySqlCommand("delete_employee");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("@amatricule", matricule);

                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                commande.Prepare();
                commande.ExecuteNonQuery();
                con.Close();
                reload();
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
                MySqlCommand commande = new MySqlCommand("delete_project");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("@anoProjet", noProjet);

                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                commande.Prepare();
                commande.ExecuteNonQuery();
                con.Close();
                reload();
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
                MySqlCommand commande = new MySqlCommand("delete_employeeproject");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("@aid", id);

                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                commande.Prepare();
                commande.ExecuteNonQuery();
                con.Close();
                reload();
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
                MySqlCommand commande = new MySqlCommand("edit_employe");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("@amatricule", matricule);
                commande.Parameters.AddWithValue("@anom", nom);
                commande.Parameters.AddWithValue("@aprenom", prenom);
                commande.Parameters.AddWithValue("@aemail", email);
                commande.Parameters.AddWithValue("@aadresse", adresse);
                commande.Parameters.AddWithValue("@astatut", statut);
                commande.Parameters.AddWithValue("@atauxHoraire", tauxHoraire);
                commande.Parameters.AddWithValue("@aphoto", photo.ToString());

                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                commande.Prepare();
                commande.ExecuteNonQuery();
                con.Close();
                reload();
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
                MySqlCommand commande = new MySqlCommand("edit_client");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("@aid", id);
                commande.Parameters.AddWithValue("@anom", nom);
                commande.Parameters.AddWithValue("@aadresse", adresse);
                commande.Parameters.AddWithValue("@anumTel", numTel);
                commande.Parameters.AddWithValue("@aemail", email);

                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }

                commande.Prepare();
                commande.ExecuteNonQuery();

                con.Close();
                reload();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void ModifierProjet(string noProjet, string titre, string dateDebut, string description, string statut, int nbEmployes, int client, double budget)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("edit_project");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("@anoProjet", noProjet);
                commande.Parameters.AddWithValue("@atitre", titre);
                commande.Parameters.AddWithValue("@adateDebut", DateOnly.Parse(dateDebut));
                commande.Parameters.AddWithValue("@adescription", description);
                commande.Parameters.AddWithValue("@astatut", statut);
                commande.Parameters.AddWithValue("@anbEmployes", nbEmployes);
                commande.Parameters.AddWithValue("@aclient", client);
                commande.Parameters.AddWithValue("@abudget", budget);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                commande.Prepare();
                commande.ExecuteNonQuery();

                con.Close();
                reload();
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
                MySqlCommand commande = new MySqlCommand("edit_employeeproject");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("@aid", id);
                commande.Parameters.AddWithValue("@amatricule", matricule);
                commande.Parameters.AddWithValue("@anoProjet", noProjet);
                commande.Parameters.AddWithValue("@anbHeures", nbHeures);

                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                commande.Prepare();
                commande.ExecuteNonQuery();

                con.Close();
                reload();
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

            reloadClient();
            reloadEmploye();
            reloadProject();
            reloadEmployeeProject();
        }

        private void reloadEmploye()
        {
            listeEmploye.Clear();
            MySqlCommand commande = new MySqlCommand("get_allemployees");
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
                string dateNaissance = Convert.ToString(r["dateNaissance"]);
                string dateEmbauche = Convert.ToString(r["dateEmbauche"]);
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

        private void reloadProject()
        {
            listeProjet.Clear();
            MySqlCommand commande = new MySqlCommand("get_allprojects");
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
        }

        private void reloadClient()
        {
            listeClient.Clear();
            MySqlCommand commande = new MySqlCommand("get_allclients");
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
        }

        private void reloadEmployeeProject()
        {
            listeEmploye_projet.Clear();
            MySqlCommand commande = new MySqlCommand("get_allemployeeprojects");
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
                string nomEmploye = (string)r["Nom employé"];
                string titre = (string)r["titre"];

                Employe_projet ep = new Employe_projet
                {
                    Id = id,
                    Matricule = matricule,
                    NoProjet = noProjet,
                    NbHeures = nbHeures,
                    Salaire = salaire,
                    NomEmploye = nomEmploye,
                    Titre = titre

                };

                listeEmploye_projet.Add(ep);
            }
            r.Close();
            con.Close();
        }


    }
}
