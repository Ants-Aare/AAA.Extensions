using UnityEngine;

namespace AAA.GlobalVariables.Variables
{
    [System.Serializable]
    public class IntReference : GlobalVariableReference<int, IntVariable>
    {
        public IntReference(int value) : base()
        {
            ConstantValue = value;
        }
        public static bool operator <(IntReference a, int b) => a.Value < b;
        public static bool operator >(IntReference a, int b) => a.Value > b;
        public static bool operator <=(IntReference a, int b) => a.Value <= b;
        public static bool operator >=(IntReference a, int b) => a.Value >= b;

        public static IntReference operator -(IntReference a, int b)
        {
            a.Value -= b;
            return a;
        }
        public static IntReference operator +(IntReference a, int b)
        {
            a.Value += b;
            return a;
        }

        public static IntReference operator ++(IntReference a)
        {
            a.Value++;
            return a;
        }

        public static IntReference operator --(IntReference a)
        {
            a.Value--;
            return a;
        }
    }
}