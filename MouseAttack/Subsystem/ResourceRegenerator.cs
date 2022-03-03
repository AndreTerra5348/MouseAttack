using Godot;
using MouseAttack.Characteristic;
using System;

namespace MouseAttack.Subsystem
{
    public class ResourceRegenerator : Timer
    {
        private readonly ResourcePool _resource;
        private readonly Stats _regen;

        public ResourceRegenerator()
        {
        }

        public ResourceRegenerator(ResourcePool resource, Stats regen)
        {
            _resource = resource;
            _resource.Used += OnResourceUsed;
            _regen = regen;
        }        

        public override void _EnterTree()
        {
            Connect(Signals.Timer.Timeout, this, nameof(Regenerate));
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
