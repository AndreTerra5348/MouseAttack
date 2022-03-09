namespace MouseAttack.Misc
{
    public interface IPropertyConvertor
    {
        object ConvertProperty(string sourcePropertyName, object sourcePropertyValue, string targetPropertyName);
    }
}