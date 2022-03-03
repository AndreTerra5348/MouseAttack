using Godot;
using System;

namespace MouseAttack.Action.Module
{
    public interface IProjectile
    {
        float Speed { get; }
    }
    public class ProjectileEffect<T> : KinematicBody2D where T : DamageEffect, IProjectile
    {
        private readonly T _effect;
        private readonly Vector2 _direction;

        public ProjectileEffect(T effect, Vector2 direction)
        {
            _effect = effect;
            _direction = direction;
        }

        public override void _PhysicsProcess(float delta)
        {
            MoveAndCollide(_direction * _effect.Speed);
        }
    }
}
