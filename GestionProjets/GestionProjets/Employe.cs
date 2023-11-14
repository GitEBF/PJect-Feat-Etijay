using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProjets
{
    internal class Employe
    {
        public Employe() { }
        public Employe(string matricule, string nom, string prenom, DateTime dateNaissance, string email, string adresse, DateTime dateEmbauche, int tauxHoraire, string photo, string statut) {
            matricule = matricule;
            nom = nom;
            prenom = prenom;
            dateNaissance = dateNaissance;
            email = email;
            adresse = adresse;
            dateEmbauche = dateEmbauche;
            tauxHoraire = tauxHoraire;
            photo = photo;
            statut = statut;
        }

        public string matricule { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string dateNaissance { get; set; }
        public string email { get; set; }
        public string adresse { get; set;}
        public DateTime dateEmbauche { get; set; }
        public int tauxHoraire { get; set; }   
        public string photo { get; set; }
        public string statut { get; set; }


    }
}
