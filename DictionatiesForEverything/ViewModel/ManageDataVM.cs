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
        private CurrentState currentState;

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

            var filter = AllItemGlossary.Where(x => x.GlossaryId == SelectedGlossary.Id).ToList();
            FilteredItems.Clear();

            foreach (var item in filter)
            {
                FilteredItems.Add(item);
            }
        }

        public RelayCommand OpenAddGlossaryWindowCommand => new RelayCommand(obj => OpenNewWindow(new AddGlossaryAndTerms(), CurrentState.CreateGlossary));
        public RelayCommand OpenAddTermWindowCommand => new RelayCommand(obj => OpenNewWindow(new AddGlossaryAndTerms(), CurrentState.CreateTerm)); 

        private void OpenNewWindow(Window window, CurrentState state)
        {
            currentState = state;
            window.DataContext = this;
            window.Owner = _currentMainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }

        public RelayCommand AddNewGlossaryOrTermCommand => new RelayCommand(obj => 
        {
            if (currentState == CurrentState.CreateGlossary)
            {
                if (string.IsNullOrWhiteSpace(NewGlossaryName))
                {
                    MessageBox.Show("Название глоссария не может быть пустым или содержать только пробелы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                ManageData.AddGlossary(NewGlossaryName);
            }
            else if (currentState == CurrentState.CreateTerm)
            {
                if (string.IsNullOrWhiteSpace(NewGlossaryName))
                {
                    MessageBox.Show("Термин не может быть пустым или содержать только пробелы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                ManageData.AddGlossaryItem(NewGlossaryName, "", SelectedGlossary.Id);
            }

            var curerentSelectedGlossary = SelectedGlossary;
            Application.Current.Windows.OfType<AddGlossaryAndTerms>().FirstOrDefault()?.Close();
            AllGlossaries = ManageData.GetGlossaries();
            AllItemGlossary = ManageData.GetGlossaryItems();
            SelectedGlossary = AllGlossaries.Where(x => x.Id == curerentSelectedGlossary.Id).First();
            FilteredItemsBySelectedGlossary();
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
