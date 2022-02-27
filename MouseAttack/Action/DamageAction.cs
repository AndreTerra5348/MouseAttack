using Godot;
using MouseAttack.Entity.Monster;

namespace MouseAttack.Action
{
    public class DamageAction : WorldAction
    {
        DamageActionData _damageActionData;
        public override void SetData(CommonActionData commonActionData)
        {
            base.SetData(commonActionData);
            _damageActionData = commonActionData as DamageActionData;
        }

        protected override void OnBodyEntered(Node body)
        {
            var enemy = body as CommonMonster;
            if (enemy == null)
                return;

            enemy.Hit(_damageActionData.Damage);
        }
    }
}
