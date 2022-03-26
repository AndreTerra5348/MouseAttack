using Godot;
using MouseAttack.Extensions;
using MouseAttack.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Misc
{
    public class QueueFreer : Node
    {
        [Export]
        int _duration = 1;
        [Export]
        float _queueFreeDelay = 0.3f;

        public override void _Ready()
        {
            TreeSharer.GetNode<GridController>().RoundFinished += OnRoundFinished;
            ElapseTurn();
        }

        private void OnRoundFinished(object sender, EventArgs e) =>
            ElapseTurn();

        async private void ElapseTurn()
        {
            _duration -= 1;

            if (_duration > 0)
                return;

            TreeSharer.GetNode<GridController>().RoundFinished -= OnRoundFinished;

            await this.CreateTimer(_queueFreeDelay);

            GetParent().QueueFree();
        }
    }
}
