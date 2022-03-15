using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Player.UI
{
    public class CooldownTextureProgress : TextureProgress
    {
        public void StartCooldown(float cooldown)
        {
            MaxValue = cooldown;
            Value = cooldown;
            SetProcess(true);
        }
        public override void _Ready() => SetProcess(false);
        public override void _Process(float delta)
        {
            Value -= delta;
            if (Value <= 0)
                SetProcess(false);
        }
    }
}
