using Godot;
using MouseAttack.Entity;
using MouseAttack.Entity.Player;
using MouseAttack.Entity.Monster;
using MouseAttack.Extensions;
using MouseAttack.World;

namespace MouseAttack.Action
{
    public class DamageEffect : CollidableEffect
    {
        DamageAction _damageAction;
        public override void _Ready()
        {
            base._Ready();
            _damageAction = Action as DamageAction;
        }

        protected override void OnAreaEntered(Area2D area)
        {
            CommonEntity target = area as CommonEntity;
            if (target == null)
                return;
            _damageAction.ApplyDamage(User, target.Character);
        }
    }
}
