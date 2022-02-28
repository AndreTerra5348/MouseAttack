using Godot;
using MouseAttack.Entity.Monster;

namespace MouseAttack.Action
{
    public class DamageEffect : CollidableEffect
    {
        DamageAction _damageActionData;

        public override void _Ready()
        {
            base._Ready();
            Position = GetViewport().GetMousePosition();
        }
        protected override void OnBodyEntered(Node body)
        {
            var enemy = body as CommonMonster;
            if (enemy == null)
                return;

            var damageAction = CommonAction as DamageAction;
            enemy.Hit(damageAction.Damage);
        }
    }
}
