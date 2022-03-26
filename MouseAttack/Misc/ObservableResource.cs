using Godot;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MouseAttack.Misc
{
    public class ObservableResource : Resource, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
