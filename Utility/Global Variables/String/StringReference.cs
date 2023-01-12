namespace AAA.Utility.GlobalVariables
{
    [System.Serializable]
    public class StringReference : GlobalVariableReference<string, StringVariable>
    {
        public StringReference(string value)
        {
            ConstantValue = value;
        }
    }
}