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
using Microsoft.WindowsAppSDK.Runtime.Packages;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GestionProjets
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class pageZoomEmploye : Page
    {
        Employe item;
        int index;
        public pageZoomEmploye()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            index = (int)e.Parameter;
            item = (Employe)e.Parameter;
            tbl_Matricule.Text = item.Matricule.ToString();
            tbl_NomPrenom.Text = "Nom: " + item.Prenom + ' ' + item.Nom;
            tbl_Email.Text = "Email: " + item.Email;
            tbl_Adresse.Text = "Adresse: " + item.Adresse;
            tbl_Statut.Text = "Statut: " + item.Statut;
            tbl_dateEmbauche.Text = "DateEmbauche: " + item.DateEmbauche;
            tbl_dateNaissance.Text = "DateNaissance: " + item.DateNaissance;
            tbl_tauxHoraire.Text = "TauxHoraire: " + item.TauxHoraire;
        }

        private void btn_Modifier_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(pageModifierEmploye), item);
        }

        private void btn_Supprimer_Click(object sender, RoutedEventArgs e)
        {
            SingletonEmploye singleton = SingletonEmploye.getInstance()
            this.Frame.Navigate(typeof(pageGestionEmploye));

            string commandText = "delete from materiel WHERE matricule=" + SingletonEmploye.getInstance().GetEmploye(index).Matricule;
            bdInstance.edit(commandText);
            SingletonMateriel.getInstance().supprimer(index);
        }
    }
}
