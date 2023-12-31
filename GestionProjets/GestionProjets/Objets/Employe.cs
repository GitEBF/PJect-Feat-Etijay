﻿using Microsoft.UI.Xaml.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProjets.Objets
{
    internal class Employe
    {
        string matricule, nom, prenom, email, adresse, statut, photo;
        DateTime dateNaissance, dateEmbauche;
        double tauxHoraire;
        public Employe(MySqlDataReader r)
        {
            nom = r.GetString("nom");
            prenom = r.GetString("prenom");
            email = r.GetString("email");
            adresse = r.GetString("adresse");
            statut = r.GetString("statut");
            photo = r.GetString("photo");
            dateEmbauche = r.GetDateTime("dateEmbauche");
            DateNaissance = r.GetDateTime("dateNaissance");
            tauxHoraire = r.GetDouble("tauxHoraire");
            matricule = r.GetString("matricule");
            statut = r.GetString("statut");
        }
        public Employe(string nom, string prenom, DateTime dateNaissance, string email, string adresse, DateTime dateEmbauche, double tauxHoraire, string photo, string statut)
        {
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
