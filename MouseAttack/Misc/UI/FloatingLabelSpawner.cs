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
    public class FloatingLabelSpawner : Node
    {
        FloatingLabelLayer FloatingLabelLayer => TreeSharer.GetNode<FloatingLabelLayer>();
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
                FloatingLabelLayer.AddChild(this);
            };
            monsterEntity.Dead += async (s, e) =>
            {
                if (IsProcessing())              
                    await ToSignal(this, nameof(ProcessDisabled));                
                QueueFree();
            };

        }

        public override void _Process(float delta)
        {
            if (IsInstanceValid(_instance) && _instance.AnimationPosition <= _animationPositionThreashold)
                return;
            _instance = _spawnRequests.Dequeue();
            FloatingLabelLayer.AddChild(_instance);

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