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
using MouseAttack.Item.Misc;

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

    public class PlayerSkill : ObservableNode, ISharable
    {
        public event EventHandler<CooldownStartedEventArgs> CooldownStarted;
        
        GridController GridController => TreeSharer.GetNode<GridController>();
        PlayArea PlayArea => TreeSharer.GetNode<PlayArea>();
        PlayerEntity PlayerEntity => TreeSharer.GetNode<PlayerEntity>();
        PlayerCharacter Character => PlayerEntity.Character;
        PlayerInventory PlayerInventory => TreeSharer.GetNode<PlayerInventory>();

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
        public CommonSkill SelectedSkill
        {
            get => _slottedSkills[SelectedSlotIndex];
            set => _slottedSkills[SelectedSlotIndex] = value;
        }
        public bool IsSelectedSlotEmpty => SelectedSkill == null;


        public PlayerSkill() =>
            TreeSharer.RegistryNode(this);

        public override void _Ready()
        {
            GridController.RoundFinished += (s, e) =>
            {
                foreach (CommonSkill skill in PlayerInventory.Skills)
                {
                    if (!skill.OnCooldown)
                        continue;

                    skill.ElapseCooldown();
                }

                SetProcessInput(true);
            };
            PlayerInventory.Initialized += (s, e) => SelectedSkill = PlayerInventory.MainAttack;
        }

        public override void _Input(InputEvent @event)
        {            
            if (@event.IsActionPressed(InputAction.LMB) && PlayArea.OnPlayArea)
                UseSkill();
            else if (@event.IsActionPressed(InputAction.ElapseTurn))
                EndTurn();
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

        public void SkillSelected(CommonSkill skill, int slot) =>
            _slottedSkills[slot] = skill;

        private void UseSkill()
        {            
            if (IsSelectedSlotEmpty)
                return;

            if (SelectedSkill.OnCooldown)
                return;

            if (!Character.HasEnoughMana(SelectedSkill.ManaCost))
                return;

            Character.UseMana(SelectedSkill.ManaCost);

            SelectedSkill.StartCooldown();

            CommonWorldEffect worldEffect = SelectedSkill.GetWorldEffect();
            worldEffect.User = PlayerEntity;
            worldEffect.Position = GetViewport().GetSnappedMousePosition(Values.CellSize);
            GridController.AddChild(worldEffect);
            CooldownStarted?.Invoke(this, new CooldownStartedEventArgs(SelectedSlotIndex, SelectedSkill.Cooldown));            
            EndTurn();
        }

        private void EndTurn()
        {
            SetProcessInput(false);
            GridController.ElapseTurn();
        }
    }
}