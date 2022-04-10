using Godot;
using MouseAttack.Extensions;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.World.UI
{
    public abstract class SystemButton<T> : Button where T : SystemPanel, ISharable
    {
        [Export]
        float BlickTimeout { get; set; } = 0.5f;
        [Export]
        NodePath SubButtonPath { get; set; }
        [Export]
        NodePath AlertIconPath { get; set; }

        protected T Panel => TreeSharer.GetNode<T>();
        protected TextureRect AlertIcon { get; private set; }
        TextureButton _subButton = null;

        public new bool Disabled
        {
            get => base.Disabled;
            set
            {
                base.Disabled = value;
                _subButton.Disabled = value;
            }
        }

        protected bool OnAlert { get; set; }

        public override void _Ready()
        {
            AlertIcon = GetNode<TextureRect>(AlertIconPath);
            _subButton = GetNode<TextureButton>(SubButtonPath);
        }

        public override void _Pressed() =>
            Panel.Visible = !Panel.Visible;

        protected void SetAlertEnable(bool enable)
        {
            OnAlert = enable;
            if (enable)
            {
                AlertIcon.Show();
                BlinkAlertIcon();
            }
        }

        async void BlinkAlertIcon()
        {
            AlertIcon.Visible = !AlertIcon.Visible;
            await this.CreateTimer(BlickTimeout);
            if (OnAlert)
                BlinkAlertIcon();
            else
                AlertIcon.Hide();
        }
    }
}
