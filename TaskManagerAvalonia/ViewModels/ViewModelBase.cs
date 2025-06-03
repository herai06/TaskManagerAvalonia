using System.ComponentModel;
using System.Runtime.CompilerServices;
using ReactiveUI;

namespace TaskManagerAvalonia.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
