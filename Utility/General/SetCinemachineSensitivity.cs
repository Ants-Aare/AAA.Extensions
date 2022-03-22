#if CINEMACHINE
using UnityEngine;
using Cinemachine;
using AAA.Utility.GlobalVariables;

namespace AAA.Utility.General
{
    public class SetCinemachineSensitivity : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private FloatRangeVariable sensitivityX;
        [SerializeField] private FloatRangeVariable sensitivityY;

        private CinemachinePOV povComponent;

        private void Start()
        {
            povComponent = virtualCamera.GetCinemachineComponent<CinemachinePOV>();
            if (povComponent == null)
            {
                Debug.LogError("Could not find CinemachinePOV Component");
                Destroy(this);
                return;
            }

            sensitivityX.OnChanged += UpdateSensitivityX;
            sensitivityY.OnChanged += UpdateSensitivityY;

            UpdateSensitivityX();
            UpdateSensitivityY();
        }

        public void UpdateSensitivityX()
        {
            povComponent.m_HorizontalAxis.m_MaxSpeed = sensitivityX.Value.Value;
        }
        public void UpdateSensitivityY()
        {
            povComponent.m_VerticalAxis.m_MaxSpeed = sensitivityY.Value.Value;
        }
    }
}
#endif