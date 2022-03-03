using Godot;
using MouseAttack.Action.Monster;
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

        CollidableEffect _effect;
        CollidableEffect Effect => _effect ?? (_effect = GetParent<CollidableEffect>());

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
            Effect.Position = Effect.Position.MoveToward(_target, _action.Speed);
        }
    }
}
