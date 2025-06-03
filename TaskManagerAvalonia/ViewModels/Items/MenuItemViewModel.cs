using System.Collections.Generic;
using ReactiveUI;

namespace TaskManagerAvalonia.ViewModels.Items
{
    public class MenuItemViewModel : ReactiveObject
    {
        public string Title { get; }
        public bool IsLast { get; }
        public List<string> AllowedRoles { get; }

        public MenuItemViewModel(string title, bool isLast, List<string> allowedRoles = null)
        {
            Title = title;
            IsLast = isLast;
            AllowedRoles = allowedRoles ?? new List<string>();
        }
    }
}
