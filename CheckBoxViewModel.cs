using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;

namespace Biathlon_Esthivaukan
{
    public class CheckBoxViewModel : INotifyPropertyChanged
    {
        private bool _isChecked;
        private string _text;

        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (SetProperty(ref _isChecked, value))
                {
                    // Utilisation de DisplayAlert pour afficher un message
                    Debug.WriteLine($"{Text} is checked: {_isChecked}");
                }
            }
        }

        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
