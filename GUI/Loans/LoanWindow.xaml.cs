using Data;
using Items;
using library_app.GUI.GuiMenu;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Security.Cryptography;
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

            GenreFilter();
            LoadItems();
            ChangeSelection();
            LoadSumary();
        }

        private void LoadItems()
        {
            using(var db = new LibraryContext())
            {
                List<LibraryItem> items = db.LibraryItems.Where(i=> !i.lost).AsNoTracking().OrderBy(i => i.title).ToList();
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

            if(Available.IsChecked == true && i.item.availability != Availability.Available)
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

        private void GenreFilter()
        {

            if (allItems == null) return;

            bool anyTypeFilterActive = Books.IsChecked == true || Films.IsChecked == true || Music.IsChecked == true || Videogames.IsChecked == true;

            IEnumerable<SelectableItem> filteredItems = allItems.Where(i =>
            {
                if (anyTypeFilterActive)
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

                return true;
            });

            List<string> availableGenre = new List<string> { "All" };
            availableGenre.AddRange(filteredItems.Select(i => i.item.genre).Distinct().OrderBy(g => g).ToList());

            string? previousGenereFilter = cmbGenre.SelectedItem as string;

            cmbGenre.ItemsSource = availableGenre;

            cmbGenre.SelectedItem = availableGenre.Contains(previousGenereFilter ?? "") ? previousGenereFilter : "All";
        }

        private void Filter_Changed(object sender, RoutedEventArgs e)
        {
            GenreFilter();
            view?.Refresh();
        }

        private void BtnConfirmLoan_Click(object sender, RoutedEventArgs e)
        {

            selectedItems = allItems.Where(i => i.Selected).Select(s => s.item).ToList();
            int maxLoans = UserSession.currentUser is User ? 5 : 10;

            if (selectedItems.Count == 0)
            {
                MessageBox.Show("Please, select at least an item");
                return;
            }

            //Limite de loans
            else if (UserSession.currentUser!.loanList.Count >= maxLoans && selectedItems.Any(i => i.availability == Availability.Available))
            {
                MessageBox.Show("You have already reach the Loan limit. You can only select Lent or in Mainteneance Items for waitlist");
                return;
            }

            Checkout checkItems = new Checkout(selectedItems);
            checkItems.Show();

            this.Close();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {

            selectedItems = allItems.Where(i => i.Selected).Select(s => s.item).ToList();

            if (selectedItems.Count > 0)
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

        private void LoadSumary()
        {

            int maxAllowedLoan = UserSession.currentUser is User ? 5 : 10;

            int selected = allItems.Count(i => i.Selected);
            int activeLoans = UserSession.currentUser!.loanList.Count;
            int available = maxAllowedLoan - activeLoans;

            txtSelectedCount.Text = selected.ToString();
            txtActiveLoans.Text = activeLoans.ToString();
            txtAvailableLoanSlots.Text = available.ToString();

        }

        private void ChangeSelection()
        {
            foreach (var selectableItem in allItems)
            {
                selectableItem.PropertyChanged += SelectableItem_PropertyChanged;
            }
        }

        private void SelectableItem_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectableItem.Selected))
            {
                LoadSumary();
            }
        }
    }
}
