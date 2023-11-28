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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GestionProjets
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class pageGestionClient : Page
    {
        ObservableCollection<Client> listeClient = new ObservableCollection<Client>();
        public pageGestionClient()
        {
            this.InitializeComponent();
            listeClient = SingletonClient.getInstance().getClientListe();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            listeClient = SingletonClient.getInstance().getClientListe();
            lv_liste.ItemsSource = listeClient;
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            string searchTermId = searchBoxId.Text.ToLower();
            string searchTermNom = searchBoxNom.Text.ToLower();

            var filteredList = listeClient
                .Where(item => item.Id.ToString().Contains(searchTermId) && item.Nom.ToString().Contains(searchTermNom))
                .ToList();
            lv_liste.ItemsSource = filteredList;
        }

        private void lv_liste_ItemClick(object sender, SelectionChangedEventArgs e)
        {
            this.Frame.Navigate(typeof(pageZoomClient), lv_liste.SelectedIndex);
        }
    }
}
