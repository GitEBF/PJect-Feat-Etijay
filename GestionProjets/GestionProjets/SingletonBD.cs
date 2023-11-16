using Microsoft.WindowsAppSDK.Runtime.Packages;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GestionProjets
{
    internal class SingletonBD
    {
        MySqlConnection con = new MySqlConnection("Server=cours.cegep3r.info;Database=2248734-jérémy-thibeault;Uid=2248734;Pwd=2248734;");
        static SingletonBD instance = null;

        public SingletonBD()
        {
        }

        public static SingletonBD getInstance()
        {
            if (instance == null)
                instance = new SingletonBD();

            return instance;
        }

        public void edit(string CommandText)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = CommandText;
                con.Open();
                commande.ExecuteNonQuery();
                con.Close();
            }
            catch { }

        }

        public void addEmploye(string nom, string prenom, DateTime dateNaissance, string adresse, DateTime dateEmbauche, int tauxHoraire, string photo, string statut)
        {
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "CALL InsertEmploye(@nom,@prenom,@dateNaissance,@adresse,@dateEmbauche,@tauxHoraire,@photo,@statut)";
            con.Open();
            command.Parameters.AddWithValue("@nom", nom);
            command.Parameters.AddWithValue("@prenom", prenom);
            command.Parameters.AddWithValue("@dateNaissance", dateNaissance);
            command.Parameters.AddWithValue("@adresse", adresse);
            command.Parameters.AddWithValue("@dateEmbauche", dateEmbauche);
            command.Parameters.AddWithValue("@tauxHoraire", tauxHoraire);
            command.Parameters.AddWithValue("@photo", photo);
            command.Parameters.AddWithValue("@statut", statut);
            command.ExecuteNonQuery();
            con.Close();
        }

        


    }
}
