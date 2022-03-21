using Godot;
using MouseAttack.Skill.Module;
using MouseAttack.Skill.Monster;
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

namespace MouseAttack.Entity.Monster
{
    public class MonsterSkillController : Node
    {
        [Export]
        MonsterDamage _skill = null;
        [Export]
        NodePath _graphicsPath = "";
        Node2D _graphics;

        MonsterEntity _monsterEntity;

        int _minAttackRange = 2;
        public int AttackRange => _skill.Range + _minAttackRange;
        PlayerEntity PlayerEntity => TreeSharer.GetNode<PlayerEntity>();
        GridController GridController => TreeSharer.GetNode<GridController>();

        Tween _tween = new Tween();
        float TweenSpeed { get; set; } = 0.2f;

        const string TweenProperty = "global_position";
        public override void _Ready()
        {
            _monsterEntity = GetParent<MonsterEntity>();
            _graphics = GetNode<Node2D>(_graphicsPath);
            AddChild(_tween);
            
        }

        public SignalAwaiter Attack()
        {
            if (_skill.Range == 0)
                return StartApproachAnimation();
            return this.CreateTimer(0.2f);
        }

        private SignalAwaiter StartApproachAnimation()
        {
            Vector2 currentPosition = _graphics.GlobalPosition;
            _tween.InterpolateProperty(_graphics,
                TweenProperty,
                _graphics.GlobalPosition,
                PlayerEntity.GlobalPosition,
                TweenSpeed);

            UseSkill();

            _tween.InterpolateProperty(_graphics,
                TweenProperty,
                PlayerEntity.GlobalPosition,
                currentPosition,
                TweenSpeed, Tween.TransitionType.Linear, Tween.EaseType.InOut, TweenSpeed);

            _tween.Start();
            return ToSignal(_tween, Signals.TweenAllCompleted);
        }

        async private void UseSkill()
        {
            await this.CreateTimer(TweenSpeed);
            CommonWorldEffect worldEffect = _skill.GetWorldEffectInstance();
            worldEffect.Skill = _skill;
            worldEffect.User = _monsterEntity;
            worldEffect.GlobalPosition = PlayerEntity.GlobalPosition;
            GridController.AddChild(worldEffect);
        }

    }
}
