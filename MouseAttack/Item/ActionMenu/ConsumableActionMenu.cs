using Godot;
using MouseAttack.GUI;
using MouseAttack.Item.Data;
using MouseAttack.Misc;
using MouseAttack.Misc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item.ActionMenu
{
    public class ConsumableActionMenu : ItemActionMenu
    {
        const string UseText = "Use";

        [Export]
        NodePath CooldownProgressPath = "";
        CooldownBar _cooldownProgress;
        protected FloatingLabelProvider FloatingLabelProvider =>
            TreeSharer.GetNode<FloatingLabelProvider>();
        new ConsumableItem Item => base.Item as ConsumableItem;
        public override void _Ready()
        {
            base._Ready();
            _cooldownProgress = GetNode<CooldownBar>(CooldownProgressPath);
            Item.CooldownStarted += OnCooldownStarted;
        }        

        public override void _ExitTree() =>
            Item.CooldownStarted -= OnCooldownStarted;

        private void OnCooldownStarted(object sender, CooldownEventArgs e) =>
            _cooldownProgress.Start(e.Cooldown);

        public override void AddAction()
        {
            if (Item.CanUse)
                AddAction(UseText, Use);
            base.AddAction();
        }

        private void Use()
        {
            if (!Item.CanUse)
                return;

            Item.Use();

            var label = FloatingLabelProvider.GetLabel();
            label.Text = Item.UseText;
            label.Color = Item.EffectColor;
            label.Position = RectGlobalPosition;
            DragPreviewParent.AddChild(label);
        }
    }
}
