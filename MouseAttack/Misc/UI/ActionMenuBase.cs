using Godot;
using MouseAttack.Constants;
using MouseAttack.Extensions;
using MouseAttack.Item.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Misc.UI
{
    public class MenuAction
    {
        public readonly Action Action;
        public readonly string Name;        
    }
    public abstract class ActionMenuBase : MenuButton
    {
        List<Action> _actions = new List<Action>();

        public override void _EnterTree()
        {
            GetPopup().Connect(Signals.IndexPressed, this, nameof(OnIndexPressed));
            RectMinSize = new Vector2(30, 30);
            ButtonMask = (int)ButtonList.MaskRight;
            MouseFilter = MouseFilterEnum.Pass;
        }

        public override void _Ready() =>
            AddAction();

        public abstract void AddAction();

        protected virtual void OnIndexPressed(int index)
        {
            Action action = _actions[index];
            action();
        }            

        protected void AddAction(string label, Action action)
        {
            GetPopup().AddItem(label);
            _actions.Add(action);
        }

        protected void ClearOptions() =>
            GetPopup().Clear();

    }
}
