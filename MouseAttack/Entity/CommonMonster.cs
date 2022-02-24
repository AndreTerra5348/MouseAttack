using Godot;
using System;

namespace MouseAttack.Entity
{
    public class CommonMonster : KinematicBody2D
    {
        [Export]
        public CommonMonsterData EnemyData;
        public override void _Ready()
        {
            EnemyData.ResetResources();
            EnemyData.Health.Depleted += OnHealthDepleted;
        }

        private void OnHealthDepleted(object sender, EventArgs e)
        {
            // death animation
            // wait animation to finish
            // queue free
            GD.Print("Enemy is Dead");
        }

        public override void _PhysicsProcess(float delta)
        {
            // Move(Vector2.Down);
        }

        public void Move(Vector2 direction)
        {
            MoveAndCollide(direction * EnemyData.MovementSpeed.Value);
        }

        public void Hit(int damage)
        {
            EnemyData.Health.Decrease(damage);
            GD.PrintT("Enemy Health:", EnemyData.Health);
            // Hit feedback
            // Iframes
        }
    }
}

