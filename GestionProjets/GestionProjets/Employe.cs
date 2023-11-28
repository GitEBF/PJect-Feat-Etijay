using Microsoft.UI.Xaml.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProjets
{
    internal class Employe
    {
        string matricule, nom, prenom, email, adresse, statut, photo;
        DateTime dateNaissance, dateEmbauche;
        double tauxHoraire;
        public Employe(MySqlDataReader r) {
            this.nom = r.GetString("nom");
            this.prenom = r.GetString("prenom");
            this.email = r.GetString("email");
            this.adresse = r.GetString("adresse");
            this.statut = r.GetString("statut");
            this.photo = r.GetString("photo");
            this.dateEmbauche = r.GetDateTime("dateEmbauche");
            this.DateNaissance = r.GetDateTime("dateNaissance");
            this.tauxHoraire = r.GetDouble("tauxHoraire");
            this.matricule = r.GetString("matricule");
            this.statut = r.GetString("statut");
        }
        public Employe(string nom, string prenom, DateTime dateNaissance, string email, string adresse, DateTime dateEmbauche, int tauxHoraire, string photo, string statut) {
            this.nom = nom;
            this.prenom = prenom;
            this.dateNaissance = dateNaissance;
            this.email = email;
            this.adresse = adresse;
            this.dateEmbauche = dateEmbauche;
            this.tauxHoraire = tauxHoraire;
            this.photo = photo;
            this.statut = statut;
            matricule = "HEHEHEHA";
        }

        public string Matricule { get => matricule; set => matricule = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Email { get => email; set => email = value; }
        public string Adresse { get => adresse; set => adresse = value; }
        public string Statut { get => statut; set => statut = value; }
        public string Photo { get => photo; set => photo = value; }
        public DateTime DateNaissance { get => dateNaissance; set => dateNaissance = value; }
        public DateTime DateEmbauche { get => dateEmbauche; set => dateEmbauche = value; }
        public double TauxHoraire { get => tauxHoraire; set => tauxHoraire = value; }
    }
}
