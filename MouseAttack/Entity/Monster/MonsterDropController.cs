using Godot;
using MouseAttack.Entity.Player.Inventory;
using MouseAttack.Item.Data;
using MouseAttack.Item.Provider;
using MouseAttack.Misc;
using MouseAttack.Misc.UI;
using MouseAttack.World.Monster;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MouseAttack.Entity.Monster
{
    public class MonsterDropController : Node
    {
        const string ExpTextFormat = "+{0} EXP";
        Color _expTextColor = new Color("96eeaa12");

        DropLabelProvider DropLabelProvider =>
            TreeSharer.GetNode<DropLabelProvider>();
        FloatingLabelProvider FloatingLabelProvider =>
            TreeSharer.GetNode<FloatingLabelProvider>();
        DropProvider DropProvider =>
            TreeSharer.GetNode<DropProvider>();

        public override void _Ready()
        {
            MonsterEntity monsterEntity = GetParent<MonsterEntity>();

            monsterEntity.Dead += (s, e) =>
            {
                FloatingLabel expLabel = FloatingLabelProvider.GetLabel();
                expLabel.Text = String.Format(ExpTextFormat, monsterEntity.Character.Experience);
                expLabel.Position = monsterEntity.Position;
                expLabel.Color = _expTextColor;
                monsterEntity.QueueFloatingLabel(expLabel);

                List<CommonItem> drops = DropProvider.GetDrop(monsterEntity.Level);
                foreach (CommonItem drop in drops)
                {
                    FloatingLabel dropLabel = DropLabelProvider.GetLabel(drop);
                    dropLabel.Text = drop.Name;
                    dropLabel.Position = monsterEntity.Position;
                    monsterEntity.QueueFloatingLabel(dropLabel);
                }
            };
        }
    }
}
