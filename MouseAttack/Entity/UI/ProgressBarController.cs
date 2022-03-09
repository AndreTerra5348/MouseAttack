using Godot;
using MouseAttack.Misc;

namespace MouseAttack.Entity.Player.UI
{
    public abstract class ProgressBarController<T> : ObserverNode where T : ObservableNode
    {
        [Export]
        NodePath _sourcePath = "";
        protected T Source { get; private set; }
        protected ProgressBar ProgressBar { get; private set; }
        public override void _Ready()
        {
            base._Ready();
            Source = GetNode<T>(_sourcePath);
            ProgressBar = GetNode<ProgressBar>(nameof(ProgressBar));
        }
    }
}
