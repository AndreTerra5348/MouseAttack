using Godot;
using MouseAttack.Characteristic;
using MouseAttack.Constants;
using System;

namespace MouseAttack.Characteristic.System
{
    public class ResourceRegenerator : Timer
    {
        [Export]
        NodePath _regenPath = "";        
        ResourcePool _resource;
        Stats _regen;

        public override void _EnterTree()
        {
            Connect(Signals.Timeout, this, nameof(Regenerate));
        }

        public override void _Ready()
        {
            _resource = GetParent<ResourcePool>();
            _resource.Used += OnResourceUsed;
            _regen = GetNode<Stats>(_regenPath);
        }

        private void OnResourceUsed(object sender, EventArgs e)
        {
            if (IsStopped())
                Start();
        }

        private void Regenerate()
        {
            _resource.Regenerate(_regen.Value);
            if (_resource.IsFull)
                Stop();
        }
    }
}
