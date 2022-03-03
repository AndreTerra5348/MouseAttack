using Godot;
using MouseAttack.Action;
using MouseAttack.Action.Module;
using MouseAttack.Action.Monster;
using MouseAttack.Extensions;
using MouseAttack.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Monster
{
    public class CastleAttacker : Node
    {
        [Export]
        MonsterDamage _action;
        CastleDetector _castleDetector;
        MonsterEntity _monsterEntity;

        Stage _stage;
        Stage Stage => _stage ?? (_stage = this.GetStage());

        Timer _attackTimer;
        Timer AttackTimer => _attackTimer ?? (_attackTimer = new Timer());
        Vector2 _collisionPoint = Vector2.Zero;

        public override void _Ready()
        {
            AddChild(AttackTimer);
            AttackTimer.Connect(Signals.Timer.Timeout, this, nameof(OnAttackTimerTimeout));
            AttackTimer.WaitTime = _action.CooldownTimeout;
        }

        public void SetMonsterEntity(MonsterEntity monsterEntity)
        {
            _monsterEntity = monsterEntity;
            _castleDetector = _monsterEntity.CastleDetector;
            _castleDetector.Range = _action.Range;
            _castleDetector.Detected += OnCastleDetected;
            _castleDetector.Lost += OnCastleLost;
        }

        private void OnAttackTimerTimeout()
        {
            if (!_castleDetector.IsInRange)
                return;

            var effectInstance = _action.GetEffectInstance<CollidableEffect>();
            effectInstance.Action = _action;
            effectInstance.User = _monsterEntity.Character;
            effectInstance.Position = _monsterEntity.Position;
            effectInstance.AddChild(new Mover(_action, _collisionPoint));
            Stage.AddChild(effectInstance);
            _action.Use();
        }
        private void OnCastleDetected(object sender, CastleDetectedEventArgs e)
        {
            _collisionPoint = e.CollisionPoint;
            AttackTimer.Start();
        }
        private void OnCastleLost(object sender, EventArgs e) => AttackTimer.Stop();
    }
}
