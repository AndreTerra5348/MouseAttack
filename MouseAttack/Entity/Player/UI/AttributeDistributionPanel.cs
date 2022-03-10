using Godot;
using MouseAttack.Characteristic;
using MouseAttack.Constants;
using MouseAttack.Extensions;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Player.UI
{
    public class AttributeDistributionPanel : Control
    {
        const string RemainingPointsTextFormat = "Remaining points: {0}";
        [Export]
        NodePath _characterPath = "";
        [Export]
        NodePath _gridPath = "";
        [Export]
        NodePath _remainingPointsLabelPath = "";
        [Export]
        NodePath _closeButtonPath = "";

        List<StatsType> _stats = new List<StatsType>()
        {
            StatsType.Damage,
            StatsType.Defense,
            StatsType.Health,
            StatsType.Mana,
            StatsType.CriticalRate,
            StatsType.CriticalDamage
        };

        public new bool Visible 
        {
            get => base.Visible;
            set
            {
                base.Visible = value;
                GetTree().Paused = Visible;
            }
        }

        public override void _Ready()
        {
            base._Ready();
            PlayerCharacter character = GetNode<PlayerCharacter>(_characterPath);
            GridContainer grid = GetNode<GridContainer>(_gridPath);
            Label remainingPointsLabel = GetNode<Label>(_remainingPointsLabelPath);
            Button closeButton = GetNode<Button>(_closeButtonPath);            

            foreach (StatsType type in _stats)
            {
                // Get character stats
                string statsName = Enum.GetName(typeof(StatsType), type);
                Stats stats = character.GetNode<Stats>(statsName);

                // Set Name label
                Label nameLabel = new Label();
                nameLabel.Text = StatsConstants.NamingMap[type];
                grid.AddChild(nameLabel);

                // Bind Value Label
                Label valueLabel = new Label();
                stats.Bind(nameof(Stats.Value), valueLabel, nameof(Label.Text), propertyConvertor: (object value) => ((float)value).ToString("0.0"));
                grid.AddChild(valueLabel);           

                // Connect Plus Button
                Button plusButton = new Button();
                plusButton.Text = "+";
                plusButton.RectMinSize = new Vector2(25, 25);
                plusButton.Disabled = true;
                plusButton.SizeFlagsHorizontal = (int)(SizeFlags.ShrinkEnd | SizeFlags.Expand);

                plusButton.Connect(Signals.Pressed, character, nameof(PlayerCharacter.AddAttributePoint),
                    new Godot.Collections.Array { stats });
                character.Listen(nameof(PlayerCharacter.AttributePoints),
                    callback: () => plusButton.Disabled = character.AttributePoints == 0);

                grid.AddChild(plusButton);

                // Connect Minus Button
                Button minusButton = new Button();
                minusButton.Text = "-";
                minusButton.RectMinSize = new Vector2(25, 25);

                minusButton.Connect(Signals.Pressed, character, nameof(PlayerCharacter.RemoveAttributePoint),
                    new Godot.Collections.Array { stats });
                stats.Listen(nameof(Stats.Points), callback: () => minusButton.Disabled = stats.Points == 0);
                grid.AddChild(minusButton);
            }

            character.Bind(nameof(PlayerCharacter.AttributePoints), remainingPointsLabel, nameof(Label.Text),
                propertyConvertor: (object value) => String.Format(RemainingPointsTextFormat, value));

            closeButton.Connect(Signals.Pressed, this, nameof(OnCloseButtonPressed));

            character.Listen(nameof(Character.Level), callback: () => { Visible = true; });
        }

        public override void _UnhandledKeyInput(InputEventKey @event)
        {
            if (@event.IsActionPressed("AttributePanel"))
                Visible = !Visible;
        }

        private void OnCloseButtonPressed() => Visible = false;

        
    }
}
