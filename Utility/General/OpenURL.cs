using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class OpenURL : MonoBehaviour
{
    [Header("Values")]
    [SerializeField]
    private string urlToOpen;

    private const string TWITTER_ADDRESS = "http://twitter.com/intent/tweet";

    public void TryOpenURL()
    {
        Application.OpenURL (urlToOpen);  
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
}