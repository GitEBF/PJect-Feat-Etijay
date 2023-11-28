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
        int index;
        public pageZoomProjet()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            index = (int)e.Parameter;
            item = SingletonProjet.getInstance().GetProjet(index);
            tbl_Num.Text = item.Num.ToString();
            tbl_Titre.Text = "Titre: " + item.Titre;
            tbl_DateDebut.Text = "Date de d�but: " + item.DateDebut;
            tbl_Description.Text = "Description: " + item.Description;
            tbl_Budget.Text = "Budget: " + item.Budget;
            tbl_NbEmploye.Text = "Nombre employ�: " + item.NbEmploye;
            tbl_TotalSalaire.Text = "Salaire total: " + item.TotalSalaire;
            tbl_IdClient.Text = "Id client: " + item.IdClient;
            tbl_Statut.Text = "Statut: " + item.Statut;
            if (!SingletonBD.getInstance().isUserLoggedIn())
            {
                admin.Visibility = Visibility.Collapsed;
            }
        }

        private void btn_Modifier_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(pageModifierProjet), item);
        }

        private void btn_Supprimer_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(pageGestionProjet));
            SingletonBD.getInstance().deleteProjet(SingletonProjet.getInstance().GetProjet(index).Num);
            SingletonProjet.getInstance().supprimer(index);
        }
    }
}