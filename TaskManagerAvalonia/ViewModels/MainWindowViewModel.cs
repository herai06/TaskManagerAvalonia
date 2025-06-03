using Avalonia.Controls;
using ReactiveUI;
using TaskManagerAvalonia.Models;

namespace TaskManagerAvalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public static MainWindowViewModel Instance;

        public MainWindowViewModel()
        {
            Instance = this;
        }

        public static PostgresContext myConnection = new PostgresContext();

        UserControl _uc = new Authorization();

        public UserControl UC
        {
            get => _uc;
            set => this.RaiseAndSetIfChanged(ref _uc, value);
        }
    }
}
