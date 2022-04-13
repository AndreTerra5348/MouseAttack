using Godot;
using MouseAttack.Constants;
using MouseAttack.Extensions;
using MouseAttack.Misc;
using MouseAttack.Misc.UI;
using MouseAttack.World;
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
    public abstract class CommonEntity : Area2D, ICursorHoverable, IInitializable
    {
        public event EventHandler Initialized;
        public Character Character { get; private set; }
        protected abstract string CharacterName { get; }
        protected FloatingLabelSpawner FloatingLabelSpawner { get; private set; }
        protected GridController GridController => 
            TreeSharer.GetNode<GridController>();

        public override void _Ready()
        {
            FloatingLabelSpawner = GetNode<FloatingLabelSpawner>(nameof(FloatingLabelSpawner));
            Character = GetNode<Character>(CharacterName);
            Character.Dead += (s, e) => OnDeath();
            ZIndex = ZOrder.Entity;            
        }

        protected void OnEntityInitialized() =>
            Initialized?.Invoke(this, EventArgs.Empty);

        public void QueueFloatingLabel(FloatingLabel floatingLabel) =>
            FloatingLabelSpawner.QueueFloatingLabel(floatingLabel);

        protected abstract void OnDeath();
        public abstract void OnCursorEntered();
        public abstract void OnCursorExited();
        public abstract SignalAwaiter Act();
    }
}
