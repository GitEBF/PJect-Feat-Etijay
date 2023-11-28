using Microsoft.UI.Xaml.Media;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProjets.Objets
{
    internal class EmployeProjet
    {
        string numProjet, matriculeEmploye;
        double nbHeure;
        public EmployeProjet(MySqlDataReader r)
        {
            numProjet = r.GetString("numProjet");
            matriculeEmploye = r.GetString("matriculeEmploye");
            nbHeure = r.GetInt16("nbHeures");
        }
        public EmployeProjet() { }
        public EmployeProjet(string numProjet, string matriculeEmploye, double nbHeure)
        {
            this.numProjet = numProjet;
            this.matriculeEmploye = matriculeEmploye;
            this.nbHeure = nbHeure;
        }

        public string NumProjet { get => numProjet; set => numProjet = value; }
        public string MatriculeEmploye { get => matriculeEmploye; set => matriculeEmploye = value; }
        public double NbHeure { get => nbHeure; set => nbHeure = value; }

    }
}
