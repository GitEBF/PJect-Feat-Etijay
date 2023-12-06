using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GestionProjets {
    public sealed partial class ContentDialogEmployeProjet : ContentDialog {

        int nbHeures;
        bool accepted = false;

        public ContentDialogEmployeProjet(string titre) {
            this.InitializeComponent();
            tbl_titre.Text = titre;
        }

        public int NbHeures { get => nbHeures; set => nbHeures = value; }
        public bool Accepted { get => accepted; set => accepted = value; }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args) {
            try {
                nbHeures = int.Parse(tb_heures.Text);
                accepted = true;
            } catch {

            }
        }
    }
}
