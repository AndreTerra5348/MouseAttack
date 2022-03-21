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

namespace MouseAttack.Entity.Player.UI.AttributeDistribution
{
    public class AttributeDistributionPanel : PanelContainer
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
        [Export]
        PackedScene _plusButtonScene = null;
        [Export]
        PackedScene _minusButtonScene = null;

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
            // Get References
            PlayerCharacter character = GetNode<PlayerCharacter>(_characterPath);
            GridContainer grid = GetNode<GridContainer>(_gridPath);
            Label remainingPointsLabel = GetNode<Label>(_remainingPointsLabelPath);
            Button closeButton = GetNode<Button>(_closeButtonPath);

            var statsList = character
                            .GetChildren()
                            .Cast<Stats>()
                            .OrderBy(x => !x.Editable);

            foreach (Stats stats in statsList)
            {

                // Set Name label
                Label nameLabel = new Label();
                nameLabel.Text = StatsConstants.NamingMap[stats.Type];
                grid.AddChild(nameLabel);

                // Bind Value Label
                Label valueLabel = new Label();
                stats.Bind(nameof(Stats.Value), valueLabel, nameof(Label.Text),
                    propertyConvertor: (value) => ((float)value).ToString("0.0"));
                grid.AddChild(valueLabel);

                if (!stats.Editable)
                {

                    grid.AddChild(new Control());
                    grid.AddChild(new Control());
                    continue;
                }

                // Connect Plus Button
                Button plusButton = _plusButtonScene.Instance<Button>();
                plusButton.Disabled = true;
                plusButton.SizeFlagsHorizontal = (int)(SizeFlags.ShrinkEnd | SizeFlags.Expand);

                plusButton.Connect(Signals.Pressed, character, nameof(PlayerCharacter.AddAttributePoint),
                    new Godot.Collections.Array { stats });
                character.Listen(nameof(PlayerCharacter.AttributePoints),
                    onChanged: () => plusButton.Disabled = character.AttributePoints == 0);

                grid.AddChild(plusButton);

                // Connect Minus Button
                Button minusButton = _minusButtonScene.Instance<Button>();

                minusButton.Connect(Signals.Pressed, character, nameof(PlayerCharacter.RemoveAttributePoint),
                    new Godot.Collections.Array { stats });
                stats.Listen(nameof(Stats.Points),
                    onChanged: () => minusButton.Disabled = stats.Points == 0);
                grid.AddChild(minusButton);
            }

            // Bind Attribute Points
            character.Bind(nameof(PlayerCharacter.AttributePoints), remainingPointsLabel, nameof(Label.Text),
                propertyConvertor: (value) => string.Format(RemainingPointsTextFormat, value));

            closeButton.Connect(Signals.Pressed, this, nameof(OnCloseButtonPressed));

            // Bind Level change to Visibility
            character.Listen(nameof(character.Level), onChanged: () => { Visible = true; });
        }

        public override void _UnhandledKeyInput(InputEventKey @event)
        {
            if (@event.IsActionPressed(InputAction.AttributePanel))
                Visible = !Visible;
        }

        private void OnCloseButtonPressed() =>
            Visible = false;


    }
}
