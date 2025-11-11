using DictionatiesForEverything.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DictionatiesForEverything.ViewModel
{
    public class ManageDataVM : INotifyPropertyChanged
    {
        private ObservableCollection<Glossary> _allGlossaries = ManageData.GetGlossaries();
        private ObservableCollection<GlossaryItem> _allItemGlossary = ManageData.GetGlossaryItems();
        private ObservableCollection<GlossaryItem>? _filteredItems = new ObservableCollection<GlossaryItem>();
        private Glossary? _selectedGlossary;
        private GlossaryItem? _selectedGlossaryItem;
        
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

        // Реализация интерфейса INotifyPropertyChanged для обновления UI
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
