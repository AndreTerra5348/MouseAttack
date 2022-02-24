using Godot;

namespace MouseAttack.Interaction
{
    public class CommonAction : Node2D
    {
        private CommonActionData commonActionData;
        public virtual void Initialize(CommonActionData commonActionData) => this.commonActionData = commonActionData;

        protected T GetActionData<T>() where T : CommonActionData
        {
            return commonActionData as T;
        }
    }
}
