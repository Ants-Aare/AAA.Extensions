using System.Collections;
using UnityEngine;

namespace AAA.Utility.GameObjectUtil
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}