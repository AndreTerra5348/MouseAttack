using Godot;
using MouseAttack.Action.Monster;
using MouseAttack.Action.WorldEffect;
using System;

namespace MouseAttack.Action.Module
{
    /// <summary>
    /// When added as a child of CollidableEffect Scene, moves to target
    /// </summary>
    public class Mover : Node2D
    {
        readonly IMonsterAction _action;
        readonly Vector2 _target;
        DamageWorldEffect _effect;

        public override void _Ready()
        {
            base._Ready();
            _effect = GetParent<DamageWorldEffect>();
        }

        public Mover()
        {
        }

        public Mover(IMonsterAction action, Vector2 target)
        {
            _action = action;
            _target = target;
        }

        public override void _Process(float delta)
        {
            _effect.Position = _effect.Position.MoveToward(_target, _action.Speed).Snapped(Vector2.One);
        }
    }
}
