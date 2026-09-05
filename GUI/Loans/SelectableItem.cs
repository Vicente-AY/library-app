using Items;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Runtime.CompilerServices;

namespace library_app.GUI.Loans
{
    public class SelectableItem : INotifyPropertyChanged
    {
        public LibraryItem item { get; }

        private bool selected;
        public bool Selected
        {
            get => selected; 
            set { selected = value; OnPropertyChanged(); } 
        }

        public SelectableItem(LibraryItem item)
        {
            this.item = item;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }





}
