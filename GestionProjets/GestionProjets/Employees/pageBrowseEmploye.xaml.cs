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

        public pageBrowseEmploye()
        {
            this.InitializeComponent();
            SingletonBD.getInstance().LoadAllEmploye();
            listeEmploye = SingletonEmploye.getInstance().getEmployeListeNoProjects();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            listeEmploye = SingletonEmploye.getInstance().getEmployeListe();
            lv_liste.ItemsSource = listeEmploye;
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e) {
            string searchTermNom = searchBoxNom.Text.ToLower();

            var filteredList = listeEmploye
                .Where(item => item.Nom.ToString().Contains(searchTermNom))
                .ToList();
            lv_liste.ItemsSource = filteredList;
        }

        private void lv_liste_ItemClick(object sender, SelectionChangedEventArgs e) {
            this.Frame.Navigate(typeof(pageZoomEmploye), lv_liste.SelectedIndex);
        }
    }
}
