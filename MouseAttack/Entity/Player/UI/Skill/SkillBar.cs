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
using MouseAttack.Misc;

namespace MouseAttack.Entity.Player.UI.Skill
{
    public class SkillBar : Control
    {
        PlayerSkill PlayerSkill => TreeSharer.GetNode<PlayerSkill>();
        int SelectedSlotIndex
        {
            get => PlayerSkill.SelectedSlotIndex;
            set => PlayerSkill.SelectedSlotIndex = value;
        }

        public override void _Ready()
        {
            PlayerSkill.CooldownStarted += (s, e) =>
                GetChild<SkillSlot>(e.Slot).Use(e.Cooldown);

            PlayerSkill.Listen(nameof(PlayerSkill.SelectedSlotIndex),
                onChanged: () => GetChild<SkillSlot>(SelectedSlotIndex).Pressed = true);

            foreach (SkillSlot slot in GetChildren().OfType<SkillSlot>())
            {
                int index = slot.GetIndex();

                slot.Listen(nameof(SkillSlot.Item),
                    onChanged: () => PlayerSkill.SkillSelected(slot.Item as CommonSkill, index));

                slot.Connect(Signals.Pressed, this, nameof(OnSkillSlotSelected),
                    new Godot.Collections.Array { index });
            }

            if (PlayerSkill.IsSelectedSlotEmpty)
                return;

            SkillSlot slotZero = GetChild<SkillSlot>(0);
            slotZero.Item = PlayerSkill.SelectedSkill;
            slotZero.Item.IsSlotted = true;
        }

        private void OnSkillSlotSelected(int selectedSlotIndex) =>
            SelectedSlotIndex = selectedSlotIndex;
    }
}
