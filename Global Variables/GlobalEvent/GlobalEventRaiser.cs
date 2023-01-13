using UnityEngine;

namespace AAA.GlobalVariables.Variables
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