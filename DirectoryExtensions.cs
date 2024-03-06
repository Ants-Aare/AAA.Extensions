using System.IO;
using UnityEngine;

namespace AAA.Extensions
{
    public static class DirectoryExtensions
    {
        public static bool DeleteIfExists(string path, bool recursive)
        {
            if (!Directory.Exists(path))
            {
                return false;
            }

            Directory.Delete(path, recursive);

            return true;
        }

        public static bool CreateDirectoryIfDoesNotExists(string path)
        {
            if (Directory.Exists(path))
            {
                return false;
            }

            Directory.CreateDirectory(path);

            return true;
        }

        public static string AbsolutePathToAssetsRelativePath(string absolutePath)
        {
            if (string.IsNullOrEmpty(absolutePath))
            {
                return string.Empty;
            }

            var isNotValidOrRelative = !absolutePath.StartsWith(Application.dataPath);

            if (isNotValidOrRelative)
            {
                return absolutePath;
            }

            var relativePath = string.Empty;

            if (Application.dataPath.Length < absolutePath.Length)
            {
                relativePath = absolutePath.Substring(Application.dataPath.Length + 1);
            }

            return Path.Combine($"Assets{Path.DirectorySeparatorChar}", relativePath);
        }

        public static string AssetsRelativePathToAbsolutePath(string assetsRelativePath)
        {
            if (string.IsNullOrEmpty(assetsRelativePath))
            {
                return string.Empty;
            }

            var assetsLeght = $"Assets{Path.DirectorySeparatorChar}".Length;

            if (assetsRelativePath.Length < assetsLeght)
            {
                return assetsRelativePath;
            }

            assetsRelativePath = assetsRelativePath.Substring(assetsLeght);

            return Path.Combine(Application.dataPath, assetsRelativePath);
        }
    }
}
