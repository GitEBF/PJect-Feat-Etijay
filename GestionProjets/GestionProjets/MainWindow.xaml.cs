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
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GestionProjets
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }
        public MainWindow()
        {
            Instance = this;
            this.InitializeComponent();
            SingletonBD.getInstance().LoadAllEmploye();
            SingletonBD.getInstance().LoadAllClient();
            SingletonBD.getInstance().LoadAllProjet();
            SingletonBD.getInstance().LoadAllEmployeProjet();
            if (SingletonBD.getInstance().checkIfFirstUse())
            {
                //contentFrame.Navigate(typeof(pageGestionProjet));
                if (SingletonBD.getInstance().isUserLoggedIn())
                {
                    NavItem_Connexion.Content = "Déconnexion";
                }
            } else
            {
                //contentFrame.Navigate(typeof(pageConnexion));
            }
        }
        public void UpdateNavItemConnexionContent(string newContent)
        {
            NavItem_Connexion.Content = newContent;
        }

        private void navView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            NavigationViewItem selectedNavItem = args.SelectedItem as NavigationViewItem;
            switch (selectedNavItem.Name)
            {
                case "NavItem_Creation":
                    
                    break;
                case "NavItem_Disposition":
                    contentFrame.Navigate(typeof(pageGestionEmploye));
                    break;
                case "NavItem_SaveFile":
                    
                    break;
                case "NavItem_LoadFile":
                    
                    break;
                case "NavItem_Connexion":
                    contentFrame.Navigate(typeof(pageConnexion));
                    break;
            }
        }

    }
}

