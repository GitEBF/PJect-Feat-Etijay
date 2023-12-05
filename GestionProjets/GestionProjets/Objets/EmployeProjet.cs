using GestionProjets.Singletons;
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
        string nomEmploye;
        double salaireEmploye;
        double salaireTotal;
        public EmployeProjet(MySqlDataReader r)
        {
            numProjet = r.GetString("numProjet");
            matriculeEmploye = r.GetString("matriculeEmploye");
            nbHeure = r.GetInt16("nbHeures");
            Employe emp = SingletonEmploye.getInstance().GetEmployeWithMatricule(matriculeEmploye);
            nomEmploye = emp.Prenom + ' ' + emp.Nom;
            salaireEmploye = emp.TauxHoraire;
            salaireTotal = salaireEmploye * nbHeure;

        }
        public EmployeProjet() { }
        public EmployeProjet(string numProjet, string matriculeEmploye, double nbHeure, string nomEmploye, double salaireEmploye, double salaireTotal)
        {
            this.numProjet = numProjet;
            this.matriculeEmploye = matriculeEmploye;
            this.nbHeure = nbHeure;
            this.nomEmploye = nomEmploye;
            this.salaireEmploye = salaireEmploye;
            this.salaireTotal   = salaireTotal;
        }

        public string NumProjet { get => numProjet; set => numProjet = value; }
        public string MatriculeEmploye { get => matriculeEmploye; set => matriculeEmploye = value; }
        public double NbHeure { get => nbHeure; set => nbHeure = value; }
        public string NomEmploye { get => nomEmploye; set =>  nomEmploye = value; }
        public double SalaireEmploye { get => salaireEmploye; set => salaireEmploye = value;}
        public double SalaireTotal { get => salaireTotal; set => salaireTotal = value; }
    }
}
