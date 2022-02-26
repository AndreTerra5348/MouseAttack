using Godot;
using MouseAttack.Extensions;
using MouseAttack.World.Autoload;
using System;

namespace MouseAttack.Entity.Monster
{
    public class CommonMonster : CommonEntity
    {
        [Export]
        public CommonMonsterData MonsterData;

        Vector2 _castleDirection;
        public override void _Ready()
        {
            MonsterData.ResetResources();
            MonsterData.Health.Depleted += OnHealthDepleted;
            var worldProxy = this.GetAutoload<WorldProxy>();
            _castleDirection = Position.DirectionTo(worldProxy.CastlePosition);
            LookAt(worldProxy.CastlePosition);            
        }

        async private void OnHealthDepleted(object sender, EventArgs e)
        {
            MonsterData.Health.Depleted -= OnHealthDepleted;
            // death animation

            // TODO: change to await for the animation to finish
            await ToSignal(GetTree().CreateTimer(0.5f), Signals.Timer.Timeout);
            QueueFree();
        }

        public override void _PhysicsProcess(float delta)
        {
            MoveAndCollide(_castleDirection * MonsterData.MovementSpeed.Value);
        }

        public void Hit(int damage)
        {
            MonsterData.Health.Decrease(damage);
            GD.PrintT("Enemy Health:", MonsterData.Health);
            // Hit feedback
            // Iframes
        }

        protected override void OnMouseEntered()
        {
            GD.PrintT("OnMouseEntered CommonMonster");
        }
    }
}

