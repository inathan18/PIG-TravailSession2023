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
    public sealed partial class Login : ContentDialog
    {
        string nom;
        string pwd;
        public Login()
        {
            this.InitializeComponent();
        }


        public string Nom
        {
            get => nom; 
        }
        public string Pwd
        {
            get => pwd;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (Singleton.getInstance().checkUsers())
            {
                nom = tbxUser.Text;
                pwd = Singleton.getInstance().genererSHA256(pwdBox.Password);

                if (Singleton.getInstance().AdminLogin(nom, pwd) == false)
                {

                    args.Cancel = true;
                    tblError.Visibility = Visibility.Visible;
                }
                else
                {
                    tblError.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                bool errorUser, errorPwd;
                if (tbxUser.Text != null)
                {
                    nom = tbxUser.Text;
                    errorUser = false;
                }
                else
                {
                    errorUser = true;
                }
                if (pwdBox.Password != null)
                {
                    pwd = pwdBox.Password;
                    errorPwd = false;
                }
                else
                {
                    errorPwd = true;
                }

                if (errorUser == false && errorPwd == false)
                {
                    Singleton.getInstance().createUser(nom, pwd);
                }
                
                
                
                
            }
            
        }
    }
}
