#if ADDRESSABLES
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;
using System.IO;
#endif

namespace AAA.Extensions
{
    public static class AddressablesExtension
    {
        public static bool AssetExists(object key)
        {
#if ADDRESSABLES
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
                // Only works if keys are asset file paths
                return File.Exists(System.IO.Path.Combine(Application.dataPath, (string)key));
    #endif
            }
#endif
            return false;
        }
    }
}