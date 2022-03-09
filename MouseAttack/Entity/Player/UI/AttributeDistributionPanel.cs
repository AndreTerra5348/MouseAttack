using Godot;
using MouseAttack.Characteristic;
using MouseAttack.Constants;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Player.UI
{
    public class AttributeDistributionPanel : ObserverNode
    {
        const string RemainingPointsTextFormat = "Remaining points: {0}";
        [Export]
        NodePath _characterPath = "";
        [Export]
        NodePath _gridPath = "";
        [Export]
        NodePath _remainingPointsLabelPath = "";

        List<StatsType> _stats = new List<StatsType>()
        {
            StatsType.Damage,
            StatsType.Defense,
            StatsType.Health,
            StatsType.Mana,
            StatsType.CriticalRate,
            StatsType.CriticalDamage
        };        

        public override void _Ready()
        {
            base._Ready();
            PlayerCharacter character = GetNode<PlayerCharacter>(_characterPath);
            GridContainer grid = GetNode<GridContainer>(_gridPath);
            Label remainingPointsLabel = GetNode<Label>(_remainingPointsLabelPath);

            DataBindings.Add(new Binding(character, nameof(PlayerCharacter.Level), this, nameof(Visible), 
                (object value) => Visible = !Visible));

            DataBindings.Add(new Binding(character, nameof(PlayerCharacter.AttributePoints), remainingPointsLabel, nameof(Label.Text), 
                (object value) => String.Format(RemainingPointsTextFormat, value)));

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
                DataBindings.Add(new Binding(stats, nameof(Stats.Value), valueLabel, nameof(Label.Text)));
                grid.AddChild(valueLabel);

                // Bind Points Label
                Label pointsLabel = new Label();
                DataBindings.Add(new Binding(stats, nameof(Stats.Points), pointsLabel, nameof(Label.Text)));
                grid.AddChild(pointsLabel);                

                // Bind and Connect Plus Button
                Button plusButton = new Button();
                DataBindings.Add(new Binding(character, nameof(PlayerCharacter.AttributePoints), plusButton, nameof(Button.Disabled),
                (object value) => (int)value == 0));
                plusButton.Text = "+";
                plusButton.Connect(Signals.Pressed, character, nameof(PlayerCharacter.AddAttributePoint),
                    new Godot.Collections.Array { stats });
                grid.AddChild(plusButton);

                // Bind and Connect Minus Button
                Button minusButton = new Button();
                DataBindings.Add(new Binding(stats, nameof(Stats.Points), minusButton, nameof(Button.Disabled), 
                    (object value) => (int)value == 0));
                minusButton.Text = "-";
                minusButton.Connect(Signals.Pressed, character, nameof(PlayerCharacter.RemoveAttributePoint),
                    new Godot.Collections.Array { stats });
                grid.AddChild(minusButton);
            }

            Initialize();
        }
    }
}
