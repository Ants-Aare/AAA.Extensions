using UnityEngine;

namespace AAA.Utility.GameObjectUtil
{
    public class ActivateGameObject : MonoBehaviour
    {
        [SerializeField] private GameObject targetGameObject;
        
        public void GameObjectSetActive(bool setActive)
        {
            targetGameObject.SetActive(setActive);
        }
    }
}