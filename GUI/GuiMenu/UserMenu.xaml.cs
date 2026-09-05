using library_app.GUI.Loans;
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

namespace library_app.GUI.GuiMenu
{
    public partial class UserMenu : Window
    {

        public UserMenu()
        {
            InitializeComponent();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs f)
        {
            LoanWindow lWindow = new LoanWindow();
            lWindow.Show();

            this.Close();
        }

        private void BtnReturnItem_Click(object sender, RoutedEventArgs f)
        {
            if(UserSession.currentUser!.loanList.Count() <= 0)
            {
                MessageBox.Show("You have no active Loans");
                return;
            }

            ReturnWindow rWindow = new ReturnWindow();
            rWindow.Show();

            this.Close();
        }
    }
}
