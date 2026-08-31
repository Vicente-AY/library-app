using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Data;
using Microsoft.EntityFrameworkCore;
using Items;
using library_app.GUI.GuiMenu;

namespace library_app.GUI.Loans
{
    /// <summary>
    /// Lógica de interacción para LoanWindow.xaml
    /// </summary>
    public partial class LoanWindow : Window
    {
        private List<SelectableItem> allItems = new List<SelectableItem>();
        List<LibraryItem> selectedItems = new List<LibraryItem>();
        private ICollectionView view = null!;

        public LoanWindow()
        {
            InitializeComponent();

            List<string> genre = new List<string> { "All" };
            
            using(var db = new LibraryContext())
            {
                List<string> genreList = db.LibraryItems.Select(i => i.genre).Distinct().ToList();
                genre.AddRange(genreList);
            }

            cmbGenre.ItemsSource = genre;
            cmbGenre.SelectedIndex = 0;

            System.Diagnostics.Debug.WriteLine("Base directory: " + AppDomain.CurrentDomain.BaseDirectory);
            System.Diagnostics.Debug.WriteLine("Current directory: " + Environment.CurrentDirectory);

            LoadItems();
        }

        private void LoadItems()
        {
            using(var db = new LibraryContext())
            {
                List<LibraryItem> items = db.LibraryItems.AsNoTracking().ToList();
                allItems = items.Select(i => new SelectableItem(i)).ToList();
                System.Diagnostics.Debug.WriteLine(items[0].imageRoute);
            }

            view = CollectionViewSource.GetDefaultView(allItems);
            view.Filter = FilterItem;
            icItems.ItemsSource = view;
        }

        private bool FilterItem(object obj)
        {
            if (obj is not SelectableItem i) return false;

            bool anyFilterActive = Books.IsChecked == true || Films.IsChecked == true || Music.IsChecked == true || Videogames.IsChecked == true;

            if (anyFilterActive)
            {
                bool typeMatch = (Books.IsChecked == true && i.item is Book) ||
                (Films.IsChecked == true && i.item is Film) ||
                (Music.IsChecked == true && i.item is MusicAlbum) ||
                (Videogames.IsChecked == true && i.item is Videogame);

                if (!typeMatch)
                {
                    return false;
                }
            }

            if(Available.IsChecked == true && i.item.availability != Utils.Availability.Available)
            {
                return false;
            }

            if(cmbGenre.SelectedItem is string genre && genre != "All" && i.item.genre != genre)
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                string query = txtSearch.Text.Trim();
                if (!i.item.title.Contains(query, StringComparison.OrdinalIgnoreCase)) return false;
            }

            return true;
        }

        private void Filter_Changed(object sender, RoutedEventArgs e)
        {
            view?.Refresh();
        }

        private void BtnConfirmLoan_Click(object sender, RoutedEventArgs e)
        {
            selectedItems = allItems.Where(i => i.Selected).Select(s => s.item).ToList();

            if(selectedItems.Count == 0)
            {
                MessageBox.Show("Please, select at least an item");
            }

            //cambiar a otra vista donde el usuario vea lo que ha seleccionado

        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if(selectedItems.Count > 0)
            {
                var result = MessageBox.Show("You have items selected. Are you sure you want to cancell the operation?",
                    "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if(result == MessageBoxResult.No)
                {
                    return;
                }
            }

            selectedItems.Clear();

            UserMenu userM = new UserMenu();
            userM.Show();

            this.Close();

        }
    }
}
