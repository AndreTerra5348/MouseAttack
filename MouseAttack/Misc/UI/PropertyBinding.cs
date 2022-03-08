namespace MouseAttack.Misc.UI
{
    public class PropertyBinding
    {
        public string TargetPropertyName { get; }
        public string SourcePropertyName { get; }
        public PropertyBinding(string targetPropertyName, string sourcePropertyName)
        {
            TargetPropertyName = targetPropertyName;
            SourcePropertyName = sourcePropertyName;
        }
    }
}
