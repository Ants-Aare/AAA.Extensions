#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Search;

namespace AAA.Utility.Extensions
{
    public static class AssetDatabaseExtensions
    {
         public static async Task<List<T>> FindGameObjectsByComponentTypeAsync<T>() where T : UnityEngine.Object
        {
            List<T> ret = new List<T>();

            if (!SearchService.IsIndexReady(null))
            {
                SearchService.Refresh();

                while (!SearchService.IsIndexReady(null))
                {
                    await Task.Yield();
                }
            }

            Type type = typeof(T);
            ISearchList results = SearchService.Request($"p: t:prefab t:{type.Name}", SearchFlags.Synchronous);

            foreach (SearchItem result in results)
            {
                T casted = (T)result.ToObject(typeof(T));

                ret.Add(casted);
            }

            return ret;
        }

         public static List<T> FindAssetsByTypeAndName<T>(string name) where T : UnityEngine.Object
         {
             Type type = typeof(T);
             string fullFilter = $"{name} t:{type.Name}";
             string[] guids = AssetDatabase.FindAssets(fullFilter);

             List<T> assets = new List<T>();

             foreach (string guid in guids)
             {
                 string assetPath = AssetDatabase.GUIDToAssetPath(guid);

                 T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);

                 if (asset != null)
                 {
                     assets.Add(asset);
                 }
             }

             return assets;
         }

         public static bool TryFindFirstAssetByTypeAndName<T>(string name, out T asset) where T : UnityEngine.Object
         {
             Type type = typeof(T);
             string fullFilter = $"{name} t:{type.Name}";
             string[] guids = AssetDatabase.FindAssets(fullFilter);

             foreach (var guid in guids)
             {
                 string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                 asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);

                 if(asset != null)
                 {
                     return true;
                 }
             }

             asset = default;
             return false;
         }

        public static List<T> FindAssetsByType<T>() where T : UnityEngine.Object
        {
            return FindAssetsByTypeAndName<T>(string.Empty);
        }

        public static bool TryFindFirstAssetByType<T>(out T asset) where T : UnityEngine.Object
        {
            return TryFindFirstAssetByTypeAndName<T>(string.Empty, out asset);
        }

        public static T FindAssetByTypeOrFail<T>() where T : UnityEngine.Object
        {
            if (!TryFindFirstAssetByType<T>(out var asset))
            {
                throw new InvalidOperationException();
            }

            return asset;
        }

        public static bool TryFindAssetByGuid<T>(string guid, out T asset) where T : UnityEngine.Object
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);

            if (string.IsNullOrEmpty(assetPath))
            {
                asset = default;
                return false;
            }

            asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);

            return asset != null;
        }

        public static bool TryLoadAssetAtPath<T>(string assetPath, out T asset) where T : UnityEngine.Object
        {
            asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            return asset != null;
        }

        public static void RemoveAllSubAssets(UnityEngine.Object obj)
        {
            var assets = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(obj));
            foreach (var asset in assets)
            {
                if (!AssetDatabase.IsSubAsset(asset))
                {
                    continue;
                }

                AssetDatabase.RemoveObjectFromAsset(asset);
            }
        }

        public static string[] GetAssetsGuidAtPath(string assetsPath)
        {
            return AssetDatabase.FindAssets(string.Empty, new[] { assetsPath });
        }
    }
}
#endif
