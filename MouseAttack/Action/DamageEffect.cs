using Godot;
using MouseAttack.Entity;
using MouseAttack.Entity.Castle;
using MouseAttack.Entity.Monster;
using MouseAttack.Entity.Player;
using MouseAttack.Extensions;
using MouseAttack.World;

namespace MouseAttack.Action
{
    public class DamageEffect : CollidableEffect
    {
        IAttacker _attacker;
        DamageAction _damageAction;
        public override void _Ready()
        {
            base._Ready();
            _attacker = Character as IAttacker;
            _damageAction = CommonAction as DamageAction;
        }

        protected override void OnAreaEntered(Area2D area)
        {
            CastleEntity castle = area as CastleEntity;
            if (castle == null)
                return;
            ApplyDamage(castle.Character);
        }

        protected override void OnBodyEntered(Node body)
        {
            MonsterEntity monster = body as MonsterEntity;
            if (monster == null)
                return;
            ApplyDamage(monster.Character);
        }

        private void ApplyDamage(IDefender defender) => _damageAction.ApplyDamage(_attacker, defender);
    }
}
