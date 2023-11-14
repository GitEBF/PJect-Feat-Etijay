using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProjets
{
    internal class Projet
    {
        public Projet() { }
        public Projet(string num,string titre, DateTime dateDebut, string description, int budget, int nbEmploye, int totalSalaire, int idClient, string statut) { }
        public string num { get; set; }
        public string titre { get; set;}
        public string dateDebut { get; set;}
        public string description { get; set;}
        public int budget { get; set;}
        public int nbEmploye { get; set;}
        public int totalSalaire { get; set;}
        public int idClient { get; set;}
        public string statut { get; set;}

    }
}
