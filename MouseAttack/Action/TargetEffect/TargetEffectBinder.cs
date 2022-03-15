using Godot;
using MouseAttack.Action.WorldEffect;
using MouseAttack.Extensions;
using MouseAttack.Misc;
using MouseAttack.World;
using System;

namespace MouseAttack.Action.TargetEffect
{
    public abstract class TargetEffectBinder<T> : Node where T : Node
    {        
        [Export]
        PackedScene _scene = null;
        Stage _stage;

        public override void _Ready()
        {
            base._Ready();
            _stage = this.GetStage();
            CommonWorldEffect effect = GetParent<CommonWorldEffect>();
            effect.ActionApplied += OnActionApplied;
        }

        private void OnActionApplied(object sender, ActionEventArgs e)
        {
            T instance = _scene.Instance<T>();
            OnBeforeAddChild(instance, e);
            _stage.AddChild(instance);
            OnAfterAddChild(instance, e);
        }

        protected virtual void OnBeforeAddChild(T instance, ActionEventArgs e)
        {
        }

        protected virtual void OnAfterAddChild(T instance, ActionEventArgs e)
        {
        }

    }
}
