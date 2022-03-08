using Godot;
using MouseAttack.Entity;
using MouseAttack.Entity.Player;
using MouseAttack.Entity.Monster;
using MouseAttack.Extensions;
using MouseAttack.World;
using System;
using MouseAttack.Constants;

namespace MouseAttack.Action.WorldEffect
{
    public class DamageWorldEffect : CollidableWorldEffect
    {
        new DamageAction Action => base.Action as DamageAction;

        Random _random = new Random();
        Stage _stage;


        public override void _Ready()
        {
            base._Ready();
            _stage = this.GetStage();
            if (Action.DamageTimeout > 0)
                QueueFreeTimer.WaitTime = Action.DamageTimeout * Action.Hits;
        }

        sealed async protected override void OnCollision(CommonEntity target)
        {
            for (int i = 0; i < Action.Hits; i++)
            {
                float damage = Action.GetDamage(User, target.Character);
                target.Character.Hit(damage);
                OnActionApplied(target, damage);
                if (target.Character.IsDead)
                    User.Experience += target.Character.Experience;
                if (Action.DamageTimeout == 0)
                    return;
                await ToSignal(GetTree().CreateTimer(Action.DamageTimeout), Signals.Timeout);
            }
        }
    }
}
