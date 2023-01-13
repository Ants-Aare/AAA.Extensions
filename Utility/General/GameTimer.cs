using UnityEngine;
using AAA.GlobalVariables.Variables;

namespace AAA.Utility.General
{
    public class GameTimer : MonoBehaviour
    {
        [SerializeField] private BoolReference isTimerRunning;
        [SerializeField] private FloatVariable timer;

        private void Update()
        {
            if(isTimerRunning.Value)
                timer.Value += Time.deltaTime;
        }

        public void ResetTimerValue()
        {
            timer.Value = 0f;
        }
    }
}