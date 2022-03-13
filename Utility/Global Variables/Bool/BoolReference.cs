namespace AAA.Utility.GlobalVariables
{
    [System.Serializable]
    public class BoolReference : GlobalVariableReference<bool, BoolVariable>
    {
        public BoolReference(bool value)
        {
            constantValue = value;
        }
    }
}