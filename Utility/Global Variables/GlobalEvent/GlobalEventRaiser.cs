using UnityEngine;

namespace AAA.Utility.GlobalVariables
{
    public class GlobalEventRaiser : MonoBehaviour
    {
        [SerializeField] private GlobalEvent globalEvent;

        public void RaiseEvent()
        {
            globalEvent?.Raise();
        }
    }
}