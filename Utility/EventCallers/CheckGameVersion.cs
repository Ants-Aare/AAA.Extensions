using System.Net.Mime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
// using Newtonsoft.Json;

namespace AAA.Utility.EventCallers
{
    public class CheckGameVersion : MonoBehaviour
    {
        [SerializeField] private bool checkOnStart = true;
        [SerializeField] private string url;
        [SerializeField] private UnityEvent onCorrectVersion, onWrongVersion, onNetworkError;

        void Start()
        {
            if (checkOnStart)
            {
                CheckVersion();
            }
        }

        public void CheckVersion()
        {
#if UNITY_EDITOR
            onCorrectVersion?.Invoke();
            return;
#endif

            // StartCoroutine(GetJson());
        }

        private IEnumerator GetJson()
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                switch (webRequest.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                        onNetworkError?.Invoke();
                        break;
                    case UnityWebRequest.Result.DataProcessingError:
                        Debug.LogError("Error: " + webRequest.error);
                        onNetworkError?.Invoke();
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        Debug.LogError("HTTP Error: " + webRequest.error);
                        onNetworkError?.Invoke();
                        break;
                    case UnityWebRequest.Result.Success:
                        // GameInfo gameInfo = JsonConvert.DeserializeObject<GameInfo>(webRequest.downloadHandler.text);
                        // CheckVersion(gameInfo);
                        break;
                }
            }
        }

        public void CheckVersion(GameInfo gameInfo)
        {
            if (Application.version.ToLowerInvariant().Replace(".", string.Empty) == gameInfo.buildVersion.ToLowerInvariant().Replace(".", string.Empty))
            {
                onCorrectVersion?.Invoke();
            }
            else
            {
                onWrongVersion?.Invoke();
            }
        }
    }

    public class GameInfo
    {
        public string buildVersion;
    }
}