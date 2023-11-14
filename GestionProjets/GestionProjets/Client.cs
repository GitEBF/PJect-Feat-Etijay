using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProjets
{
    internal class Client
    {
        public Client() { }
        public Client(int id, string nom, string adresse, string numTelephone, string email) {
            id = id;
            nom = nom;
            adresse = adresse;
            numTelephone = numTelephone;
            email = email;
        }

        public int id { get; set; }
        public string nom { get; set; }
        public string adresse { get; set; }
        public string numTelephone { get; set; }
        public string email { get; set; }
    
    }
}
