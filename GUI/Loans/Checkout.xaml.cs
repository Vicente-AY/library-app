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
using Items;

namespace library_app.GUI.Loans
{
    /// <summary>
    /// Lógica de interacción para Checkout.xaml
    /// </summary>
    public partial class Checkout : Window
    {

        List<LibraryItem> items = new List<LibraryItem>();

        public Checkout(List<LibraryItem> selectedItems)
        {
            InitializeComponent();

            this.items = selectedItems;
        }

        private void BtnRemoveItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnCancelLoan_Click(object sender, RoutedEventArgs e)
        {

        }

        public void BtnConfirmLoan_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
