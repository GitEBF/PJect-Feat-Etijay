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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GestionProjets.Employees
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class pageBrowseEmploye : Page
    {
        ObservableCollection<Employe> listeEmploye = new ObservableCollection<Employe>();
        Projet item;
        Employe employeModif;
        int index;

        public pageBrowseEmploye()
        {
            this.InitializeComponent();
            SingletonBD.getInstance().LoadAllEmploye();
            listeEmploye = SingletonEmploye.getInstance().getEmployeListeNoProjects();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            lv_liste.ItemsSource = listeEmploye;
            if (e.Parameter is Projet projetParam)
            {
                item = projetParam;
                tb_info.Text = "Cliquez sur un nom pour ajouter un/une employé(e) au projet.";
            }
            

            if (listeEmploye.Count == 0) {
                tb_info.Text = "Aucun employé est disponible pour le moment";
            }
            
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e) {
            string searchTermNom = searchBoxNom.Text.ToLower();

            var filteredList = listeEmploye
                .Where(item => item.Nom.ToString().Contains(searchTermNom))
                .ToList();
            lv_liste.ItemsSource = filteredList;
        }

        private async void lv_liste_ItemClick(object sender, SelectionChangedEventArgs e) {
            if (item != null)
            {       
                try {
                    Employe employe = SingletonEmploye.getInstance().getEmployeNoProjects(lv_liste.SelectedIndex);
                    string titre = "Veuillez mettre les heures que " + employe.Prenom + " à travailler dans le projet " + item.Titre;
                    ContentDialogEmployeProjet dialog = new ContentDialogEmployeProjet(titre);
                    dialog.XamlRoot = GridBase.XamlRoot;
                    dialog.Title = "Ajout d'un employé.";
                    dialog.PrimaryButtonText = "Accepter";
                    dialog.SecondaryButtonText = "Canceller";
                    dialog.DefaultButton = ContentDialogButton.Primary;

                    ContentDialogResult resultat = await dialog.ShowAsync();
                    if (resultat == ContentDialogResult.Primary && dialog.Accepted) {
                        SingletonBD.getInstance().addEmployeProjet(item.Num, employe.Matricule, dialog.NbHeures);
                    }
                    
                    this.Frame.Navigate(typeof(pageGestionProjet));
                } catch { 
                }
     
                  
            }
        }

    }
}
