using Godot;
using MouseAttack.Characteristic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.World.Monster.UI
{
    public class BonusPanel : Control
    {
        [Export]
        NodePath _labelContainerPath = "";
        VBoxContainer _labelContainer;

        [Export]
        NodePath _monsterProgressorPath = "";
        MonsterProgressor _monsterProgressor;
        Dictionary<StatsType, Label> _labels = new Dictionary<StatsType, Label>();
        
        public override void _Ready()
        {
            _monsterProgressor = GetNode<MonsterProgressor>(_monsterProgressorPath);
            _monsterProgressor.ApplicableBonuschanged += OnApplicableBonusChanged;
            _labelContainer = GetNode<VBoxContainer>(_labelContainerPath);
            Hide();
        }

        private void OnApplicableBonusChanged(object sender, EventArgs e)
        {
            UpdateLabels();
            if (!Visible)
                Show();
        }

        private void UpdateLabels()
        {
            foreach (var item in _monsterProgressor.ApplicableBonuses)
            {
                StatsType type = item.Key;
                float value = item.Value;

                if (!_labels.ContainsKey(type))
                {
                    _labels[type] = new Label();
                    _labels[type].Align = Label.AlignEnum.Center;
                    _labels[type].MouseFilter = MouseFilterEnum.Pass;
                    _labelContainer.AddChild(_labels[type]);
                }

                _labels[type].Text = GetLabelText(type, value);
            }
        }

        private string GetLabelText(StatsType type, float value) => $"{StatsConstants.NameMap[type]}: {(value / 100.0f).ToString("0%")}";
    }
}
