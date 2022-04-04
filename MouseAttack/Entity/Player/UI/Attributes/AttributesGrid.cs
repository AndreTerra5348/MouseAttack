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

namespace MouseAttack.Entity.Player.UI.Attributes
{
    public class AttributesGrid : GridContainer
    {
        [Export]
        PackedScene _plusButtonScene = null;
        [Export]
        PackedScene _minusButtonScene = null;
        PlayerEntity PlayerEntity => TreeSharer.GetNode<PlayerEntity>();
        PlayerCharacter Character => PlayerEntity.Character;

        List<StatsType> _stats = new List<StatsType>()
        {
            StatsType.Damage,
            StatsType.Defense,
            StatsType.Health,
            StatsType.Mana,
            StatsType.CriticalRate,
            StatsType.CriticalDamage
        };

        Color _normalStatsColor = Colors.White;
        Color _alteredStatsColor = Colors.Cyan;

        public override void _Ready() =>
            PlayerEntity.Initialized += (s, e) => SetupGrid();


        private void SetupGrid()
        {
            var statsList = Character
                            .GetChildren()
                            .Cast<Stats>()
                            .OrderBy(x => !x.Editable);

            foreach (Stats stats in statsList)
            {

                // Set Name label
                Label nameLabel = new Label();
                nameLabel.Text = StatsConstants.FullNameMap[stats.Type];
                AddChild(nameLabel);

                // Bind Value Label
                Label valueLabel = new Label();
                stats.Bind(nameof(Stats.Value), valueLabel, nameof(Label.Text),
                    propertyConvertor: (value) => ((float)value).ToString("0.0"));

                stats.Listen(nameof(Stats.AlteredPercentage), 
                    onChanged: () => 
                    {
                        Color color = stats.AlteredPercentage > 0 ? _alteredStatsColor : _normalStatsColor;
                        valueLabel.AddColorOverride(Overrides.FontColor, color);
                    });

                AddChild(valueLabel);

                if (!stats.Editable)
                {
                    AddChild(new Control());
                    AddChild(new Control());
                    continue;
                }

                // Connect Plus Button
                Button plusButton = _plusButtonScene.Instance<Button>();
                plusButton.Disabled = true;
                plusButton.SizeFlagsHorizontal = (int)(SizeFlags.ShrinkEnd | SizeFlags.Expand);

                plusButton.Connect(Signals.Pressed, Character, nameof(PlayerCharacter.AddAttributePoint),
                    new Godot.Collections.Array { stats });
                Character.Listen(nameof(PlayerCharacter.AttributePoints),
                    onChanged: () => plusButton.Disabled = Character.AttributePoints == 0);

                AddChild(plusButton);

                // Connect Minus Button
                Button minusButton = _minusButtonScene.Instance<Button>();

                minusButton.Connect(Signals.Pressed, Character, nameof(PlayerCharacter.RemoveAttributePoint),
                    new Godot.Collections.Array { stats });
                stats.Listen(nameof(Stats.Points),
                    onChanged: () => minusButton.Disabled = stats.Points == 0);
                AddChild(minusButton);
            }
        }
    }
}
