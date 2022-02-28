using Godot;
using MouseAttack.Character;
using System;

namespace MouseAttack.Subsystem
{
    public interface IResourceRegenerable
    {
        event EventHandler ResourceUsed;
        bool IsResourceFull { get; }
        void Regenerate();
    }
    public class ResourceRegenerator : Timer
    {
        IResourceRegenerable _resourceRegenerable;


        public ResourceRegenerator()
        {
        }

        public ResourceRegenerator(IResourceRegenerable resourceRegenerable)
        {
            _resourceRegenerable = resourceRegenerable;
            _resourceRegenerable.ResourceUsed += OnResourceUsed;
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

        void Regenerate()
        {
            _resourceRegenerable.Regenerate();
            if (_resourceRegenerable.IsResourceFull)
                Stop();
        }
    }
}
