using Godot;
using MouseAttack.Skill.Monster;
using MouseAttack.Skill.WorldEffect;
using System;

namespace MouseAttack.Skill.Module
{
    /// <summary>
    /// When added as a child of CollidableEffect Scene, moves to target
    /// </summary>
    public class Mover : Node2D
    {
        //readonly IMonsterSkill _skill;
        //readonly Vector2 _target;
        //DamageWorldEffect _effect;

        //public override void _Ready()
        //{
        //    base._Ready();
        //    _effect = GetParent<DamageWorldEffect>();
        //}

        //public Mover()
        //{
        //}

        //public Mover(IMonsterSkill skill, Vector2 target)
        //{
        //    _skill = skill;
        //    _target = target;
        //}

        //public override void _Process(float delta)
        //{
        //    _effect.Position = _effect.Position.MoveToward(_target, _skill.Speed).Snapped(Vector2.One);
        //}
    }
}
