namespace AAA.GlobalVariables.Variables
{
    public class SetIntVariable : SetGlobalVariable<int, IntVariable>
    {
        public void Increase(int amount)
        {
            variable.Value += amount;
        }
        public void Decrease(int amount)
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