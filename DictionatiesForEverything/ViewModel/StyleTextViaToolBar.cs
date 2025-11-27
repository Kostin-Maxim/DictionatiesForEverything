using DictionatiesForEverything.Model;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace DictionatiesForEverything.ViewModel
{
    internal class StyleTextViaToolBar : INotifyPropertyChanged
    {
        private FontFamily _selectedFontFamily;
        public FontFamily SelectedFontFamily
        {
            get => _selectedFontFamily;
            set
            {
                _selectedFontFamily = value;
                OnPropertyChanged(nameof(SelectedFontFamily));
            }
        }
        public ICollection<FontFamily> FontFamilyCollection => Fonts.SystemFontFamilies;

        public  StyleTextViaToolBar()
        {
            SelectedFontFamily = FontFamilyCollection.First();
        }


        private TextSelection? _selection;

        public void SetSelection(TextSelection? sel) => _selection = sel;
        public RelayCommand BoldCommand => new RelayCommand(obj => ApplyProp(TextElement.FontWeightProperty, FontWeights.Bold));

        private void ApplyProp(DependencyProperty prop, object value)
        {
            _selection.ApplyPropertyValue(prop, value);
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
