using Godot;
using MouseAttack.Action.Module;
using MouseAttack.Action.Monster;
using MouseAttack.Action.WorldEffect;
using MouseAttack.Constants;
using MouseAttack.Entity.Player;
using MouseAttack.World;
using MouseAttack.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MouseAttack.Misc;

namespace MouseAttack.Entity.Monster
{
    public class MonsterActionController : Area2D
    {
        public event EventHandler Detected;
        public event EventHandler Lost;

        [Export]
        MonsterDamage _action = null;
        MonsterEntity _monsterEntity;
        Stage _stage;
        Timer _attackTimer = new Timer();
        Vector2 _collisionPoint = Vector2.Zero;
        bool _inRange = false;

        CircleShape2D Shape => ShapeOwnerGetShape(0, 0) as CircleShape2D;

        public override void _EnterTree()
        {
            Connect(Signals.AreaEntered, this, nameof(OnAreaEntered));
            Connect(Signals.AreaExited, this, nameof(OnAreaExited));
        }

        public override void _Ready()
        {
            Shape.Radius = _action.Range;
            _stage = this.GetStage();
            _monsterEntity = GetParent<MonsterEntity>();
            _attackTimer.Connect(Signals.Timeout, this, nameof(OnAttackTimerTimeout));
            _attackTimer.WaitTime = _action.CooldownTimeout;
            AddChild(_attackTimer);
        }

        private void OnAreaEntered(Area2D area)
        {
            if (!(area is PlayerEntity))
                return;

            _inRange = true;
            Shape2D areaShape = area.ShapeOwnerGetShape(0, 0);
            Godot.Collections.Array points = Shape.CollideAndGetContacts(GlobalTransform, areaShape, area.GlobalTransform);
            _collisionPoint = points.First<Vector2>();
            _attackTimer.Start();
            Detected?.Invoke(this, EventArgs.Empty);
        }

        private void OnAreaExited(Area2D area)
        {
            if (!(area is PlayerEntity))
                return;

            _inRange = false;
            Lost?.Invoke(this, EventArgs.Empty);
        }

        private void OnAttackTimerTimeout()
        {
            if (!_inRange)
                return;

            var effectInstance = _action.GetWorldEffectInstance<CollidableWorldEffect>();
            effectInstance.Action = _action;
            effectInstance.User = _monsterEntity.Character;
            effectInstance.GlobalPosition = _monsterEntity.GlobalPosition;
            effectInstance.AddChild(new Mover(_action, _collisionPoint));
            _stage.AddChild(effectInstance);
            _action.Use();
        }
    }
}
