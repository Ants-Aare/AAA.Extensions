using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace AAA.Extensions
{
    public static class FileExtensions
    {
        public static async Task SaveBytesAsync(string filePath, byte[] data, CancellationToken cancellationToken)
        {
            try
            {
                var fileAlreadyExists = File.Exists(filePath);

                if (!fileAlreadyExists)
                {
                    var filePathDirectory = Path.GetDirectoryName(filePath);

                    if (string.IsNullOrEmpty(filePathDirectory))
                    {
                        return;
                    }

                    Directory.CreateDirectory(filePathDirectory);
                }
                else
                {
                    File.Delete(filePath);
                }

                await using (var sourceStream = File.Open(filePath, FileMode.OpenOrCreate))
                {
                    sourceStream.Seek(0, SeekOrigin.End);

                    await sourceStream.WriteAsync(data, 0, data.Length, cancellationToken);
                }
            }
            catch (Exception exception)
            {
                Debug.LogError($"There was an exception trying to save bytes async to filePath {filePath}, at {nameof(FileExtensions)}. Exception: {exception}");
            }
        }
    }
}
