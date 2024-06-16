using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Biathlon_Esthivaukan
{
    public class SuiviPerfViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _time1;
        private string _time2;
        private string _time3;

        public SuiviPerfViewModel()
        {
            // Initialize with default values
            Time1 = GetFormattedTime(Runpage.getTime1());
            Time2 = GetFormattedTime(Runpage.getTime2());
            Time3 = GetFormattedTime(Runpage.getTime3());
        }

        public string Time1
        {
            get => _time1;
            set => SetProperty(ref _time1, value);
        }

        public string Time2
        {
            get => _time2;
            set => SetProperty(ref _time2, value);
        }

        public string Time3
        {
            get => _time3;
            set => SetProperty(ref _time3, value);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value)) return false;
            backingStore = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private string GetFormattedTime(TimeSpan? time)
        {
            return time.HasValue ? time.Value.ToString(@"mm\:ss") : "A définir";
        }
    }
 
}
