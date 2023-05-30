using UnityEngine;
using AAA.Utility.CustomUnityEvents;

namespace AAA.Utility.GameObjectUtil
{
    public class GroupSwitch : MonoBehaviour
    {
        [SerializeField] private int currentIndex = 0;
        [SerializeField] private GameObject[] groupObjects;
        [SerializeField] private IntUnityEvent onActiveGroupChanged;

        #if UNITY_EDITOR
        void OnValidate()
        {
            if (currentIndex >= groupObjects.Length)
                currentIndex = groupObjects.Length - 1;
            if (currentIndex < 0)
                currentIndex = 0;
        }
        #endif
        private void Start()
        {
            for (var i = 0; i < groupObjects.Length; i++)
            {
                if (groupObjects[i] != null)
                    groupObjects[i].SetActive(i == currentIndex);
            }
        }

        public void ActivateGroupObject(int index)
        {
            if (groupObjects[index] != null)
                groupObjects[currentIndex]?.SetActive(false);

            currentIndex = Mathf.Clamp(index, 0, groupObjects.Length);

            if (groupObjects[index] != null)
                groupObjects[currentIndex]?.SetActive(true);

            onActiveGroupChanged?.Invoke(currentIndex);
        }
    }
}