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
        int index;
        public pageZoomProjet()
        {
            this.InitializeComponent();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            index = (int)e.Parameter;
            item = SingletonProjet.getInstance().GetProjet(index);
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
            }
            if (item.Statut == "Terminé")
            {
                btn_State.Visibility = Visibility.Collapsed;
            }

            AddTextBlocksToStackPanel(item.NbEmploye);
        }

        private void AddTextBlocksToStackPanel(int numberOfTextBlocks) {
            SingletonBD.getInstance().LoadAllEmployeProjet();
            ObservableCollection<Employe> listeEmployes = SingletonEmployeProjet.getInstance().GetEmployeFromProject(item);
            for (int i = 1; i <= numberOfTextBlocks; i++) {
                TextBlock textBlock = new TextBlock();
                if (listeEmployes.Count >= i)
                {
                    textBlock.Text = $"{listeEmployes[i - 1].Prenom + ' ' + listeEmployes[i - 1].Nom}";
                }
                else
                {
                    textBlock.Text = "Vide";
                }
                textBlock.Margin = new Thickness(5);
                stk_employee.Children.Add(textBlock);
                Button button = new Button();
                if (listeEmployes.Count >= i)
                {
                    int index = i - 1;
                    button.Content = "Modifier";
                    button.Click += (sender, e) => {
                        if (index >= 0 && index < listeEmployes.Count)
                        {
                            this.Frame.Navigate(typeof(pageBrowseEmploye), new Tuple<Projet, Employe>(item, listeEmployes[index]));
                            //on va voir
                        }
                    };
                }
                else
                {
                    button.Content = "Ajouter";
                    button.Click += (sender, e) => { this.Frame.Navigate(typeof(pageBrowseEmploye), item); };
                }
                button.FontSize = 9;
                button.Margin = new Thickness(5);
                stk_buttonEmployee.Children.Add(button);
            }
        }

        private void btn_State_Click(object sender, RoutedEventArgs e)
        {
            tbl_Statut.Text = "Statut: Terminé";
            btn_State.Visibility = Visibility.Collapsed;
            SingletonBD.getInstance().UpdateProjetStatus(item.Num);
        }

        private void btn_Modifier_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(pageModifierProjet), item);
        }

        private void btn_Supprimer_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(pageGestionProjet));
            SingletonBD.getInstance().deleteEmployeeProjectByProject(SingletonProjet.getInstance().GetProjet(index).Num);
            SingletonBD.getInstance().deleteProjet(SingletonProjet.getInstance().GetProjet(index).Num);
            SingletonProjet.getInstance().supprimer(index);
        }
    }
}
