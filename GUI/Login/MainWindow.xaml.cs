using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Utils;
using Users;

namespace library-app.GUI.Login
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(){

            string login = txtUser.Text;
            string pass = txtPass.Text;

            if(!string.IsNullOrWhiteSpace(login) && !string.IsNullOrWhiteSpace(pass)){
                LoginVerifier loginVerifier = new LoginVerifier();

                User? user = loginVerifier.CheckLogin(login);

                if(loginVerifier.CheckPass(pass, user))
                {

                }
            }


            

        }

        private void BtnRegister_Click(){

        }
    }
}
