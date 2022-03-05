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
        private readonly IMonsterAction _action;
        private readonly Vector2 _target;

        DamageEffect _effect;
        DamageEffect Effect => _effect ?? (_effect = GetParent<DamageEffect>());

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
            Effect.Position = Effect.Position.MoveToward(_target, _action.Speed).Snapped(Vector2.One);
        }
    }
}
