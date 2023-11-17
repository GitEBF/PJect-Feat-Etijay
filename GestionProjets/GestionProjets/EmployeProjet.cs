using Microsoft.UI.Xaml.Media;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProjets
{
    internal class EmployeProjet
    {
        string numProjet, matriculeEmploye;
        int nbHeure;
        public EmployeProjet(MySqlDataReader r)
        {
            this.numProjet = r.GetString("numProjet");
            this.matriculeEmploye = r.GetString("matriculeEmploye");
            this.nbHeure = r.GetInt16("nbHeure");
        }
        public EmployeProjet() { }
        public EmployeProjet(string numProjet, string matriculeEmploye, int nbHeure)
        {
            this.numProjet = numProjet;
            this.matriculeEmploye = matriculeEmploye;
            this.nbHeure = nbHeure;
        }

        public string NumProjet { get => numProjet; set => numProjet = value; }
        public string MatriculeEmploye { get => matriculeEmploye; set => matriculeEmploye = value; }
        public int NbHeure { get => nbHeure; set => nbHeure = value; }
     
    }
}
