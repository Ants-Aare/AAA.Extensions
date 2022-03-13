using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;

public class GUIDToAssetPath : MonoBehaviour
{
    public string GUID;
    public string assetPath;

    [Button]
    public void GetAssetPath()
    {
        #if UNITY_EDITOR
        assetPath = AssetDatabase.GUIDToAssetPath(GUID);
        #endif
    }
}