using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DictionatiesForEverything.ViewModel
{
    internal class StyleTextViaToolBar
    {
        private bool _isBold;
        private FontWeight _fontWeight;
        public bool IsBold
        {
            get => _isBold;
            set
            {
                _isBold = value;
                FontWeight = value ? FontWeights.Bold : FontWeights.Normal;
                OnPropertyChanged(nameof(IsBold));
            }
        }

        public FontWeight FontWeight
        {
            get => _fontWeight;
            set
            {
                _fontWeight = value;
                OnPropertyChanged(nameof(FontWeight));
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
