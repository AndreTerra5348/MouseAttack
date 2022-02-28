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
        [MakeCopy]
        public ResourceData Mana { get; private set; }
        [Export]
        [MakeCopy]
        public StatsData ManaRegen { get; private set; }
        [Export]
        [MakeCopy]
        public StatsData Damage { get; private set; }
        public bool IsResourceFull => Mana.IsFull;

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