using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Misc.UI
{
    public class ObservableLabel : Label, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public new string Text
        {
            get => base.Text;
            set
            {
                if (base.Text == value)
                    return;
                base.Text = value;
                OnPropertyChanged();
            }
        }
    }
}
