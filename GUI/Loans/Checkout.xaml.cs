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
using Users;

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
                    GoBack();

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

            var alreadyLoanedItems = items.Where(i => UserSession.currentUser!.loanList.Any(l => l.item.id == i.id)).ToList();

            if(alreadyLoanedItems.Count > 0)
            {
                string titles = string.Join(", ", alreadyLoanedItems.Select(i => i.title));

                var result = MessageBox.Show($"You have already Loaned the next Items: {titles}. Do you want to deselect the items?",
                             "Duplicate Loan", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if(result == MessageBoxResult.Yes)
                {
                    foreach(var i in alreadyLoanedItems)
                    {
                        items.Remove(i);
                    }
                    
                    if(items.Count == 0)
                    {
                        GoBack();
                    }
                }
            }

            //aquí iria la opción de entrar en la lista de espera.
            List<LibraryItem> waitList = items.Where(i => i.availability != Utils.Availability.Available).ToList();

            if(waitList.Count > 0)
            {
                string titles = string.Join(", ", waitList.Select(i => i.title));

                var result = MessageBox.Show($"The next Items are not available: {titles}. Do you want to deselect the items?",
                             "Not Available Items", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    foreach (var i in alreadyLoanedItems)
                    {
                        items.Remove(i);
                    }

                    if (items.Count == 0)
                    {
                        GoBack();
                    }
                }
            }

            //Hacer logica de crear loans
        }

        private void LvCheckoutItems_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (lvCheckoutItems.View is GridView gridView)
            {
                // Ancho total de la lista menos un margen para la barra de desplazamiento
                double totalWidth = lvCheckoutItems.ActualWidth - 35;

                if (totalWidth <= 0) return;

                // Asignamos porcentajes del ancho total a las columnas
                gridView.Columns[0].Width = totalWidth * 0.08; // Header / Imagen (5%)
                gridView.Columns[1].Width = totalWidth * 0.28; // Title (27%)
                gridView.Columns[2].Width = totalWidth * 0.18; // Creator (18%)
                gridView.Columns[3].Width = totalWidth * 0.08; // Year (8%)
                gridView.Columns[4].Width = totalWidth * 0.10; // Media (10%)
                gridView.Columns[5].Width = totalWidth * 0.10; // Genre (12%)
                gridView.Columns[6].Width = totalWidth * 0.10; // Availability (10%)
                gridView.Columns[7].Width = totalWidth * 0.10; // Botón (10%)
            }
        }

        private void GoBack()
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
