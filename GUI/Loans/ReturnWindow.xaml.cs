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

namespace library_app.GUI.Loans
{
    /// <summary>
    /// Lógica de interacción para ReturnWindow.xaml
    /// </summary>
    public partial class ReturnWindow : Window
    {
        public ReturnWindow()
        {
            InitializeComponent();
        }

        private void BtnReturnAll_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnReturnItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnCancelReturn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LvCheckoutItems_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (lvReturnItems.View is GridView gridView)
            {
                // Ancho total de la lista menos un margen para la barra de desplazamiento
                double totalWidth = lvReturnItems.ActualWidth - 35;

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
    }
}
