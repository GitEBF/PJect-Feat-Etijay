using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.Text.RegularExpressions;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GestionProjets
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class pageModifierEmploye : Page
    {
        Employe item;
        public pageModifierEmploye()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            item = (Employe)e.Parameter;
            tb_Nom.Text = item.Nom;
            tb_Prenom.Text = item.Prenom;
            tb_Email.Text = item.Email;
            tb_Adresse.Text = item.Adresse;
            dp_DateNaissance.SelectedDate = item.DateNaissance;
            tb_TauxHoraire.Text = item.TauxHoraire.ToString();
            tb_Photo.Text = item.Photo;
            cb_Statut.SelectedValue = item.Statut;
            dp_DateEmbauche.SelectedDate = item.DateEmbauche;
        }

        private void btn_Modifier_Click(object sender, RoutedEventArgs e)
        {

            string nom, prenom, email, adresse, statut, photo;
            nom = prenom = email = adresse = statut = photo = "";
            double tauxHoraire = 0;
            DateTime dateEmbauche = DateTime.Now;
            DateTime dateNaissance = DateTime.Now;

            Object[] tabValInsert = { tb_Nom.Text, tb_Prenom.Text, tb_Email.Text, tb_Adresse.Text, dp_DateEmbauche.SelectedDate, tb_TauxHoraire.Text, tb_Photo.Text, cb_Statut.SelectedValue, dp_DateNaissance.SelectedDate };
            string[] tabNom = { "Nom", "Prenom", "Email", "Adresse", "date d\'embauche", "taux horaire", "Photo", "Statut", "Date de naissance" };
            TextBlock[] tabTxtBlock = { tblErreur_Nom, tblErreur_Prenom, tblErreur_Email, tblErreur_Adresse, tblErreur_DateEmbauche, tblErreur_TauxHoraire, tblErreur_Photo, tblErreur_Statut, tblErreur_DateNaissance };
            bool erreur = false;

            for (int i = 0; i < tabNom.Length; i++)
            {
                tabTxtBlock[i].Text = "";
                if (tabValInsert[i] == null || string.IsNullOrEmpty(tabValInsert[i].ToString()))
                {
                    erreur = true;
                    tabTxtBlock[i].Text = "Le valeur du champ '" + tabNom[i] + "' est vide.";

                }
            }

            if (!erreur)
            {
                Regex regex = new Regex("^[0-9]{1,}[.,][0-9]{2}$|^[0-9]{1,}$");
                Match match = regex.Match((string)tabValInsert[5]);
                if (match.Success)
                {
                    tauxHoraire = double.Parse(match.Value.Replace('.', ','));

                }
                else
                {
                    tabTxtBlock[5].Text = "Entrez un prix comme ceci 10000.00 ou 233";
                    erreur = true;
                }

                Regex regex2 = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
                Match match2 = regex2.Match((string)tabValInsert[2]);
                if (match2.Success)
                {
                    email = match2.Value;

                }
                else
                {
                    tabTxtBlock[2].Text = "Email Invalide";
                    erreur = true;
                }

                if ((DateTimeOffset.Now - ((DateTimeOffset)tabValInsert[4])).TotalDays < (3 * 365))
                {

                    statut = "Journalier";

                }
                else
                {
                    statut = tabValInsert[7].ToString();
                }

                nom = (string)tabValInsert[0];
                prenom = (string)tabValInsert[1];
                adresse = (string)tabValInsert[3];
                dateEmbauche = ((DateTimeOffset)tabValInsert[4]).DateTime;

                photo = (string)tabValInsert[6];
                dateNaissance = ((DateTimeOffset)tabValInsert[8]).DateTime;
            }

            if (!erreur)
            {
                SingletonBD.getInstance().updateEmploye(item.Matricule, nom, prenom, email, dateNaissance, adresse, dateEmbauche, tauxHoraire, photo, statut);
                this.Frame.Navigate(typeof(pageGestionEmploye));
            }

        }
    }
}
