using Microsoft.UI.Xaml.Media;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProjets
{
    internal class Projet
    {
        string num, titre, description, statut;
        double budget;
        int totalSalaire, idClient, nbEmploye;
        DateTime dateDebut;
        public Projet(MySqlDataReader r)
        {
            this.num = r.GetString("num");
            this.titre = r.GetString("titre");
            this.description = r.GetString("description");
            this.statut = r.GetString("statut");
            this.budget = r.GetDouble("budget");
            this.nbEmploye = r.GetInt16("nbEmploye");
            this.totalSalaire = r.GetInt16("totalSalaire");
            this.idClient = r.GetInt16("idClient");
            this.dateDebut = r.GetDateTime("dateDebut");
        }
        public Projet() { }
        public Projet(string num,string titre, DateTime dateDebut, string description, int budget, int nbEmploye, int totalSalaire, int idClient, string statut) {
            this.num = num;
            this.titre = titre;
            this.dateDebut = dateDebut;
            this.description = description;
            this.budget = budget;
            this.nbEmploye = nbEmploye;
            this.totalSalaire = totalSalaire;
            this.idClient = idClient;
            this.statut = statut;
        }

        public string Num { get => num; set => num = value; }
        public string Titre { get => titre; set => titre = value; }
        public string Description { get => description; set => description = value; }
        public string Statut { get => statut; set => statut = value; }
        public double Budget { get => budget; set => budget = value; }
        public int NbEmploye { get => nbEmploye; set => nbEmploye = value; }
        public int TotalSalaire { get => totalSalaire; set => totalSalaire = value; }
        public int IdClient { get => idClient; set => idClient = value; }
        public DateTime DateDebut { get => dateDebut; set => dateDebut = value; }
    }
}
