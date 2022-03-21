using Godot;
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
        int _duration = 2;
        int _turns = 0;

        public override void _Ready()
        {
            TreeSharer.GetNode<GridController>().RoundFinished += OnRoundFinished;
        }

        private void OnRoundFinished(object sender, EventArgs e)
        {
            _turns++;
            if (_turns < _duration)
                return;
            TreeSharer.GetNode<GridController>().RoundFinished -= OnRoundFinished;
            GetParent().QueueFree();
        }
    }
}
