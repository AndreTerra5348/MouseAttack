using Godot;
using MouseAttack.Entity.Monster;
using MouseAttack.Extensions;
using MouseAttack.World;

namespace MouseAttack.Action
{
    public class DamageEffect : CollidableEffect
    {
        Stage _stage;
        public override void _Ready()
        {
            base._Ready();
            Position = GetViewport().GetMousePosition();
            _stage = this.GetStage();
        }

        protected override void OnBodyEntered(Node body)
        {
            MonsterEntity monster = body as MonsterEntity;
            if (monster == null)
                return;
            MonsterCharacter monsterCharacter = monster.Character;

            var damageAction = CommonAction as DamageAction;
            var isCritical = _stage.Player.IsCritical;
            var playerDamage = _stage.Player.Damage.Value;
            var playerCriticalDamage = _stage.Player.CriticalDamage.Value;
            playerDamage = isCritical ? playerDamage * playerCriticalDamage : playerDamage;
            var skillDamage = damageAction.Damage;
            var monsterDefense = monsterCharacter.Defense.Value;
            var finalDamage = playerDamage + skillDamage - monsterDefense;
            monsterCharacter.Hit(finalDamage < 0 ? 0 : finalDamage);
        }
    }
}
