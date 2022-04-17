using Godot;
using MouseAttack.Skill.WorldEffect;
using MouseAttack.Constants;
using MouseAttack.Entity.Player;
using MouseAttack.World;
using MouseAttack.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MouseAttack.Misc;
using MouseAttack.Skill.Data;

namespace MouseAttack.Entity.Monster
{
    public class MonsterSkillController : Node
    {
        DamageSkill _skill;
        [Export]
        NodePath GraphicsPath { get; set; } = "";
        Node2D _graphics;
        [Export]
        int Range { get; set; } = 0;

        MonsterEntity _monsterEntity;
        int _minAttackRange = 2;
        public int AttackRange => Range + _minAttackRange;
        PlayerEntity PlayerEntity => TreeSharer.GetNode<PlayerEntity>();
        GridController GridController => TreeSharer.GetNode<GridController>();

        Tween _tween = new Tween();
        float TweenSpeed { get; set; } = 0.1f;

        public override void _Ready()
        {
            _monsterEntity = GetParent<MonsterEntity>();
            _graphics = GetNode<Node2D>(GraphicsPath);
            var skillFactory = GetChild<DamageSkillFactory>((_monsterEntity.Level-1) % GetChildCount());
            _skill = skillFactory.CreateItem<DamageSkill>();
            AddChild(_tween);
        }

        public SignalAwaiter Attack()
        {
            if (Range == 0)
                return StartApproachAnimation();
            return UseRangedSkill();
        }

        private SignalAwaiter StartApproachAnimation()
        {
            Vector2 currentPosition = _graphics.GlobalPosition;
            _tween.InterpolateProperty(_graphics,
                GodotProperties.GlobalPosition,
                _graphics.GlobalPosition,
                PlayerEntity.GlobalPosition,
                TweenSpeed);

            UseCloseRangeSkill();

            _tween.InterpolateProperty(_graphics,
                GodotProperties.GlobalPosition,
                PlayerEntity.GlobalPosition,
                currentPosition,
                TweenSpeed, Tween.TransitionType.Linear, Tween.EaseType.InOut, TweenSpeed);

            _tween.Start();
            return ToSignal(_tween, Signals.TweenAllCompleted);
        }

        async private void UseCloseRangeSkill()
        {
            await this.CreateTimer(TweenSpeed);
            CommonWorldEffect worldEffect = GetWorldEffect(PlayerEntity.GlobalPosition);
            GridController.AddChild(worldEffect);
        }

        private SignalAwaiter UseRangedSkill()
        {
            CommonWorldEffect worldEffect = GetWorldEffect(_monsterEntity.GlobalPosition);
            Mover mover = new Mover(worldEffect);
            GridController.AddChild(mover);
            return ToSignal(mover, Signals.TreeExiting);
        }

        private CommonWorldEffect GetWorldEffect(Vector2 position)
        {
            CommonWorldEffect worldEffect = _skill.GetWorldEffect();
            worldEffect.User = _monsterEntity;
            worldEffect.Position = position;
            return worldEffect;
        }

    }
}
