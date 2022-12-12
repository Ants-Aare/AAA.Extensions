using UnityEngine;

namespace AAA.Utility.GlobalVariables
{
    [System.Serializable]
    public class IntReference : GlobalVariableReference<int, IntVariable>
    {
        public IntReference(int value)
        {
            constantValue = value;
        }

        public static IntReference operator -(IntReference a, int b)
        {
            a.Value = a.Value - b;
            return a;
        }

        public static bool operator <(IntReference a, int b) => a.Value < b;
        public static bool operator >(IntReference a, int b) => a.Value > b;
        public static bool operator <=(IntReference a, int b) => a.Value <= b;
        public static bool operator >=(IntReference a, int b) => a.Value >= b;

        public static IntReference operator +(IntReference a, int b)
        {
            a.Value = a.Value + b;
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