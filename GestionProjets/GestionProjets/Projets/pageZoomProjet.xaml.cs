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
using GestionProjets.Singletons;
using GestionProjets.Objets;
using System.Collections.ObjectModel;
using GestionProjets.Employees;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GestionProjets
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class pageZoomProjet : Page
    {
        Projet item;
        ObservableCollection<EmployeProjet> listeEmployes;
        public pageZoomProjet()
        {
            this.InitializeComponent();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            item = (Projet)e.Parameter;
            tbl_Num.Text = "N. " + item.Num.ToString();
            tbl_Titre.Text = item.Titre;
            tbl_DateDebut.Text = "Date de début: " + item.DateDebut;
            tbl_Description.Text = "Description: " + item.Description;
            tbl_Budget.Text = "Budget: " + item.Budget.ToString("F2") + "$";
            tbl_NbEmploye.Text = "Nombre max d'employé: " + item.NbEmploye;
            tbl_TotalSalaire.Text = "Salaire total: " + item.TotalSalaire.ToString("F2") + "$";
            tbl_IdClient.Text = "Id client: " + item.IdClient;
            tbl_Statut.Text = "Statut: " + item.Statut;
            if (!SingletonBD.getInstance().isUserLoggedIn())
            {
                admin.Visibility = Visibility.Collapsed;
                btn_Ajouter.Visibility = Visibility.Collapsed;
                btn_State.Visibility = Visibility.Collapsed;
            }
            if (item.Statut == "Terminé")
            {
                btn_State.Visibility = Visibility.Collapsed;
            }
            listeEmployes = SingletonEmployeProjet.getInstance().GetEmployeFromProject(item);
            lv_liste.ItemsSource = listeEmployes;
            if (lv_liste.Items.Count >= item.NbEmploye)
            {
                btn_Ajouter.Visibility = Visibility.Collapsed;
            }
        }

        private async void btn_State_Click(object sender, RoutedEventArgs e)
        {
            try {
                ContentDialogFermerProjet dialog = new ContentDialogFermerProjet();
                dialog.XamlRoot = GridBase.XamlRoot;
                dialog.Title = "Fermer le projet";
                dialog.PrimaryButtonText = "Oui";
                dialog.SecondaryButtonText = "Non";
                dialog.DefaultButton = ContentDialogButton.Primary;

                ContentDialogResult resultat = await dialog.ShowAsync();
                if (resultat == ContentDialogResult.Primary) {
                    tbl_Statut.Text = "Statut: Terminé";
                    btn_State.Visibility = Visibility.Collapsed;
                    SingletonBD.getInstance().UpdateProjetStatus(item.Num);
                }
            } catch {
            }
        }

        private void btn_Modifier_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(pageModifierProjet), item);
        }

        private void btn_Supprimer_Click(object sender, RoutedEventArgs e)
        {
            SingletonBD.getInstance().deleteEmployeeProjectByProject(item.Num);
            SingletonBD.getInstance().deleteProjet(item.Num);
            this.Frame.Navigate(typeof(pageGestionProjet));
        }

        private void lv_liste_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_Ajouter_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(pageBrowseEmploye), item);
        }
    }
}
