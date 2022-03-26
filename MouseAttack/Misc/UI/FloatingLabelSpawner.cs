using Godot;
using MouseAttack.Constants;
using MouseAttack.Entity;
using MouseAttack.Entity.Monster;
using MouseAttack.Extensions;
using MouseAttack.Misc;
using MouseAttack.Skill.Data;
using MouseAttack.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Misc.UI
{
    public class FloatingLabelEventArgs : EventArgs
    {
        public FloatingLabel FloatingLabel { get; private set; }

        public FloatingLabelEventArgs(PackedScene floatingLabelScene, string text, Vector2 position)
        {
            FloatingLabel = floatingLabelScene.Instance<FloatingLabel>();
            FloatingLabel.Text = text;
            FloatingLabel.Position = position;
        }
    }

    public class FloatingLabelSpawner : Node
    {
        GridController GridController => TreeSharer.GetNode<GridController>();

        FloatingLabel _instance;
        float _animationPositionThreashold = 0.5f;
        Queue<FloatingLabel> _spawnRequests = new Queue<FloatingLabel>();

        [Signal]
        delegate void ProcessDisabled();

        public override void _Ready()
        {
            SetProcess(false);
            MonsterEntity monsterEntity = GetParentOrNull<MonsterEntity>();
            if (monsterEntity == null)
                return;

            monsterEntity.Initialized += (s, e) =>
            {
                monsterEntity.RemoveChild(this);
                GridController.AddChild(this);
            };
            monsterEntity.Dead += async (s, e) =>
            {
                if (IsProcessing())
                {                    
                    await ToSignal(this, nameof(ProcessDisabled));
                }
                QueueFree();
            };

        }

        public override void _Process(float delta)
        {
            if (IsInstanceValid(_instance) && _instance.AnimationPosition <= _animationPositionThreashold)
                return;
            _instance = _spawnRequests.Dequeue();
            GridController.AddChild(_instance);

            if (_spawnRequests.Count == 0)
            {
                SetProcess(false);
                EmitSignal(nameof(ProcessDisabled));
            }
        }
        public void QueueFloatingLabel(FloatingLabel floatingLabel)
        {
            _spawnRequests.Enqueue(floatingLabel);
            if (!IsProcessing())
                SetProcess(true);
        }
    }
}