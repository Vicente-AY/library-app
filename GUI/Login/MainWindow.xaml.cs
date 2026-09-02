using library_app.GUI.GuiMenu;
using ProgramExceptions;
using System;
using System.Collections.Generic;
using System.IO;
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
using Users;
using Utils;

namespace library_app.GUI.Login
{
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs f)
        {

            try {
                string login = txtUser.Text;
                string pass = txtPass.Password;

                if (string.IsNullOrWhiteSpace(login)){
                    MessageBox.Show("Please, introduce a Username");
                }
                else if(string.IsNullOrWhiteSpace(pass))
                {
                    MessageBox.Show("Please, introduce a Password");
                }

                if (!string.IsNullOrWhiteSpace(login) && !string.IsNullOrWhiteSpace(pass))
                {
                    LoginVerifier loginVerifier = new LoginVerifier();

                    User? user = loginVerifier.CheckLogin(login);

                    if (loginVerifier.CheckPass(pass, user))
                    {

                        UserSession.Login(user!);

                        UserMenu userMenu = new UserMenu();
                        userMenu.Show();

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("The information provided is incorrect. Please, try again.");
                    }
                }
            }
            catch (Exception ex) when (ex is FormatException ||
                                       ex is EmptyException ||
                                       ex is NumberOutOfRangeException ||
                                       ex is IOException)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Unexpected Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {

            Register register = new Register();
            register.Show();

            this.Close();
        }
    }
}
