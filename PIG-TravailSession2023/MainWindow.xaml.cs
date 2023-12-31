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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace PIG_TravailSession2023
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

        }
        

        private void navView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var item = (NavigationViewItem)args.SelectedItem;

            switch (item.Name)
            {
                case "iEmploye":
                    mainFrame.Navigate(typeof(PageEmploye));
                    break;

                case "iClient":
                    mainFrame.Navigate(typeof(PageClient));
                    break;

                case "iProjet":
                    mainFrame.Navigate(typeof(PageProjet));
                    break;

                case "iAdmin":
                    mainFrame.Navigate(typeof(PageAdmin));
                    break;
            }
        }

        private async void btnWrite_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileSavePicker();

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);

            picker.SuggestedFileName = "ListeProjets";
            picker.FileTypeChoices.Add("Fichier CSV", new List<string>() { ".csv" });

            Windows.Storage.StorageFile monFichier = await picker.PickSaveFileAsync();

            List<Projet> liste = Singleton.getInstance().getListeProjetBDCSV();
            if (monFichier != null)
            {
                await Windows.Storage.FileIO.WriteLinesAsync(monFichier, liste.ConvertAll(x=>x.toCSV()), Windows.Storage.Streams.UnicodeEncoding.Utf8);
            }

        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (Singleton.getInstance().getStatut())
            {
                Singleton.getInstance().logout();
            }
            else
            {


                if (Singleton.getInstance().checkUsers())
                {
                    Login login = new Login();
                    login.XamlRoot = MainGrid.XamlRoot;
                    login.Title = "Authentification";
                    login.PrimaryButtonText = "Se connecter";
                    login.CloseButtonText = "Annuler";
                    login.DefaultButton = ContentDialogButton.Primary;
                    ContentDialogResult resultat = await login.ShowAsync();


                    if (resultat == ContentDialogResult.Primary)
                    {


                    }
                }
                else
                {
                    Login create = new Login();
                    create.XamlRoot = MainGrid.XamlRoot;
                    create.Title = "Cr�ation de compte";
                    create.PrimaryButtonText = "Cr�er le compte";
                    create.CloseButtonText = "Annuler";
                    create.DefaultButton = ContentDialogButton.Primary;
                    ContentDialogResult resultat = await create.ShowAsync();

                    if (resultat == ContentDialogResult.Primary)
                    {

                    }


                }
            }
        }
    }
}
