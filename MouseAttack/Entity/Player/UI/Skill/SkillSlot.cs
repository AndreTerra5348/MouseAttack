using Godot;
using MouseAttack.Skill.Data;
using MouseAttack.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Player.UI.Skill
{
    public class SkillSlot : Slot<CommonSkill>
    {
        [Export]
        NodePath _cooldownPath = "";
        CooldownProgressBar _cooldown;

        public override void _Ready()
        {
            base._Ready();
            _cooldown = GetNode<CooldownProgressBar>(_cooldownPath);
            RemoveItem();
        }

        public void Use(int cooldown) =>
            _cooldown.StartCooldown(cooldown);
    }
}