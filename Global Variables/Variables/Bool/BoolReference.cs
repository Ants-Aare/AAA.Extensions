namespace AAA.GlobalVariables.Variables
{
    [System.Serializable]
    public class BoolReference : GlobalVariableReference<bool, BoolVariable>
    {
        public BoolReference(bool value) : base()
        {
            ConstantValue = value;
        }
    }
}