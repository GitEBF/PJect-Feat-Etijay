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
using GestionProjets.Singletons;
using GestionProjets.Objets;
using Microsoft.UI.Xaml.Media.Imaging;

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
        public pageZoomEmploye()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            item = (Employe)e.Parameter;
            tbl_Matricule.Text = item.Matricule.ToString();
            tbl_NomPrenom.Text = "Nom: " + item.Prenom + ' ' + item.Nom;
            tbl_Email.Text = "Email: " + item.Email;
            tbl_Adresse.Text = "Adresse: " + item.Adresse;
            tbl_Statut.Text = "Statut: " + item.Statut;
            tbl_dateEmbauche.Text = "DateEmbauche: " + item.DateEmbauche;
            tbl_dateNaissance.Text = "DateNaissance: " + item.DateNaissance;
            tbl_tauxHoraire.Text = "TauxHoraire: " + item.TauxHoraire.ToString("F2") + "$";
            photo.Source = new BitmapImage(new Uri(item.Photo, UriKind.Absolute));
            string projectName = SingletonBD.getInstance().getEmployeCurrentProject(item.Matricule);
            tbl_travailSur.Text = "L'employé travail présentement sur le projet: " + projectName;
            if (!SingletonBD.getInstance().isUserLoggedIn())
            {
                admin.Visibility = Visibility.Collapsed;
            }
        }

        private void btn_Modifier_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(pageModifierEmploye), item);
        }

        private void btn_Supprimer_Click(object sender, RoutedEventArgs e)
        {
            SingletonBD.getInstance().DeleteAllEmployeeProjectByEmployee(item.Matricule);
            SingletonBD.getInstance().deleteEmployee(item.Matricule);
            this.Frame.Navigate(typeof(pageGestionEmploye));
        }
    }
}
