using Godot;
using MouseAttack.Character;
using MouseAttack.Misc;
using MouseAttack.Subsystem;
using System;

namespace MouseAttack.Player
{
    public class PlayerEntity : Node, IResourceRegenerable
    {
        [Export]
        [MakeCopy]
        public ResourcePool Mana { get; private set; }
        [Export]
        [MakeCopy]
        public Stats ManaRegen { get; private set; }
        [Export]
        [MakeCopy]
        public Stats Damage { get; private set; }
        [Export]
        [MakeCopy]
        public Stats CriticalRate { get; private set; }
        [Export]
        [MakeCopy]
        public Stats CriticalDamage { get; private set; }

        public bool IsResourceFull => Mana.IsFull;

        public bool IsCritical => CriticalRate.Value <= _random.Next(100);

        Random _random = new Random();

        public event EventHandler ResourceUsed;

        public override void _Ready()
        {
            AddChild(new ResourceRegenerator(this));
            base._Ready();
            Mana.Reset();
        }


        public bool HasEnoughMana(float value) => Mana.CurrentValue >= value;

        public void UseMana(float value)
        {
            Mana.Use(value);
            ResourceUsed?.Invoke(this, EventArgs.Empty);
        }

        public void Regenerate() => Mana.Regenerate(ManaRegen.Value);
    }
}