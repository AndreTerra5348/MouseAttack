using Godot;
using MouseAttack.Constants;
using MouseAttack.Extensions;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.World.UI
{
    public class SystemPanel : PanelContainer, INotifyPropertyChanged, ISharable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public new bool Visible
        {
            get => base.Visible;
            set
            {
                if (base.Visible == value)
                    return;
                base.Visible = value;
                OnPropertyChanged();
                if (Visible)
                    Update();
            }
        }

        public SystemPanel() =>
            TreeSharer.RegistryNode(this);


        public override void _UnhandledKeyInput(InputEventKey @event)
        {
            if (@event.IsActionPressed(InputAction.MenuPanel))
                Visible = !Visible;
        }
    }
}
