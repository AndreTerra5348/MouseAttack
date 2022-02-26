using Godot;
using MouseAttack.Extensions;
using MouseAttack.World.Autoload;
using System;

namespace MouseAttack.Entity.Castle
{
    public class Castle : CommonEntity
    {
        [Export]
        public CastleData CastleData;
        public override void _Ready()
        {
            var worldProxy = this.GetAutoload<WorldProxy>();
            worldProxy.RegistryCastle(this);
            CastleData.ResetResources();
            CastleData.Health.Depleted += OnHealthDepleted;
        }

        private void OnHealthDepleted(object sender, EventArgs e)
        {
            // Game Over
        }

        protected override void OnRightMouseButtonClicked()
        {
            GD.Print("clicked on castle");
        }


    }
}
