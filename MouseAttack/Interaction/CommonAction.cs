using Godot;

namespace MouseAttack.Interaction
{
    public class CommonAction : Node2D
    {
        private CommonActionData commonActionData;

        public void SetActionData(CommonActionData commonActionData)
        {
            this.commonActionData = commonActionData;
        }

        protected virtual void OnCommonActionInit() { }

        protected T GetActionData<T>() where T : CommonActionData
        {
            return commonActionData as T;
        }

    }
}
