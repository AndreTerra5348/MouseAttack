using Godot;
using MouseAttack.Constants;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MouseAttack.Entity
{
    /// <summary>
    /// Base class for PlayerEntity and MonsterEntity
    /// </summary>
    public abstract class CommonEntity : Area2D
    {
        public Character Character { get; private set; }
        protected abstract string CharacterName { get; }

        public override void _Ready()
        {
            Character = GetNode<Character>(CharacterName);
            Character.Dead += OnDeath;
            ZIndex = ZOrder.Entity;
        }

        public override void _EnterTree()
        {
            Connect(Signals.MouseEntered, this, nameof(OnMouseEntered));
            Connect(Signals.MouseExited, this, nameof(OnMouseExited));
        }
        protected abstract void OnDeath(object sender, EventArgs e);
        protected abstract void OnMouseEntered();
        protected abstract void OnMouseExited();
    }
}
