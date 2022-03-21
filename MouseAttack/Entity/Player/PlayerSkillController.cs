using Godot;
using MouseAttack.Skill;
using MouseAttack.Skill.WorldEffect;
using System.Linq;
using MouseAttack.Extensions;
using System;
using MouseAttack.World;
using MouseAttack.Misc;
using MouseAttack.Constants;
using MouseAttack.Skill.Data;

namespace MouseAttack.Entity.Player
{
    public class CooldownStartedEventArgs : EventArgs
    {
        public readonly int Slot;
        public readonly int Cooldown;

        public CooldownStartedEventArgs(int slot, int cooldown)
        {
            Slot = slot;
            Cooldown = cooldown;
        }
    }

    public class PlayerSkillController : ObservableNode
    {
        public event EventHandler<CooldownStartedEventArgs> CooldownStarted;

        [Export]
        public SkillDB SkillDB { get; private set; }

        bool IsSelectedSlotEmpty => _slottedSkills[SelectedSlotIndex] == null;
        GridController GridController => TreeSharer.GetNode<GridController>();
        PlayArea PlayArea => TreeSharer.GetNode<PlayArea>();
        PlayerEntity PlayerEntity => TreeSharer.GetNode<PlayerEntity>();
        PlayerCharacter PlayerCharacter => PlayerEntity.Character;

        readonly CommonSkill[] _slottedSkills = new CommonSkill[5];
        int _selectSlotIndex = 0;

        public int SelectedSlotIndex
        {
            set
            {
                if (_selectSlotIndex == value)
                    return;
                _selectSlotIndex = value;
                OnPropertyChanged();
            }
            get => _selectSlotIndex;
        }

        public override void _Ready()
        {

            GridController.RoundFinished += (s, e) =>
            {
                foreach (CommonSkill skill in SkillDB.Skills)
                {
                    if (!skill.IsUnlocked)
                        continue;

                    if (!skill.OnCooldown)
                        continue;

                    skill.ElapseCooldown();
                }

                SetProcessInput(true);
            };

            AddChild(new SkillCursor());
        }


        public override void _Notification(int what)
        {
            if (what == NotificationPaused)
                SetProcessInput(false);
            else if(what == NotificationUnparented)
                SetProcessInput(true);
        }

        public override void _Input(InputEvent @event)
        {            
            if (@event.IsActionPressed(InputAction.LMB) && PlayArea.OnPlayArea)
                UseSkill();
            else if (@event.IsActionPressed(InputAction.ElapseTurn))
                ElapseTurn();
            else
            {
                for (int i = 0; i < _slottedSkills.Length; i++)
                {                    
                    if (@event.IsActionPressed(String.Format(InputAction.HotkeyFormat, i + 1)))
                    {
                        SelectedSlotIndex = i;
                        break;
                    }
                }
            }          
        }

        public void SetSkill(CommonSkill skill, int slot) =>
            _slottedSkills[slot] = skill;


        private void UseSkill()
        {            
            
            if (IsSelectedSlotEmpty)
                return;

            CommonSkill skill = _slottedSkills[SelectedSlotIndex];

            if (skill.OnCooldown)
                return;

            if (!PlayerCharacter.HasEnoughMana(skill.Cost))
                return;


            PlayerCharacter.UseMana(skill.Cost);

            
            skill.StartCooldown();

            CommonWorldEffect worldEffect = skill.GetWorldEffectInstance();            
            worldEffect.Skill = skill;
            worldEffect.User = PlayerEntity;
            worldEffect.Position = GetViewport().GetSnappedMousePosition(Values.CellSize);
            GridController.AddChild(worldEffect);
            CooldownStarted?.Invoke(this, new CooldownStartedEventArgs(SelectedSlotIndex, skill.Cooldown));            
            ElapseTurn();
        }

        private void ElapseTurn()
        {
            SetProcessInput(false);
            GridController.ElapseTurn();
        }
    }
}