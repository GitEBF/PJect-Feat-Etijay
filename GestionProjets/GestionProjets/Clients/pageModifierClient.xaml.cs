using GestionProjets.Objets;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GestionProjets
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class pageModifierClient : Page
    {
        Client item;
        public pageModifierClient()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            item = (Client)e.Parameter;
            tb_Nom.Text = item.Nom;
            tb_Adresse.Text = item.Adresse;
            tb_NumTelephone.Text = item.NumTelephone;
            tb_Email.Text = item.Email;
        }

        private void btn_Modifier_Click(object sender, RoutedEventArgs e)
        {

            string nom, adresse, numTelephone, email;
            nom = adresse = numTelephone = email = "";

            Object[] tabValInsert = { tb_Nom.Text, tb_Adresse.Text, tb_NumTelephone.Text, tb_Email.Text };
            string[] tabNom = { "Nom", "Adresse", "Numéro téléphone", "Email" };
            TextBlock[] tabTxtBlock = { tblErreur_Nom, tblErreur_Adresse, tblErreur_NumTelephone, tblErreur_Email };
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

                Regex regex = new Regex(@"^\d{3}-\d{3}-\d{4}$");
                Match match = regex.Match((string)tabValInsert[2]);
                if (match.Success)
                {
                    numTelephone = match.Value.ToString();
                }
                else
                {
                    tabTxtBlock[2].Text = "Numéro de téléphone Invalide";
                    erreur = true;
                }
                Regex regex2 = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
                Match match2 = regex2.Match((string)tabValInsert[3]);
                if (match2.Success)
                {
                    email = match2.Value;

                }
                else
                {
                    tabTxtBlock[3].Text = "Email Invalide";
                    erreur = true;
                }

                nom = (string)tabValInsert[0];
                adresse = (string)tabValInsert[1];
                numTelephone = (string)tabValInsert[2];
                email = (string)tabValInsert[3];
            }


            if (!erreur)
            {
                string strError = SingletonBD.getInstance().updateClient(item.Id, nom, adresse, numTelephone, email);
                if (strError != null) {
                    tblGlobal.Text = strError;
                } else {
                    this.Frame.Navigate(typeof(pageGestionClient));
                }
            }

        }
    }
}
