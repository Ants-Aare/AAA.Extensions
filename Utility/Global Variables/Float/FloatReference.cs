namespace AAA.Utility.GlobalVariables
{
    [System.Serializable]
    public class FloatReference : GlobalVariableReference<float, FloatVariable>
    {
        public FloatReference(float value)
        {
            constantValue = value;
        }
    }
}