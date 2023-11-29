using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using GestionProjets.Objets;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GestionProjets.Projets {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    

    public sealed partial class pageCreationProjet : Page {

        Client item;
        public pageCreationProjet() {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            item = (Client)e.Parameter;
        }

        private void btn_Creer_Click(object sender, RoutedEventArgs e) {

            string titre, description;
            titre = description = "";
            int nbEmploye = 1;
            double budget = 0;

            DateTime dateDebut = DateTime.Now;
            Object[] tabValInsert = { tb_Titre.Text, dp_DateDebut.SelectedDate, tb_Description.Text, tb_Budget.Text, cb_nbEmploye.SelectedValue };
            string[] tabNom = { "Titre", "Date de début", "Description", "Budget", "Nombre d'employés" };
            TextBlock[] tabTxtBlock = { tblErreur_Titre, tblErreur_DateDebut, tblErreur_Description, tblErreur_Budget, tblErreur_nbEmploye };
            bool erreur = false;

            for (int i = 0; i < tabNom.Length; i++) {
                tabTxtBlock[i].Text = "";
                if (tabValInsert[i] == null || string.IsNullOrEmpty(tabValInsert[i].ToString())) {
                    erreur = true;
                    tabTxtBlock[i].Text = "Le valeur du champ '" + tabNom[i] + "' est vide.";

                }
            }

            if (!erreur) {
                Regex regex = new Regex("^[0-9]{1,}[.,][0-9]{2}$|^[0-9]{1,}$");
                Match match = regex.Match((string)tabValInsert[3]);
                if (match.Success) {
                    budget = double.Parse(match.Value.Replace('.', ','));

                } else {
                    tabTxtBlock[5].Text = "Entrez un prix comme ceci 10000.00 ou 233";
                    erreur = true;
                }

                titre = (string)tabValInsert[0];
                dateDebut = ((DateTimeOffset)tabValInsert[1]).DateTime;
                description = (string)tabValInsert[2];
                nbEmploye = (int)tabValInsert[4];
            }


            if (!erreur) {
                SingletonBD.getInstance().addProjet(titre, dateDebut, description, budget, nbEmploye, clientTrucBinouche);
                this.Frame.Navigate(typeof(pageGestionProjet));
            }

        }
    }
}
