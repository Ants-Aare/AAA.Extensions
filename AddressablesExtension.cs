using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
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

        public static bool IsNullOrEmpty<T>(this AssetReferenceT<T> reference)
            where T : Object
        {
            if (reference == null)
                return true;
            if (string.IsNullOrEmpty(reference.AssetGUID))
                return true;
            if (string.IsNullOrEmpty(reference.RuntimeKey.ToString()))
                return true;
            if (!reference.RuntimeKeyIsValid())
                return true;
            return false;
        }

        // public static bool TryGetResourceLocator<T>(object key, out IResourceLocator result)
        // {
        //     if (key != null)
        //     {
        //         foreach (IResourceLocator resourceLocator in Addressables.ResourceLocators)
        //         {
        //             if (resourceLocator.Locate(key, typeof(T), out _))
        //             {
        //                 result = resourceLocator;
        //
        //                 return true;
        //             }
        //         }
        //     }
        //
        //     result = null;
        //
        //     return false;
        // }
    }
    
    
    
    //I don't understand why this isn't included in unity by default???
    //Copied from here: https://forum.unity.com/threads/something-like-assetreferencet-sceneasset.812085/
    [System.Serializable]
    public class AssetReferenceScene : AssetReference
    {
        /// <summary>
        /// Construct a new AssetReference object.
        /// </summary>
        /// <param name="guid">The guid of the asset.</param>
        public AssetReferenceScene(string guid) : base(guid)
        {
        }
 
 
        /// <inheritdoc/>
        public override bool ValidateAsset(Object obj)
        {
#if UNITY_EDITOR
            var type = obj.GetType();
            return typeof(UnityEditor.SceneAsset).IsAssignableFrom(type);
#else
        return false;
#endif
 
        }
 
        /// <inheritdoc/>
        public override bool ValidateAsset(string path)
        {
#if UNITY_EDITOR
            var type = UnityEditor.AssetDatabase.GetMainAssetTypeAtPath(path);
            return typeof(UnityEditor.SceneAsset).IsAssignableFrom(type);
#else
        return false;
#endif
        }
 
#if UNITY_EDITOR
        /// <summary>
        /// Type-specific override of parent editorAsset.  Used by the editor to represent the asset referenced.
        /// </summary>
        public new UnityEditor.SceneAsset editorAsset => (UnityEditor.SceneAsset)base.editorAsset;
#endif
    }
}