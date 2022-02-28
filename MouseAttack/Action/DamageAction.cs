using Godot;
using MouseAttack.Entity.Monster;

namespace MouseAttack.Action
{
    public class DamageAction : WorldAction
    {
        DamageActionData _damageActionData;

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

            var damageAction = ActionData as DamageActionData;
            enemy.Hit(damageAction.Damage);
        }
    }
}
