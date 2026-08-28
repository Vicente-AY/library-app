using ProgramExceptions;
using System;
using System.Collections.Generic;
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
using Data;

namespace library_app.GUI.Login
{
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        public void BtnRegister_Click(object sender, RoutedEventArgs f)
        {

            try
            {
                string login = txtUser.Text;
                string pass = txtPass.Password;
                string pass2 = txtPass2.Password;

                if (!pass.Equals(pass2))
                {
                    throw new NotMatchException("The passwords dont match. Please try again");
                }

                string userLogin = RegistrationVerifier.CheckLogin(login);
                string userPass = RegistrationVerifier.CheckPass(pass);

                User newUser = new User(userLogin, userPass);

                using (LibraryContext db = new LibraryContext())
                {
                    db.Users.Add(newUser);
                    db.SaveChanges();
                }

                MessageBox.Show("User register successfuly", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);

                MainWindow main = new MainWindow();
                main.Show();

                this.Close();

            }
            catch(Exception ex) when (ex is FormatException ||
                                      ex is ShortStringException ||
                                      ex is NotFirstUppercaseException ||
                                      ex is WhiteSpaceException ||
                                      ex is NotMatchException)
            {
                MessageBox.Show(ex.Message, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Unexpected Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void BtnLogin_Click(object sender, RoutedEventArgs e)
        {

            MainWindow main = new MainWindow();
            main.Show();

            this.Close();
        }
    }
}
