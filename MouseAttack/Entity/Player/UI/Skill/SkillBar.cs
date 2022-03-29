using Godot;
using MouseAttack.Skill;
using MouseAttack.Constants;
using MouseAttack.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MouseAttack.Skill.Data;

namespace MouseAttack.Entity.Player.UI.Skill
{
    public class SkillBar : Control
    {
        [Export]
        NodePath _playerSkillControllerPath = "";
        PlayerSkillController _playerSkillController;

        int SelectedSlotIndex
        {
            get => _playerSkillController.SelectedSlotIndex;
            set => _playerSkillController.SelectedSlotIndex = value;
        }

        public override void _Ready()
        {
            _playerSkillController = GetNode<PlayerSkillController>(_playerSkillControllerPath);

            _playerSkillController.CooldownStarted += (s, e) =>
                GetChild<SkillSlot>(e.Slot).Use(e.Cooldown);

            _playerSkillController.Listen(nameof(PlayerSkillController.SelectedSlotIndex),
                onChanged: () => GetChild<SkillSlot>(SelectedSlotIndex).Pressed = true);

            foreach (SkillSlot slot in GetChildren().OfType<SkillSlot>())
            {
                int index = slot.GetIndex();

                slot.Listen(nameof(SkillSlot.Item),
                    onChanged: () => _playerSkillController.SetSkill(slot.Item as CommonSkill, index));

                slot.Connect(Signals.Pressed, this, nameof(OnSkillSlotSelected),
                    new Godot.Collections.Array { index });
            }

            if (_playerSkillController.IsSelectedSlotEmpty)
                return;

            SkillSlot slotZero = GetChild<SkillSlot>(0);
            slotZero.Item = _playerSkillController.SelectedSkill;
            slotZero.Item.IsSlotted = true;
        }

        private void OnSkillSlotSelected(int selectedSlotIndex) =>
            SelectedSlotIndex = selectedSlotIndex;
    }
}
