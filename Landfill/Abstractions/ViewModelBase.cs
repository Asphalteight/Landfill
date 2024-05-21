using System;
using System.Windows.Input;

namespace Landfill.Abstractions
{
    public abstract class ViewModelBase : ObservableObject
    {
        public static void RunCommand(Action<object> action) => new ViewModelCommand(action).Execute(null);
        public static void RunCommand(ICommand command) => command.Execute(null);
    }
}
