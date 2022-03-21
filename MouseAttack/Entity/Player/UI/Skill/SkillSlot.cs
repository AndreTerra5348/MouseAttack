using Godot;
using MouseAttack.Skill.Data;
using MouseAttack.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Player.UI.Skill
{
    public class DragAndDropData<T> : Node
    {
        public T Data { get; private set; }
        public event EventHandler Dropped;

        public DragAndDropData(T data)
        {
            Data = data;
        }

        public void DataDropped() => Dropped?.Invoke(this, EventArgs.Empty);
    }

    public class SkillSlot : Button, INotifyPropertyChanged
    {
        [Export]
        NodePath _cooldownPath = "";
        CooldownTextureProgress _cooldown;
        [Export]
        NodePath _iconPath = "";
        TextureRect _icon;

        public new Texture Icon
        {
            get => _icon.Texture;
            set => _icon.Texture = value;
        }

        CommonSkill _skill;
        public CommonSkill Skill
        {
            get => _skill;
            private set
            {
                if (_skill == value)
                    return;
                _skill = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override void _Ready()
        {
            _cooldown = GetNode<CooldownTextureProgress>(_cooldownPath);
            _icon = GetNode<TextureRect>(_iconPath);
            RemoveSkill();
        }

        public override bool CanDropData(Vector2 position, object data) =>
            data is DragAndDropData<CommonSkill>;

        public override void DropData(Vector2 position, object data) =>
            SetSkill((data as DragAndDropData<CommonSkill>).Data);

        public override object GetDragData(Vector2 position)
        {
            SetDragPreview(new SkillDragPreview(_icon.Duplicate() as TextureRect));
            var data = new DragAndDropData<CommonSkill>(Skill);
            RemoveSkill();
            return data;
        }

        public override Control _MakeCustomTooltip(string forText) =>
            this.MakeCustomTooltip(forText);

        public void Use(int cooldown) =>
            _cooldown.StartCooldown(cooldown);

        public void SetSkill(CommonSkill skill)
        {
            Skill = skill;
            Icon = Skill.Icon;
            HintTooltip = Skill.Tooltip;
        }

        private void RemoveSkill()
        {
            Skill = null;
            Icon = null;
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}