using DictionatiesForEverything.Model;
using DictionatiesForEverything.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DictionatiesForEverything.ViewModel
{
    public class ManageDataVM : INotifyPropertyChanged
    {
        private ObservableCollection<Glossary> _allGlossaries = ManageData.GetGlossaries();
        private ObservableCollection<GlossaryItem> _allItemGlossary = ManageData.GetGlossaryItems();
        private ObservableCollection<GlossaryItem>? _filteredItems = new ObservableCollection<GlossaryItem>();
        private Glossary? _selectedGlossary;
        private GlossaryItem? _selectedGlossaryItem;
        private string? _newGlossaryName;
        private Window _currentMainWindow = Application.Current.MainWindow;

        public ManageDataVM()
        {
            SelectedGlossary = AllGlossaries[0];
        }
        public string? NewGlossaryName
        {
            get => _newGlossaryName;
            set
            {
                _newGlossaryName = value;
                OnPropertyChanged(nameof(NewGlossaryName));
            }
        }

        public ObservableCollection<Glossary> AllGlossaries
        { 
            get => _allGlossaries;
            set
            {
                _allGlossaries = value;
                OnPropertyChanged(nameof(AllGlossaries));
            }
        }
       
        public ObservableCollection<GlossaryItem> AllItemGlossary
        {
            get => _allItemGlossary;
            set
            {
                _allItemGlossary = value;
                OnPropertyChanged(nameof(AllItemGlossary));
            }
        }

        public ObservableCollection<GlossaryItem> FilteredItems
        { 
            get => _filteredItems;
            set 
            {
                _filteredItems = value;
                OnPropertyChanged(nameof(FilteredItems));
            }
            
        }

        public Glossary SelectedGlossary
        {
            get => _selectedGlossary;
            set 
            { 
                _selectedGlossary = value;
                OnPropertyChanged(nameof(SelectedGlossary));
                FilteredItemsBySelectedGlossary();
            }
        }
        
        public GlossaryItem SelectedGlossaryItem
        {
            get => _selectedGlossaryItem;
            set
            {
                _selectedGlossaryItem = value;
                OnPropertyChanged(nameof(SelectedGlossaryItem));
            }
        }

        private void FilteredItemsBySelectedGlossary()
        {
            if (SelectedGlossary == null)
            { 
                FilteredItems.Clear();
                return;
            }

            var filter = _allItemGlossary.Where(x => x.GlossaryId == _selectedGlossary.Id).ToList();
            FilteredItems.Clear();

            foreach (var item in filter)
            {
                FilteredItems.Add(item);
            }
        }

        public RelayCommand OpenAddGlossaryWindowCommand => new RelayCommand(obj =>
        {
            AddGlossary addGlossary = new AddGlossary();
            addGlossary.DataContext = this;
            addGlossary.Owner = _currentMainWindow;
            addGlossary.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addGlossary.ShowDialog();
            AllGlossaries = ManageData.GetGlossaries();
        });

        public RelayCommand AddNewGlossaryCommand => new RelayCommand(obj => 
        {
            if (string.IsNullOrWhiteSpace(NewGlossaryName))
            {
                MessageBox.Show("Название глоссария не может быть пустым или содержать только пробелы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ManageData.AddGlossary(NewGlossaryName);
            Application.Current.Windows.OfType<AddGlossary>().FirstOrDefault()?.Close();
            AllGlossaries = ManageData.GetGlossaries();
        });

        // Реализация интерфейса INotifyPropertyChanged для обновления UI
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
