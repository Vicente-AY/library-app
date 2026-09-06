using Items;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace library_app.GUI.Loans
{
    /// <summary>
    /// Lógica de interacción para ItemDetailsWindow.xaml
    /// </summary>
    public partial class ItemDetailsWindow : Window
    {
        public ItemDetailsWindow(LibraryItem item)
        {
            InitializeComponent();
            LoadData(item);
        }

        public void LoadData(LibraryItem item)
        {
            lblItemTitle.Text = item.title;

            if (!string.IsNullOrEmpty(item.imageRoute))
            {
                try
                {
                    string fullPath = Path.IsPathRooted(item.imageRoute)
                        ? item.imageRoute
                        : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, item.imageRoute);

                    if (File.Exists(fullPath))
                    {
                        var bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(fullPath, UriKind.Absolute);
                        bitmap.CacheOption = BitmapCacheOption.OnLoad; // evita bloquear el archivo
                        bitmap.EndInit();
                        imgCover.Source = bitmap;
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"Imagen no encontrada: {fullPath}");
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error cargando imagen: {ex.Message}");
                }
            }

            stkDetails.Children.Clear();
            stkDetails.Children.Add(lblItemTitle);

            switch (item)
            {
                case Book book:
                    ChargeBookData(book);
                    break;
                case Film film:
                    ChargeFilmData(film);
                    break;
                case MusicAlbum album:
                    ChargeAlbumData(album);
                    break;
                case Videogame vGame:
                    ChargeGameData(vGame);
                    break;
            }
        }

        private void ChargeBookData(Book book)
        {

            AddDataField("ID: ", book.id.ToString());
            AddDataField("Media: ", book.media);
            AddDataField("Release Year: ", book.year.ToString());
            AddDataField("Genre: ", book.genre);

            AddDataField("Number of Pages: ", book.pages.ToString());
            AddDataField("Author/s: ", string.Join(", ", book.author));
            AddDataField("ISBN: ", book.isbn);
            AddDataField("Editorial: ", book.editorial);
            AddDataField("Original Language: ", book.originalLanguage);
            AddDataField("Version Language: ", book.versionLanguage);
        }

        private void ChargeFilmData(Film film)
        {
            AddDataField("ID: ", film.id.ToString());
            AddDataField("Media: ", film.media);
            AddDataField("Release Year: ", film.year.ToString());
            AddDataField("Genre: ", film.genre);

            AddDataField("Duration: ", film.duration.ToString() + " Minutes");
            AddDataField("Director/s: ", string.Join(", ", film.director));
            AddDataField("Screen Writer: ", film.screenWriter);
            AddDataField("Production Company: ", film.productionCompany);
            AddDataField("Available Languages: ", string.Join(", ", film.versionLanguages));
            AddDataField("Available Format: ", film.format);
        }

        private void ChargeAlbumData(MusicAlbum album)
        {
            AddDataField("ID: ", album.id.ToString());
            AddDataField("Media: ", album.media);
            AddDataField("Release Year: ", album.year.ToString());
            AddDataField("Genre: ", album.genre);

            AddDataField("Duration: ", album.duration.ToString() + " Minutes");
            AddDataField("Band: ", album.band);
            AddDataField("Songs: ", string.Join(", ", album.listOfSongs));
            AddDataField("Recording Studio: ", album.recordingStudio);
            AddDataField("Label: ", album.label);
        }

        private void ChargeGameData(Videogame game)
        {
            AddDataField("ID: ", game.id.ToString());
            AddDataField("Media: ", game.media);
            AddDataField("Release Year: ", game.year.ToString());
            AddDataField("Genre: ", game.genre);

            AddDataField("Developer: ", game.developer);
            AddDataField("Engine: ", game.engine);
            AddDataField("Publisher: ", game.publisher);
            AddDataField("Available Languages: ", string.Join(", ", game.versionLanguages));
            AddDataField("Available Platform: ", game.platform);
        }

        private void AddDataField(string label, string? value)
        {
            if (string.IsNullOrWhiteSpace(value) || value == "0") return;

            StackPanel row = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(0, 0, 0, 10)
            };

            TextBlock txtLabel = new TextBlock
            {
                Text = label.ToUpper(),
                FontSize = 11,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.Gray
            };

            TextBlock txtValue = new TextBlock
            {
                Text = value,
                FontSize = 14,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0,2,0,0)
            };

            row.Children.Add(txtLabel);
            row.Children.Add(txtValue);

            stkDetails.Children.Add(row);
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
