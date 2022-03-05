using Godot;
using MouseAttack.Entity;
using MouseAttack.Entity.Player;
using MouseAttack.Entity.Monster;
using MouseAttack.Extensions;
using MouseAttack.World;
using System;

namespace MouseAttack.Action.WorldEffect
{
    public class DamageEffect : CollidableEffect
    {
        new DamageAction Action => base.Action as DamageAction;

        Stage _stage;
        Stage Stage => _stage ?? (_stage = this.GetStage());

        Random _random = new Random();
        public override void _Ready()
        {
            base._Ready();
            if (Action.DamageTimeout > 0)
                QueueFreeTimer.WaitTime = Action.DamageTimeout * Action.Hits; ;
        }

        sealed async protected override void OnCollision(CommonEntity target)
        {
            for (int i = 0; i < Action.Hits; i++)
            {
                Action.ApplyDamage(User, target.Character);

                if (Action.HasTargetEffectScene())
                {
                    Node2D instance = Action.GetTargetEffectScene<Node2D>();
                    instance.GlobalPosition = target.GlobalPosition;
                    instance.Rotation = _random.Next();
                    instance.ZIndex = target.ZIndex - 1;
                    Stage.AddChild(instance);
                }

                if (Action.DamageTimeout == 0)
                    return;
                await ToSignal(GetTree().CreateTimer(Action.DamageTimeout), Signals.Timer.Timeout);
            }
        }
    }
}
