using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;

namespace AAA.Utility.General
{
    public class GUIDToAssetPath : MonoBehaviour
    {
        #if UNITY_EDITOR

        [SerializeField] private string GUID;
        [SerializeField] private string assetPath;

        [Button]
        public void GetAssetPath()
        {
            #if UNITY_EDITOR
            assetPath = AssetDatabase.GUIDToAssetPath(GUID);
            #endif
        }
        #endif
    }
}