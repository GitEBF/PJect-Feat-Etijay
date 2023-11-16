using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProjets
{
    internal class Client
    {
        int id;
        string nom, adresse, numTelephone, email;
        public Client() { }
        public Client(int id, string nom, string adresse, string numTelephone, string email) {
            this.id = id;
            this.nom = nom;
            this.adresse = adresse;
            this.numTelephone = numTelephone;
            this.email = email;
        }

        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Adresse { get => adresse; set => adresse = value; }
        public string NumTelephone { get => numTelephone; set => numTelephone = value; }
        public string Email { get => email; set => email = value; }
    }
}
