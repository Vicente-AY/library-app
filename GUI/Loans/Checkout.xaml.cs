using Items;
using library_app.GUI.GuiMenu;
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

namespace library_app.GUI.Loans
{
    /// <summary>
    /// Lógica de interacción para Checkout.xaml
    /// </summary>
    public partial class Checkout : Window
    {

        private ObservableCollection<LibraryItem> items;

        public Checkout(List<LibraryItem> selectedItems)
        {
            InitializeComponent();

            this.items = new ObservableCollection<LibraryItem>(selectedItems);
            lvCheckoutItems.ItemsSource = items;
        }

        private void BtnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is LibraryItem itemToRemove)
            {
                items.Remove(itemToRemove);

                if (items.Count == 0)
                {
                    var result = MessageBox.Show("You have removed all the Selected Items",
                                 "Confirm", MessageBoxButton.RetryCancel, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Retry)
                    {
                        LoanWindow lWindow = new LoanWindow();
                        lWindow.Show();
                    }
                    if (result == MessageBoxResult.Cancel)
                    {
                        UserMenu uMenu = new UserMenu();
                        uMenu.Show();
                    }

                    this.Close();

                }
            }
        }

        private void BtnCancelLoan_Click(object sender, RoutedEventArgs e)
        {
            if(items.Count > 0)
            {
                var result = MessageBox.Show("You have items pending. Do you want to Cancel?", 
                             "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if(result == MessageBoxResult.Yes)
                {
                    items.Clear();

                    UserMenu uMenu = new UserMenu();
                    uMenu.Show();

                    this.Close();
                }
                else
                {
                    return;
                }
            }
        }

        public void BtnConfirmLoan_Click(object sender, RoutedEventArgs e)
        {
            //Hacer logica de cerar loans
        }

    }
}
