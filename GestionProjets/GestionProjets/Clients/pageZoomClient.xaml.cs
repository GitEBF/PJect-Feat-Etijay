using GestionProjets.Objets;
using GestionProjets.Projets;
using GestionProjets.Singletons;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.WindowsAppSDK.Runtime.Packages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GestionProjets
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class pageZoomClient : Page
    {
        Client item;
        int index;
        public pageZoomClient()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            item = (Client)e.Parameter;
            tbl_Id.Text = item.Id.ToString();
            tbl_Nom.Text = "Nom: " + item.Nom;
            tbl_Adresse.Text = "Adresse: " + item.Adresse;
            tbl_numTelephone.Text = "T�l�phone: " + item.NumTelephone;
            tbl_email.Text = "Email: " + item.Email;
            if (!SingletonBD.getInstance().isUserLoggedIn())
            {
                admin.Visibility = Visibility.Collapsed;
            }
        }

        private void btn_Modifier_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(pageModifierClient), item);
        }

        private void btn_Supprimer_Click(object sender, RoutedEventArgs e)
        {
            SingletonBD.getInstance().DeleteProjectsByClient(item.Id);
            SingletonBD.getInstance().deleteClient(item.Id);
            this.Frame.Navigate(typeof(pageGestionClient));
        }

        private void btn_Projet_Click(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(pageCreationProjet), item);
        }
    }
}
