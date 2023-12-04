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
using GestionProjets.Singletons;
using GestionProjets.Objets;

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
            tb_TauxHoraire.Text = item.TauxHoraire.ToString();
            tb_Photo.Text = item.Photo;
            cb_Statut.SelectedValue = item.Statut;
        }

        private void btn_Modifier_Click(object sender, RoutedEventArgs e)
        {

            string nom, prenom, email, adresse, statut, photo;
            nom = prenom = email = adresse = statut = photo = "";
            double tauxHoraire = 0;

            Object[] tabValInsert = { tb_Nom.Text, tb_Prenom.Text, tb_Email.Text, tb_Adresse.Text, tb_TauxHoraire.Text, tb_Photo.Text, cb_Statut.SelectedValue };
            string[] tabNom = { "Nom", "Prenom", "Email", "Adresse", "taux horaire", "Photo", "Statut"};
            TextBlock[] tabTxtBlock = { tblErreur_Nom, tblErreur_Prenom, tblErreur_Email, tblErreur_Adresse, tblErreur_TauxHoraire, tblErreur_Photo, tblErreur_Statut };
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
                Regex regex = new Regex("^[0-9]{1,}[.,][0-9]{1,2}$|^[0-9]{1,}$");
                Match match = regex.Match((string)tabValInsert[4]);
                if (match.Success)
                {
                    tauxHoraire = double.Parse(match.Value.Replace(',', '.'));
                    if (tauxHoraire >= 120) {
                        erreur = true;
                        tabTxtBlock[4].Text = "Maximum de 120$";
                    }
                    if (tauxHoraire < 15) {
                        erreur = true;
                        tabTxtBlock[4].Text = "Minimum de 15$";
                    }
                }
                else
                {
                    tabTxtBlock[4].Text = "Entrez un prix comme ceci 10000.00 ou 233";
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

                if ((DateTimeOffset.Now - ((DateTimeOffset)item.DateEmbauche)).TotalDays < (3 * 365))
                {
                    if (statut == "Permanent") {
                        erreur = true;
                        tabTxtBlock[6].Text = "Il ne peux pas être permanent, car il n'a pas travaillé plus de 3 ans.";
                    }
                    statut = tabValInsert[6].ToString();

                }
                else
                {
                    statut = tabValInsert[6].ToString();
                }

                nom = (string)tabValInsert[0];
                prenom = (string)tabValInsert[1];
                adresse = (string)tabValInsert[3];


                photo = (string)tabValInsert[5];

            }

            if (!erreur)
            {
                string strError = SingletonBD.getInstance().updateEmploye(item.Matricule, nom, prenom, email, adresse, tauxHoraire, photo, statut);
                if (strError != null) {
                    tblGlobal.Text = strError;
                } else {
                    this.Frame.Navigate(typeof(pageGestionEmploye));
                }
            }

        }
    }
}
