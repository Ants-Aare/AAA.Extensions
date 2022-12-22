using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using NaughtyAttributes;

public class GetImageFromURL : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private string url;

    [NaughtyAttributes.Button]
    public void UpdateImage()
    {
        StartCoroutine(DownloadImage(url));
    }

    public void UpdateImage(string imageUrl)
    {
        StartCoroutine(DownloadImage(imageUrl));
    }

    IEnumerator DownloadImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
            Debug.Log(request.error);
        else
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;

            image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f));
        }
    }
}