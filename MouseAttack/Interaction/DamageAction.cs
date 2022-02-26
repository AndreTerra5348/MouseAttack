using Godot;
using MouseAttack.Entity.Monster;

namespace MouseAttack.Interaction
{
    public class DamageAction : WorldAction
    {

        protected override void OnBodyEntered(Node body)
        {
            var enemy = body as CommonMonster;
            if (enemy == null)
                return;

            var damageActionData = GetActionData<DamageActionData>();
            enemy.Hit(damageActionData.Damage);
        }
    }
}
