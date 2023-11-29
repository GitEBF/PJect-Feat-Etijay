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
using System.Collections.ObjectModel;
using GestionProjets.Singletons;
using GestionProjets.Objets;
using GestionProjets.Projets;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GestionProjets
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class pageGestionProjet : Page
    {
        ObservableCollection<Projet> listeProjet = new ObservableCollection<Projet>();
        public pageGestionProjet()
        {
            this.InitializeComponent();
            listeProjet = SingletonProjet.getInstance().getProjetListe();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            listeProjet = SingletonProjet.getInstance().getProjetListe();
            lv_liste.ItemsSource = listeProjet;
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            string searchTermMatricule = searchBoxMatricule.Text.ToLower();
            string searchTermNom = searchBoxNom.Text.ToLower();

            var filteredList = listeProjet
                .Where(item => item.Num.ToLower().Contains(searchTermMatricule) && item.Titre.ToString().Contains(searchTermNom))
                .ToList();
            lv_liste.ItemsSource = filteredList;
        }

        private void lv_liste_ItemClick(object sender, SelectionChangedEventArgs e)
        {
            this.Frame.Navigate(typeof(pageZoomProjet), lv_liste.SelectedIndex);
        }

        private void btn_Ajouter_Click(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(pageCreationProjet));
        }
    }
}
