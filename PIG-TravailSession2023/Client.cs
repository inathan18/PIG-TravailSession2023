using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIG_TravailSession2023
{
    class Client
    {
        int id;
        string nom, adresse, numTel, email;

        public Client ()
        {

        }

        public Client(int id, string nom, string adresse, string numTel, string email)
        {
            this.id = id;
            this.nom = nom;
            this.adresse = adresse;
            this.numTel = numTel;
            this.email = email;
        }

        public int Id
        {
            get => id;
            set => id = value;
        }

        public string Nom { get => nom; set => nom = value; }

        public string Adresse { get => adresse; set => adresse = value; }

        public string NumTel { get => numTel; set => numTel = value; }

        public string Email { get => email; set => email = value; }

        public string toCSV()
        {
            return id.ToString() + ';' + nom + ";" + adresse + ';' + email;
        }
    }
}
