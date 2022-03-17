using UnityEngine;

namespace AAA.Utility.Animation
{
    public class AnimatorChanger : MonoBehaviour
    {
        [Header("Values")]
        [SerializeField] private string propertyName;

        [Header("References")]
        [SerializeField] private Animator anim = null;

        #if UNITY_EDITOR
        private void OnValidate()
        {
            if(anim == null)
                anim = GetComponentInChildren<Animator>();
        }
        #endif
        
        public void SetBool(bool value)
        {
            anim.SetBool(propertyName, value);
        }

        public void SetBool(int value)
        {
            anim.SetBool(propertyName, value != 0);
        }

        public void SetInt(int value)
        {
            anim.SetInteger(propertyName, value);
        }

        public void SetFloat(float value)
        {
            anim.SetFloat(propertyName, value);
        }

        public void SetTrigger()
        {
            anim.SetTrigger(propertyName);
        }

        public void SetTrigger(string propertyName)
        {
            anim.SetTrigger(propertyName);
        }
    }
}