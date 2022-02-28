using Godot;
using MouseAttack.Character;
using MouseAttack.Misc;
using MouseAttack.Subsystem;
using System;

namespace MouseAttack.Player
{
    public class PlayerEntity : Node2D, IResourceRegenerable
    {
        [Export]
        [MakeUnique]
        ResourceData _mana = null;
        [Export]
        [MakeUnique]
        StatsData _manaRegeneration = null;
        [Export]
        [MakeUnique]
        StatsData _damage = null;
        public bool IsResourceFull => _mana.IsFull;

        public event EventHandler ResourceUsed;

        public override void _Ready()
        {
            AddChild(new ResourceRegenerator(this));
            base._Ready();
            _mana.Reset();
        }


        public bool HasEnoughMana(float value) => _mana.CurrentValue >= value;

        public void UseMana(float value)
        {
            Console.WriteLine($"Antes de usar CurrentValue { _mana.CurrentValue}");
            _mana.Use(value);
            Console.WriteLine($"Depois de usar CurrentValue { _mana.CurrentValue}");
            ResourceUsed?.Invoke(this, EventArgs.Empty);
        }

        public void Regenerate() => _mana.Regenerate(_manaRegeneration.Value);
    }
}