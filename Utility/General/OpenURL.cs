using UnityEngine;
using UnityEngine.Networking;

namespace AAA.Utility.General
{
    public class OpenURL : MonoBehaviour
    {
        [Header("Values")]
        [SerializeField]private string urlToOpen;

        private const string TWITTER_ADDRESS = "http://twitter.com/intent/tweet";

        public void TryOpenURL()
        {
            Application.OpenURL(urlToOpen);
        }

        public void TryOpenURL(string url)
        {
            if (url != "")
            {
                Application.OpenURL(url);
            }
        }

        public void ShareToTwitter(string message)
        {
            Application.OpenURL(TWITTER_ADDRESS + "?text=" + UnityWebRequest.EscapeURL(message));
        }
        
        // public void ShareToInstagram(string message)
        // {
        //     Application.OpenURL(TWITTER_ADDRESS + "?text=" + UnityWebRequest.EscapeURL(message));
        // }
    }
}