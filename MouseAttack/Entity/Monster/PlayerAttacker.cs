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
        MonsterEntity _monsterEntity;
        Stage _stage;
        Timer _attackTimer;
        Vector2 _collisionPoint = Vector2.Zero;

        public override void _Ready()
        {
            _stage = this.GetStage();
            _monsterEntity = GetParent<MonsterEntity>();
            AddChild(_attackTimer = new Timer());
            _attackTimer.Connect(Signals.Timer.Timeout, this, nameof(OnAttackTimerTimeout));
            _attackTimer.WaitTime = _action.CooldownTimeout;
            _monsterEntity.Initialized += OnMonsterEntityInitialized;
        }

        private void OnMonsterEntityInitialized(object sender, EventArgs e)
        {
            _monsterEntity.PlayerDetector.Range = _action.Range;
            _monsterEntity.PlayerDetector.Detected += OnPlayerDetected;
            _monsterEntity.PlayerDetector.Lost += OnPlayerLost;
        }

        private void OnAttackTimerTimeout()
        {
            if (!_monsterEntity.PlayerDetector.IsInRange)
                return;

            var effectInstance = _action.GetWorldEffectInstance<CollidableEffect>();
            effectInstance.Action = _action;
            effectInstance.User = _monsterEntity.Character;
            effectInstance.GlobalPosition = _monsterEntity.GlobalPosition;
            effectInstance.AddChild(new Mover(_action, _collisionPoint));
            _stage.AddChild(effectInstance);
            _action.Use();
        }
        private void OnPlayerDetected(object sender, PlayerDetectedEventArgs e)
        {
            _collisionPoint = e.CollisionPoint;
            _attackTimer.Start();
        }
        private void OnPlayerLost(object sender, EventArgs e) => _attackTimer.Stop();
    }
}
