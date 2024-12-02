

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HangmanAssignment.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;


        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(propertyName));
        }
    }
}
