using UnityEngine;
using Cinemachine;

namespace AAA.Utility.General
{
    public class SetVcam : MonoBehaviour
    {
        [SerializeField] private int newPriority = 101;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        public void SetVcamActive()
        {
            virtualCamera.m_Priority = newPriority;
            virtualCamera.MoveToTopOfPrioritySubqueue();
        }
        public void ResetVcam()
        {
            virtualCamera.m_Priority = 0;
        }
    }
}