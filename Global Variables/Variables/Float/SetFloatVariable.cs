namespace AAA.GlobalVariables.Variables
{
    public class SetFloatVariable : SetGlobalVariable<float, FloatVariable>
    {
        public void Increase(float amount)
        {
            variable.Value += amount;
        }
        public void Decrease(float amount)
        {
            variable.Value -= amount;
        }
        
        public void Increment()
        {
            variable.Value++;
        }
        public void Decrement()
        {
            variable.Value--;
        }
    }
}