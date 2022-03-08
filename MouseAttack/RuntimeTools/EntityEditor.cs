using Godot;
using MouseAttack.Characteristic;
using MouseAttack.Constants;
using MouseAttack.Entity;
using System;
using System.Reflection;

namespace MouseAttack.RuntimeTools
{
    public class EntityEditor : Control
    {
        [Export]
        NodePath _tabNodePath = "";
        [Export(PropertyHint.File, "*.tscn")]
        string _playerScenePath = "";
        [Export(PropertyHint.Dir)]
        string _monsterDBPath = "";

        TabContainer _tab;

        const string PlayerCharacterNodePathFormat = "PlayerCharacter/{0}";
        const string MonsterCharacterNodePathFormat = "MonsterEntity/MonsterCharacter/{0}";
        public override void _Ready()
        {
            _tab = GetNode<TabContainer>(_tabNodePath);
            BuildGrid("Player", _playerScenePath, PlayerCharacterNodePathFormat);

            using (Directory directory = new Directory())
            {
                directory.Open(_monsterDBPath);
                directory.ListDirBegin();
                string filename = directory.GetNext();
                while(filename != "")
                {
                    filename = directory.GetNext();
                    if (!directory.CurrentIsDir() || filename == "." || filename == "..")
                        continue;

                    string directoryPath = _monsterDBPath + "/" + filename;
                    string sceneFile = GetScenePathInDir(directoryPath);
                    if (sceneFile == String.Empty)
                        continue;
                    string scenePath = directoryPath + "/" + sceneFile;
                    BuildGrid(filename, scenePath, MonsterCharacterNodePathFormat);

                }
            }
            
        }

        public string GetScenePathInDir(string path)
        {
            using (Directory directory = new Directory())
            {
                directory.Open(path);
                directory.ListDirBegin();
                string filename = directory.GetNext();
                while (filename != "")
                {
                    filename = directory.GetNext();
                    if (directory.CurrentIsDir())
                        continue;

                    if (filename.EndsWith(".tscn"))
                        return filename;
                }
            }
            return String.Empty;
        }

        private void BuildGrid(string name, string scenePath, string statsPathFormat)
        {
            // Create grid
            GridContainer grid = new GridContainer();
            grid.Name = name;
            grid.Columns = 4;
            grid.Set(Overrides.CustomConstantsHSeparation, 10);

            grid.SizeFlagsVertical = ((int)SizeFlags.Expand);

            // Load scene and instance
            PackedScene scene = ResourceLoader.Load<PackedScene>(scenePath);
            Node node = scene.Instance(PackedScene.GenEditState.Instance);

            // Add Header
            AddTextToGrid(grid, "Stats");
            AddTextToGrid(grid, "Points");
            AddTextToGrid(grid, "Value per Point");
            AddTextToGrid(grid, "Value");

            // Add Values
            foreach (string statsName in Enum.GetNames(typeof(StatsType)))
            {
                string nodePath = String.Format(statsPathFormat, statsName);
                if (!node.HasNode(nodePath))
                    continue;

                Label label = new Label();
                label.Text = statsName;
                grid.AddChild(label);

                Stats stats = node.GetNode<Stats>(nodePath);

                Label valueLabel = new Label();

                SpinBox pointsEdit = new SpinBox();
                pointsEdit.Value = stats.Points;
                grid.AddChild(pointsEdit);
                pointsEdit.Connect(Signals.ValueChanged, this, nameof(OnStatsValueChanged),
                    new Godot.Collections.Array { stats, nameof(stats.Points), valueLabel });

                SpinBox valuePerPointEdit = new SpinBox();
                valuePerPointEdit.Step = 0.01f;
                valuePerPointEdit.Value = stats.ValuePerPoint;
                grid.AddChild(valuePerPointEdit);
                valuePerPointEdit.Connect(Signals.ValueChanged, this, nameof(OnStatsValueChanged),
                    new Godot.Collections.Array { stats, nameof(stats.ValuePerPoint), valueLabel });


                valueLabel.Text = stats.Value.ToString();
                grid.AddChild(valueLabel);
            }

            // Add Apply button
            Button saveButton = new Button();
            saveButton.Text = "Apply";
            grid.AddChild(saveButton);

            saveButton.Connect(Signals.Pressed, this, nameof(OnApplyButtonPressed),
                new Godot.Collections.Array { scenePath, scene, node });

            _tab.AddChild(grid);
        }

        private void OnStatsValueChanged(float value, Stats stats, string propName, Label valueLabel)
        {
            if(propName == nameof(stats.Points))
                typeof(Stats).GetProperty(propName).SetValue(stats, (int)value);
            else
                typeof(Stats).GetProperty(propName).SetValue(stats, value);
            valueLabel.Text = stats.Value.ToString();
        }

        private void OnApplyButtonPressed(string scenePath, PackedScene scene, Node node)
        {
            scene.Pack(node);
            ResourceSaver.Save(scenePath, scene);
        }

        private Label AddTextToGrid(GridContainer grid, string text)
        {
            Label lbl = new Label();
            lbl.Text = text;
            grid.AddChild(lbl);
            return lbl;
        }

    }
}