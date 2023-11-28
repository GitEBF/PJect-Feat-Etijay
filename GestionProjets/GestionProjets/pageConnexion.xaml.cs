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
using Windows.UI;
using GestionProjets.Singletons;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GestionProjets
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class pageConnexion : Page
    {
        public pageConnexion()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            MainWindow mainWindow = MainWindow.Instance;
            if (SingletonBD.getInstance().isUserLoggedIn())
            {
                SingletonBD.getInstance().loginLogout(); 
                this.Frame.Navigate(typeof(pageGestionProjet));
                mainWindow.UpdateNavItemConnexionContent("Connexion");
                var foreground = (Color)Microsoft.UI.Xaml.Application.Current.Resources["SystemFillColorSuccess"];
                var background = (Color)Microsoft.UI.Xaml.Application.Current.Resources["SystemFillColorSuccessBackground"];
                mainWindow.UpdateNavItemColor(foreground, background);
            }
            if (SingletonBD.getInstance().checkIfFirstUse()) {
                title.Text = "Connexion compte administrateur";
                createText.Text = "Se connecter";
            }
        }

        private void btConnexion_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = MainWindow.Instance;
            if (nomAdd.Text is null) {
                nomErreur.Text = "Veuillez écrire un nom valide";
            } else if (passwordAdd.Text is null)
            {
                passwordErreur.Text = "Veuillez écrire un mot de passe valide";
            } else
            {
                if (SingletonBD.getInstance().checkIfFirstUse())
                {
                    if (SingletonBD.getInstance().Connexion(nomAdd.Text, passwordAdd.Text))
                    {
                        SingletonBD.getInstance().loginLogout();
                        mainWindow.UpdateNavItemConnexionContent("Déconnexion");
                        var foreground = (Color)Microsoft.UI.Xaml.Application.Current.Resources["SystemFillColorCritical"];
                        var background = (Color)Microsoft.UI.Xaml.Application.Current.Resources["SystemFillColorCriticalBackground"];
                        mainWindow.UpdateNavItemColor(foreground, background);
                        this.Frame.Navigate(typeof(pageGestionProjet));
                    } else
                    {
                        nomErreur.Text = "Le nom ou le mot de passe est invalide";
                        passwordErreur.Text = "Le nom ou le mot de passe est invalide";
                    }
                } else
                {
                    SingletonBD.getInstance().createUser(nomAdd.Text, passwordAdd.Text);
                    mainWindow.UpdateNavItemConnexionContent("Déconnexion");
                    var foreground = (Color)Microsoft.UI.Xaml.Application.Current.Resources["SystemFillColorCritical"];
                    var background = (Color)Microsoft.UI.Xaml.Application.Current.Resources["SystemFillColorCriticalBackground"];
                    mainWindow.UpdateNavItemColor(foreground, background);
                    this.Frame.Navigate(typeof(pageGestionProjet));
                }
            }
        }
    }
}
