using Godot;
using MouseAttack.Constants;
using MouseAttack.Entity.Player;
using MouseAttack.Skill.WorldEffect;
using MouseAttack.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Misc
{
    public class Mover : Node
    {
        const float _speed = 200.0f;
        PlayerEntity PlayerEntity => TreeSharer.GetNode<PlayerEntity>();
        Vector2 Target => PlayerEntity.GlobalPosition;
        CommonWorldEffect _effect;

        public Mover() { }

        public Mover(CommonWorldEffect effect)
        {
            AddChild(_effect = effect);
            _effect.QueueFreed += (s, e) =>
                QueueFree();
        }

        public override void _Ready()
        {
            var damageEffect = _effect as DamageWorldEffect;
            if (damageEffect == null)
                return;
            damageEffect.Sprite.LookAt(Target);
        }


        public override void _Process(float delta) =>
            _effect.GlobalPosition = _effect.GlobalPosition.MoveToward(Target, _speed * delta);
    }
}
