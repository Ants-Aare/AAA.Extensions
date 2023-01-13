namespace AAA.GlobalVariables.Variables
{
    [System.Serializable]
    public class StringReference : GlobalVariableReference<string, StringVariable>
    {
        public StringReference(string value) : base()
        {
            ConstantValue = value;
        }
    }
}