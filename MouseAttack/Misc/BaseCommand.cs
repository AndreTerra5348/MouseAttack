using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MouseAttack.Misc
{
    public abstract class BaseCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) =>
            true;

        public abstract void Execute(object parameter);

        protected void OnCanExecute() =>
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
