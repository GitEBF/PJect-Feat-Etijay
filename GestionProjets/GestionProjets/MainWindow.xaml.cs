using GestionProjets.Objets;
using GestionProjets.Singletons;
using Microsoft.UI;
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
using static System.Net.Mime.MediaTypeNames;

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
        public bool activePane = false;
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
                contentFrame.Navigate(typeof(pageGestionProjet));
                if (SingletonBD.getInstance().isUserLoggedIn())
                {
                    NavItem_Connexion.Content = "Déconnexion";
                    var foreground = (Color)Microsoft.UI.Xaml.Application.Current.Resources["SystemFillColorCritical"];
                    var background = (Color)Microsoft.UI.Xaml.Application.Current.Resources["SystemFillColorCriticalBackground"];
                    UpdateNavItemColor(foreground, background);
                }
            } else
            {
                contentFrame.Navigate(typeof(pageConnexion));
            }
        }
        public void UpdateNavItemConnexionContent(string newContent)
        {
            NavItem_Connexion.Content = newContent;
        }

        public void UpdateNavItemColor(Color foreground, Color background)
        {
            var foregroundColor = new SolidColorBrush(foreground);
            var backgroundColor = new SolidColorBrush(background);

            myBitmapIcon.Foreground = foregroundColor;
            NavItem_Connexion.Foreground = foregroundColor;
            NavItem_Connexion.Background = backgroundColor;
        }

        private void navView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            NavigationViewItem selectedNavItem = args.SelectedItem as NavigationViewItem;
            switch (selectedNavItem.Name)
            {
                case "NavItem_Creation":
                    contentFrame.Navigate(typeof(pageGestionProjet));
                    break;
                case "NavItem_Disposition":
                    contentFrame.Navigate(typeof(pageGestionEmploye));
                    break;
                case "NavItem_SaveFile":
                    contentFrame.Navigate(typeof(pageGestionClient));
                    break;
                case "NavItem_LoadFile":
                    if (activePane == false) {
                        activePane = true;

                        btExport_Click();
                    }
                    break;
                case "NavItem_Connexion":
                    contentFrame.Navigate(typeof(pageConnexion));
                    break;
            }
        }

        private async void btExport_Click()
        {
            var picker = new Windows.Storage.Pickers.FileSavePicker();
            picker.SuggestedFileName = "Projet";
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(new Window());
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);

            picker.SuggestedFileName = "test2";
            picker.FileTypeChoices.Add("Fichier CSV", new List<string>() { ".csv" });

            //crée le fichier
            Windows.Storage.StorageFile monFichier = await picker.PickSaveFileAsync();



            //écrit dans le fichier chacune des lignes du tableau
            await Windows.Storage.FileIO.WriteLinesAsync(monFichier, SingletonProjet.getInstance().getProjetListe().ToList().ConvertAll(x => x.ToString()), Windows.Storage.Streams.UnicodeEncoding.Utf8);
        }

    }
}

