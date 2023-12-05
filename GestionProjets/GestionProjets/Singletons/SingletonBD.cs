using Microsoft.WindowsAppSDK.Runtime.Packages;
using MySql.Data.MySqlClient;
using System;
using GestionProjets.Singletons;
using GestionProjets.Objets;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GestionProjets {
    internal class SingletonBD {
        MySqlConnection con = new MySqlConnection("Server=cours.cegep3r.info;Database=a2023_420325ri_fabeq17;Uid=2248734;Pwd=2248734;");
        static SingletonBD instance = null;

        public SingletonBD() {
        }

        public static SingletonBD getInstance() {
            if (instance == null)
                instance = new SingletonBD();

            return instance;
        }

        public void LoadAll(string tableName, Action<MySqlDataReader> table) {
            using MySqlCommand command = con.CreateCommand();
            con.Open();
            command.CommandText = $"SELECT * FROM {tableName}";

            using MySqlDataReader r = command.ExecuteReader();
            while (r.Read()) {
                table(r);
            }
            con.Close();
        }

        // ----------------------------------------------------------------------------------- Employee -------------------------------------------------------------------------------

        public string addEmploye(string nom, string prenom, string email, DateTime dateNaissance, string adresse, DateTime dateEmbauche, double tauxHoraire, string photo, string statut) {
            try {
                MySqlCommand command = con.CreateCommand();
                command.CommandText = "CALL InsertEmploye(@nom,@prenom,@email,@dateNaissance,@adresse,@dateEmbauche,@tauxHoraire,@photo,@statut)";
                con.Open();
                command.Parameters.AddWithValue("@nom", nom);
                command.Parameters.AddWithValue("@prenom", prenom);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@dateNaissance", dateNaissance);
                command.Parameters.AddWithValue("@adresse", adresse);
                command.Parameters.AddWithValue("@dateEmbauche", dateEmbauche);
                command.Parameters.AddWithValue("@tauxHoraire", tauxHoraire);
                command.Parameters.AddWithValue("@photo", photo);
                command.Parameters.AddWithValue("@statut", statut);
                command.ExecuteNonQuery();
            } catch (Exception ex) {
                return ex.Message;
            } finally {
                con.Close();
            }
            return null;
        }

        public string updateEmploye(string matricule, string nom, string prenom, string email, string adresse, double tauxHoraire, string photo, string statut) {
            try {
                MySqlCommand command = con.CreateCommand();
                command.CommandText = "CALL UpdateEmployee(@matricule,@nom,@prenom,@email,@adresse,@tauxHoraire,@photo,@statut)";
                con.Open();
                command.Parameters.AddWithValue("@matricule", matricule);
                command.Parameters.AddWithValue("@nom", nom);
                command.Parameters.AddWithValue("@prenom", prenom);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@adresse", adresse);
                command.Parameters.AddWithValue("@tauxHoraire", tauxHoraire);
                command.Parameters.AddWithValue("@photo", photo);
                command.Parameters.AddWithValue("@statut", statut);
                command.ExecuteNonQuery();
            } catch (Exception ex) {
                return ex.Message;
            } finally {
                con.Close();
            }
            return null;
        }


        public void deleteEmployee(string matricule) {
            try {
                MySqlCommand command = con.CreateCommand();
                command.CommandText = "CALL DeleteEmployee(@matricule)";
                con.Open();
                command.Parameters.AddWithValue("@matricule", matricule);
                command.ExecuteNonQuery();
            } catch (Exception ex) {
            } finally {
                con.Close();
            }
        }


        public void LoadAllEmploye() {
            SingletonEmploye sEmploye = SingletonEmploye.getInstance();
            sEmploye.refresh();
            LoadAll("employes", (r) => {
                Employe employe = new Employe(r);
                sEmploye.ajouter(employe);
            });
        }

        // ----------------------------------------------------------------------------------- Client -------------------------------------------------------------------------------

        public string addClient(string nom, string adresse, string numTelephone, string email) {
            try {
                MySqlCommand command = con.CreateCommand();
                command.CommandText = "CALL InsertClient(@nom, @adresse, @numTelephone, @email)";
                con.Open();
                command.Parameters.AddWithValue("@nom", nom);
                command.Parameters.AddWithValue("@adresse", adresse);
                command.Parameters.AddWithValue("@numTelephone", numTelephone);
                command.Parameters.AddWithValue("@email", email);
                command.ExecuteNonQuery();
            } catch (Exception ex) {
                return ex.Message;
            } finally {
                con.Close();
            }
            return null;
        }
        public string updateClient(int id, string nom, string adresse, string numTelephone, string email) {
            try {
                MySqlCommand command = con.CreateCommand();
                command.CommandText = "CALL UpdateClient(@id, @nom, @adresse, @numTelephone, @email)";
                con.Open();
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@nom", nom);
                command.Parameters.AddWithValue("@adresse", adresse);
                command.Parameters.AddWithValue("@numTelephone", numTelephone);
                command.Parameters.AddWithValue("@email", email);
                command.ExecuteNonQuery();
            } catch (Exception ex) {
                return ex.Message;
            } finally {
                con.Close();
            }
            return null;
        }
        public void deleteClient(int id) {
            try {
                MySqlCommand command = con.CreateCommand();
                command.CommandText = "CALL DeleteClient(@id)";
                con.Open();
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            } catch (Exception ex) {
            } finally {
                con.Close();
            }
        }

        public void LoadAllClient() {
            SingletonClient sClient = SingletonClient.getInstance();
            sClient.refresh();
            LoadAll("clients", (r) => {
                Client client = new Client(r);
                sClient.ajouter(client);
            });
        }

        // ----------------------------------------------------------------------------------- Project -------------------------------------------------------------------------------

        public string addProjet(string titre, DateTime dateDebut, string description, double budget, int nbEmploye, int idClient) {
            try {
                MySqlCommand command = con.CreateCommand();
                command.CommandText = "CALL InsertProjet(@titre, @dateDebut, @description, @budget, @nbEmploye, @idClient)";
                con.Open();
                command.Parameters.AddWithValue("@titre", titre);
                command.Parameters.AddWithValue("@dateDebut", dateDebut);
                command.Parameters.AddWithValue("@description", description);
                command.Parameters.AddWithValue("@budget", budget);
                command.Parameters.AddWithValue("@nbEmploye", nbEmploye);
                command.Parameters.AddWithValue("@idClient", idClient);
                command.ExecuteNonQuery();
            } catch (Exception ex) {
                return ex.Message;
            } finally {
                con.Close();
            }
            return null;
        }
        public string updateProjet(string num, string titre, DateTime dateDebut, string description, double budget, int nbEmploye) {
            try {
                MySqlCommand command = con.CreateCommand();
                command.CommandText = "CALL UpdateProject(@num, @titre, @dateDebut, @description, @budget, @nbEmploye)";
                con.Open();
                command.Parameters.AddWithValue("@num", num);
                command.Parameters.AddWithValue("@titre", titre);
                command.Parameters.AddWithValue("@dateDebut", dateDebut);
                command.Parameters.AddWithValue("@description", description);
                command.Parameters.AddWithValue("@budget", budget);
                command.Parameters.AddWithValue("@nbEmploye", nbEmploye);
                command.ExecuteNonQuery();
            } catch (Exception ex) {
                // Handle the exception here
                return ex.Message;
            } finally {
                con.Close();
            }
            return null;
        }
        public void deleteProjet(string num) {
            try {
                MySqlCommand command = con.CreateCommand();
                command.CommandText = "CALL DeleteProject(@num)";
                con.Open();
                command.Parameters.AddWithValue("@num", num);
                command.ExecuteNonQuery();
            } catch (Exception ex) {
            } finally {
                con.Close();
            }
        }

        public void LoadAllProjet() {
            SingletonProjet sProjet = SingletonProjet.getInstance();
            sProjet.refresh();
            LoadAll("projets", (r) => {
                Projet projet = new Projet(r);
                sProjet.ajouter(projet);
            });
        }

        // ----------------------------------------------------------------------------------- EmployeeProject -------------------------------------------------------------------------------

        public void addEmployeProjet(string numProjet, string matriculeEmploye, int nbHeures) {
            try {
                MySqlCommand command = con.CreateCommand();
                command.CommandText = "CALL InsertEmployeProjet(@numProjet, @matriculeEmploye, @nbHeures)";
                con.Open();
                command.Parameters.AddWithValue("@numProjet", numProjet);
                command.Parameters.AddWithValue("@matriculeEmploye", matriculeEmploye);
                command.Parameters.AddWithValue("@nbHeures", nbHeures);
                command.ExecuteNonQuery();
            } catch (Exception ex) {
            } finally {
                con.Close();
            }
        }
        public void updateEmployeProjet(string numProjet, string matriculeEmploye, int nbHeures) {
            try {
                MySqlCommand command = con.CreateCommand();
                command.CommandText = "CALL UpdateEmployeeProject(@numProjet, @matriculeEmploye, @nbHeures)";
                con.Open();
                command.Parameters.AddWithValue("@numProjet", numProjet);
                command.Parameters.AddWithValue("@matriculeEmploye", matriculeEmploye);
                command.Parameters.AddWithValue("@nbHeures", nbHeures);
                command.ExecuteNonQuery();
            } catch (Exception ex) {
            } finally {
                con.Close();
            }
        }
        public void deleteEmployeeProjectByEmployee(string numProjet, string matriculeEmploye) {
            try {
                MySqlCommand command = con.CreateCommand();
                command.CommandText = "CALL DeleteEmployeeProjectByEmployee(@numProjet, @matriculeEmploye)";
                con.Open();
                command.Parameters.AddWithValue("@numProjet", numProjet);
                command.Parameters.AddWithValue("@matriculeEmploye", matriculeEmploye);
                command.ExecuteNonQuery();
            } catch (Exception ex) {
            } finally {
                con.Close();
            }
        }
        public void DeleteAllEmployeeProjectByEmployee(string matriculeEmploye)
        {
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "CALL DeleteAllEmployeeProjectByEmployee(@matriculeEmploye)";
            con.Open();
            command.Parameters.AddWithValue("@matriculeEmploye", matriculeEmploye);
            command.ExecuteNonQuery();
            con.Close();
        }
        public void deleteEmployeeProjectByProject(string numProjet)
        {
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "CALL DeleteEmployeeProjectByProject(@numProjet)";
            con.Open();
            command.Parameters.AddWithValue("@numProjet", numProjet);
            command.ExecuteNonQuery();
            con.Close();
        }

        public void LoadAllEmployeProjet() {
            SingletonEmployeProjet sEmployeProjet = SingletonEmployeProjet.getInstance();
            sEmployeProjet.refresh();
            LoadAll("employesprojets", (r) => {
                EmployeProjet employeProjet = new EmployeProjet(r);
                sEmployeProjet.ajouter(employeProjet);
            });
        }

        // ----------------------------------------------------------------------------------- User -------------------------------------------------------------------------------

        public void createUser(string name, string password) {
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "CALL CreateUser(@name, @password)";
            con.Open();
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@password", password);
            command.ExecuteNonQuery();
            con.Close();
        }

        public bool checkIfFirstUse() {
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "CALL CheckIfFirstUse()";
            con.Open();
            bool result = (bool)command.ExecuteScalar();
            con.Close();
            return result;
        }

        public bool isUserLoggedIn() {
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "CALL IsUserLoggedIn()";
            con.Open();
            bool result = (bool)command.ExecuteScalar();
            con.Close();
            return result;
        }
        public bool Connexion(string name, string password) {
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "CALL Connexion(@name, @password)";
            con.Open();
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@password", password);
            bool result = (bool)command.ExecuteScalar();
            con.Close();
            return result;
        }
        public void loginLogout() {
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "CALL LoginLogout()";
            con.Open();
            command.ExecuteNonQuery();
            con.Close();
        }

        public string getEmployeCurrentProject(string matriculeEmploye)
        {
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "CALL CheckIfEmployeWorkOnCurrentProject(@matricule);";
            con.Open();
            command.Parameters.AddWithValue("@matricule", matriculeEmploye);
            string result;
            object resultObj = command.ExecuteScalar();
            con.Close();
            if (resultObj != DBNull.Value)
            {
                result = (string)resultObj;
                return result;
            }
            else
            {
                return "";
            }
        }



        public string GetClientNameById(int id)
        {
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT f_GetClientNameById(@id);";
            con.Open();
            command.Parameters.AddWithValue("@id", id);
            string result = (string)command.ExecuteScalar();
            con.Close();
            return result;
        }

        public void UpdateProjetStatus(string num)
        {
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "CALL UpdateProjectStatus('@num');";
            con.Open();
            command.Parameters.AddWithValue("@num", num);
            command.ExecuteNonQuery();
            con.Close();
        }

    }
}
