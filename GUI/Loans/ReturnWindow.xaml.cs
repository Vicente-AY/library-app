using Items;
using library_app.GUI.GuiMenu;
using library_app.Utils;
using Loans;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace library_app.GUI.Loans
{
    public partial class ReturnWindow : Window
    {
        User currentUser = null!;
        private ObservableCollection<Loan> loans;

        public ReturnWindow()
        {

            InitializeComponent();

            this.currentUser = UserSession.currentUser!;
            this.loans = new ObservableCollection<Loan>(currentUser.loanList);
            lvReturnItems.ItemsSource = loans;
        }

        private void BtnReturnAll_Click(object sender, RoutedEventArgs e)
        {

            ReturnItems rItems = new ReturnItems();
            foreach (var loan in loans)
            {
                rItems.ReturnLoan(loan, currentUser);
            }

            MessageBox.Show("You have Return all the Loaned Items",
            "Confirm", MessageBoxButton.OK, MessageBoxImage.Information);

            GoBack();
        }

        private void BtnReturnItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is Loan returnLoan)
            {
                ReturnItems rItems = new ReturnItems();
                rItems.ReturnLoan(returnLoan, currentUser);

                loans.Remove(returnLoan);

                if (loans.Count == 0)
                {
                    MessageBox.Show("You have Return all the Loaned Items",
                    "Confirm", MessageBoxButton.OK, MessageBoxImage.Information);

                    GoBack();
                }
            }
        }

        private void BtnCancelReturn_Click(object sender, RoutedEventArgs e)
        {
            GoBack();
        }

        private void GoBack()
        {

            UserMenu uMenu = new UserMenu();
            uMenu.Show();

            this.Close();
        }

        private void LvCheckoutItems_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (lvReturnItems.View is GridView gridView)
            {
                // Ancho total de la lista menos un margen para la barra de desplazamiento
                double totalWidth = lvReturnItems.ActualWidth - 35;

                if (totalWidth <= 0) return;

                // Asignamos porcentajes del ancho total a las columnas
                gridView.Columns[0].Width = totalWidth * 0.10; // Header / Imagen (5%)
                gridView.Columns[1].Width = totalWidth * 0.30; // Title (27%)
                gridView.Columns[2].Width = totalWidth * 0.20; // Fehca creacion (18%)
                gridView.Columns[3].Width = totalWidth * 0.20; // Fecha de devolucion esperada(8%)
                gridView.Columns[4].Width = totalWidth * 0.10; // prestamo extendido (10%)
                gridView.Columns[5].Width = totalWidth * 0.10; // Botón (10%)
            }
        }
    }
}
