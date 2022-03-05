using Godot;
using MouseAttack.Action;
using MouseAttack.Action.Module;
using MouseAttack.Action.Monster;
using MouseAttack.Action.WorldEffect;
using MouseAttack.Extensions;
using MouseAttack.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Monster
{
    public class PlayerAttacker : Node
    {
        [Export]
        MonsterDamage _action = null;
        PlayerDetector PlayerDetector => MonsterEntity.PlayerDetector;
        MonsterEntity _monsterEntity;
        MonsterEntity MonsterEntity => _monsterEntity ?? (_monsterEntity = GetParent<MonsterEntity>());
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
            MonsterEntity.Initialized += OnMonsterEntityInitialized;
        }

        private void OnMonsterEntityInitialized(object sender, EventArgs e)
        {
            PlayerDetector.Range = _action.Range;
            PlayerDetector.Detected += OnPlayerDetected;
            PlayerDetector.Lost += OnPlayerLost;
        }

        private void OnAttackTimerTimeout()
        {
            if (!PlayerDetector.IsInRange)
                return;

            var effectInstance = _action.GetWorldEffectInstance<CollidableEffect>();
            effectInstance.Action = _action;
            effectInstance.User = MonsterEntity.Character;
            effectInstance.GlobalPosition = MonsterEntity.GlobalPosition;
            effectInstance.AddChild(new Mover(_action, _collisionPoint));
            Stage.AddChild(effectInstance);
            _action.Use();
        }
        private void OnPlayerDetected(object sender, PlayerDetectedEventArgs e)
        {
            _collisionPoint = e.CollisionPoint;
            AttackTimer.Start();
        }
        private void OnPlayerLost(object sender, EventArgs e) => AttackTimer.Stop();
    }
}
