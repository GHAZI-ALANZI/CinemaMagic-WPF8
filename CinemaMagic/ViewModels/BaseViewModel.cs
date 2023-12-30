using System.ComponentModel;

namespace CinemaMagic.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        // INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
