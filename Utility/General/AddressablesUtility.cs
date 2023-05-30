#if ADDRESSABLES
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;
using System.IO;

namespace AAA.Utility.General
{
    public static class AddressablesUtility
    {
        public static bool AssetExists(object key)
        {
            if (Application.isPlaying)
            {
                foreach (var l in Addressables.ResourceLocators)
                {
                    IList<IResourceLocation> locs;
                    
                    if (l.Locate(key, null, out locs))
                        return true;
                }
                return false;
            }
            else if (Application.isEditor && !Application.isPlaying)
            {

    #if UNITY_EDITOR
                // note: my keys are always asset file paths
                return File.Exists(System.IO.Path.Combine(Application.dataPath, (string)key));
    #endif
            }
            return false;
        }
    }
}
#endif